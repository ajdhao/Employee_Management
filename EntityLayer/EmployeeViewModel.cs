using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public class EmployeeViewModel
    {
        public List<Employee> employees { get; set; }

        public List<Country> countries { get; set; }
        public List<State> states { get; set; }
        public List<City> cities { get; set; }
    }
}
