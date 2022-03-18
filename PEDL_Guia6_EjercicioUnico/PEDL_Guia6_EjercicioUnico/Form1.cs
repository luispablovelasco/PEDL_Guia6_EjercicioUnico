using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace PEDL_Guia6_EjercicioUnico
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            lblaltura.Text = "";
            lblnivel.Text = "";
            lblsuma.Text = "";
            lbltotalnodos.Text = "";

        }

        //Declaraciond e valraible a utilizar
        int Dato = 0;
        int cont = 0;
        Arbol_Binario mi_Arbol = new Arbol_Binario(null); //Creacion del objeto arbol
        Graphics g; //Definicion del objeto grafico

        Nodo_Arbol nodoArbolAux = new Nodo_Arbol(); //Nodo para los recorridos

        //Lista para hacer la suma de los valores de los nodos
        public List<int> valoresNodos = new List<int>();

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(this.BackColor);
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g = e.Graphics;
            mi_Arbol.DibujarArbol(g, this.Font, Brushes.Blue, Brushes.White, Pens.Black, Brushes.White);
        }

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            if (txtDato.Text == "")
            {
                MessageBox.Show("Debe Ingresar un Valor");
            }
            else
            {
                Dato = int.Parse(txtDato.Text);
                if (Dato <= 0 || Dato >= 100)
                    MessageBox.Show("Solo Recibe Valores desde 1 hasta 99", "Error de Ingreso");
                else
                {
                    mi_Arbol.Insertar(Dato);
                    valoresNodos.Add(Dato); //Añadimos el dato tambien en la lista para sumarlo despues
                    txtDato.Clear();
                    txtDato.Focus();
                    cont++;
                    Refresh();
                    Refresh();
                }
            }

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (txtEliminar.Text == "")
            {
                MessageBox.Show("Debe ingresar el valor a eliminar");
            }
            else
            {
                Dato = Convert.ToInt32(txtEliminar.Text);
                if (Dato <= 0 || Dato >= 100)
                {
                    MessageBox.Show("Sólo se adminten valores entre 1 y 99", "Error de Ingreso");
                }
                else
                {
                    mi_Arbol.Eliminar(Dato);
                    valoresNodos.Remove(Dato); //Eliminamos el mismo elemento pero de la lista de suma
                    txtEliminar.Clear();
                    txtEliminar.Focus();
                    cont--;
                    Refresh();
                    Refresh();
                }
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (txtBuscar.Text == "")
            {
                MessageBox.Show("Debe ingresar el valor a buscar");
            }
            else
            {
                Dato = Convert.ToInt32(txtBuscar.Text);
                if (Dato <= 0 || Dato >= 100)
                {
                    MessageBox.Show("Sólo se admiten valores entre 1 y 99", "Error de Ingreso");
                }
                else
                {
                    mi_Arbol.Buscar(Dato);
                    txtBuscar.Clear();
                    txtBuscar.Focus();
                    Refresh();
                    Refresh();
                }
            }
        }

        private void cboxRecorridos_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cboxRecorridos.SelectedItem.Equals("En - Orden"))
            {
                //InOrden o EnOrden
                listBox1.Items.Clear();
                nodoArbolAux.listInOrden.Clear();

                nodoArbolAux.inOrden(mi_Arbol.Raiz);
                foreach (var valores in nodoArbolAux.listInOrden)
                {
                    listBox1.Items.Add(valores);
                }


            }

            if (cboxRecorridos.SelectedItem.Equals("Pre - Orden"))
            {
                //PreOrden
                listBox1.Items.Clear();
                nodoArbolAux.listPreOrden.Clear();

                nodoArbolAux.preOrden(mi_Arbol.Raiz);
                foreach (var valores in nodoArbolAux.listPreOrden)
                {
                    listBox1.Items.Add(valores);
                }

            }

            if (cboxRecorridos.SelectedItem.Equals("Post - Orden"))
            {
                //PostOrden
                listBox1.Items.Clear();
                nodoArbolAux.listPostOrden.Clear();

                nodoArbolAux.postOrden(mi_Arbol.Raiz);
                foreach (var valores in nodoArbolAux.listPostOrden)
                {
                    listBox1.Items.Add(valores);
                }

            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Actualizamos los datos de la investigación

            //Suma de la información de los nodos
            int suma = valoresNodos.Sum();
            int alturaTotal = mi_Arbol.calcularAltura();
            lblsuma.Text = "" + suma;
            lbltotalnodos.Text = "" + cont; //Numero de nodos
            lblaltura.Text = "" + alturaTotal; //Alatura del arbol

        }
    }
}
