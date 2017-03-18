using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace I_Progra_Analisis
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Digite el tamaño que desea que tenga el Tetravex \n");
            int n = Convert.ToInt32(Console.ReadLine());
            Tetravex tetravex = new Tetravex(n);
        }
    }
}
