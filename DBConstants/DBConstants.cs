using System.Configuration;

namespace DBConstants
{
    public static class DBConstants
    {
      
        public static string ConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["EmployeeDB"].ConnectionString;
            }
        }

        public static string stp_Emp_GetAllEmployeeDetails = "stp_Emp_GetAllEmployeeDetails";
        public static string stp_Emp_CreateEmployee = "stp_Emp_CreateEmployee";
        public static string stp_Emp_EditEmployee = "stp_Emp_EditEmployee";
        //Hard Delete
        //public static string stp_Emp_DeleteEmployee = "stp_Emp_DeleteEmployee"; 

        // soft Delete
        public static string stp_Emp_DeleteEmployee = "stp_Emp_DeleteEmployee";

        public static string stp_Emp_GetCountry = "stp_Emp_GetCountry";

       
        public static string stp_Emp_GetStateByCountryID = "stp_Emp_GetStateByCountryID";
        public static string stp_Emp_GetAllCitiesByStateId = "stp_Emp_GetAllCitiesByStateId";

    }
}
    

