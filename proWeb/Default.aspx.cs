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
            if (!IsPostBack) //Para que se ejecute la primera vez que inicia la página
            {

                ENCategory enc = new ENCategory();
                List<ENCategory> categories = enc.ReadAll();
                Dictionary<int, string> data = new Dictionary<int, string>();
                foreach (var cat in categories)
                {
                    data.Add(cat.Id, cat.Name);
                }

                ddlCategory.DataSource = data;
                ddlCategory.DataTextField = "Value"; //asignar id de la categoria como key y el nombre de la categoria como value
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
                if (!int.TryParse(txtAmount.Text, out amount))
                {
                    throw new Exception();
                }
                if (!float.TryParse(txtPrice.Text, out price))
                {
                    throw new Exception();
                }
                else
                {
                    price = (float)Math.Round(price, 2);
                }
                if (!int.TryParse(ddlCategory.SelectedValue, out valuecat))
                {
                    throw new Exception();
                }
                if (!DateTime.TryParseExact(txtCreationDate.Text, formatofecha, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out Correctformat)) //Compara el formato de la fecha con el formato del segundo string y si es igual devuelve true y esa fecha en Correctformat
                {
                    throw new ArgumentException();
                }
                if ((txtCode.Text.Length > 16 || txtCode.Text.Length < 1) || (txtName.Text.Length > 32) || (amount < 0 || amount > 9999) || (price < 0 || price > 9999.99) || (valuecat > 4 || valuecat < 1))
                {
                    EtiquetaFallo.Visible = true;
                    throw new Exception();
                }
                else
                {

                    return true;
                }
            }
            catch (Exception ex)
            {
                EtiquetaFallo.Visible = true;
                Console.WriteLine("Product operation has failed.Error: {0}", ex.Message);
                return false;
            }

        }//Metodo creado para comprobar formato introducido, pasa los parametros por referencia
        private bool CheckCode()
        {
            try
            {
                if ((txtCode.Text.Length > 16 || txtCode.Text.Length < 1))
                {
                    EtiquetaFallo.Visible = true;
                    throw new Exception();
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                EtiquetaFallo.Visible = true;
                Console.WriteLine("Product operation has failed.Error: {0}", ex.Message);
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
                    LimpiarCampos();
                }

            }
        }
        //A partir de este método (Delete) solamente comprobaremos el formato del código ya que no necesitamos nada más para borrar/leer de las diferentes formas posibles en una consulta SQL, ponemos el resto de valores a valor mínimo por si el formato es incorrecto
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
                    LimpiarCampos();
                }
            }
        }
        //ReadFirst realimente no necesita nada para su consulta SQL por lo que se crea el ENProduct vacío para llamar este método
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
                ddlCategory.SelectedIndex = producto.Category - 1; // Ya que los IDs de las categorías comienzan desde 1 y el índice del DropDownList comienza en 0, el indice de la DropDrownList siempre será 1 más que el índice de la categoría seleccionada.
                txtCreationDate.Text = producto.CreationDate.ToString();
            }
            else
            {
                EtiquetaFallo.Visible = true;
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
                    ddlCategory.SelectedIndex = producto.Category - 1; // Ya que los IDs de las categorías comienzan desde 1 y el índice del DropDownList comienza en 0, el indice de la DropDrownList siempre será 1 más que el índice de la categoría seleccionada.
                    txtCreationDate.Text = producto.CreationDate.ToString();
                }
                else
                {
                    EtiquetaFallo.Visible = true;
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
                    ddlCategory.SelectedIndex = producto.Category - 1; // Ya que los IDs de las categorías comienzan desde 1 y el índice del DropDownList comienza en 0, el indice de la DropDrownList siempre será 1 más que el índice de la categoría seleccionada.
                    txtCreationDate.Text = producto.CreationDate.ToString();
                }
                else
                {
                    EtiquetaFallo.Visible = true;
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
                    ddlCategory.SelectedIndex = producto.Category - 1; // Ya que los IDs de las categorías comienzan desde 1 y el índice del DropDownList comienza en 0, el indice de la DropDrownList siempre será 1 más que el índice de la categoría seleccionada.
                    txtCreationDate.Text = producto.CreationDate.ToString();
                }
                else
                {
                    EtiquetaFallo.Visible = true;
                    LimpiarCampos();
                }
            }

        }

    }
}