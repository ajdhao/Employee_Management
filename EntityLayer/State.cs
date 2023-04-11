using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EntityLayer
{
    public class State
    {
        public int State_Id { get; set; }
        public int? Country_Id { get; set; }
        public string StateName { get; set; }
       // public virtual ICollection<Employee> Employees { get; set; }
    }
}