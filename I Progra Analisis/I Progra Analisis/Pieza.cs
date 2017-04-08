using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace I_Progra_Analisis
{
    class Pieza
    {
        private int _numero;
        private int[] _lados;  //arriba,izquierda,abajo,derecha

        public Pieza(int numero)
        {
            this._numero = numero;
            this._lados = new int[4];
            for (int i = 0; i < 4; i++)
            {
                this._lados[i] = 0;
            }
        }

        public void SetNumero(int numero)
        {
            this._numero = numero;
        }
        public int GetNumero()
        {
            return this._numero;
        }

        public void SetLado(int lado, int valor)//Si se quisiera poner un lado de la pieza manualmente
        {
            this._lados[lado] = valor;
        }
        public int GetLado(int lado)
        {
            return this._lados[lado];
        }

        public void SetLados(int[] lados)
        {
            for (int i = 0; i < 4; i++)
            {
                this._lados[i] = lados[i];
            }
        }
        public int[] GetLados()
        {
            return this._lados;
        }
    }

}
