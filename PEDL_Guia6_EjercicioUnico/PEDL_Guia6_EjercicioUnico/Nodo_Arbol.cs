using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Threading;

namespace PEDL_Guia6_EjercicioUnico
{
    class Nodo_Arbol
    {

        public int info;
        public Nodo_Arbol Izquierdo;
        public Nodo_Arbol Derecho;
        public Nodo_Arbol Padre;
        public int altura;
        public int nivel;
        public Rectangle nodo; //Para dibujar el nodo del arbol

        int n = 0, a = 1;

        private const int Radio = 30; //Varaible que define el tamaño de los circulos qu4e representa los nodos del arbo
        private const int DistanciaH = 80; //Variable para el manejo de distancia horizontal
        private const int DistanciaV = 10; //Variable para el manejo de distancia vertical
        private int CoordenadaX; //Variable para manejar posicion en X
        private int CoordenadaY; //Variable para manejar posicion en Y
        Graphics col;
        private Arbol_Binario arbol;

        public Nodo_Arbol()
        {
        }

        public Arbol_Binario Arbol //Constructor por defecto para el objeto de tipo arbol
        {
            get { return arbol; }
            set { arbol = value; }
        }


        //Constructor con parametros
        public Nodo_Arbol(int nueva_info, Nodo_Arbol izquierdo, Nodo_Arbol derecho, Nodo_Arbol padre)
        {
            info = nueva_info;
            Izquierdo = izquierdo;
            Derecho = derecho;
            Padre = padre;
            altura = 0;
        }

        public Nodo_Arbol Insertar(int x, Nodo_Arbol t, int Level) //Función para insertar un nodo en el arbol
        {
            if (t == null)
            {
                t = new Nodo_Arbol(x, null, null, null);
                t.nivel = Level;
            }
            else if (x < t.info) //si el valor a insertar es menor que la raíz
            {
                Level++;
                t.Izquierdo = Insertar(x, t.Izquierdo, Level);
            }
            else if (x > t.info) //si el valor a insertar es mayor que la raíz
            {
                Level++;
                t.Derecho = Insertar(x, t.Derecho, Level);
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Dato existente en el Arbol", "Error de Ingreso");
            }
            return t;
        }

