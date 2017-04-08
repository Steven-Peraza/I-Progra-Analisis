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
            cont = 0;
        }
        public bool Comparar(int[] vec, int posicion, int cantidad, int algoritmo)
        {
            asignaciones += 5;
            comparaciones += 1;
            for (int i = posicion; i < cantidad; i++)
            {
                asignaciones += 4;
                comparaciones += 1;
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
                            //Console.WriteLine("diferente anterior primera fila");
                            //////////////////////////////////////////////////////////////////////////////////los returns son asignaciones
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
                        asignaciones += 2;
                        Tuple<int, int> posicionPiezaArriba = GetPosicionNumero(vec[i-this._tamaño]);
                        Pieza piezaArriba = GetPieza(posicionPiezaArriba.Item1, posicionPiezaArriba.Item2);
                        comparaciones += 1;
                        if (piezaArriba.GetLado(2) != piezaA.GetLado(0))
                        {
                            //Console.WriteLine("diferente arriba primera columna");
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
                            //Console.WriteLine("diferente arriba o atras");
                            asignaciones += 1;
                            return false;
                        }
                    }
                }
            }
            comparaciones += 2;
            if (algoritmo == 1)
            {
                // la comparacion de aqui no va por que desde antes se añadio la del elseif y como no se hizo, la uso como si fuera la siguiente
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
            else
            {
                asignaciones += 1;
                return true;
            }
        }
        public void tanteo(Pieza[]solucion, List<Pieza>[] listaIzquierda, int cont)
        {
            Console.WriteLine("empieza");
            List<Pieza>[] listaAux = new List<Pieza>[9] { new List<Pieza>(), new List<Pieza>(), new List<Pieza>(), new List<Pieza>(), new List<Pieza>(), new List<Pieza>(), new List<Pieza>(), new List<Pieza>(), new List<Pieza>() };
            asignaciones += 1;
            comparaciones += 2;
            if (cont < this._tamaño * this._tamaño)
            {
                if ((cont != 0) && (listaIzquierda[(solucion[cont - 1].GetLado(3)) - 1] != null))
                {
                    if (cont < this._tamaño)
                    {
                        Console.WriteLine(listaIzquierda[(solucion[cont-1].GetLado(0))-1]);
                        Console.WriteLine(listaIzquierda[(solucion[cont - 1].GetLado(3)) - 1].Count);
                            int Azopotamadre = listaIzquierda[(solucion[cont - 1].GetLado(3)) - 1].Count;
                            while (Azopotamadre > 0)
                            {
                                Azopotamadre -= 1;
                                if (listaIzquierda[(solucion[cont - 1].GetLado(3)) - 1].Count != 0)//Osea si hay convinacion
                                {
                                    Console.WriteLine("prueba");
                                    solucion[cont] = listaIzquierda[(solucion[cont - 1].GetLado(3)) - 1][0];
                                    listaAux[(solucion[cont - 1].GetLado(3)) - 1].Add(listaIzquierda[(solucion[cont - 1].GetLado(3)) - 1][0]);
                                    listaIzquierda[(solucion[cont - 1].GetLado(3)) - 1].Remove(listaIzquierda[(solucion[cont - 1].GetLado(3)) - 1][0]);
                                    tanteo(solucion, listaIzquierda, cont + 1);
                                    return;
                                }
                                listaIzquierda[(solucion[cont - 1].GetLado(3)) - 1].Add(solucion[cont]);
                                listaAux[(solucion[cont - 1].GetLado(3)) - 1].Remove(solucion[cont]);
                            }
                        
                    }
                    else
                    {
                        if ((cont % this._tamaño == 0) && (listaIzquierda[(solucion[cont - 1].GetLado(2)) - 1] != null))
                        {
                            //aqui revisa el numero de abajo de la de arriba,
                            //pero como encuentro ese numero en la ficha
                            //sera q lo mejor es hacer el for q recorra lo q resta?
                            for (int i = 0; i < 9; i++)
                            {
                                int Azopotamadre = listaIzquierda[i].Count;
                                while (Azopotamadre > 0)
                                {
                                    Azopotamadre -= 1;
                                    Console.WriteLine((listaIzquierda[i][0].GetLado(0)));
                                    if ((listaIzquierda[i][0].GetLado(0) == solucion[cont-this._tamaño].GetLado(2)) && (listaIzquierda[(solucion[cont - 1].GetLado(0)) - 1] != null) && (listaIzquierda[(solucion[cont - 1].GetLado(2)) - 1] != null))
                                    {
                                        listaAux[i].Add(listaIzquierda[i][0]);
                                        solucion[cont] = listaIzquierda[i][0];
                                        listaIzquierda[i].Remove(listaIzquierda[i][0]);
                                        tanteo(solucion, listaIzquierda, cont + 1);
                                        return;
                                    }
                                    listaIzquierda[(solucion[cont - 1].GetLado(3)) - 1].Add(solucion[cont]);
                                    listaAux[(solucion[cont - 1].GetLado(3)) - 1].Remove(solucion[cont]);
                                }
                            }
                            //aqui es q no encontró uno con pieza arriba igual
                        }
                        else
                        {
                            // los 2 verificaciones
                                int azopotamadre = listaIzquierda[(solucion[cont - 1].GetLado(3)) - 1].Count;
                                while (azopotamadre > 0)
                                {
                                    azopotamadre -= 1;
                                    if (listaIzquierda[(solucion[cont - 1].GetLado(3)) - 1][0].GetLado(0) == solucion[cont - this._tamaño].GetLado(2))
                                    {
                                        //se quita y se pone al final,pero se lleva el azopotamadre
                                        listaAux[(solucion[cont - 1].GetLado(3)) - 1].Add(listaIzquierda[(solucion[cont - 1].GetLado(3)) - 1][0]);
                                        solucion[cont] = listaIzquierda[(solucion[cont - 1].GetLado(3)) - 1][0];
                                        listaIzquierda[(solucion[cont - 1].GetLado(3)) - 1].Remove(listaIzquierda[(solucion[cont - 1].GetLado(3)) - 1][0]);
                                        tanteo(solucion, listaIzquierda, cont + 1);
                                        return;
                                    }
                                    listaIzquierda[(solucion[cont - 1].GetLado(3)) - 1].Add(solucion[cont]);
                                    listaAux[(solucion[cont - 1].GetLado(3)) - 1].Remove(solucion[cont]);
                                }
                            
                        }
                    }
                        //escoge la siguiente y compara
                }
                else
                {
                    for (int i = 0; i < 9; i++)
                    {
                        int Azopotamadre = listaIzquierda[i].Count;
                        while (Azopotamadre > 0)
                        {
                            Azopotamadre -= 1;


                            if (listaIzquierda[i].Count != 0)
                            {
                                Console.WriteLine("aassdd");
                                Pieza piezaActual = (Pieza)listaIzquierda[i][0];
                                listaAux[i].Add(piezaActual);
                                solucion[cont] = piezaActual;
                                listaIzquierda[i].Remove(piezaActual);
                                tanteo(solucion, listaIzquierda, cont + 1);
                            }
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < this._tamaño * this._tamaño; i++)
                {
                    Console.WriteLine(_solucion[i] + ", ");
                }
                Console.WriteLine(".............");
                for (int i = 0; i < this._tamaño * this._tamaño; i++)
                {
                    Console.WriteLine(solucion[i].GetNumero() + ", ");
                }
            }
                
        }
        /*public void tanteo2(Pieza[] vec, ArrayList[] listaIzquierda, int cont)
        {
            asignaciones += 4;
            int Azopotamadre = 0;
            comparaciones += 1;
            if (cont < this._tamaño * this._tamaño)
            {
                asignaciones += 1;
                comparaciones += 1;
                for (int i = 0; i < 9; i++)
                {
                    Azopotamadre = listaIzquierda[i].Count;
                    while (Azopotamadre > 0)
                    {
                        Azopotamadre -= 1;
                        asignaciones += 1;
                        comparaciones += 2;
                        if (listaIzquierda[i].Count == 0)
                        {
                            asignaciones += 1;
                            break;
                        }
                        vec[cont] = (Pieza)listaIzquierda[i][0]);
                        listaIzquierda[i].Remove(listaIzquierda[i][0]);
                        asignaciones += 2;
                        comparaciones += 1;
                        if (cont != 0)
                        {
                            comparaciones += 1;
                            if (Comparar(vec, cont, cont + 1, 2))
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
                    Console.Write(vec[j] + ",");
                }
                Console.WriteLine();
                Console.WriteLine("Cantidad de asignaciones: " + asignaciones + "\nCantidad de comparaciones: " + comparaciones);
            }
        }*/
        public void descarte(int[] vec,ArrayList lista, int cont)
        {
            asignaciones += 4;
            int Azopotamadre = 0;
            comparaciones += 1;
            if (cont < this._tamaño * this._tamaño)
            {
                Azopotamadre = lista.Count;
                asignaciones += 1;
                comparaciones += 1;
                while(Azopotamadre > 0)
                {
                    Azopotamadre -= 1;
                    asignaciones += 1;
                    comparaciones += 2;
                    if (lista.Count == 0)
                    {
                        asignaciones += 1;
                        break;
                    }
                    vec[cont] = Convert.ToInt32(lista[0]);
                    lista.Remove(lista[0]);
                    asignaciones += 2;
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
            asignaciones += 4;
            int aux;
            comparaciones += 1;
            if (k < n)
            {
                asignaciones += 1;
                comparaciones += 1;
                for (int i = k; i < n; i++)
                {
                    asignaciones += 2;
                    comparaciones += 1;
                    aux = vec[k];
                    vec[k] = vec[i];
                    vec[i] = aux;
                    asignaciones += 3;
                    if (!FuerzaBruta(vec, k + 1, n))
                    {
                        asignaciones += 1;
                        return false;
                    }
                    else
                    {
                        aux = vec[k];
                        vec[k] = vec[i];
                        vec[i] = aux;
                        asignaciones += 3;
                    }

                }
                asignaciones += 1;
                return true;
            }
            else
            {
                cont += 1;
                asignaciones += 1;
                comparaciones += 1;
                if (Comparar(vec, 0, vec.Length, 1))
                {
                    Console.WriteLine("Solucion actual");
                    for (int i = 0; i < n; i++)
                    {
                        Console.Write(vec[i] + ",");
                    }
                    Console.WriteLine();
                    Console.WriteLine("termino en " + cont);
                    Console.WriteLine("Cantidad de asignaciones: " + asignaciones + "\nCantidad de comparaciones: " + comparaciones);
                    asignaciones += 1;
                    return false;
                }
                //Console.WriteLine("sigue");
                //Console.WriteLine("Cantidad de asignaciones: " + asignaciones + "\nCantidad de comparaciones: " + comparaciones);
                asignaciones += 1;
                return true;
            }
        }
    }
}