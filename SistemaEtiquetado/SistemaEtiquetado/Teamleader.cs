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
    public partial class Teamleader : Form
    {
        public Teamleader(string nombre)
        {
            InitializeComponent();
            lblmensajeTeamleader.Text = nombre;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form formulario = new Registro();
            formulario.Show();
        }

        private void Teamleader_Load(object sender, EventArgs e)
        {
            //Conexion a la base de datos a traves de la clase conexion.cs
            Conexion.Conectar();
            //Llena los datos del datagridview con la base de datos
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
        //Evento Boton Salir
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
