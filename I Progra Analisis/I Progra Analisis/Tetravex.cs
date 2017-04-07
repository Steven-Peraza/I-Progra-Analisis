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
            asignaciones += 1;
            comparaciones += 1;
            for (int i = posicion; i < cantidad; i++)
            {
                asignaciones += 3;
                //Console.WriteLine("empieza fuerza bruta");
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
                        asignaciones += 1;
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
            //Console.WriteLine("Acaba de empezar");
            int Azopotamadre = 0;
            asignaciones += 1;
            /*for (int j = 0; j < this._tamaño * this._tamaño; j++)
            {
                Console.Write(vec[j]+",");
            }
            //Console.ReadKey();
            Console.WriteLine();*/
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
                    /*//Console.WriteLine("despues de asignar");
                    for (int j = 0; j < this._tamaño * this._tamaño; j++)
                    {
                        Console.Write(vec[j]+",");
                    }
                    Console.WriteLine();
                    //Console.ReadKey();*/
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
        /*
        public bool CompruebaLista(ArrayList combinaciones,int numeroLista)
        {
            foreach (Tuple<int, int> a in combinaciones)
            {
                if (a.Item1 > cont)
                {
                    combinaciones.Remove(a);
                }
                else
                {
                    if (a.Item1 == cont && a.Item2 == numeroLista)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public void tanteo(int[] vec)
        {
            ArrayList removidos = new ArrayList();
            cont = 0;
            ArrayList auxList = new ArrayList();
            ArrayList combinaciones = new ArrayList();
            for (int i = 1; i <= this._tamaño * this._tamaño; i++)
                auxList.Add(i);
            while (vec[vec.Length - 1] == 0)
            {
                Console.WriteLine("asd");
                if (auxList.Count == 0)
                {
                    while (removidos.Count != 0)
                    {
                        auxList.Add(removidos[0]);
                        removidos.Remove(removidos[0]);
                    }
                }
                    int numeroLista = Convert.ToInt32(auxList[0]);
                if (CompruebaLista(combinaciones, numeroLista))
                {
                    removidos.Add(auxList[0]);
                    auxList.Remove(0);
                }
                else
                {
                    if (auxList.Count != 0)
                    {
                        vec[cont] = Convert.ToInt32(auxList[0]);
                        Tuple<int, int> posicionPiezaA = GetPosicionNumero(vec[cont]);
                        Pieza piezaA = GetPieza(posicionPiezaA.Item1, posicionPiezaA.Item2);
                        removidos.Add(auxList[0]);
                        auxList.Remove(auxList[0]);
                        Tuple<int, int> combinacion = new Tuple<int, int>(cont, vec[cont]);
                        combinaciones.Add(combinacion);
                        if (cont != 0)
                        {
                            if (cont < this._tamaño)
                            {
                                if (cont != 0)
                                {
                                    Tuple<int, int> posicionPiezaAnterior = GetPosicionNumero(vec[cont - 1]);
                                    Pieza piezaAnterior = this._tablero[posicionPiezaAnterior.Item1, (posicionPiezaAnterior.Item2)];
                                    if (piezaAnterior.GetLado(3) != piezaA.GetLado(1))
                                    {
                                        removidos.Add(vec[cont]);
                                        vec[cont] = 0;
                                    }
                                    else
                                    {
                                        cont += 1;
                                    }
                                }
                                else
                                {
                                    cont += 1;
                                }
                            }
                            else
                            {
                                if (cont % this._tamaño == 0)
                                {
                                    Tuple<int, int> posicionPiezaArriba = GetPosicionNumero(vec[cont - this._tamaño]);
                                    Pieza piezaArriba = GetPieza(posicionPiezaArriba.Item1, posicionPiezaArriba.Item2);
                                    if (piezaArriba.GetLado(2) != piezaA.GetLado(0))
                                    {
                                        removidos.Add(vec[cont]);
                                        vec[cont] = 0;
                                    }
                                    else
                                    {
                                        cont += 1;
                                    }
                                }
                                else
                                {
                                    Tuple<int, int> posicionPiezaArriba = GetPosicionNumero(vec[cont - this._tamaño]);
                                    Tuple<int, int> posicionPiezaAnterior = GetPosicionNumero(vec[cont - 1]);
                                    Pieza piezaAnterior = this._tablero[posicionPiezaAnterior.Item1, posicionPiezaAnterior.Item2];
                                    Pieza piezaArriba = this._tablero[posicionPiezaArriba.Item1, posicionPiezaArriba.Item2];
                                    if (piezaAnterior.GetLado(3) != piezaA.GetLado(1) || piezaArriba.GetLado(2) != piezaA.GetLado(0))
                                    {
                                        removidos.Add(vec[cont]);
                                        vec[cont] = 0;
                                    }
                                    else
                                    {
                                        cont += 1;
                                    }

                                }
                            }
                        }
                        else
                            cont += 1;
                    }
                    else
                    {
                        while (removidos.Count != 0)
                        {
                            auxList.Add(removidos[0]);
                            removidos.Remove(removidos[0]);
                        }
                        cont -= 1;
                    }

                }
        }
            if (vec.SequenceEqual(this._solucion))
                Console.WriteLine("A cachete");*/
            
            
           /* public void tanteo(int[] vec)
            {
                ArrayList removidos = new ArrayList();
                cont = 0;
                ArrayList auxList = new ArrayList();
                ArrayList combinaciones = new ArrayList();
                for (int i = 1; i <= this._tamaño * this._tamaño; i++)
                    auxList.Add(i);

                while (vec[vec.Length - 1] == 0)
                {
                    Console.WriteLine("empieza");
                    for (int i = 0; i < vec.Length; i++)
                        Console.Write(vec[i]);
                    Console.WriteLine();
                        int numeroLista = Convert.ToInt32(auxList[0]);
                        if (CompruebaLista(combinaciones,numeroLista))
                        {
                            removidos.Add(auxList[0]);
                            auxList.Remove(0);
                            vec[cont] = 0;
                        }
                        else
                        {
                            Console.WriteLine("no esta");
                            if (auxList.Count != 0)
                            {
                                vec[cont] = Convert.ToInt32(auxList[0]);
                                Tuple<int, int> posicionPiezaA = GetPosicionNumero(vec[cont]);
                                Pieza piezaA = GetPieza(posicionPiezaA.Item1, posicionPiezaA.Item2);
                                removidos.Add(auxList[0]);
                                auxList.Remove(auxList[0]);
                                Tuple<int, int> combinacion = new Tuple<int, int>(cont, vec[cont]);
                                combinaciones.Add(combinacion);
                                for (int i = 0; i < vec.Length; i++)
                                    Console.Write(vec[i]);
                                Console.WriteLine();
                                Console.WriteLine("lista: ");
                                for (int i = 0; i < auxList.Count; i++)
                                    Console.Write(auxList[i]);
                                Console.WriteLine();
                                Console.WriteLine("removidos: ");
                                for (int i = 0; i < removidos.Count; i++)
                                    Console.Write(removidos[i]);
                                Console.WriteLine();
                                Console.WriteLine("--------------------------");
                                Console.WriteLine();
                                Console.ReadKey();
                                if (cont < this._tamaño)
                                {
                                    if (cont != 0)
                                    {
                                        Tuple<int, int> posicionPiezaAnterior = GetPosicionNumero(vec[cont - 1]);
                                        Pieza piezaAnterior = this._tablero[posicionPiezaAnterior.Item1, (posicionPiezaAnterior.Item2)];
                                        if (piezaAnterior.GetLado(3) != piezaA.GetLado(1))
                                        {
                                            removidos.Add(vec[cont]);
                                            vec[cont] = 0;
                                        }
                                        else
                                        {
                                            cont += 1;
                                        }
                                    }
                                    else
                                    {
                                        cont += 1;
                                    }
                                }
                                else
                                {
                                    if (cont % this._tamaño == 0)
                                    {
                                        Tuple<int, int> posicionPiezaArriba = GetPosicionNumero(vec[cont - this._tamaño]);
                                        Pieza piezaArriba = GetPieza(posicionPiezaArriba.Item1, posicionPiezaArriba.Item2);
                                        if (piezaArriba.GetLado(2) != piezaA.GetLado(0))
                                        {
                                            removidos.Add(vec[cont]);
                                            vec[cont] = 0;
                                        }
                                        else
                                        {
                                            cont += 1;
                                        }
                                    }
                                    else
                                    {
                                        Tuple<int, int> posicionPiezaArriba = GetPosicionNumero(vec[cont - this._tamaño]);
                                        Tuple<int, int> posicionPiezaAnterior = GetPosicionNumero(vec[cont - 1]);
                                        Pieza piezaAnterior = this._tablero[posicionPiezaAnterior.Item1, posicionPiezaAnterior.Item2];
                                        Pieza piezaArriba = this._tablero[posicionPiezaArriba.Item1, posicionPiezaArriba.Item2];
                                        if (piezaAnterior.GetLado(3) != piezaA.GetLado(1) || piezaArriba.GetLado(2) != piezaA.GetLado(0))
                                        {
                                            removidos.Add(vec[cont]);
                                            vec[cont] = 0;
                                        }
                                        else
                                        {
                                            cont += 1;
                                        }

                                    }
                                }
                            }
                            else
                            {
                                while (removidos.Count != 0)
                                {
                                    auxList.Add(removidos[0]);
                                    removidos.Remove(removidos[0]);
                                }
                                cont -= 1;
                            }
                        }
                            vec[cont] = Convert.ToInt32(auxList[0]);
                            Tuple<int, int> posicionPiezaA = GetPosicionNumero(vec[cont]);
                            Pieza piezaA = GetPieza(posicionPiezaA.Item1, posicionPiezaA.Item2);
                            removidos.Add(auxList[0]);
                            auxList.Remove(auxList[0]);
                            Tuple<int, int> combinacion = new Tuple<int, int>(cont, vec[cont]);
                            combinaciones.Add(combinacion);
                            cont += 1;





        }            
            
            else
            {

                cont -= 1;
                removidos.Add(vec[cont]);
                vec[cont] = 0;
                while (removidos.Count != 0)
                {
                    auxList.Add(removidos[0]);
                    removidos.Remove(0);
                }
            }*/
        
        public bool FuerzaBruta(int[] vec, int k, int n)
        {
            Console.WriteLine("Acaba de empezar");
            for (int j = 0; j < n; j++)
            {
                Console.Write(vec[j]);
            }
            Console.ReadKey();
            Console.WriteLine();
            int aux;
            if (k<n)
            {
                for (int i=k;i<n;i++)
                {
                    Console.WriteLine("Entro");
                    for (int j = 0; j < n; j++)
                    {
                        Console.Write(vec[j]);
                    }
                    Console.ReadKey();
                    Console.WriteLine();
                    aux = vec[k];
                    vec[k] = vec[i];
                    vec[i] = aux;
                    Console.WriteLine("Despues de hacer unas asignaciones");
                    for (int j = 0; j < n; j++)
                    {
                        Console.Write(vec[j]);
                    }
                    Console.ReadKey();
                    Console.WriteLine();
                    //GeneraPermutacion(vec, k + 1, n);
                    if (!FuerzaBruta(vec, k + 1, n))
                    {
                        return false;
                    }
                    else
                    {
                        aux = vec[k];
                        vec[k] = vec[i];
                        vec[i] = aux;
                        
                    }
                    
                }
                return true;
            }
            else
            {
                cont += 1;
                //FuerzaBruta(vec);
                /*for (int i = 0; i < n; i++)
                {
                    Console.Write(vec[i] + ",");
                }
                Console.WriteLine();*/
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
                    return false;
                }
                //Console.WriteLine(cont);
                return true;
            }
        }
    }
}