using System;
using System.Data.SqlTypes;
using System.Drawing;
using System.Security.Policy;

namespace Fcn
{
    public abstract class Function
    {
        private Random random = new Random();
        protected float[] x;
        bool enable = true;
        protected Color f_color;

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
        public virtual float f(float _x) => 0;
        public virtual string SaveString()
        {
            return this.GetType().ToString();
        }
    }
    class Bac_n : Function
    {
        int n;
        public int N
        {
            get
            {
                return n;
            }
            set
            {
                n = value;
            }
        }
        public Bac_n(int i)
        {
            n = i;
            x = new float[3];
        }
        public override float f(float _x)
        {
            float y_relust = 0;
            if (n > 0) n++;
            else n--;
            if(n > 0)
            {
                for(int i = 0; i < n; i++)
                {
                    y_relust += x[i] * Pow(_x, n - i - 1);
                }
                n--;
            }
            else
            {
                for (int i = 0; i < -n; i++)
                {
                    y_relust += x[i] / Pow(_x, -n - i - 1);
                }
                n++;
            }
            return y_relust;
        }
        public override string SaveString()
        {
            string v = "";
            int m = 0;
            if (n > 0) m = n;
            else m = 2;
            for(int i = 0; i < m; i++)
            {
                v += x[i].ToString() + "*";
            }
            return String.Format("{0}\n{1}\n{2}\n{3}\n",base.SaveString(), n, v, color.ToArgb());
        }
        float Pow(float x, int n)
        {
            float relust = 1;
            for (int i = 0; i < n; i++)
                relust *= x;
            return relust;
        }
        public override string ToString()
        {
            if(n != -1)
            {
                string v = "y = ";
                n++;
                for(int i = 0; i < n - 1; i++)
                {
                    string pv = string.Empty;
                    if (x[i] != 0)
                    {
                        if (Math.Abs(x[i]) != 1)
                            pv = string.Format("{0}x^{1}", Math.Abs(x[i]), n - i - 1);
                        else
                            pv = string.Format("x^{0}", n - i - 1);
                        if (i != 0)
                        {
                            if (x[i] > 0) pv = " + " + pv;
                            if (x[i] < 0) pv = " - " + pv;
                        }
                        v += pv;
                    }
                }
                if (x[n - 1] > 0) v += " + " + x[n - 1];
                if (x[n - 1] < 0) v += " - " + x[n - 1];
                n--;
                return v;
            }
            else
            {
                string v = string.Format("y = 1 / {0}x", x[0]);
                if (x[1] > 0) return v + " + " + x[1];
                if (x[1] < 0) return v + " - " + x[1];
                return v;
            }
        }
    }
    class Circle : Function
    {
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
        }
        public float B
        {
            get
            {
                return x[1];
            }
        }
        public float R
        {
            get 
            {
                return x[2];
            }
        }
        public override string SaveString()
        {
            string v = string.Format("{0}*{1}*{2}", x[0], x[1], x[2]);
            return string.Format("{0}\n{1}\n{2}\n",base.SaveString(), v, color.ToArgb());
        }
        public override string ToString()
        {
            string a = string.Empty, b = string.Empty;
            if (x[0] > 0) a = " - " + x[0];
            else a = " + " + x[0];
            if (x[1] > 0) b = " - " + x[1];
            else b = " + " + x[1];
            string v = string.Empty;
            if (a != string.Empty) v += string.Format("(x{0})^2 + ", a);
            else v += "x^2 + ";
            if (b != string.Empty) v += string.Format("(y{0})^2", b);
            else v += "y^2";
            return v + " = " + x[2] * x[2];
        }
    }
}   //          (x - a)^2 + (y - b)^2 = R   =>  y = b + sqrt(R - (x - a)^2)