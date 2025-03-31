﻿using System;
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
        private int category;
        public string Code
        {
            get { return code; }
            set { code = value; }
        }
        
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public int Amount
        {
            get { return amount; }
            set { amount = value; }
        }

        public float Price
        {
            get { return price; }
            set { price = value; }
        }

        public DateTime CreationDate 
        {
            get { return creationDate; }
            set { creationDate = value; }
        }

        public int Category
        {
            get { return category; }
            set { category = value; }
        }

        public ENProduct() {}
        public ENProduct(string code, string name, int amount, float price, int category, DateTime creationDate) {
            this.code = code;
            this.name = name;
            this.amount = amount;
            this.price = price;
            this.category = category;
            this.creationDate = creationDate;
        } 
        public bool Create() {
            CADProduct cad = new CADProduct();
            return cad.Create(this);
        }
        public bool Update() {
            CADProduct cad = new CADProduct();
            return cad.Update(this);
        }
        public bool Delete() {
            CADProduct cad = new CADProduct();
            return cad.Delete(this);
        }
        public bool Read() {
            CADProduct cad = new CADProduct();
            return cad.Read(this);
        }
        public bool ReadFirst() {
            CADProduct cad = new CADProduct();
            return cad.ReadFirst(this);
        }
        public bool ReadNext() { 
            CADProduct cad = new CADProduct();
            return cad.ReadNext(this);
        }
        public bool ReadPrev() {
            CADProduct cad = new CADProduct();
            return cad.ReadPrev(this);
        }

    }
}
