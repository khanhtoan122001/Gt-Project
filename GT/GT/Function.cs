﻿using System;
using System.Data.SqlTypes;
using System.Drawing;
using System.Security.Policy;
using System.Collections;

namespace Fcn
{
    public enum Type { Variable, Value, Operator, Function, Result, Bracket, Comma, Error }
    public struct Symbol
    {
        public string m_name;
        public double m_value;
        public Type m_type;
        public override string ToString()
        {
            return m_name;
        }
    }
    public delegate Symbol EvaluateFunctionDelegate(string name, params Object[] args);
    public class Function
    {
        private Random random = new Random();
        protected float[] x;
        bool enable = true;
        protected Color f_color;
        string str_save;
        string Name;
        public ArrayList arr;
        public string name
        {
            set => Name = value;
            get => Name;
        }
        public double Result
        {
            get
            {
                return m_result;
            }
        }

        public ArrayList Equation
        {
            get
            {
                return (ArrayList)m_equation.Clone();
            }
        }
        public ArrayList Postfix
        {
            get
            {
                return (ArrayList)m_postfix.Clone();
            }
        }
        public double f(double x)
        {
            //return Math.Tan(x);

            Symbol sl;
            sl.m_type = Fcn.Type.Variable;
            sl.m_name = "x";
            sl.m_value = x;

            arr[0] = sl;
            this.Variables = arr;
            this.EvaluatePostfix();

            if (this.Error)
            {
                return double.NaN;
            }
            return this.Result;
        }
        public EvaluateFunctionDelegate DefaultFunctionEvaluation
        {
            set
            {
                m_defaultFunctionEvaluation = value;
            }
        }

        public bool Error
        {
            get
            {
                return m_bError;
            }
        }

        public string ErrorDescription
        {
            get
            {
                return m_sErrorDescription;
            }
        }

