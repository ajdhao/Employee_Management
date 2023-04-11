using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EntityLayer
{
    public class Country
    {
        public int Country_Id { get; set; }
        public string CountryName { get; set; }

        //public virtual ICollection<Employee> Employees { get; set; }
    }
}