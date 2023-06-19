using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SistemaEtiquetado
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //Cadena de conexión 

        SqlConnection con = new SqlConnection("server=DESKTOP-P97UQ56 ; database=etiquetas ; integrated security = true");


        public void logear(string usuario, string contrasena){
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT Nombre, Tipo_usuario FROM usuarios WHERE Usuario = @usuario AND Password = @password", con);
                cmd.Parameters.AddWithValue("usuario", usuario);
                cmd.Parameters.AddWithValue("password", contrasena);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                if (dt.Rows.Count == 1)
                {
                    this.Hide();
                    if (dt.Rows[0][1].ToString() == "Admin")
                    {
                        new Admin(dt.Rows[0][0].ToString()).Show();
                    }
                
                    else if (dt.Rows[0][1].ToString() == "Teamleader")
                    {
                        new Teamleader(dt.Rows[0][0].ToString()).Show();
                    }

                    else if (dt.Rows[0][1].ToString() == "Operador")
                    {
                        new Usuario(dt.Rows[0][0].ToString()).Show();
                    }

                }

                else
                {
                    MessageBox.Show("Usuario y/o Contraseña Incorrecta");
                }
            }
                catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            logear(this.txtUsuario.Text, this.txtPassword.Text);
        }

    }
}
