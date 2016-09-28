using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TestApplication.Abstract;
using TestApplication.Models;

namespace TestApplication.Concrete
{
    public class EfLannisterRepository : ILannisterRepository
    {
        private EfDbContext context = new EfDbContext();

        public IEnumerable<Lannister> Lannisters
        {
            get
            {
                return context.Lannisters;
            }
        }
    }
}