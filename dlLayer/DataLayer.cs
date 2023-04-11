using EntityLayer;
using DBConstants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace DlLayer
{
    public class DataLayer
    {
        public List<Employee> GetAllEmployessDetails()
        {
            List<Employee> employees = new List<Employee>();
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(DBConstants.DBConstants.ConnectionString);
                SqlCommand cmd = new SqlCommand(DBConstants.DBConstants.stp_Emp_GetAllEmployeeDetails, con);
                
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Employee emps = new Employee()
                        {
                            Row_Id = (int)reader["Row_Id"],
                            EmployeeCode = reader["EmployeeCode"].ToString(),
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            EmailAddress = reader["EmailAddress"].ToString(),
                            MobileNumber = reader["MobileNumber"].ToString(),
                            PanNumber = reader["PanNumber"].ToString(),
                            PassportNumber = reader["PassportNumber"].ToString(),
                            DateOfBirth = reader["DateOfBirth"].ToString(),
                            DateOfJoinee = reader["DateOfJoinee"].ToString(),
                            CountryId = (int)reader["CountryId"],
                            StateId = (int)reader["StateId"],
                            CityId = (int)reader["CityId"],


                            Country = new Country()
                            {
                                //Country_Id = (int)reader["Country_Id"],
                              CountryName = reader["CountryName"].ToString()
                            },
                            
                            State = new State()
                            {
                                //State_Id = (int)reader["State_Id"],
                                StateName = reader["StateName"].ToString()
                            },
                            City = new City()
                            {
                                //City_Id = (int)reader["City_Id"],
                                CityName = reader["CityName"].ToString()
                            },
                            ProfileImage = reader["ProfileImage"].ToString(),
                            Gender = reader["Gender"].ToString(),
                            IsActive = (bool)reader["IsActive"],
                        };
                        employees.Add(emps);
                    }
                    
                }
                
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if(con != null)
                {
                    con.Close();
                }
            }
            return employees;
        }


        public List<Country> GetCountry()
        {
            List<Country> Country = new List<Country>();
            SqlConnection con = null;

            try
            {
                con = new SqlConnection(DBConstants.DBConstants.ConnectionString);
                SqlCommand cmd = new SqlCommand(DBConstants.DBConstants.stp_Emp_GetCountry, con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Country C = new Country()
                        {
                            Country_Id = (int)reader["Country_Id"],
                            CountryName = reader["CountryName"].ToString()
                        };
                        Country.Add(C);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
            return Country;
        }

        //-------------------bind values By ID ----------------------
        public List<State> GetStateByCountryID(int CountryId)
        {
            List<State> States = new List<State>();
            SqlConnection con = null;

            try
            {
                con = new SqlConnection(DBConstants.DBConstants.ConnectionString);
                SqlCommand cmd = new SqlCommand(DBConstants.DBConstants.stp_Emp_GetStateByCountryID, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CountryId", CountryId);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        State S = new State()
                        {
                            State_Id = (int)reader["State_Id"],
                            Country_Id = (int)reader["CountryId"],
                            StateName = reader["StateName"].ToString()
                        };
                        States.Add(S);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
            return States;
        }

        public List<City> GetCityByStateID(int StateId)
        {
            List<City> Cities = new List<City>();
            SqlConnection con = null;

            try
            {
                con = new SqlConnection(DBConstants.DBConstants.ConnectionString);
                SqlCommand cmd = new SqlCommand(DBConstants.DBConstants.stp_Emp_GetAllCitiesByStateId, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StateId", StateId);
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        City ct = new City()
                        {
                            City_Id = (int)reader["City_Id"],
                            State_Id = (int)reader["StateId"],
                            CityName = reader["CityName"].ToString()
                        };
                        Cities.Add(ct);
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
            return Cities;
        }

        #region Bind_AllListFo_Collection
        //-------------------bind all values ----------------------
        //public List<State> GetStateList()
        //{
        //    List<State> States = new List<State>();
        //    SqlConnection con = null;

        //    try
        //    {
        //        con = new SqlConnection(DBConstants.DBConstants.ConnectionString);
        //        SqlCommand cmd = new SqlCommand(DBConstants.DBConstants.stp_Emp_GetListState, con);
        //        cmd.CommandType = CommandType.StoredProcedure;

        //        con.Open();
        //        SqlDataReader reader = cmd.ExecuteReader();

        //        if (reader.HasRows)
        //        {
        //            while (reader.Read())
        //            {
        //                State S = new State()
        //                {
        //                    State_Id = (int)reader["State_Id"],
        //                    Country_Id = (int)reader["CountryId"],
        //                    StateName = reader["StateName"].ToString()
        //                };
        //                States.Add(S);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }
        //    finally
        //    {
        //        if (con != null)
        //        {
        //            con.Close();
        //        }
        //    }
        //    return States;
        //}

        //public List<City> GetCityList()
        //{
        //    List<City> Cities = new List<City>();
        //    SqlConnection con = null;

        //    try
        //    {
        //        con = new SqlConnection(DBConstants.DBConstants.ConnectionString);
        //        SqlCommand cmd = new SqlCommand(DBConstants.DBConstants.stp_Emp_GetListcity, con);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        con.Open();

        //        SqlDataReader reader = cmd.ExecuteReader();

        //        if (reader.HasRows)
        //        {
        //            while (reader.Read())
        //            {
        //                City ct = new City()
        //                {
        //                    City_Id = (int)reader["City_Id"],
        //                    State_Id = (int)reader["StateId"],
        //                    CityName = reader["CityName"].ToString()
        //                };
        //                Cities.Add(ct);
        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }
        //    finally
        //    {
        //        if (con != null)
        //        {
        //            con.Close();
        //        }
        //    }
        //    return Cities;
        //}
        //------------------ ------------------------------------

        #endregion

        public bool CreateEmployee(Employee employee)
        {
            SqlConnection con = null;

            try
            {
                con = new SqlConnection(DBConstants.DBConstants.ConnectionString);
                SqlCommand cmd = new SqlCommand(DBConstants.DBConstants.stp_Emp_CreateEmployee, con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@FirstName", employee.FirstName);
                cmd.Parameters.AddWithValue("@LastName", employee.LastName);
                cmd.Parameters.AddWithValue("@EmailAddress", employee.EmailAddress);
                cmd.Parameters.AddWithValue("@MobileNumber", employee.MobileNumber);
                cmd.Parameters.AddWithValue("@PanNumber", employee.PanNumber);

                cmd.Parameters.AddWithValue("@PassportNumber", employee.PassportNumber);
                cmd.Parameters.AddWithValue("@DateOfBirth", employee.DateOfBirth);
                cmd.Parameters.AddWithValue("@DateOfJoinee", employee.DateOfJoinee);
                cmd.Parameters.AddWithValue("@CountryId", employee.CountryId);
                cmd.Parameters.AddWithValue("@StateId", employee.StateId);

                cmd.Parameters.AddWithValue("@CityId", employee.CityId);
                cmd.Parameters.AddWithValue("@ProfileImage", employee.ProfileImage);
                cmd.Parameters.AddWithValue("@Gender", employee.Gender);
                cmd.Parameters.AddWithValue("@IsActive", employee.IsActive);
                con.Open();
                cmd.ExecuteNonQuery();
               
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
            return true;
        }

        public bool DeleteEmployee(int? id)
        {
            SqlConnection con = null;

            try
            {
                con = new SqlConnection(DBConstants.DBConstants.ConnectionString);
                SqlCommand cmd = new SqlCommand(DBConstants.DBConstants.stp_Emp_DeleteEmployee, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Row_Id", id);
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
            return true;
        }       

        public bool EditsEmployee(Employee employee)
        {
            SqlConnection con = null;

            try
            {
                con = new SqlConnection(DBConstants.DBConstants.ConnectionString);
                SqlCommand cmd = new SqlCommand(DBConstants.DBConstants.stp_Emp_EditEmployee, con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Row_Id", employee.Row_Id);
                cmd.Parameters.AddWithValue("@FirstName", employee.FirstName);
                cmd.Parameters.AddWithValue("@LastName", employee.LastName);
                cmd.Parameters.AddWithValue("@EmailAddress", employee.EmailAddress);
                cmd.Parameters.AddWithValue("@MobileNumber", employee.MobileNumber);

                cmd.Parameters.AddWithValue("@PanNumber", employee.PanNumber);
                cmd.Parameters.AddWithValue("@PassportNumber", employee.PassportNumber);
                cmd.Parameters.AddWithValue("@DateOfBirth", employee.DateOfBirth);
                cmd.Parameters.AddWithValue("@DateOfJoinee", employee.DateOfJoinee);

                cmd.Parameters.AddWithValue("@CountryId", employee.CountryId);
                //cmd.Parameters.AddWithValue("@Country", employee.Country);

                cmd.Parameters.AddWithValue("@StateId", employee.StateId);
                //cmd.Parameters.AddWithValue("@State", employee.State);

                cmd.Parameters.AddWithValue("@CityId", employee.CityId);
                //cmd.Parameters.AddWithValue("@City", employee.City);

                cmd.Parameters.AddWithValue("@ProfileImage", employee.ProfileImage);
                cmd.Parameters.AddWithValue("@Gender", employee.Gender);
                cmd.Parameters.AddWithValue("@IsActive", employee.IsActive);
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
            return true;
        }


     

    }
}
