using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using TransrodenProyecto.Models; // importar los modelos
using System.Data.SqlClient; //usos de bd
using System.Data;

namespace TransrodenProyecto.Logica
{
    public class LO_Usuario
    {
        public Usuario EncontrarUsuario(string correo, string clave)
        {
            Usuario objeto = new Usuario();

            using (SqlConnection con = new SqlConnection("Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=Transroden;Integrated Security=True;MultipleActiveResultSets=True")) //Acá va la conexión a la bd
            {
                string query = "select Nombre, Apellidos, Cedula, Correo, Clave, Telefono, Rol from USUARIOS where Correo = @pcorreo and Clave = @pclave"; //Para fines prácticos, pero esto debería ser con SPs 

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@pcorreo", correo);
                cmd.Parameters.AddWithValue("@pclave", clave);
                cmd.CommandType = CommandType.Text;

                con.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read()) {

                        objeto = new Usuario()
                        {

                            Nombre = dr["Nombre"].ToString(),
                            Apellidos = dr["Apellidos"].ToString(),
                            Cedula = dr["Cedula"].ToString(),
                            Correo = dr["Correo"].ToString(),
                            Clave = dr["Clave"].ToString(),
                            Telefono = dr["Telefono"].ToString(),
                            Rol = (int)(Rol)dr["Rol"], //Revisar si esto sirve

                        };
                    
                    }
                }

            }

            return objeto;
        }
    }
}