        public ArrayList Variables
        {
            get
            {
                ArrayList var = new ArrayList();
                foreach (Symbol sym in m_equation)
                {
                    if ((sym.m_type == Type.Variable) && (!var.Contains(sym)))
                        var.Add(sym);
                }
                return var;
            }
            set
            {
                foreach (Symbol sym in value)
                {
                    for (int i = 0; i < m_postfix.Count; i++)
                    {
                        if ((sym.m_name == ((Symbol)m_postfix[i]).m_name) && (((Symbol)m_postfix[i]).m_type == Type.Variable))
                        {
                            Symbol sym1 = (Symbol)m_postfix[i];
                            sym1.m_value = sym.m_value;
                            m_postfix[i] = sym1;
                        }
                    }
                }
            }
        }
        public void Parse(string equation)
        {
            int state = 1;
            string temp = "";
            Symbol ctSymbol;

            m_bError = false;
            m_sErrorDescription = "None";

            m_equation.Clear();
            m_postfix.Clear();

            int nPos = 0;


            //-- Remove all white spaces from the equation string --
            equation = equation.Trim();
            while ((nPos = equation.IndexOf(' ')) != -1)
                equation = equation.Remove(nPos, 1);

            for (int i = 0; i < equation.Length - 1; i++)
                if (Char.IsNumber(equation[i]))
                {
                    if (Char.IsLetter(equation[i + 1]))
                        equation = string.Format("{0}*{1}", equation.Substring(0, i + 1), equation.Substring(i + 1));
                    switch (equation[i + 1].ToString())
                    {
                        case ",":
                            break;
                        case "(":
                        case "[":
                        case "{":
                            equation = string.Format("{0}*{1}", equation.Substring(0, i + 1), equation.Substring(i + 1));
                            break;
                        default:
                            break;
                    }
                }

            str_save = equation;

            if(equation[0] == '-')
            {
                equation = "0" + equation; 
            }

            for (int i = 0; i < equation.Length; i++)
            {
                switch (state)
                {
                    case 1:
                        if (Char.IsNumber(equation[i]))
                        {
                            state = 2;
                            temp += equation[i];
                        }
                        else if (Char.IsLetter(equation[i]))
                        {
                            state = 3;
                            temp += equation[i];
                        }
                        else
                        {
                            ctSymbol.m_name = equation[i].ToString();
                            ctSymbol.m_value = 0;
                            switch (ctSymbol.m_name)
                            {
                                case ",":
                                    ctSymbol.m_type = Type.Comma;
                                    break;
                                case "(":
                                case ")":
                                case "[":
                                case "]":
                                case "{":
                                case "}":
                                    ctSymbol.m_type = Type.Bracket;
                                    break;
                                default:
                                    ctSymbol.m_type = Type.Operator;
                                    break;
                            }
                            m_equation.Add(ctSymbol);
                        }
                        break;
                    case 2:
                        if ((Char.IsNumber(equation[i])) || (equation[i] == '.'))
                            temp += equation[i];
                        else if (!Char.IsLetter(equation[i]))
                        {
                            state = 1;
                            ctSymbol.m_name = temp;
                            ctSymbol.m_value = Double.Parse(temp);
                            ctSymbol.m_type = Type.Value;
                            m_equation.Add(ctSymbol);
                            ctSymbol.m_name = equation[i].ToString();
                            ctSymbol.m_value = 0;
                            switch (ctSymbol.m_name)
                            {
                                case ",":
                                    ctSymbol.m_type = Type.Comma;
                                    break;
                                case "(":
                                case ")":
                                case "[":
                                case "]":
                                case "{":
                                case "}":
                                    ctSymbol.m_type = Type.Bracket;
                                    break;
                                default:
                                    ctSymbol.m_type = Type.Operator;
                                    break;
                            }
                            m_equation.Add(ctSymbol);
                            temp = "";
                        }
                        break;
                    case 3:
                        if (Char.IsLetterOrDigit(equation[i]))
                            temp += equation[i];
                        else
                        {
                            state = 1;
                            ctSymbol.m_name = temp;
                            ctSymbol.m_value = 0;
                            if (equation[i] == '(')
                                ctSymbol.m_type = Type.Function;
                            else
                            {
                                if (ctSymbol.m_name == "pi")
                                    ctSymbol.m_value = System.Math.PI;
                                else if (ctSymbol.m_name == "e")
                                    ctSymbol.m_value = System.Math.E;
                                ctSymbol.m_type = Type.Variable;
                            }
                            m_equation.Add(ctSymbol);
                            ctSymbol.m_name = equation[i].ToString();
                            ctSymbol.m_value = 0;
                            switch (ctSymbol.m_name)
                            {
                                case ",":
                                    ctSymbol.m_type = Type.Comma;
                                    break;
                                case "(":
                                case ")":
                                case "[":
                                case "]":
                                case "{":
                                case "}":
                                    ctSymbol.m_type = Type.Bracket;
                                    break;
                                default:
                                    ctSymbol.m_type = Type.Operator;
                                    break;
                            }
                            m_equation.Add(ctSymbol);
                            temp = "";
                        }
                        break;
                }
            }
            if (temp != "")
            {
                ctSymbol.m_name = temp;
                if (state == 2)
                {
                    ctSymbol.m_value = Double.Parse(temp);
                    ctSymbol.m_type = Type.Value;
                }
                else
                {
                    if (ctSymbol.m_name == "pi")
                        ctSymbol.m_value = System.Math.PI;
                    else if (ctSymbol.m_name == "e")
                        ctSymbol.m_value = System.Math.E;
                    else
                        ctSymbol.m_value = 0;
                    ctSymbol.m_type = Type.Variable;
                }
                m_equation.Add(ctSymbol);
            }
            this.Infix2Postfix();
            this.arr = this.Variables;
            this.EvaluatePostfix();
        }
        public void Infix2Postfix()
        {
            Symbol tpSym;
            Stack tpStack = new Stack();
            foreach (Symbol sym in m_equation)
            {
                if ((sym.m_type == Type.Value) || (sym.m_type == Type.Variable))
                    m_postfix.Add(sym);
                else if ((sym.m_name == "(") || (sym.m_name == "[") || (sym.m_name == "{"))
                    tpStack.Push(sym);
                else if ((sym.m_name == ")") || (sym.m_name == "]") || (sym.m_name == "}"))
                {
                    if (tpStack.Count > 0)
                    {
                        tpSym = (Symbol)tpStack.Pop();
                        while ((tpSym.m_name != "(") && (tpSym.m_name != "[") && (tpSym.m_name != "{"))
                        {
                            m_postfix.Add(tpSym);
                            tpSym = (Symbol)tpStack.Pop();
                        }
                    }
                }
                else
                {
                    if (tpStack.Count > 0)
                    {
                        tpSym = (Symbol)tpStack.Pop();
                        while ((tpStack.Count != 0) && ((tpSym.m_type == Type.Operator) || (tpSym.m_type == Type.Function) || (tpSym.m_type == Type.Comma)) && (Precedence(tpSym) >= Precedence(sym)))
                        {
                            m_postfix.Add(tpSym);
                            tpSym = (Symbol)tpStack.Pop();
                        }
                        if (((tpSym.m_type == Type.Operator) || (tpSym.m_type == Type.Function) || (tpSym.m_type == Type.Comma)) && (Precedence(tpSym) >= Precedence(sym)))
                            m_postfix.Add(tpSym);
                        else
                            tpStack.Push(tpSym);
                    }
                    tpStack.Push(sym);
                }
            }
            while (tpStack.Count > 0)
            {
                tpSym = (Symbol)tpStack.Pop();
                m_postfix.Add(tpSym);
            }
        }
        public void EvaluatePostfix()
        {
            Symbol tpSym1, tpSym2, tpResult;
            Stack tpStack = new Stack();
            ArrayList fnParam = new ArrayList();
            m_bError = false;
            foreach (Symbol sym in m_postfix)
            {
                if ((sym.m_type == Type.Value) || (sym.m_type == Type.Variable) || (sym.m_type == Type.Result))
                    tpStack.Push(sym);
                else if (sym.m_type == Type.Operator)
                {
                    tpSym1 = (Symbol)tpStack.Pop();
                    tpSym2 = (Symbol)tpStack.Pop();
                    tpResult = Evaluate(tpSym2, sym, tpSym1);
                    if (tpResult.m_type == Type.Error)
                    {
                        m_bError = true;
                        m_sErrorDescription = tpResult.m_name;
                        return;
                    }
                    tpStack.Push(tpResult);
                }
                else if (sym.m_type == Type.Function)
                {
                    fnParam.Clear();
                    tpSym1 = (Symbol)tpStack.Pop();
                    if ((tpSym1.m_type == Type.Value) || (tpSym1.m_type == Type.Variable) || (tpSym1.m_type == Type.Result))
                    {
                        tpResult = EvaluateFunction(sym.m_name, tpSym1);
                        if (tpResult.m_type == Type.Error)
                        {
                            m_bError = true;
                            m_sErrorDescription = tpResult.m_name;
                            return;
                        }
                        tpStack.Push(tpResult);
                    }
                    else if (tpSym1.m_type == Type.Comma)
                    {
                        while (tpSym1.m_type == Type.Comma)
                        {
                            tpSym1 = (Symbol)tpStack.Pop();
                            fnParam.Add(tpSym1);
                            tpSym1 = (Symbol)tpStack.Pop();
                        }
                        fnParam.Add(tpSym1);
                        tpResult = EvaluateFunction(sym.m_name, fnParam.ToArray());
                        if (tpResult.m_type == Type.Error)
                        {
                            m_bError = true;
                            m_sErrorDescription = tpResult.m_name;
                            return;
                        }
                        tpStack.Push(tpResult);
                    }
                    else
                    {
                        tpStack.Push(tpSym1);
                        tpResult = EvaluateFunction(sym.m_name);
                        if (tpResult.m_type == Type.Error)
                        {
                            m_bError = true;
                            m_sErrorDescription = tpResult.m_name;
                            return;
                        }
                        tpStack.Push(tpResult);
                    }
                }
            }
            if (tpStack.Count == 1)
            {
                tpResult = (Symbol)tpStack.Pop();
                m_result = tpResult.m_value;
            }
        }

