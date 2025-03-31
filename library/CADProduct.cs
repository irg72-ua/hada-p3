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
using System.Linq.Expressions;


namespace library
{
    class CADProduct
    {
        private string constring;
        public CADProduct()
        {
            constring = ConfigurationManager.ConnectionStrings["miconexion"].ToString();
        }
        public bool Create(ENProduct en)
        {
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
            catch (SqlException ex)
            {
                Console.WriteLine("Error creando producto: " + ex.Message);
                return false;
            }
        }
        public bool Update(ENProduct en)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(constring))
                {
                    con.Open();
                    String sql = "UPDATE Product SET name = @name, amount = @amount, price = @price, category = @category, creationDate = @creationDate WHERE code = @code";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {

                        cmd.Parameters.AddWithValue("@name", en.Name);
                        cmd.Parameters.AddWithValue("@amount", en.Amount);
                        cmd.Parameters.AddWithValue("@price", en.Price);
                        cmd.Parameters.AddWithValue("@category", en.Category);
                        cmd.Parameters.AddWithValue("@creationDate", en.CreationDate);
                        return true;
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error actualizando producto: " + ex.Message);
                return false;
            }
        }
        public bool Delete(ENProduct en)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(constring))
                {
                    con.Open();
                    String sql = "DELETE FROM Product WHERE code = @code";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@code", en.Code);

                        return true;
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error eliminando producto: " + ex.Message);
                return false;
            }
        }
        public bool Read(ENProduct en)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(constring))
                {
                    con.Open();
                    string sql = "SELECT * FROM Product WHERE code = @code";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@code", en.Code);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                en.Name = dr["name"].ToString();
                                en.Amount = Convert.ToInt32(dr["amount"]);
                                en.Price = Convert.ToSingle(dr["price"]);
                                en.Category = Convert.ToChar(dr["category"]);
                                en.CreationDate = Convert.ToDateTime(dr["creationDate"]);
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error leyendo producto: " + ex.Message);
                return false;
            }
        }
        public bool ReadFirst(ENProduct en)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(constring))
                {
                    con.Open();
                    string sql = "SELECT TOP 1 * FROM Product ORDER BY code ASC";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                en.Code = dr["code"].ToString();
                                en.Name = dr["name"].ToString();
                                en.Amount = Convert.ToInt32(dr["amount"]);
                                en.Price = Convert.ToSingle(dr["price"]);
                                en.Category = Convert.ToChar(dr["category"]);
                                en.CreationDate = Convert.ToDateTime(dr["creationDate"]);
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                }

            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error leyendo producto: " + ex.Message);
                return false;
            }
        }
        public bool ReadNext(ENProduct en)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(constring))
                {
                    string sql = "SELECT TOP 1 * FROM Product WHERE code > @code ORDER BY code ASC";
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@code", en.Code);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                en.Code = dr["code"].ToString();
                                en.Name = dr["name"].ToString();
                                en.Amount = Convert.ToInt32(dr["amount"]);
                                en.Price = Convert.ToSingle(dr["price"]);
                                en.Category = Convert.ToChar(dr["category"]);
                                en.CreationDate = Convert.ToDateTime(dr["creationDate"]);
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }

                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error leyendo producto: " + ex.Message);
                return false;
            }
        }
        public bool ReadPrev(ENProduct en)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(constring))
                {
                    string sql = "SELECT TOP 1 * FROM Product WHERE code < @code ORDER BY code DESC";
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@code", en.Code);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                en.Code = dr["code"].ToString();
                                en.Name = dr["name"].ToString();
                                en.Amount = Convert.ToInt32(dr["amount"]);
                                en.Price = Convert.ToSingle(dr["price"]);
                                en.Category = Convert.ToChar(dr["category"]);
                                en.CreationDate = Convert.ToDateTime(dr["creationDate"]);
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error leyendo producto: " + ex.Message);
                return false;
            }
        }
    }
}
