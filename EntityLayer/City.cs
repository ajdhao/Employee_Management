using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EntityLayer
{
    public class City
    {
        public int City_Id { get; set; }
        public int? State_Id { get; set; }
        public string CityName { get; set; }

        //public virtual ICollection<Employee> Employees { get; set; }
    }
}