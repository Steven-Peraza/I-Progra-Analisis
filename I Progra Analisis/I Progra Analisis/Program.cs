﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace I_Progra_Analisis
{
    class Program
    {
        public static string tiempoTot;
        static void Main(string[] args)
        {
            Console.WriteLine("Digite el tamaño que desea que tenga el Tetravex");
            int n = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Digite el orden que desea que tenga la matriz: \n1)Ordenado\n2)Desordenado\n3)Invertida\n");
            int n2 = Convert.ToInt32(Console.ReadLine());
            Tetravex tetravex = new Tetravex(n,n2);
            int[] _solucion = new int[n * n];
            int[] _solucion2 = new int[n * n];
            Pieza[] _solucion3 = new Pieza[n * n];
            //tetravex.tanteo(_solucion);
            //Console.ReadKey();
            for (int i = 0; i < n * n; i++)
                _solucion[i] = i + 1;
            ArrayList auxList = new ArrayList();
            for (int i = 1; i <= n * n; i++)
                auxList.Add(i);
            List<Pieza>[] listaIzquierda = new List<Pieza>[9] { new List<Pieza>(), new List<Pieza>(), new List<Pieza>(), new List<Pieza>(), new List<Pieza>(), new List<Pieza>(), new List<Pieza>(), new List<Pieza>(), new List<Pieza>() };
            //listaIzquierda[0].Add();
            //Console.WriteLine(listaIzquierda[0]);
            //Console.ReadKey();
            for (int i = 1; i <= n*n; i++)
            {
                Tuple<int, int> posicionPA = tetravex.GetPosicionNumero(i);
                Pieza piezaActual = tetravex.GetPieza(posicionPA.Item1, posicionPA.Item2);
                int izquierda = piezaActual.GetLado(1);
                listaIzquierda[izquierda - 1].Add(piezaActual);

            }
            //List<Pieza>[] listaIzquierda = new List<Pieza>[9];
            //Console.WriteLine(_solucion2[1]);
            //Console.ReadKey();
            //tetravex.Permuta(_solucion);
<<<<<<< HEAD
            tetravex.resetContadores();
            var tiempo = System.Diagnostics.Stopwatch.StartNew();
            tetravex.FuerzaBruta(_solucion, 0, _solucion.Length);
            TimeSpan timeSpan = tiempo.Elapsed;
            tiempoTot = timeSpan.Hours.ToString() + "h, " + timeSpan.Minutes.ToString() + "m, " + timeSpan.Seconds.ToString() + "s, " + timeSpan.Milliseconds.ToString() + "ms";
            Console.WriteLine(tiempoTot);
            Console.ReadKey();

            tetravex.resetContadores();
            tiempo = System.Diagnostics.Stopwatch.StartNew();
            tetravex.descarte(_solucion2, auxList, 0);
            tiempo.Stop();
            timeSpan = tiempo.Elapsed;
            tiempoTot = timeSpan.Hours.ToString() + "h, " + timeSpan.Minutes.ToString() + "m, " + timeSpan.Seconds.ToString() + "s, " + timeSpan.Milliseconds.ToString() + "ms";
            Console.WriteLine(tiempoTot);
=======
            //tetravex.FuerzaBruta(_solucion, 0, _solucion.Length);
            //tetravex.descarte(_solucion2, auxList, 0);
            tetravex.tanteo(_solucion3, listaIzquierda, 0);
>>>>>>> d58beb24729106f8f4bfffbc89c4e2f806076597
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