        protected int Precedence(Symbol sym)
        {
            switch (sym.m_type)
            {
                case Type.Bracket:
                    return 5;
                case Type.Function:
                    return 4;
                case Type.Comma:
                    return 0;
            }
            switch (sym.m_name)
            {
                case "^":
                    return 3;
                case "/":
                case "*":
                case "%":
                    return 2;
                case "+":
                case "-":
                    return 1;
            }
            return -1;
        }

        protected Symbol Evaluate(Symbol sym1, Symbol opr, Symbol sym2)
        {
            Symbol result;
            result.m_name = sym1.m_name + opr.m_name + sym2.m_name;
            result.m_type = Type.Result;
            result.m_value = 0;
            switch (opr.m_name)
            {
                case "^":
                    result.m_value = System.Math.Pow(sym1.m_value, sym2.m_value);
                    break;
                case "/":
                    {
                        //if(sym2.m_value > 0)
                        result.m_value = sym1.m_value / sym2.m_value;
                        //else
                        {
                            //  result.m_name = "Divide by Zero.";
                            //  result.m_type = Type.Error;
                        }
                        break;
                    }
                case "*":
                    result.m_value = sym1.m_value * sym2.m_value;
                    break;
                case "%":
                    result.m_value = sym1.m_value % sym2.m_value;
                    break;
                case "+":
                    result.m_value = sym1.m_value + sym2.m_value;
                    break;
                case "-":
                    result.m_value = sym1.m_value - sym2.m_value;
                    break;
                default:
                    result.m_type = Type.Error;
                    result.m_name = "Không xác định được toán tử " + opr.m_name + ".";
                    break;
            }
            return result;
        }

