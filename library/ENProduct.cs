using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library
{
    class ENProduct
    {
        private string code;
        private string name;
        private int amount;
        private float price;
        private DateTime creationDate;
        public char category;

        public ENProduct() { }
        public ENProduct(string code, string name, int amount, float price, int category, DateTime creationDate) { } 
        public bool Create() { }
        public bool Update() { }
        public bool Delete() { }
        public bool Read() { }
        public bool ReadFirst() { }
        public bool ReadNext() { }
        public bool ReadPrev() { }

    }
}
