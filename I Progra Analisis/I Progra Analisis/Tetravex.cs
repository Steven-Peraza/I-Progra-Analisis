using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace I_Progra_Analisis
{
    class Tetravex
    {
        static int asignaciones;
        static int comparaciones;
        private Pieza[,] _tablero;
        private int _tamaño;
        private int[] _solucion;//Orden de la solucion
        private Tuple<int,int>[] _posicionNumero; //Posiciones de un numero
        private Random _random = new Random();
        int numeroRandom;
        int cont;
        public Tetravex(int tamaño,int orden)
        {
            this._tamaño = tamaño;
            this._tablero = new Pieza[tamaño, tamaño];
            this._posicionNumero = new Tuple<int, int>[this._tamaño * this._tamaño];
            this._solucion = new int[this._tamaño*this._tamaño];
            GenerarTablero();
            Revolver(orden);
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
        public void resetContadores()
        {
            asignaciones = 0;
            comparaciones = 0;
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
        public void Revolver(int orden)//cambia variables numero entre si, y guarda la convinacion en solucion
        {
            if(orden == 1)
            {
                cont = 0;
                for (int i = 0; i < this._solucion.Length; i++)
                {
                    this._solucion[i] = i+1;
                }
                for (int i = 0; i < this._tamaño; i++)
                {
                    for (int j = 0; j < this._tamaño; j++)
                    {
                        this._posicionNumero[this._solucion[cont] - 1] = new Tuple<int, int>(i, j);
                        cont += 1;
                    }
                }
                cont = 0;
            }
            else if (orden == 2)
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
                cont = 0;
            }
            else
            {
                cont = 0;
                for (int i = this._solucion.Length; i > 0;cont++, i--)
                {

                    this._solucion[cont] = i;
                }
                cont = 0;
                for (int i = 0; i < this._tamaño; i++)
                {
                    for (int j = 0; j < this._tamaño; j++)
                    {
                        this._tablero[i, j].SetNumero(this._solucion[cont]);
                        this._posicionNumero[this._solucion[cont] - 1] = new Tuple<int, int>(i, j);
                        cont += 1;
                    }
                }
                cont = 0;
            }
        }
        public bool Comparar(int[] vec, int posicion, int cantidad, int algoritmo)
        {
            asignaciones += 1;
            comparaciones += 1;
            for (int i = posicion; i < cantidad; i++)
            {
                asignaciones += 3;
                Tuple<int,int> posicionPiezaA = GetPosicionNumero(vec[i]);        
                Pieza piezaA = GetPieza(posicionPiezaA.Item1, posicionPiezaA.Item2);
                comparaciones += 1;
                if (i < this._tamaño)
                {
                    comparaciones += 1;
                    if (i!=0)
                    {
                        asignaciones += 2;
                        Tuple<int, int> posicionPiezaAnterior = GetPosicionNumero(vec[i-1]);
                        Pieza piezaAnterior = this._tablero[posicionPiezaAnterior.Item1,(posicionPiezaAnterior.Item2)];
                        comparaciones += 1;
                        if (piezaAnterior.GetLado(3) != piezaA.GetLado(1))
                        {
                            asignaciones += 1;
                            return false;
                        }
                    }
                }
                else
                {
                    comparaciones += 1;
                    if (i%this._tamaño ==0)
                    {
                        asignaciones += 1;
                        Tuple<int, int> posicionPiezaArriba = GetPosicionNumero(vec[i-this._tamaño]);
                        Pieza piezaArriba = GetPieza(posicionPiezaArriba.Item1, posicionPiezaArriba.Item2);
                        comparaciones += 1;
                        if (piezaArriba.GetLado(2) != piezaA.GetLado(0))
                        {
                            asignaciones += 1;
                            return false;
                        }
                    }
                    else
                    {
                        asignaciones += 4;
                        Tuple<int, int> posicionPiezaArriba = GetPosicionNumero(vec[i - this._tamaño]);
                        Tuple<int, int> posicionPiezaAnterior = GetPosicionNumero(vec[i - 1]);
                        Pieza piezaAnterior = this._tablero[posicionPiezaAnterior.Item1, posicionPiezaAnterior.Item2];
                        Pieza piezaArriba = this._tablero[posicionPiezaArriba.Item1, posicionPiezaArriba.Item2];
                        comparaciones += 1;
                        if (piezaAnterior.GetLado(3) != piezaA.GetLado(1) || piezaArriba.GetLado(2) != piezaA.GetLado(0))
                        {
                            asignaciones += 1;
                            return false;
                        }
                    }
                }
            }
            comparaciones += 1;
            if (algoritmo == 1)
            {
                comparaciones += 1;
                if (this._solucion.SequenceEqual(vec))
                {
                    asignaciones += 1;
                    return true;
                }
                else
                {
                    asignaciones += 1;
                    return false;
                }
            }
            else if (algoritmo == 2)
            {
                asignaciones += 1;
                return true;
            }
            asignaciones += 1;
            return true;
        }
        public void tanteo(int[] vec, int cont)
        {
            
            ArrayList[] listaIzquierda = new ArrayList[9];
            ArrayList[] listaDerecha = new ArrayList[9];
            //int[] lista1 = new int[this._tamaño * this._tamaño];
            //int[] lista2 = new int[this._tamaño * this._tamaño];
            int Azopotamadre = 0;
            for (int i = 1; i<= this._tamaño * this._tamaño; i++)
            {
                Tuple <int,int> posicionPA = GetPosicionNumero(i);
                Pieza piezaActual = GetPieza(posicionPA.Item1,posicionPA.Item2);
                int arriba = piezaActual.GetLado(0);
                int izquierda = piezaActual.GetLado(1);
                int abajo = piezaActual.GetLado(2);
                int derecha = piezaActual.GetLado(3);
                listaIzquierda[izquierda - 1].Add(piezaActual);
                listaDerecha[derecha-1].Add(piezaActual);
            }
            if (cont < this._tamaño * this._tamaño)
            {
                //Azopotamadre = lista.Count;
                asignaciones += 1;
                comparaciones += 2;
                for (int i = cont; Azopotamadre > 0; i++, Azopotamadre--)
                {

                }
            }
        }
       public void descarte(int[] vec,ArrayList lista, int cont)
        {
            int Azopotamadre = 0;
            asignaciones += 1;
            comparaciones += 1;
            if (cont < this._tamaño * this._tamaño)
            {
                Azopotamadre = lista.Count;
                asignaciones += 1;
                comparaciones += 2;
                for (int i = cont; Azopotamadre > 0; i++,Azopotamadre--)
                {
                    asignaciones += 1;
                    comparaciones += 1;
                    if (lista.Count == 0)
                    {
                        ///////////////////////////////////////////////////////////////////////////////////////break cuenta como asignacion?
                        asignaciones += 1;
                        break;

                    }
                    vec[cont] = Convert.ToInt32(lista[0]);
                    lista.Remove(lista[0]);
                    asignaciones += 1;
                    comparaciones += 1;
                    if (cont != 0)
                    {
                        comparaciones += 1;
                        if (Comparar(vec, cont, cont + 1,2))
                        {
                            //Console.WriteLine("comparo bien");
                            descarte(vec, lista, cont + 1);
                        }
                        else
                        {
                            lista.Add(vec[cont]);
                            vec[cont] = 0;
                            asignaciones += 2;
                        }
                    }
                    else
                    {
                        //Console.WriteLine("asd");
                        descarte(vec, lista, cont + 1);
                    }           
                }
                comparaciones += 1;
                if (lista.Count != 0)
                {
                    vec[cont] = 0;
                    //Console.WriteLine("aassdd");
                    cont -= 1;
                    lista.Add(vec[cont]);
                    vec[cont] = 0;
                    asignaciones += 4;
                    //descarte(vec, lista, cont);
                }
            }
            else
            {
                Console.WriteLine("fin");
                for (int j = 0; j < this._tamaño * this._tamaño; j++)
                {
                    Console.Write(vec[j]+",");
                }
                Console.WriteLine();
                Console.WriteLine("Cantidad de asignaciones: "+asignaciones+ "\nCantidad de comparaciones: "+comparaciones);
            }
        }

        public bool FuerzaBruta(int[] vec, int k, int n)
        {
            int aux;
            asignaciones++;
            comparaciones++;
            if (k<n)
            {
                asignaciones += 2;
                comparaciones++;
                for (int i=k; i<n; i++)
                {
                    asignaciones += 3;
                    aux = vec[k];
                    vec[k] = vec[i];
                    vec[i] = aux;
                    if (!FuerzaBruta(vec, k + 1, n))
                    {
                        asignaciones++;
                        return false;
                    }
                    else
                    {
                        asignaciones += 3;
                        aux = vec[k];
                        vec[k] = vec[i];
                        vec[i] = aux;
                    }                  
                }
                asignaciones++;
                return true;
            }
            else
            {
                asignaciones++;
                cont += 1;

                if (Comparar(vec,0,vec.Length,1))
                {
                    Console.WriteLine("Solucion original");
                    for (int i = 0; i < n; i++)
                    {
                        Console.Write(this._solucion[i] + ",");
                    }
                    Console.WriteLine();
                    Console.WriteLine("Solucion actual");
                    for (int i = 0; i < n; i++)
                    {
                        Console.Write(vec[i] + ",");
                    }
                    Console.WriteLine();
                    Console.WriteLine("termino en "+cont);
                    Console.WriteLine("Cantidad de asignaciones: " + asignaciones + "\nCantidad de comparaciones: " + comparaciones);
                    asignaciones++;
                    return false;
                }
                asignaciones++;
                return true;
            }
        }
    }
}