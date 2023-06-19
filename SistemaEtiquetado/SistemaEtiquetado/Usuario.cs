using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace SistemaEtiquetado
{
    public partial class Usuario : Form
    {
        public Usuario(string nombre)
        {
            InitializeComponent();
            lblmensaje.Text = nombre;
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            //La seccion de abajo es para poner por default un valor en el combobox
            //comboBox1.SelectedIndex = 1;
        }

        private void Usuario_Load(object sender, EventArgs e)
        {
            //Desactiva el boton hasta ingresar los caracteres
            button2.Enabled = false;

            //Conexion a BD

            Conexion.Conectar();

            dataGridView1.DataSource = llenar_grid();
        }

        public DataTable llenar_grid()
        {
            Conexion.Conectar();
            DataTable dt = new DataTable();
            string consulta = "SELECT * FROM piezas_elaboradas";
            SqlCommand cmd = new SqlCommand(consulta, Conexion.Conectar());

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(dt);
            return dt;

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                //Saca la fila 3 del datagridview (y este a su vez de la BD) para mostrarla en el combobox
                comboBox1.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                //Saca la fila 4 del datagridview (de la bd) para mostrarla en el textbox
                txtnum_empleado.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            }

            catch
            {

            }
        }

        private void txtnum_empleado_TextChanged_1(object sender, EventArgs e)
        {
            if (txtnum_empleado.Text.Length < 6)
            {
                button2.Enabled = false; // Deshabilitar el botón si el cuadro de texto tiene menos de 6 caracteres
            }
            else
            {
                button2.Enabled = true; // Habilitar el botón si el cuadro de texto tiene al menos 6 caracteres
            }
        }

        //Evento imprimir etiqueta
        private void button2_Click_1(object sender, EventArgs e)
        {
            Conexion.Conectar();
            string insertar = "INSERT INTO piezas_elaboradas (fecha_elaboracion, numparte, num_empleado)VALUES(@fecha_elaboracion,@numparte,@num_empleado)";
            SqlCommand cmd1 = new SqlCommand(insertar, Conexion.Conectar());

            DateTime fechaActual = DateTime.Now;

            cmd1.Parameters.AddWithValue("@fecha_elaboracion", fechaActual);
            cmd1.Parameters.AddWithValue("@numparte", comboBox1.Text);
            cmd1.Parameters.AddWithValue("@num_empleado", txtnum_empleado.Text);

            cmd1.ExecuteNonQuery();

            MessageBox.Show("Los Datos Fueron Agregados Correctamente!!!");

            dataGridView1.DataSource = llenar_grid();

            //INICIA TEST
            string seleccion = comboBox1.SelectedItem.ToString();

            // Imprime la etiqueta correspondiente según la selección
            if (seleccion == "U55X - REAR DOOR FIXED GLASS TEM LH PRIV")
            {
                Form formulario = new imp01();
                formulario.Show();
                MessageBox.Show("Se imprimio correctamente: U55X - REAR DOOR FIXED GLASS TEM LH PRIV");

            }
            else if (seleccion == "U55X - REAR DOOR FIXED GLASS TEM RH PRIV")
            {
                Form formulario = new imp02();
                formulario.Show();
                MessageBox.Show("Se imprimio correctamente: U55X - REAR DOOR FIXED GLASS TEM RH PRIV");
            }
            else if (seleccion == "REAR DOOR FIXED GLASS TEM LH SOLAR")
            {
                Form formulario = new imp03();
                formulario.Show();
                MessageBox.Show("Se imprimio correctamente: REAR DOOR FIXED GLASS TEM LH SOLAR");
            }
            else if (seleccion == "REAR DOOR FIXED GLASS TEM RH SOLAR")
            {
                Form formulario = new imp04();
                formulario.Show();
                MessageBox.Show("Se imprimio correctamente: REAR DOOR FIXED GLASS TEM RH SOLAR");
            }
            else
            {
                MessageBox.Show("ERROR");
            }
        }

        //Funcion Salir
        private void button4_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}