        protected Symbol EvaluateFunction(string name, params Object[] args)
        {
            Symbol result;
            result.m_name = "";
            result.m_type = Type.Result;
            result.m_value = 0;
            switch (name)
            {
                case "cos":
                    if (args.Length == 1)
                    {
                        result.m_name = name + "(" + ((Symbol)args[0]).m_value.ToString() + ")";
                        result.m_value = System.Math.Cos(((Symbol)args[0]).m_value);
                    }
                    else
                    {
                        result.m_name = "Tham số không đúng: " + name + ".";
                        result.m_type = Type.Error;
                    }
                    break;
                case "sin":
                    if (args.Length == 1)
                    {
                        result.m_name = name + "(" + ((Symbol)args[0]).m_value.ToString() + ")";
                        result.m_value = System.Math.Sin(((Symbol)args[0]).m_value);
                    }
                    else
                    {
                        result.m_name = "Tham số không đúng: " + name + ".";
                        result.m_type = Type.Error;
                    }
                    break;
                case "tan":
                    if (args.Length == 1)
                    {
                        result.m_name = name + "(" + ((Symbol)args[0]).m_value.ToString() + ")";
                        result.m_value = System.Math.Tan(((Symbol)args[0]).m_value);
                    }
                    else
                    {
                        result.m_name = "Tham số không đúng: " + name + ".";
                        result.m_type = Type.Error;
                    }
                    break;
                case "cosh":
                    if (args.Length == 1)
                    {
                        result.m_name = name + "(" + ((Symbol)args[0]).m_value.ToString() + ")";
                        result.m_value = System.Math.Cosh(((Symbol)args[0]).m_value);
                    }
                    else
                    {
                        result.m_name = "Tham số không đúng: " + name + ".";
                        result.m_type = Type.Error;
                    }
                    break;
                case "sinh":
                    if (args.Length == 1)
                    {
                        result.m_name = name + "(" + ((Symbol)args[0]).m_value.ToString() + ")";
                        result.m_value = System.Math.Sinh(((Symbol)args[0]).m_value);
                    }
                    else
                    {
                        result.m_name = "Tham số không đúng: " + name + ".";
                        result.m_type = Type.Error;
                    }
                    break;
                case "tanh":
                    if (args.Length == 1)
                    {
                        result.m_name = name + "(" + ((Symbol)args[0]).m_value.ToString() + ")";
                        result.m_value = System.Math.Tanh(((Symbol)args[0]).m_value);
                    }
                    else
                    {
                        result.m_name = "Tham số không đúng: " + name + ".";
                        result.m_type = Type.Error;
                    }
                    break;
                case "log":
                    if (args.Length == 1)
                    {
                        result.m_name = name + "(" + ((Symbol)args[0]).m_value.ToString() + ")";
                        result.m_value = System.Math.Log10(((Symbol)args[0]).m_value);
                    }
                    else
                    {
                        result.m_name = "Tham số không đúng: " + name + ".";
                        result.m_type = Type.Error;
                    }
                    break;
                case "ln":
                    if (args.Length == 1)
                    {
                        result.m_name = name + "(" + ((Symbol)args[0]).m_value.ToString() + ")";
                        result.m_value = System.Math.Log(((Symbol)args[0]).m_value, 2);
                    }
                    else
                    {
                        result.m_name = "Tham số không đúng: " + name + ".";
                        result.m_type = Type.Error;
                    }
                    break;
                case "logn":
                    if (args.Length == 2)
                    {
                        result.m_name = name + "(" + ((Symbol)args[0]).m_value.ToString() + "'" + ((Symbol)args[1]).m_value.ToString() + ")";
                        result.m_value = System.Math.Log(((Symbol)args[0]).m_value, ((Symbol)args[1]).m_value);
                    }
                    else
                    {
                        result.m_name = "Tham số không đúng: " + name + ".";
                        result.m_type = Type.Error;
                    }
                    break;
                case "sqrt":
                    if (args.Length == 1)
                    {
                        result.m_name = name + "(" + ((Symbol)args[0]).m_value.ToString() + ")";
                        result.m_value = System.Math.Sqrt(((Symbol)args[0]).m_value);
                    }
                    else
                    {
                        result.m_name = "Tham số không đúng: " + name + ".";
                        result.m_type = Type.Error;
                    }
                    break;
                case "abs":
                    if (args.Length == 1)
                    {
                        result.m_name = name + "(" + ((Symbol)args[0]).m_value.ToString() + ")";
                        result.m_value = System.Math.Abs(((Symbol)args[0]).m_value);
                    }
                    else
                    {
                        result.m_name = "Tham số không đúng: " + name + ".";
                        result.m_type = Type.Error;
                    }
                    break;
                case "acos":
                    if (args.Length == 1)
                    {
                        result.m_name = name + "(" + ((Symbol)args[0]).m_value.ToString() + ")";
                        result.m_value = System.Math.Acos(((Symbol)args[0]).m_value);
                    }
                    else
                    {
                        result.m_name = "Tham số không đúng: " + name + ".";
                        result.m_type = Type.Error;
                    }
                    break;
                case "asin":
                    if (args.Length == 1)
                    {
                        result.m_name = name + "(" + ((Symbol)args[0]).m_value.ToString() + ")";
                        result.m_value = System.Math.Asin(((Symbol)args[0]).m_value);
                    }
                    else
                    {
                        result.m_name = "Tham số không đúng: " + name + ".";
                        result.m_type = Type.Error;
                    }
                    break;
                case "atan":
                    if (args.Length == 1)
                    {
                        result.m_name = name + "(" + ((Symbol)args[0]).m_value.ToString() + ")";
                        result.m_value = System.Math.Atan(((Symbol)args[0]).m_value);
                    }
                    else
                    {
                        result.m_name = "Tham số không đúng: " + name + ".";
                        result.m_type = Type.Error;
                    }
                    break;
                case "exp":
                    if (args.Length == 1)
                    {
                        result.m_name = name + "(" + ((Symbol)args[0]).m_value.ToString() + ")";
                        result.m_value = System.Math.Exp(((Symbol)args[0]).m_value);
                    }
                    else
                    {
                        result.m_name = "Tham số không đúng: " + name + ".";
                        result.m_type = Type.Error;
                    }
                    break;
                default:
                    if (m_defaultFunctionEvaluation != null)
                        result = m_defaultFunctionEvaluation(name, args);
                    else
                    {
                        result.m_name = "Không tìm thấy hàm: " + name + ".";
                        result.m_type = Type.Error;
                    }
                    break;
            }
            return result;
        }
        public bool Enable
        {
            get => enable;
            set => enable = value;
        }

