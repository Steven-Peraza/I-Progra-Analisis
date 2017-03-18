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
            Console.WriteLine("Digite el tamaño que desea que tenga el Tetravex");
            int n = Convert.ToInt32(Console.ReadLine());
            Tetravex tetravex = new Tetravex(n);


            for (int i = 0; i < n; i++) //Imprime piezas
            {
                for (int j = 0; j < n; j++)
                {
                    Console.WriteLine("Pieza "+i+","+j);
                    Pieza pieza = tetravex.getPieza(i,j);
                    for (int k = 0; k < 4; k++)
                    {
                        Console.WriteLine(pieza.getLado(k));
                    }
                    Console.ReadKey();
                }
            }
        }
    }
}
