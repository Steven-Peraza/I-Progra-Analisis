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
        private int[] _solucionActual;
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
        public void FuerzaBrutra()
        {

        }
        /*public void Permuta(int[] s)
        {
            //Iniciamos este array auxiliar para
            //marcar los caracteres que ya combinamos
            int[] d = new int[s.Length];
            bool[] marcas = new bool[s.Length];
            //Llamamos al método recursivo
            Permuta(s, d, marcas);
        }

        public void Permuta(int[] original, int[] permutacion, bool[] marcas)
        {
            //Imprimimos la combinación si ya cambiamos
            //todas las letras una vez
            if (permutacion[permutacion.Length-1] != 0)
            {
                for (int a = 0; a < permutacion.Length; a++)
                    Console.Write(permutacion[a]);
                Console.WriteLine();
                //for (int a = 0; a < permutacion.Length; a++)
                  //  permutacion[a] = 0;
            }
            for (int i = 0; i < marcas.Length; i++)
            {
                //Vemos si está marcada para no volverla a permutar
                if (!marcas[i])
                {
                    //Marcamos el caracter que vamos a permutar
                    marcas[i] = true;
                    //Invocamos al metodo recursivo añadiendo
                    //un caracter al string que permutamos
                    for (int j = 0; j < permutacion.Length; j++)
                    {
                        if (permutacion[j] == 0)
                        {
                            permutacion[j] = original[i];
                            break;
                        }
                    }
                    //Console.WriteLine("pieza = "+ original[i]);
                    Permuta(original, permutacion, marcas);
                    //Desmarcamos el caracter para poder usarlo
                    //en otras combinaciones
                    marcas[i] = false;
                    //for (int a = 0; a < permutacion.Length; a++)
                      //    permutacion[a] = 0;
                        //permutacion[i] = 0;
                }
            }
        }*/
        /*public void Permuta(string s)
        {
            //Iniciamos este array auxiliar para
            //marcar los caracteres que ya combinamos
            bool[] marcas = new bool[s.Length];
            //Llamamos al método recursivo
            Permuta(s, "", marcas);
        }
        public void Permuta(string original, string permutacion, bool[] marcas)
        {
            //Imprimimos la combinación si ya cambiamos
            //todas las letras una vez
            if (original.Length == permutacion.Length)
                Console.WriteLine(permutacion);


            for (int i = 0; i < marcas.Length; i++)
            {
                Console.WriteLine("permutacion al inicio "+permutacion);
                //Vemos si está marcada para no volverla a permutar
                if (!marcas[i])
                {
                    //Marcamos el caracter que vamos a permutar
                    marcas[i] = true;
                    //Invocamos al metodo recursivo añadiendo
                    //un caracter al string que permutamos
                    Console.WriteLine(permutacion+ " permutacion"+ original[i]+ " lugar");
                    Permuta(original, permutacion + original[i], marcas);
                    Console.WriteLine("permutacion despues de llamar al void " + permutacion);
                    //Desmarcamos el caracter para poder usarlo
                    //en otras combinaciones
                    marcas[i] = false;
                }
            }
        }*/
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
                Console.WriteLine();
                for (int i = 0; i<n; i++)
                {
                    Console.Write(vec[i]+"\t");
                }
            }
        }
    }
}