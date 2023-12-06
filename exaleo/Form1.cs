    using System;
    using System.Drawing;
    using System.Windows.Forms;

    public partial class Form1 : Form
    {
        private ArbolBinario arbol;
        private TextBox txtEntrada;
        private Button btnInsertar, btnEliminar, btnBuscar, btnRecorrido;
        private Nodo nodoSeleccionado; // Nodo seleccionado durante la búsqueda


        private void InitializeComponent()
        {
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Text = "Form1";
        }

        public Form1()
        {
            InitializeComponent();
            arbol = new ArbolBinario();


            txtEntrada = new TextBox();
            txtEntrada.Location = new System.Drawing.Point(13, 13);
            this.Controls.Add(txtEntrada);

            btnInsertar = new Button();
            btnInsertar.Text = "Insertar";
            btnInsertar.Location = new System.Drawing.Point(13, 40);
            btnInsertar.Click += new EventHandler(btnInsertar_Click);
            this.Controls.Add(btnInsertar);

            btnEliminar = new Button();
            btnEliminar.Text = "Eliminar";
            btnEliminar.Location = new System.Drawing.Point(13, 70);
            btnEliminar.Click += new EventHandler(btnEliminar_Click);
            this.Controls.Add(btnEliminar);

            btnBuscar = new Button();
            btnBuscar.Text = "Buscar";
            btnBuscar.Location = new System.Drawing.Point(13, 100);
            btnBuscar.Click += new EventHandler(btnBuscar_Click);
            this.Controls.Add(btnBuscar);

            btnRecorrido = new Button();
            btnRecorrido.Text = "Recorrido";
            btnRecorrido.Location = new System.Drawing.Point(13, 130);
            btnRecorrido.Click += new EventHandler(btnRecorrido_Click);
            this.Controls.Add(btnRecorrido);
        }

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            int valor = int.Parse(txtEntrada.Text);
            arbol.Insertar(valor);
            this.Invalidate(); // Esto redibujará el formulario, incluyendo el árbol.
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            int valor = int.Parse(txtEntrada.Text);
            arbol.Eliminar(valor);
            this.Invalidate(); // Esto redibujará el formulario, incluyendo el árbol.
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                int valor = int.Parse(txtEntrada.Text);
                nodoSeleccionado = null; // Restablece el nodo seleccionado
                BuscarNodoConPadre(arbol.raiz, valor, null);

                if (nodoSeleccionado != null)
                {
                    // Resalta el nodo encontrado
                    nodoSeleccionado.color = Color.Red; // Cambia el color del nodo a rojo
                    this.Invalidate(); // Vuelve a dibujar el árbol

                    // Muestra el mensaje con el padre
                    Nodo padre = ObtenerPadre(arbol.raiz, valor);
                    string mensaje = $"Valor {valor} encontrado en el árbol.";
                    if (padre != null)
                    {
                        mensaje += $" El padre es {padre.valor}.";
                    }
                    MessageBox.Show(mensaje, "Resultado de la búsqueda");
                }
                else
                {
                    // Muestra un mensaje de advertencia
                    MessageBox.Show($"Valor {valor} no encontrado en el árbol. Mostrando tipos de recorridos.", "Resultado de la búsqueda");

                    // Muestra los tipos de recorridos
                    string tiposRecorridos = "Tipos de Recorridos:\n" +
                                            "1. Preorden\n" +
                                            "2. Inorden\n" +
                                            "3. Postorden\n" +
                                            "4. Anchura";
                    MessageBox.Show(tiposRecorridos, "Tipos de Recorridos Disponibles");
                }

                // Restablece el color original después de cerrar el mensaje
                if (nodoSeleccionado != null)
                {
                    nodoSeleccionado.color = Color.LightBlue; // Cambia el color del nodo a negro
                    this.Invalidate(); // Vuelve a dibujar el árbol
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Por favor, ingresa un valor numérico válido.", "Error de entrada");
            }
        }

        private void BuscarNodoConPadre(Nodo nodo, int valor, Nodo padre)
        {
            if (nodo == null)
            {
                return;
            }
            else if (valor < nodo.valor)
            {
                BuscarNodoConPadre(nodo.izquierdo, valor, nodo);
            }
            else if (valor > nodo.valor)
            {
                BuscarNodoConPadre(nodo.derecho, valor, nodo);
            }
            else
            {
                // Nodo encontrado
                nodoSeleccionado = nodo;
            }
        }

        private Nodo ObtenerPadre(Nodo nodo, int valor)
        {
            if (nodo == null || (nodo.izquierdo == null && nodo.derecho == null))
            {
                return null;
            }

            if ((nodo.izquierdo != null && nodo.izquierdo.valor == valor) ||
                (nodo.derecho != null && nodo.derecho.valor == valor))
            {
                return nodo;
            }

            if (valor < nodo.valor)
            {
                return ObtenerPadre(nodo.izquierdo, valor);
            }
            else
            {
                return ObtenerPadre(nodo.derecho, valor);
            }
        }

        private void btnRecorrido_Click(object sender, EventArgs e)
    {
        RealizarRecorrido();
    }

  private void RealizarRecorrido()
{
    // Recorrido en Preorden
    var preordenResultados = new List<int>();
    arbol.RecorridoPreorden(nodo => 
    {
        VisitarNodo(nodo);
        preordenResultados.Add(nodo.valor);
    });
    MostrarResultadoRecorrido("Recorrido en Preorden", preordenResultados);
    LimpiarColores(); // Limpiar colores antes de realizar otro recorrido

    // Recorrido en Inorden
    var inordenResultados = new List<int>();
    arbol.RecorridoInorden(nodo => 
    {
        VisitarNodo(nodo);
        inordenResultados.Add(nodo.valor);
    });
    MostrarResultadoRecorrido("Recorrido en Inorden", inordenResultados);
    LimpiarColores(); // Limpiar colores antes de realizar otro recorrido

    // Recorrido en Postorden
    var postordenResultados = new List<int>();
    arbol.RecorridoPostorden(nodo => 
    {
        VisitarNodo(nodo);
        postordenResultados.Add(nodo.valor);
    });
    MostrarResultadoRecorrido("Recorrido en Postorden", postordenResultados);
    LimpiarColores(); // Limpiar colores antes de realizar otro recorrido

    // Recorrido en Anchura
    var anchuraResultados = new List<int>();
    arbol.RecorridoAnchura(nodo => 
    {
        VisitarNodo(nodo);
        anchuraResultados.Add(nodo.valor);
    });
    MostrarResultadoRecorrido("Recorrido en Anchura", anchuraResultados);
    LimpiarColores(); // Limpiar colores antes de realizar otro recorrido
}

