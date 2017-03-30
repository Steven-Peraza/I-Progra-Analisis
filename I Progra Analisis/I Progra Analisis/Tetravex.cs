using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace I_Progra_Analisis
{
    class Tetravex
    {
        private Pieza[,] _tablero;
        private int _tamaño;
        private int[] _solucion;//Orden de la solucion
        private Tuple<int,int>[] _posicionNumero; //Posiciones de un numero

        private Random _random = new Random();
        int numeroRandom;
        int cont;
        int cont2;

        public Tetravex(int tamaño)
        {
            this._tamaño = tamaño;
            this._tablero = new Pieza[tamaño, tamaño];
            this._posicionNumero = new Tuple<int, int>[this._tamaño * this._tamaño];
            this._solucion = new int[this._tamaño*this._tamaño];
            GenerarTablero();
            Revolver();
        }
        public void SetPieza(int fila, int columna, Pieza pieza)
        {
            this._tablero[fila, columna] = pieza;
        }
        public Pieza GetPieza(int fila, int columna)
        {
            return this._tablero[fila, columna];
        }
        public int[] GetSolucion()
        {
            return this._solucion;
        }
        public Tuple<int,int> GetPosicionNumero(int numero)//ejemplo buscar el numero 3 estaria en la posicion 2 y dentro tendria las posiciones en la matriz
        {
            return this._posicionNumero[numero-1];
        }
        public void GenerarTablero()//genera el tablero ordenado y resuelto
        {
            cont = 1;
            for (int i = 0; i < this._tamaño; i++)
            {
                for (int j = 0; j < this._tamaño; j++)
                {
                    Pieza pieza = new Pieza(cont);
                    this._solucion[cont - 1] = cont;
                    if (i == 0)// Si es la primera fila, no reviza arriba
                    {
                        if (j == 0)//Si es la primera columna, hace todo random
                        {
                            for (int k = 0; k < 4; k++)
                            {
                                numeroRandom = _random.Next(1, 10);
                                pieza.SetLado(k, numeroRandom);
                            }
                            this._tablero[i, j] = pieza;
                            cont += 1;
                        }
                        else// si no es la primera columna reviza solo el anterior por q esta en la primera fila
                        {
                            Pieza piezaAnterior = GetPieza(i, j - 1);
                            numeroRandom = _random.Next(1, 10);
                            pieza.SetLado(0, numeroRandom);
                            pieza.SetLado(1, piezaAnterior.GetLado(3));
                            numeroRandom = _random.Next(1, 10);
                            pieza.SetLado(2, numeroRandom);
                            numeroRandom = _random.Next(1, 10);
                            pieza.SetLado(3, numeroRandom);
                            this._tablero[i, j] = pieza;
                            cont += 1;
                        }

                    }
                    else
                    {
                        if (j == 0)// si esta en la primera columna solo reviza el que tiene arriba, ya que no es la primera fila y no tiene anterior
                        {
                            Pieza piezaArriba = GetPieza(i-1, j);
                            pieza.SetLado(0, piezaArriba.GetLado(2));
                            numeroRandom = _random.Next(1, 10);
                            pieza.SetLado(1, numeroRandom);
                            numeroRandom = _random.Next(1, 10);
                            pieza.SetLado(2, numeroRandom);
                            numeroRandom = _random.Next(1, 10);
                            pieza.SetLado(3, numeroRandom);
                            this._tablero[i, j] = pieza;
                            cont += 1;
                        }
                        else// reviza anterior y arriba, ya que no es ni primera fila ni primera columna
                        {
                            Pieza piezaArriba = GetPieza(i - 1, j);
                            Pieza piezaAnterior= GetPieza(i, j-1);
                            pieza.SetLado(0,piezaArriba.GetLado(2));
                            pieza.SetLado(1,piezaAnterior.GetLado(3));
                            numeroRandom = _random.Next(1, 10);
                            pieza.SetLado(2, numeroRandom);
                            numeroRandom = _random.Next(1, 10);
                            pieza.SetLado(3, numeroRandom);
                            this._tablero[i, j] = pieza;
                            cont += 1;
                        }
                    }
                }
            }
        }
        public void Revolver()//cambia variables numero entre si, y guarda la convinacion en solucion
        {
            cont = 0;
            for (int i = this._solucion.Length; i > 0; i--)
            {
                int j = this._random.Next(i);
                int k = this._solucion[j];
                this._solucion[j] = this._solucion[i - 1];
                this._solucion[i - 1] = k;
            }
            for (int i = 0; i < this._tamaño; i++)
            {
                for (int j = 0; j < this._tamaño; j++)
                {
                    this._tablero[i, j].SetNumero(this._solucion[cont]);
                    this._posicionNumero[this._solucion[cont] - 1] = new Tuple<int, int>(i, j);
                    cont += 1;
                }
            }
        }
        public void FuerzaBruta(int[] vec)
        {
            for (int i = 0; i < vec.Length; i++)
            {
                Console.WriteLine("empieza fuerza bruta");
                Tuple<int,int> posicionPiezaA = GetPosicionNumero(vec[i]);        
                Pieza piezaA = GetPieza(posicionPiezaA.Item1, posicionPiezaA.Item2);
                if (i < this._tamaño)
                {
                    if (i!=0)
                    {
                        Tuple<int, int> posicionPiezaAnterior = GetPosicionNumero(vec[i-1]);
                        Pieza piezaAnterior = this._tablero[posicionPiezaAnterior.Item1,(posicionPiezaAnterior.Item2)];
                        if (piezaAnterior.GetLado(3) != piezaA.GetLado(1))
                        {
                            //Console.WriteLine("diferente anterior primera fila");
                            return;
                        }
                    }
                }
                else
                {
                    if (i%this._tamaño ==0)
                    {
                        Tuple<int, int> posicionPiezaArriba = GetPosicionNumero(vec[i-this._tamaño]);
                        Pieza piezaArriba = GetPieza(posicionPiezaArriba.Item1, posicionPiezaArriba.Item2);
                        if (piezaArriba.GetLado(2) != piezaA.GetLado(0))
                        {
                            //Console.WriteLine("diferente arriba primera columna");
                            return;
                        }
                    }
                    else
                    {
                        Tuple<int, int> posicionPiezaArriba = GetPosicionNumero(vec[i - this._tamaño]);
                        Tuple<int, int> posicionPiezaAnterior = GetPosicionNumero(vec[i - 1]);
                        Pieza piezaAnterior = this._tablero[posicionPiezaAnterior.Item1, posicionPiezaAnterior.Item2];
                        Pieza piezaArriba = this._tablero[posicionPiezaArriba.Item1, posicionPiezaArriba.Item2];
                        if (piezaAnterior.GetLado(3) != piezaA.GetLado(1) || piezaArriba.GetLado(2) != piezaA.GetLado(0))
                        {
                            //Console.WriteLine("diferente arriba o atras");
                            return;
                        }
                    }
                }
            }
            Console.WriteLine("Encontro solucion");
        }
        public void GeneraPermutacion(int[] vec, int k, int n)
        {
            int aux;
            if (k<n)
            {
                for (int i=k;i<n;i++)
                {
                    aux = vec[k];
                    vec[k] = vec[i];
                    vec[i] = aux;
                    GeneraPermutacion(vec, k + 1, n);
                    aux = vec[k];
                    vec[k] = vec[i];
                    vec[i] = aux;
                }
            }
            else
            {
                cont += 1;
                FuerzaBruta(vec);
                if (vec.SequenceEqual(this._solucion))
                {
                    Console.WriteLine("Solucion original");
                    for (int i = 0; i < n; i++)
                    {
                        Console.Write(this._solucion[i] + ",");
                    }
                    Console.WriteLine();
                    Console.WriteLine("Solucion");
                    for (int i = 0; i < n; i++)
                    {
                        Console.Write(vec[i] + ",");
                    }
                    Console.WriteLine();
                    Console.WriteLine("termino en "+cont);
                    return;
                }
                Console.WriteLine(cont);

                /*Console.WriteLine();
                for (int i = 0; i<n; i++)
                {
                    Console.Write(vec[i]+",");
                }*/
            }
        }
    }
}