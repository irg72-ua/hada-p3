using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using library;


namespace proWeb
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                ENCategory enc = new ENCategory();
                List<ENCategory> categories = enc.ReadAll();
                Dictionary<int, string> data = new Dictionary<int, string>();
                foreach (var cat in categories)
                {
                    data.Add(cat.Id, cat.Name);
                }

                ddlCategory.DataSource = data;
                ddlCategory.DataTextField = "Value"; 
                ddlCategory.DataValueField = "Key";
                ddlCategory.DataBind();
                ddlCategory.SelectedIndex = 0;

            }
        }

        private void LimpiarCampos()
        {
            txtCode.Text = "";
            txtName.Text = "";
            txtAmount.Text = "";
            txtPrice.Text = "";
            txtCreationDate.Text = "";
            ddlCategory.SelectedIndex = 0;
        }

        private bool CheckCreateUpdate(ref int amount, ref float price, ref int valuecat, ref DateTime Correctformat)
        {
            string formatofecha = "dd/MM/yyyy HH:mm:ss";

            try
            {
                // Amount
                if (!int.TryParse(txtAmount.Text, out amount))
                {
                    throw new FormatException("El campo Amount debe ser un número entero.");
                }

                // Price
                if (!float.TryParse(txtPrice.Text, out price))
                {
                    throw new FormatException("El campo Price debe ser un número decimal.");
                }
                else
                {
                    price = (float)Math.Round(price, 2);
                }

                // Categoría
                if (!int.TryParse(ddlCategory.SelectedValue, out valuecat))
                {
                    throw new FormatException("La categoría debe ser un número entero.");
                }

                // Fecha
                if (!DateTime.TryParseExact(txtCreationDate.Text, formatofecha,
                    System.Globalization.CultureInfo.InvariantCulture,
                    System.Globalization.DateTimeStyles.None, out Correctformat))
                {
                    throw new ArgumentException("Error en el formato de la fecha (Debe ser dd/MM/yyyy HH:mm:ss).");
                }

                // Validaciones de límites
                if (txtCode.Text.Length < 1 || txtCode.Text.Length > 16)
                {
                    throw new FormatException("El código debe tener entre 1 y 16 caracteres.");
                }

                if (txtName.Text.Length > 32)
                {
                    throw new FormatException("El nombre no puede tener más de 32 caracteres.");
                }

                if (amount < 0 || amount > 9999)
                {
                    throw new FormatException("El amount debe estar entre 0 y 9999.");
                }

                if (price < 0 || price > 9999.99)
                {
                    throw new FormatException("El price debe estar entre 0 y 9999.99.");
                }

                if (valuecat < 1 || valuecat > 4)
                {
                    throw new FormatException("Categoría fuera de rango (1 a 4).");
                }

                return true;
            }
            catch (ArgumentException ex)
            {
                EtiquetaFallo.Visible = true;
                EtiquetaFallo.Text = ex.Message;
                return false;
            }
            catch (FormatException ex)
            {
                EtiquetaFallo.Visible = true;
                EtiquetaFallo.Text = ex.Message;
                return false;
            }
            catch (Exception ex)
            {
                EtiquetaFallo.Visible = true;
                EtiquetaFallo.Text = "Operación de producto ha fallado " + ex.Message;
                return false;
            }
        }

        private bool CheckCode()
        {
            try
            {
                if (txtCode.Text.Length < 1 || txtCode.Text.Length > 16)
                {
                    throw new ArgumentOutOfRangeException("El código debe tener entre 1 y 16 caracteres.");
                }

                return true;
            }
            catch (ArgumentOutOfRangeException ex)
            {
                EtiquetaFallo.Visible = true;
                EtiquetaFallo.Text = ex.Message;
                return false;
            }
            catch (Exception ex)
            {
                EtiquetaFallo.Visible = true;
                EtiquetaFallo.Text = "Error inesperado: " + ex.Message;
                return false;
            }
        }

        protected void btnCreate_Click(object sender, EventArgs create)
        {
            EtiquetaExito.Visible = false;
            EtiquetaFallo.Visible = false;
            int amount = 0, valuecat = 0;
            float price = 0;
            DateTime Correctformat = DateTime.MinValue;
            if (CheckCreateUpdate(ref amount, ref price, ref valuecat, ref Correctformat))
            {
                ENProduct producto = new ENProduct(txtCode.Text, txtName.Text, amount, price, valuecat, Correctformat);
                if (producto.Create())
                {
                    EtiquetaExito.Visible = true;
                }
                else
                {
                    EtiquetaFallo.Visible = true;
                    EtiquetaFallo.Text = "No se ha podido crear el producto, ID repetida";
                    LimpiarCampos();
                }
            }


        }
        protected void btnUpdate_Click(object sender, EventArgs update)
        {
            EtiquetaExito.Visible = false;
            EtiquetaFallo.Visible = false;
            int amount = 0, valuecat = 0;
            float price = 0;
            DateTime Correctformat = DateTime.MinValue;
            if (CheckCreateUpdate(ref amount, ref price, ref valuecat, ref Correctformat))
            {
                ENProduct producto = new ENProduct(txtCode.Text, txtName.Text, amount, price, valuecat, Correctformat);

                if (producto.Update())
                {
                    EtiquetaExito.Visible = true;
                }
                else
                {
                    EtiquetaFallo.Visible = true;
                    EtiquetaFallo.Text = "No se ha encontrado la ID de este producto";
                    LimpiarCampos();
                }

            }
        }
        protected void btnDelete_Click(object sender, EventArgs delete)
        {
            EtiquetaExito.Visible = false;
            EtiquetaFallo.Visible = false;
            txtName.Text = "";
            int amount = 0, valuecat = 0;
            float price = 0;
            DateTime Correctformat = DateTime.MinValue;
            if (CheckCode())
            {
                ENProduct producto = new ENProduct(txtCode.Text, txtName.Text, amount, price, valuecat, Correctformat);
                if (producto.Delete())
                {
                    EtiquetaExito.Visible = true;
                }
                else
                {
                    EtiquetaFallo.Visible = true;
                    EtiquetaFallo.Text = "Error Eliminando Producto";
                    LimpiarCampos();
                }
            }
        }
        
        protected void btnReadFirst_Click(object sender, EventArgs RF)
        {
            EtiquetaExito.Visible = false;
            EtiquetaFallo.Visible = false;
            ENProduct producto = new ENProduct();
            if (producto.ReadFirst())
            {
                EtiquetaExito.Visible = true;
                txtCode.Text = producto.Code;
                txtName.Text = producto.Name;
                txtAmount.Text = producto.Amount.ToString();
                txtPrice.Text = producto.Price.ToString();
                ddlCategory.SelectedIndex = producto.Category - 1; 
                txtCreationDate.Text = producto.CreationDate.ToString();
            }
            else
            {
                EtiquetaFallo.Visible = true;
                EtiquetaFallo.Text = "Error en la búsqueda del primer producto";
                LimpiarCampos();
            }

        }
        protected void btnRead_Click(object sender, EventArgs RF)
        {
            EtiquetaExito.Visible = false;
            EtiquetaFallo.Visible = false;
            txtName.Text = "";
            int amount = 0, valuecat = 0;
            float price = 0;
            DateTime Correctformat = DateTime.MinValue;
            if (CheckCode())
            {
                ENProduct producto = new ENProduct(txtCode.Text, txtName.Text, amount, price, valuecat, Correctformat);
                if (producto.Read())
                {
                    EtiquetaExito.Visible = true;
                    txtCode.Text = producto.Code;
                    txtName.Text = producto.Name;
                    txtAmount.Text = producto.Amount.ToString();
                    txtPrice.Text = producto.Price.ToString();
                    ddlCategory.SelectedIndex = producto.Category - 1; 
                    txtCreationDate.Text = producto.CreationDate.ToString();
                }
                else
                {
                    EtiquetaFallo.Visible = true;
                    EtiquetaFallo.Text = "Error en la búsqueda del producto";
                    LimpiarCampos();
                }
            }
        }
        protected void btnReadPrev_Click(object sender, EventArgs RP)
        {
            EtiquetaExito.Visible = false;
            EtiquetaFallo.Visible = false;
            txtName.Text = "";
            int amount = 0, valuecat = 0;
            float price = 0;
            DateTime Correctformat = DateTime.MinValue;
            if (CheckCode())
            {
                ENProduct producto = new ENProduct(txtCode.Text, txtName.Text, amount, price, valuecat, Correctformat);
                if (producto.ReadPrev())
                {
                    EtiquetaExito.Visible = true;
                    txtCode.Text = producto.Code;
                    txtName.Text = producto.Name;
                    txtAmount.Text = producto.Amount.ToString();
                    txtPrice.Text = producto.Price.ToString();
                    ddlCategory.SelectedIndex = producto.Category - 1; 
                    txtCreationDate.Text = producto.CreationDate.ToString();
                }
                else
                {
                    EtiquetaFallo.Visible = true;
                    EtiquetaFallo.Text = "Error en la búsqueda del anterior producto";
                    LimpiarCampos();
                }
            }

        }
        protected void btnReadNext_Click(object sender, EventArgs RN)
        {
            EtiquetaExito.Visible = false;
            EtiquetaFallo.Visible = false;
            txtName.Text = "";
            int amount = 0, valuecat = 0;
            float price = 0;
            DateTime Correctformat = DateTime.MinValue;
            if (CheckCode())
            {
                ENProduct producto = new ENProduct(txtCode.Text, txtName.Text, amount, price, valuecat, Correctformat);
                if (producto.ReadNext())
                {
                    EtiquetaExito.Visible = true;
                    txtCode.Text = producto.Code;
                    txtName.Text = producto.Name;
                    txtAmount.Text = producto.Amount.ToString();
                    txtPrice.Text = producto.Price.ToString();
                    ddlCategory.SelectedIndex = producto.Category - 1;
                    txtCreationDate.Text = producto.CreationDate.ToString();
                }
                else
                {
                    EtiquetaFallo.Visible = true;
                    EtiquetaFallo.Text = "Error en la búsqueda del siguiente producto";
                    LimpiarCampos();
                }
            }

        }

    }
}