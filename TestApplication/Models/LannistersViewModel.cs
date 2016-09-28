using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestApplication.Models
{
    public class LannistersViewModel
    {
        public List<LannisterViewModel> Lannisters { get; set; }

        public LannistersViewModel()
        {
            Lannisters = new List<LannisterViewModel>();
        }

    }
}