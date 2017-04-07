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
            Console.WriteLine("Arriba\nIzquierda\nAbajon\nDerecha\n");
            //int[] _solucion = {1,2,3};
            for (int i = 0; i < n; i++) //Imprime piezas
            {
                for (int j = 0; j < n; j++)
                {
                    Pieza pieza = tetravex.GetPieza(i, j);
                    Console.WriteLine("Pieza " + i + "," + j + " numero: " + pieza.GetNumero());
                    for (int k = 0; k < 4; k++)
                    {
                        Console.WriteLine(pieza.GetLado(k));
                    }
                    Console.ReadKey();
                }
            }
            int[] _solucion = new int[n * n];
            tetravex.tanteo(_solucion);
            Console.ReadKey();
            for (int i = 0; i < n * n; i++)
                _solucion[i] = i + 1;
            //Console.WriteLine(_solucion2[1]);
            //Console.ReadKey();
            //tetravex.Permuta(_solucion);
            //tetravex.GeneraPermutacion(_solucion, 0, _solucion.Length);
            Console.ReadKey();

            for (int i = 0; i < n; i++) //Imprime piezas
            {
                for (int j = 0; j < n; j++)
                {
                    Pieza pieza = tetravex.GetPieza(i, j);
                    Console.WriteLine("Pieza "+i+","+j+" numero: "+pieza.GetNumero());
                    for (int k = 0; k < 4; k++)
                    {
                        Console.WriteLine(pieza.GetLado(k));
                    }
                    Console.ReadKey();
                }
            }
            Console.WriteLine("El numero 2 esta en: "+tetravex.GetPosicionNumero(2));
            Console.ReadKey();
        }
    }
}