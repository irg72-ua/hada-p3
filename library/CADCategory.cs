using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library
{
    public class CADCategory
    {
        private string _constring;

        public CADCategory()
        {
            _constring = ConfigurationManager.ConnectionStrings["miconexion"].ToString();
        }

        public bool Read(ENCategory en)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_constring))
                {
                    conn.Open();
                    string busqueda = "SELECT id, name FROM Categories WHERE name = @name;";
                    SqlCommand cmd = new SqlCommand(busqueda, conn);
                    cmd.Parameters.AddWithValue("@name", en.Name);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        en.Id = Convert.ToInt32(reader["id"]);
                        en.Name = reader["name"].ToString();
                        return true;
                    }
                    return false;
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error al leer la categoria: {0}", ex.Message);
                return false;
            }
        }

        public List<ENCategory> ReadAll()
        {
            List<ENCategory> categories = new List<ENCategory>();

            try
            {
                using (SqlConnection conn = new SqlConnection(_constring))
                {
                    conn.Open();
                    string busqueda = "SELECT * FROM Categories";
                    SqlCommand cmd = new SqlCommand(busqueda, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        ENCategory en = new ENCategory(
                            reader["name"].ToString(),
                            Convert.ToInt32(reader["id"])
                            );
                        categories.Add(en);
                    }
                    return categories;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error al recuperar categorías: {0}", e.Message);
                return categories;
            }
        }
    }
}
