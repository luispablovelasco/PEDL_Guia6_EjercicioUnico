using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Diagnostics;
using System.Threading;

namespace PEDL_Guia6_EjercicioUnico
{
    class Arbol_Binario
    {

        public Nodo_Arbol Raiz;
        public Nodo_Arbol aux;
        public int level = 0, a = 0, b = 0;

        private int altura = 0;

        public int Altura
        {
            get { return altura = 0; }
            set { altura = value; }
        }



        //Variable para los recorridos
        string preorden, enorden, posorden;


        public Arbol_Binario()
        {
            aux = new Nodo_Arbol();
        }

        public Arbol_Binario(Nodo_Arbol nueva_raiz)
        {
            Raiz = nueva_raiz;
        }

        public void Insertar(int x) //Funcion para agregar un nuevo nodo al arbol
        {

            if (Raiz == null)
            {
                Raiz = new Nodo_Arbol(x, null, null, null);
                Raiz.nivel = 0;
            }
            else
            {
                Raiz = Raiz.Insertar(x, Raiz, Raiz.nivel);
                level++;
            }

        }

        public void Eliminar(int x) //Funcion para eliminar un nodo del arbol binario
        {
            if (Raiz == null)
            {
                Raiz = new Nodo_Arbol(x, null, null, null);
            }
            else
            {
                Raiz.Eliminar(x, ref Raiz);
                level--;
            }
        }

        public void Buscar(int x)
        {
            if (Raiz != null)
            {
                Raiz.buscar(x, Raiz);
            }
        }


        // *********** Funciones para dibujar el arbol binarion en el formulario ***************** 
        //Funcion para dibujar arbol binario

        public void DibujarArbol(Graphics grafo, Font fuente, Brush Relleno, Brush RellenoFuente, Pen Lapiz, Brush encuentro)
        {
            int x = 400; // Posiciones de la raíz del árbol
            int y = 75;
            if (Raiz == null)
                return;
            Raiz.PosicionNodo(ref x, y); //Posición de cada nodo
            Raiz.DibujarRamas(grafo, Lapiz); //Dibuja los Enlaces entre nodos
                                             //Dibuja todos los Nodos
            Raiz.DibujarNodo(grafo, fuente, Relleno, RellenoFuente, Lapiz, encuentro);
        }

        public int x1 = 400; //Posiciones iniciales de la raiz del arbol
        public int y2 = 75;

        //Funcion para colorear los nodos
        public void colorear(Graphics grafo, Font fuente, Brush Relleno, Brush RellenoFuente, Pen Lapiz, Nodo_Arbol Raiz, bool post, bool inor, bool preor)
        {
            Brush entorno = Brushes.Red;
            if (inor == true)
            {
                if (Raiz != null)
                {
                    colorear(grafo, fuente, Relleno, RellenoFuente, Lapiz, Raiz.Izquierdo, post, inor, preor);
                    Raiz.colorear(grafo, fuente, entorno, RellenoFuente, Lapiz);
                    Thread.Sleep(1000);
                    // pausar la ejecución 1000 milisegundos
                    Raiz.colorear(grafo, fuente, Relleno, RellenoFuente, Lapiz);
                    colorear(grafo, fuente, Relleno, RellenoFuente, Lapiz, Raiz.Derecho, post, inor, preor);
                }
            }
            else
            if (preor == true)
            {
                if
                (Raiz != null)
                {
                    Raiz.colorear(grafo, fuente, entorno, RellenoFuente, Lapiz);
                    Thread.Sleep(1000);
                    // pausar la ejecución 1000 milisegundos
                    Raiz.colorear(grafo, fuente, Relleno, RellenoFuente, Lapiz);
                    colorear(grafo, fuente, Relleno, RellenoFuente, Lapiz, Raiz.Izquierdo, post,
                     inor, preor);
                    colorear(grafo, fuente, Relleno, RellenoFuente, Lapiz, Raiz.Derecho, post,
                    inor, preor);
                }
            }
            else
            if
            (post == true)
            {
                if
                (Raiz != null)
                {
                    colorear(grafo, fuente, Relleno, RellenoFuente, Lapiz, Raiz.Izquierdo, post, inor, preor);
                    colorear(grafo, fuente, Relleno, RellenoFuente, Lapiz, Raiz.Derecho, post, inor, preor);
                    Raiz.colorear(grafo, fuente, entorno, RellenoFuente, Lapiz);
                    Thread.Sleep(1000); // pausar la ejecución 1000 milisegundos
                    Raiz.colorear(grafo, fuente, Relleno, RellenoFuente, Lapiz);
                }
            }
        }

        

        public string preOrden(Nodo_Arbol llave)
        {
            if (llave != null)
            {
                preorden = preorden + llave.info + ", ";
                preOrden(llave.Izquierdo);
                preOrden(llave.Derecho);
            }
            return preorden;
        } //Procedimiento auxiliar

        public string inOrden(Nodo_Arbol llave)
        {
            if (llave != null)
            {
                inOrden(llave.Izquierdo);
                enorden += llave.info + ",";
                inOrden(llave.Derecho); 
            }
            return enorden;
        } //Procedimiento auxiliar

        public string posOrden(Nodo_Arbol llave)
        {
            if (llave != null)
            {
                posOrden(llave.Izquierdo);
                posOrden(llave.Derecho);
                posorden += llave.info;
            }
            return posorden;
        } //Procedimiento auxiliar

        public void calcularAltura(Nodo_Arbol Nodo)
        {
            if (Nodo != null)
            {
                a++;
                if(a == b)
                {
                    b++;
                    altura = b - 1;
                }
                calcularAltura(Nodo.Izquierdo);
                calcularAltura(Nodo.Derecho);
                a--;
            }
        }

    }
}
