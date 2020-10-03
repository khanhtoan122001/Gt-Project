using System;

namespace Fcn
{
    public class Function
    {
        protected float y_relust = 0;
        protected float[] x;
        public Function(){}
        virtual public float relust
        {
            get
            {
                return y_relust;
            }
        }
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
        public override float f(float _x)
        {
            if (n > 0) n++;
            else n--;
            if(n > 0)
                for(int i = 0; i < n; i++)
                {
                    y_relust += x[i] * Pow(_x, n - i - 1);
                }
            else
                for (int i = 0; i < -n; i++)
                {
                    y_relust += x[i] / Pow(_x, -n - i - 1);
                }
            return this.relust;
        }
        float Pow(float x, int n)
        {
            float relust = 1;
            for (int i = 0; i < n; i++)
                relust *= x;
            return relust;
        }
    }
}