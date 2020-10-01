using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Fcn;
namespace G_team
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
            Bac_n a = new Bac_n();
            float[] b = { 2, 1 };
            a.X = b;
            a.N = 2;
            Console.WriteLine(a.f(2));
        }
    }
}