        //Funciñon para eliminar un nodo de un arbol binario
        public void Eliminar(int x, ref Nodo_Arbol t)
        {
            if (t != null) //si la raíz es distinta de null
            {
                if (x < t.info) //si el valor a eliminar es menor que la raíz
                {
                    Eliminar(x, ref t.Izquierdo);
                }
                else
                {
                    if (x > t.info) //si el valor a eliminar es mayor que la raíz
                    {
                        Eliminar(x, ref t.Derecho);
                    }
                    else
                    {
                        Nodo_Arbol NodoEliminar = t; //se ubica el nodo a eliminar
                        //se verifica si tiene hijo derecho

                        if (NodoEliminar.Derecho == null)
                        {
                            t = NodoEliminar.Izquierdo;
                        }
                        else
                        {
                            //se verifica si tiene hijo izq
                            if (NodoEliminar.Izquierdo == null)
                            {
                                t = NodoEliminar.Derecho;
                            }
                            else
                            {
                                if (Alturas(t.Izquierdo) - Alturas(t.Derecho) > 0)
                                //Para verificar que hijo pasa a ser nueva raíz del subárbol
                                {
                                    Nodo_Arbol AuxiliarNodo = null;
                                    Nodo_Arbol Auxiliar = t.Izquierdo;
                                    bool bandera = false;
                                    while (Auxiliar.Derecho != null)
                                    {
                                        AuxiliarNodo = Auxiliar;
                                        Auxiliar = Auxiliar.Derecho;
                                        bandera = true;
                                    }
                                    // se crea nodo temporal
                                    t.info = Auxiliar.info;
                                    NodoEliminar = Auxiliar;
                                    if (bandera == true)
                                    {
                                        AuxiliarNodo.Derecho = Auxiliar.Izquierdo;
                                    }
                                    else
                                    {
                                        t.Izquierdo = Auxiliar.Izquierdo;
                                    }
                                }
                                else
                                {
                                    if (Alturas(t.Derecho) - Alturas(t.Izquierdo) > 0)
                                    {
                                        Nodo_Arbol AuxiliarNodo = null;
                                        Nodo_Arbol Auxiliar = t.Derecho;
                                        bool bandera = false;

                                        while (Auxiliar.Izquierdo != null)
                                        {
                                            AuxiliarNodo = Auxiliar;
                                            Auxiliar = Auxiliar.Izquierdo;
                                            bandera = true;
                                        }

                                        t.info = Auxiliar.info;
                                        NodoEliminar = Auxiliar;

                                        if (bandera == true)
                                        {
                                            AuxiliarNodo.Izquierdo = Auxiliar.Derecho;
                                        }
                                        else
                                        {
                                            t.Derecho = Auxiliar.Derecho;
                                        }
                                    }


                                    else
                                    {
                                        if (Alturas(t.Derecho) - Alturas(t.Izquierdo) == 0)
                                        {
                                            Nodo_Arbol AuxiliarNodo = null;
                                            Nodo_Arbol Auxiliar = t.Izquierdo;
                                            bool bandera = false;
                                            while (Auxiliar.Derecho != null)
                                            {
                                                AuxiliarNodo = Auxiliar;
                                                Auxiliar = Auxiliar.Derecho;
                                                bandera = true;
                                            }
                                            t.info = Auxiliar.info;
                                            NodoEliminar = Auxiliar;
                                            if (bandera == true)
                                            {
                                                AuxiliarNodo.Derecho = Auxiliar.Izquierdo;
                                            }
                                            else
                                            {
                                                t.Izquierdo = Auxiliar.Izquierdo;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            else
            {
                System.Windows.Forms.MessageBox.Show("Nodo NO existente en el arbol", "Error de eliminacion");
            }
        }//Fin de la funcion eliminar

        public void buscar(int x, Nodo_Arbol t) //Funcion buscar un nodo
        {
            if (t != null)
            {
                if (x == t.info)
                {
                    System.Windows.Forms.MessageBox.Show("Nodo encontrado en la posicion X: " + t.CoordenadaX + " Y: " + t.CoordenadaY);
                    Encontrado(t);
                }
                else
                {
                    if (x < t.info) //búsqueda en el subárbol izquierdo
                    {
                        buscar(x, t.Izquierdo);
                    }
                    else
                    {
                        if (x > t.info) //búsqueda en el subárbol derecho
                        {
                            buscar(x, t.Derecho);
                        }
                    }
                }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Nodo NO encontrado", "Error de busqueda");
            }
        }

        //Las siguientes funciones permiten dibujar el arbol binario 

        public void PosicionNodo(ref int xmin, int ymin) //Funcion posicion nodo (Donde se ha creado dibujo del nodo)
        {
            int aux1, aux2;
            CoordenadaY = (int)(ymin + Radio / 2);

            //Obtiene la posicion del sub-arbol izquierdo
            if (Izquierdo != null)
            {
                Izquierdo.PosicionNodo(ref xmin, ymin + Radio + DistanciaV);
            }
            if ((Izquierdo != null) && (Derecho != null))
            {
                xmin += DistanciaH;
            }
            //Si esxite nodo derecho y el nodo izquierdo deja un espacio entre ellos
            if (Derecho != null)
            {
                Derecho.PosicionNodo(ref xmin, ymin + Radio + DistanciaV);
            }
            if (Izquierdo != null && Derecho != null)
                CoordenadaX = (int)((Izquierdo.CoordenadaX + Derecho.CoordenadaX) / 2);
            else if (Izquierdo != null)
            {
                aux1 = Izquierdo.CoordenadaX;
                Izquierdo.CoordenadaX = CoordenadaX - 80;
                CoordenadaX = aux1;
            }
            else
        if (Derecho != null)
            {
                aux2 = Derecho.CoordenadaX;
                //no hay nodo izquierdo, centrar en nodo derecho
                Derecho.CoordenadaX = CoordenadaX + 80;
                CoordenadaX = aux2;
            }
            else
            {
                CoordenadaX = (int)(xmin + Radio / 2); xmin += Radio;
            }
        }

        //Funcion para dibujar las ramas de los nodo izquierdo y derecho
        public void DibujarRamas(Graphics grafo, Pen Lapiz)
        {

            if (Izquierdo != null)
            // Dibujará rama izquierda
            {
                grafo.DrawLine(Lapiz, CoordenadaX, CoordenadaY,
                Izquierdo.CoordenadaX, Izquierdo.CoordenadaY);
                Izquierdo.DibujarRamas(grafo, Lapiz);
            }
            if
            (Derecho != null)
            // Dibujará rama derecha
            {
                grafo.DrawLine(Lapiz, CoordenadaX, CoordenadaY,
               Derecho.CoordenadaX, Derecho.CoordenadaY);
                Derecho.DibujarRamas(grafo, Lapiz);
            }
        }

        //Funcion para dibujar el nodo en la posicion especificada
        public void DibujarNodo(Graphics grafo, Font fuente, Brush Relleno, Brush RellenoFuente, Pen Lapiz, Brush encuentro)
        {
            col = grafo;

            // Dibuja el contorno del nodo
            Rectangle rect = new Rectangle((int)(CoordenadaX - Radio / 2), (int)(CoordenadaY - Radio / 2), Radio, Radio);
            Rectangle prueba = new Rectangle((int)(CoordenadaX - Radio / 2), (int)(CoordenadaY - Radio / 2), Radio, Radio);
            grafo.FillEllipse(encuentro, rect);
            grafo.FillEllipse(Relleno, rect);
            grafo.DrawEllipse(Lapiz, rect);

            // Para dibujar el nombre del nodo, es decir el contenido
            StringFormat formato = new StringFormat();
            formato.Alignment = StringAlignment.Center;
            formato.LineAlignment = StringAlignment.Center;
            grafo.DrawString(info.ToString(), fuente, RellenoFuente, CoordenadaX, CoordenadaY, formato);

            //Dibuja los nodos hijos derecho e izquierdo

            if (Izquierdo != null)
            {
                Izquierdo.DibujarNodo(grafo, fuente, Relleno, RellenoFuente, Lapiz,
               encuentro);
            }
            if (Derecho != null)
            {
                Derecho.DibujarNodo(grafo, fuente, Relleno, RellenoFuente, Lapiz, encuentro);
            }
        }

        public void colorear(Graphics grafo, Font fuente, Brush Relleno, Brush RellenoFuente, Pen Lapiz)
        {
            //Dibuja el contorno del nodo.
            Rectangle rect = new Rectangle((int)(CoordenadaX - Radio / 2), (int)(CoordenadaY - Radio / 2), Radio, Radio);
            Rectangle prueba = new Rectangle((int)(CoordenadaX - Radio / 2), (int)(CoordenadaY - Radio / 2), Radio, Radio);
            grafo.FillEllipse(Relleno, rect);
            grafo.DrawEllipse(Lapiz, rect);

            //Dibuja el nombre
            StringFormat formato = new StringFormat();
            formato.Alignment = StringAlignment.Center;
            formato.LineAlignment = StringAlignment.Center;
            grafo.DrawString(info.ToString(), fuente, RellenoFuente, CoordenadaX, CoordenadaY, formato);
        }

        private static int Alturas(Nodo_Arbol t)
        {
            return t == null ? -1 : t.altura;
        }

        public void Encontrado(Nodo_Arbol t)
        {
            Rectangle rec = new Rectangle(t.CoordenadaX, t.CoordenadaY, 40, 40);
        }


        //Listas en donde se guardaran los recorridos de los nodos

        public List<int> listPreOrden = new List<int>();
        public List<int> listInOrden = new List<int>();
        public List<int> listPostOrden = new List<int>();

        //Recorrido pre orden (R,I,D)
        public void preOrden(Nodo_Arbol nodo)
        {

            if (nodo != null)
            {
                listPreOrden.Add(nodo.info);
                preOrden(nodo.Izquierdo);
                preOrden(nodo.Derecho);
            }

        }

        //Recorrido en orden (I,R,D)
        public void inOrden(Nodo_Arbol nodo)
        {

            if (nodo != null)
            {
                inOrden(nodo.Izquierdo);
                listInOrden.Add(nodo.info);
                inOrden(nodo.Derecho);
            }
        }

        //Recorrido Post Orden (I,D,R)
        public void postOrden(Nodo_Arbol nodo)
        {

            if (nodo != null)
            {
                postOrden(nodo.Izquierdo);
                postOrden(nodo.Derecho);
                listPostOrden.Add(nodo.info);
            }
        }

    }
}

