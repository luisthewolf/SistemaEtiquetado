using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace SistemaEtiquetado
{
    class Conexion
    {
        public static SqlConnection Conectar()
        {
            //Cadena de conexión a la base de datos en SQL Server
            SqlConnection cn = new SqlConnection("server=DESKTOP-P97UQ56 ; database=etiquetas ; integrated security = true");
            cn.Open();
            return cn;
        }
    }
}
