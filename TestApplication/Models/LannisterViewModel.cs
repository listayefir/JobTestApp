using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace TestApplication.Models
{
    public class LannisterViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public string Parent { get; set; }
        public DateTime Created { get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            return Title;
        }
    }
}