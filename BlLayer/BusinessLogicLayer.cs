using DlLayer;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlLayer
{
    public class BusinessLogicLayer
    {
        DataLayer dataLayer = null;
       public BusinessLogicLayer()
        {
            dataLayer = new DataLayer();
        }
        public List<Employee> GetAllEmployessDetails()
        {
              return dataLayer.GetAllEmployessDetails();
        }


        public List<Country> GetCountry()
        {
            return dataLayer.GetCountry();
        }


        #region Bind_AllListFo_Collection
        //public List<State> GetStateList()
        //{
        //    return dataLayer.GetStateList();
        //}

        //public List<City> GetCityList()
        //{
        //    return dataLayer.GetCityList();
        //}
        #endregion


        public List<State> GetStateByCountryID(int CountryId)
        {
            return dataLayer.GetStateByCountryID(CountryId);
        }
        public List<City> GetCityByStateID(int StateId)
        {
            return dataLayer.GetCityByStateID(StateId);
        }
        public bool CreateEmployee(Employee employee)
        {
            return dataLayer.CreateEmployee(employee);
        }
        public bool EditEmployee(Employee employee)
        {
            return dataLayer.EditsEmployee(employee);
        }
        public bool DeleteEmployee(int? id)
        {
            return dataLayer.DeleteEmployee(id);
        }

     
    }
}
