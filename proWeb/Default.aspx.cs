using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace proWeb
{
    public partial class Default : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["Miconexion"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnCreate_Click(object sender, EventArgs e)
        {
            string code = txtCode.Text.Trim();
            string name = txtName.Text.Trim();
            string amountStr = txtAmount.Text.Trim();
            string priceStr = txtPrice.Text.Trim();
            string categoryStr = ddlCategory.SelectedIndex.ToString(); // Usamos el índice como valor numérico
            string creationDateStr = txtCreationDate.Text.Trim();

            try
            {
                // Validaciones
                if (code.Length < 1 || code.Length > 16)
                    throw new Exception("Code must be between 1 and 16 characters.");

                if (name.Length > 32)
                    throw new Exception("Name cannot exceed 32 characters.");

                if (!int.TryParse(amountStr, out int amount) || amount < 0 || amount > 9999)
                    throw new Exception("Amount must be an integer between 0 and 9999.");

                if (!decimal.TryParse(priceStr, out decimal price) || price < 0 || price > 9999.99m)
                    throw new Exception("Price must be a number between 0 and 9999.99.");

                if (!DateTime.TryParseExact(creationDateStr, "dd/MM/yyyy HH:mm:ss", null, System.Globalization.DateTimeStyles.None, out DateTime creationDate))
                    throw new Exception("Creation date must be in format dd/mm/yyyy hh:mm:ss");

                // Comprobar si ya existe el producto
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM Products WHERE Code = @Code", conn);
                    checkCmd.Parameters.AddWithValue("@Code", code);

                    int count = (int)checkCmd.ExecuteScalar();
                    if (count > 0)
                        throw new Exception("A product with this code already exists.");

                    // Insertar nuevo producto
                    SqlCommand insertCmd = new SqlCommand(
                        "INSERT INTO Products (Code, Name, Amount, Category, Price, CreationDate) VALUES (@Code, @Name, @Amount, @Category, @Price, @CreationDate)", conn);
                    insertCmd.Parameters.AddWithValue("@Code", code);
                    insertCmd.Parameters.AddWithValue("@Name", name);
                    insertCmd.Parameters.AddWithValue("@Amount", amount);
                    insertCmd.Parameters.AddWithValue("@Category", categoryStr);
                    insertCmd.Parameters.AddWithValue("@Price", price);
                    insertCmd.Parameters.AddWithValue("@CreationDate", creationDate);

                    insertCmd.ExecuteNonQuery();
                }

                lblMessage.ForeColor = System.Drawing.Color.Green;
                lblMessage.Text = "Product created successfully.";
            }
            catch (Exception ex)
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = $"Error: {ex.Message}";
                Console.WriteLine("Product operation has failed. Error: {0}", ex.Message);
            }
        }
    }
}