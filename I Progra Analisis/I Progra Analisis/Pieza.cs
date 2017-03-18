using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace I_Progra_Analisis
{
    class Pieza
    {
        private int _numero;
        private int[] _lados;

        public Pieza(int numero)
        {
            this._numero = numero;
            this._lados = new int[4];
            for (int i = 0; i < 4; i++)
            {
                this._lados[i] = 0;
            }
        }

        public void setNumero(int numero)
        {
            this._numero = numero;
        }
        public int getNumero()
        {
            return this._numero;
        }

        public void setLado(int lado, int valor)//Si se quisiera poner un lado de la pieza manualmente
        {
            this._lados[lado-1] = valor;
        }
        public int getLado(int lado)
        {
            return this._lados[lado-1];
        }

        public void setLados(int[] lados)
        {
            for (int i = 0; i < 4; i++)
            {
                this._lados[i] = lados[i];
            }
        }
        public int[] getLados()
        {
            return this._lados;
        }
    }

}
