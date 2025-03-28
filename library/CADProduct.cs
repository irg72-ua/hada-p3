using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Configuration;


namespace library
{
    class CADProduct
    {
        private string constring;
        public CADProduct() {
            constring = ConfigurationManager.ConnectionStrings["miconexion"].ToString();
        }
        public bool Create(ENProduct en) {
            try
            {
                using (SqlConnection con = new SqlConnection(constring))
                {
                    con.Open();
                    string sql = "INSERT INTO Product (code, name, amount, price, category, creationDate) VALUES (@code, @name, @amount, @price, @category, @creationDate)";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@code", en.Code);
                        cmd.Parameters.AddWithValue("@name", en.Name);
                        cmd.Parameters.AddWithValue("@amount", en.Amount);
                        cmd.Parameters.AddWithValue("@price", en.Price);
                        cmd.Parameters.AddWithValue("@category", en.Category);
                        cmd.Parameters.AddWithValue("@creationDate", en.CreationDate);
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (SqlException ex) { 
                Console.WriteLine("Error creando producto: " + ex.Message);
                return false;
            }
        }   
        public bool Update(ENProduct en) {
            try
            {
                using (SqlConnection con = new SqlConnection(constring)) { 
                    con.Open();
                    String sql = "UPDATE Product SET name = @name, amount = @amount, price = @price, category = @category, creationDate = @creationDate WHERE code = @code";

                    using (SqlCommand cmd = new SqlCommand(sql, con)) {
                    
                        cmd.Parameters.AddWithValue("@name", en.Name);
                        cmd.Parameters.AddWithValue("@amount", en.Amount);
                        cmd.Parameters.AddWithValue("@price", en.Price);
                        cmd.Parameters.AddWithValue("@category", en.Category);
                        cmd.Parameters.AddWithValue("@creationDate", en.CreationDate);
                    }
                }
            }
            catch () { }   
        }
        public bool Delete(ENProduct en) { }
        public bool Read(ENProduct en) { }
        public bool ReadFirst(ENProduct en) { }
        public bool ReadNext(ENProduct en) { }
        public bool ReadPrev(ENProduct en) { }
    }
}
