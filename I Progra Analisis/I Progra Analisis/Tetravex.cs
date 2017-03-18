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

        public void generarTablero()
        {

        }
    }
}