        public Color color
        {
            set
            {
                f_color = value;
            }
            get
            {
                return f_color;
            }
        }

        public Function(){
            this.SetRandColor();
        }
        public float[] X
        {
            set
            {
                x = value;
            }
        }
        public void SetRandColor()
        {
            color = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
        }
        //public virtual float f(float _x) => 0;
        public override string ToString()
        {
            return Name + "(x) = " + str_save;
        }
        public virtual string SaveString()
        {
            return this.GetType().ToString() + "\n" + str_save + "\n" + color.ToArgb() + "\n";
        }
        protected bool m_bError = false;
        protected string m_sErrorDescription = "None";
        protected double m_result = 0;
        protected ArrayList m_equation = new ArrayList();
        protected ArrayList m_postfix = new ArrayList();
        protected EvaluateFunctionDelegate m_defaultFunctionEvaluation;
    }
    public class PointG : Function
    {
        private PointF location;
        public PointG() { }
        public PointG(string str, string color)
        {
            int index = 0;
            name = str.Substring(index, str.IndexOf('*', index));
            index = str.IndexOf('*', index);
            location.X = Convert.ToSingle(str.Substring(index + 1, str.IndexOf('*', index + 1) - index - 1));
            index = str.IndexOf('*', index + 1);
            location.Y = Convert.ToSingle(str.Substring(index + 1));
            this.color = Color.FromArgb(Convert.ToInt32(color));
        }
        
