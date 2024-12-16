using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocioPro
{
    public class Category
    {
        private int idLabel { get; set; }
        private string name { get; set; }
        private string description { get; set; }

        public Category(int idLabel, string name)
        {
            this.idLabel = idLabel;
            this.name = name;
            this.description = "";
        }

        public void setDescription(string description) {
            this.description = description;    
        }
    }

}
