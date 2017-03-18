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
        private Random _random = new Random();
        int numeroRandom;
        int cont;

        public Tetravex(int tamaño)
        {
            this._tamaño = tamaño;
            this._tablero = new Pieza[tamaño, tamaño];
            generarTablero();
        }

        public void setPieza(int fila, int columna, Pieza pieza)
        {
            this._tablero[fila, columna] = pieza;
        }
        public Pieza getPieza(int fila, int columna)
        {
            return this._tablero[fila, columna];
        }

        public void generarTablero()//genera el tablero ordenado y resuelto
        {
            cont = 1;
            for (int i = 0; i < this._tamaño; i++)
            {
                for (int j = 0; j < this._tamaño; j++)
                {
                    Pieza pieza = new Pieza(cont);
                    if (i == 0)// Si es la primera fila
                    {
                        if (j == 0)//Si es la primera columna
                        {
                            for (int k = 0; k < 4; k++)
                            {
                                numeroRandom = _random.Next(1, 10);
                                pieza.setLado(k, numeroRandom);
                            }
                            this._tablero[i, j] = pieza;
                            cont += 1;
                        }
                        else
                        {
                            Pieza piezaAnterior = getPieza(i, j - 1);
                            numeroRandom = _random.Next(1, 10);
                            pieza.setLado(0, numeroRandom);
                            pieza.setLado(1, piezaAnterior.getLado(3));
                            numeroRandom = _random.Next(1, 10);
                            pieza.setLado(2, numeroRandom);
                            numeroRandom = _random.Next(1, 10);
                            pieza.setLado(3, numeroRandom);
                            this._tablero[i, j] = pieza;
                            cont += 1;
                        }

                    }
                    else
                    {
                        if (j == 0)
                        {
                            Pieza piezaArriba = getPieza(i-1, j);
                            pieza.setLado(0, piezaArriba.getLado(2));
                            numeroRandom = _random.Next(1, 10);
                            pieza.setLado(1, numeroRandom);
                            numeroRandom = _random.Next(1, 10);
                            pieza.setLado(2, numeroRandom);
                            numeroRandom = _random.Next(1, 10);
                            pieza.setLado(3, numeroRandom);
                            this._tablero[i, j] = pieza;
                            cont += 1;
                        }
                        else
                        {
                            Pieza piezaArriba = getPieza(i - 1, j);
                            Pieza piezaAnterior= getPieza(i, j-1);
                            pieza.setLado(0,piezaArriba.getLado(2));
                            pieza.setLado(1,piezaAnterior.getLado(3));
                            numeroRandom = _random.Next(1, 10);
                            pieza.setLado(2, numeroRandom);
                            numeroRandom = _random.Next(1, 10);
                            pieza.setLado(3, numeroRandom);
                            this._tablero[i, j] = pieza;
                            cont += 1;
                        }
                    }
                }
            }
        }
    }
}