        public bool parse(string str)
        {
            str = str.ToLower();
            if (str.IndexOf(',') == -1 || str.IndexOf('(') == -1 || str.IndexOf(')') == -1)
                return false;
            str.Substring(str.IndexOf('('));
            foreach (char i in str)
                if (char.IsLetter(i))
                    return false;
            float va;
            if (float.TryParse(str.Substring(1, str.IndexOf(',') - 1), out va))
                location.X = va;
            else return false;
            str = str.Substring(str.IndexOf(',') + 1);
            str = str.Substring(0, str.Length - 1);
            if (float.TryParse(str, out va))
                location.Y = va;
            else return false;
            return true;
        }
        public PointG(string name, Point p, Point xOy, int k, float dv)
        {
            this.name = name;
            location.X = (float)(p.X - xOy.X) / k * dv;
            location.Y = -(float)(p.Y - xOy.Y) / k * dv;
        }
        public PointF I
        {
            get => location;
            set => location = value;
        }
        public override string SaveString()
        {
            return string.Format("{0}\n{1}\n", this.GetType().ToString(), string.Format("{0}*{1}*{2}\n{3}", name, location.X, location.Y, color.ToArgb()));
        }
        public override string ToString()
        {
            return string.Format("{2}({0}, {1})", location.X, location.Y, name);
        }
    }
    public class Circle : Function
    {
        public Circle() 
        {
            x = new float[3];
            x[0] = x[1] = x[2] = float.NaN;
        }
        public bool parse(string str)
        {
            if(str.IndexOf('=') == -1) return false;
            str = str.ToLower();
            str = str.Trim();
            int nPos;
            while ((nPos = str.IndexOf(' ')) != -1)
                str = str.Remove(nPos, 1);
            if (str.IndexOf("x^2") != -1)
            {
                x[0] = 0f;
                if (str.IndexOf("x^2") == 0)
                    str = str.Substring(4);
                else
                    str = str.Substring(0, str.IndexOf("x^2") - 1) + str.Substring(str.IndexOf("x^2") + 3);
            }
            if (str.IndexOf("y^2") != -1)
            {
                x[1] = 0f;
                if (str.IndexOf("y^2") == 0)
                {
                    if (str[3] == '=')
                        str = str.Substring(3);
                    else str = str.Substring(4);
                }
                else
                    str = str.Substring(0, str.IndexOf("y^2") - 1) + str.Substring(str.IndexOf("y^2") + 3);
            }
            int c;
            float va;
            int ch;
            while (true)
            {
                if (float.IsNaN(x[0]) || float.IsNaN(x[1]))
                    if (str[0] != '(') return false;
                int i;
                string s;
                if (float.IsNaN(x[0]) && float.IsNaN(x[1]))
                    s = ")^2+";
                else
                {
                    if (float.IsNaN(x[0]) || float.IsNaN(x[1]))
                    {
                        s = ")^2";
                    }
                    else
                    {
                        if (float.TryParse(str.Substring(1), out va))
                        {
                            x[2] = (float)Math.Sqrt(va);
                            return true;
                        }
                        else return false;
                    }
                }
                i = str.IndexOf(s);
                if (i == -1) return false;
                string v = str.Substring(1, i - 1);
                ch = -1;c = 0;
                for (int j = 0; j < v.Length; j++)
                {
                    if (Char.IsLetter(v[j]))
                    {
                        if (v[j] != 'x' && v[j] != 'y')
                            return false;
                        if (ch == -1) c++;
                        else return false;
                        if (c != 1) return false;
                        ch = j;
                    }
                }
                if (v[ch] == 'x')
                {
                    if (!float.IsNaN(x[0])) return false;
                    if (ch == 0)
                    {
                        if (float.TryParse(v.Substring(ch + 1), out va))
                            x[0] = -va;
                        else return false;
                    }
                    else
                    {
                        if (float.TryParse(v.Substring(0, ch - 1), out va))
                            x[0] = -va;
                        else return false;
                    }
                }
                else
                {
                    if (!float.IsNaN(x[1])) return false;
                    if (ch == 0)
                    {
                        if (float.TryParse(v.Substring(ch + 1), out va))
                            x[1] = -va;
                        else return false;
                    }
                    else
                    {
                        if (float.TryParse(v.Substring(0, ch - 1), out va))
                            x[1] = -va;
                        else return false;
                    }
                }
                str = str.Substring(i + s.Length);
            }
        }
        public Circle(PointG a, PointG b)
        {
            x = new float[3];
            x[0] = a.I.X;
            x[1] = a.I.Y;
            x[2] = (float)Math.Sqrt((a.I.X - b.I.X) * (a.I.X - b.I.X) + (a.I.Y - b.I.Y) * (a.I.Y - b.I.Y));
        } 
        public Circle(string str, int color)
        {
            x = new float[3];
            int index = 0;
            x[0] = Convert.ToSingle(str.Substring(index, str.IndexOf('*', index)));
            index = str.IndexOf('*', index);
            x[1] = Convert.ToSingle(str.Substring(index + 1, str.IndexOf('*', index + 1) - index - 1));
            index = str.IndexOf('*', index + 1);
            x[2] = Convert.ToSingle(str.Substring(index + 1));
            this.color = Color.FromArgb(color);
        }
        public PointF I
        {
            get
            {
                return new PointF(x[0], x[1]);
            }
        }
        public float A
        {
            get
            {
                return x[0];
            }
            set => x[0] = value;
        }
        
        public float B
        {
            get
            {
                return x[1];
            }
            set => x[1] = value;
        }
        public float R
        {
            get 
            {
                return x[2];
            }
            set => x[2] = value;
        }

        public override string SaveString()
        {
            string v = string.Format("{0}*{1}*{2}", x[0], x[1], x[2]);
            return string.Format("{0}\n{1}\n{2}\n", this.GetType().ToString(), v, color.ToArgb());
        }
        public override string ToString()
        {
            string a = string.Empty, b = string.Empty;
            if (x[0] > 0) a = " - " + x[0];
            else a = " + " + x[0];
            if (x[1] > 0) b = " - " + x[1];
            else b = " + " + x[1];
            if (x[0] == 0f) a = string.Empty;
            if (x[1] == 0f) b = string.Empty;
            string v = string.Empty;
            if (a != string.Empty) v += string.Format("(x{0})^2 + ", a);
            else v += "x^2 + ";
            if (b != string.Empty) v += string.Format("(y{0})^2", b);
            else v += "y^2";
            return v + " = " + x[2] * x[2];
        }
    }
}   