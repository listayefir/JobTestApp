using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using TestApplication.Models;
using System.Web.Mvc;

namespace TestApplication.Models
{
    public class PropertySorter
    {
        public List<SelectListItem> Properties { get; set; }

        public PropertySorter()
        {
            Properties = new List<SelectListItem>();
            var props = typeof(LannisterViewModel).GetProperties();
            foreach (var prop in props)
            {
                Properties.Add(new SelectListItem
                {
                    Text = prop.Name,
                    Value = prop.Name
                });
            }
        } 
    }
}