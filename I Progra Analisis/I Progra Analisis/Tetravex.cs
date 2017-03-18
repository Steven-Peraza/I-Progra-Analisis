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

        public void generarTablero()//por ahora solo genera el tablero con todos los numeros de las piezas random
        {
            cont = 1;
            for (int i = 0; i < this._tamaño; i++)
            {
                for (int j = 0; j < this._tamaño; j++)
                {
                    Pieza pieza = new Pieza(cont);
                    for (int k = 0; k < 4; k++)
                    {
                        int numeroRandom = _random.Next(1, 10);
                        pieza.setLado(k + 1, numeroRandom);
                    }
                    this._tablero[i, j] = pieza;
                    cont += 1;
                }
                
            }
                
        }
    }
}