private void MostrarResultadoRecorrido(string tipoRecorrido, List<int> resultados)
{
    string mensaje = $"{tipoRecorrido}:\n";
    mensaje += string.Join(", ", resultados.Select(r => r.ToString()));
    MessageBox.Show(mensaje, $"Resultado del {tipoRecorrido}");
}
    private void LimpiarColores()
    {
        // Restablece el color de todos los nodos a LightBlue
        arbol.RecorridoInorden(nodo =>
        {
            nodo.color = Color.LightBlue;
        });
        this.Invalidate();
        Application.DoEvents();
        System.Threading.Thread.Sleep(500);
    }
    private void VisitarNodo(Nodo nodo)
    {
        // Verifica si el nodo es nulo antes de intentar acceder a sus propiedades
        if (nodo != null)
        {
            // Implementa la acción que deseas realizar en cada nodo durante el recorrido
            nodo.color = Color.Yellow;
        }
    }
        

        private void DibujarNodo(Graphics g, Nodo nodo, int x, int y)
        {
            // Dibuja el nodo con el color asignado
            Brush brush = new SolidBrush(nodo.color);
            g.FillEllipse(brush, x, y, 30, 30);
            g.DrawEllipse(Pens.LightBlue, x, y, 30, 30);
            g.DrawString(nodo.valor.ToString(), this.Font, Brushes.Black, x + 10, y + 10);
        }

        private void DibujarArbol(Graphics g, Nodo nodo, int x, int y, int dx)
        {
            if (nodo == null) return;

            DibujarNodo(g, nodo, x, y);
            if (nodo.izquierdo != null)
            {
                g.DrawLine(Pens.Green, x + 15, y + 30, x - dx + 15, y + 60);
                DibujarArbol(g, nodo.izquierdo, x - dx, y + 60, dx / 2);
            }
            if (nodo.derecho != null)
            {
                g.DrawLine(Pens.Green, x + 15, y + 30, x + dx + 15, y + 60);
                DibujarArbol(g, nodo.derecho, x + dx, y + 60, dx / 2);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            DibujarArbol(e.Graphics, arbol.raiz, this.ClientSize.Width / 2, 20, this.ClientSize.Width / 4);
        }
    }
