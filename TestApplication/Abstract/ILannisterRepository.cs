using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using TestApplication.Models;

namespace TestApplication.Abstract
{
    public interface ILannisterRepository
    {
        IEnumerable<Lannister> Lannisters { get;}
    }
}