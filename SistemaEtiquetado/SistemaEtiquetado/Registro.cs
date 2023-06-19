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
    public partial class Registro : Form
    {
        public Registro()
        {
            InitializeComponent();

            //Soluciona la escritura en el menu desplegable solo mostrando lo requerido
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            //La seccion de abajo es para poner por default un valor en el combobox
            //comboBox1.SelectedIndex = 1;

        }
        //La parte del registro solo funciona para administrador y team leader
        private void Registro_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'etiquetasDataSet.usuarios' Puede moverla o quitarla según sea necesario.
            this.usuariosTableAdapter.Fill(this.etiquetasDataSet.usuarios);

            //Mensaje para verificar la conexion entre el sistema y la base de datos incluyendo mensaje de bienvenida

            Conexion.Conectar();
            //MessageBox.Show("ACCESO AUTORIZADO, BIENVENIDO!!!");
            
            dataGridView1.DataSource = llenar_grid();

            //La conexion en esta parte viene de la clase Conexion.CS alli viene la cadena de conexión
        }

        //Aqui se vuelve a conectar y solicita a la base de datos mostrar los datos de la tabla usuarios para mostrarlo
        //en un datagrid view

        public DataTable llenar_grid()

        {
            Conexion.Conectar();
            DataTable dt = new DataTable();
            string consulta = "SELECT * FROM usuarios";
            SqlCommand cmd = new SqlCommand(consulta, Conexion.Conectar());

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(dt);
            return dt;

        }

        //Evento para registrar nuevo usuario a la base de datos
        private void button1_Click(object sender, EventArgs e)
        {
            Conexion.Conectar();
            string insertar = "INSERT INTO usuarios (Nombre, Usuario, Password, Tipo_usuario, num_empleado)VALUES(@Nombre, @Usuario, @Password, @Tipo_usuario, @num_empleado)";
            SqlCommand cmd1 = new SqlCommand(insertar, Conexion.Conectar());
            cmd1.Parameters.AddWithValue("@Nombre", txtNombre.Text);
            cmd1.Parameters.AddWithValue("@Usuario", txtUsuario.Text);
            cmd1.Parameters.AddWithValue("@Password", txtPassword.Text);
            cmd1.Parameters.AddWithValue("@Tipo_Usuario", comboBox1.Text);
            cmd1.Parameters.AddWithValue("@num_empleado", txtnum_empleado.Text);

            cmd1.ExecuteNonQuery();

            MessageBox.Show("Los Datos Se Ingresaron Correctamente!!!");

            dataGridView1.DataSource = llenar_grid();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
        //Evento para al momento de hacer click sobre el se puedan cambiar los campos para las modificaciones del usuario
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //Al momento de seleccionar un usuario del data gridview
                //Nos va a arrojar en los textbox los datos correspondientes 
                //al usuario seleccionado
                txtNombre.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                txtUsuario.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                txtPassword.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                comboBox1.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                txtnum_empleado.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            }

            catch
            {

            }
        }

        //Boton para modificar el usuario
        private void button5_Click(object sender, EventArgs e)
        {
            Conexion.Conectar();
            string actualizar = "UPDATE usuarios SET Nombre=@NuevoNombre, Usuario=@Usuario, Password=@Password, Tipo_usuario=@Tipo_usuario, num_empleado=@num_empleado WHERE Nombre=@NombreSeleccionado";
            SqlCommand cmd2 = new SqlCommand(actualizar, Conexion.Conectar());

            cmd2.Parameters.AddWithValue("@NuevoNombre", txtNombre.Text);
            cmd2.Parameters.AddWithValue("@Usuario", txtUsuario.Text);
            cmd2.Parameters.AddWithValue("@Password", txtPassword.Text);
            cmd2.Parameters.AddWithValue("@Tipo_usuario", comboBox1.Text);
            cmd2.Parameters.AddWithValue("@NombreSeleccionado", txtNombre.Text);
            cmd2.Parameters.AddWithValue("@num_empleado", txtnum_empleado.Text);

            cmd2.ExecuteNonQuery();

            MessageBox.Show("Los Datos Se Actualizaron Correctamente!!!");
            dataGridView1.DataSource = llenar_grid();
        }

        //Este codigo es para la funcion del boton ELIMINAR usuario
        private void button4_Click(object sender, EventArgs e)
        {
            Conexion.Conectar();
            string eliminar = "DELETE FROM usuarios WHERE Nombre = @Nombre";
            SqlCommand cmd3 = new SqlCommand(eliminar, Conexion.Conectar());
            cmd3.Parameters.AddWithValue("@Nombre", txtNombre.Text);

            cmd3.ExecuteNonQuery();

            MessageBox.Show("Se Elimino Correctamente!!!");

            dataGridView1.DataSource = llenar_grid();
        }

        //Este codigo sirve para vaciar los campos e ingresar otro usuario
        private void button3_Click(object sender, EventArgs e)
        {
            txtNombre.Clear();
            txtUsuario.Clear();
            txtPassword.Clear();
            //Esta seccion sirve para que al vaciar los campos 
            //y el combobox del rol se limpie también y no se haya quedado
            //la selecciona anterior
            comboBox1.SelectedIndex = -1;
            txtnum_empleado.Clear();
        }

        //Este boton cierra el form es un exit solamente y nos devuelve al form anterior
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
