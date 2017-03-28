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

        public void FuerzaBruta()
        {
            int[] lista1 = { 1, 2, 3, 4, 5, 6 };
            int[] lista2 = { 1, 2, 3, 4, 5, 6 };
            if (lista1.SequenceEqual(lista2))
            {
                Console.WriteLine("Vamonos de joda!!!");
            }
            else
            {
                Console.WriteLine("Vamonos de joda, mentira...!!!");
            }
        }
    }
}
