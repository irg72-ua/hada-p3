using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library
{
    public class ENCategory
    {
        private string _name;
        private int _id;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public ENCategory()
        {
            _id = 0;
            _name = string.Empty;
        }

        public ENCategory(string name, int id)
        {
            _name = name;
            _id = id;
        }

        public bool Read()
        {
            try
            {
                CADCategory cat = new CADCategory();
                return cat.Read(this);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error en la operación de producto: {0}", e.Message);
                return false;
            }
        }

        public List<ENCategory> ReadAll()
        {
            CADCategory cat = new CADCategory();
            return cat.ReadAll();
        }
    }
}
