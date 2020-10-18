using System;
using System.Data.SqlTypes;
using System.Security.Policy;

namespace Fcn
{
    public abstract class Function
    {
        protected float[] x;
        public Function(){}
        public float[] X
        {
            set
            {
                x = value;
            }
        }
        public virtual float f(float _x) { return 0; }
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
        float Pow(float x, int n)
        {
            float relust = 1;
            for (int i = 0; i < n; i++)
                relust *= x;
            return relust;
        }
    }
    class Circle : Function
    {
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
        
    }
}   //          (x - a)^2 + (y - b)^2 = R   =>  y = b + sqrt(R - (x - a)^2)