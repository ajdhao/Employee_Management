using BlLayer;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeeManagement.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            BusinessLogicLayer bl = new BusinessLogicLayer();
            List<Employee> employee = bl.GetAllEmployessDetails();
            return View(employee);
        }

       
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.countryList = new SelectList(CountryList(), "Country_Id", "CountryName");
           

            return View();
        }

        [HttpPost]
        public ActionResult Create(Employee employee)
        {
            BusinessLogicLayer bl = new BusinessLogicLayer();
            if (ModelState.IsValid)
            {

                string fileName = Path.GetFileNameWithoutExtension(employee.ImageFile.FileName);
                string extension = Path.GetExtension(employee.ImageFile.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                employee.ProfileImage = "../Image/" + fileName;
                fileName = Path.Combine(Server.MapPath("../Image/" + fileName));

                employee.ImageFile.SaveAs(fileName);

                if (extension.ToLower() == ".jpeg" || extension.ToLower() == ".jpg"|| extension.ToLower() == ".jfif" || extension.ToLower() == ".png")
                {
                    if (employee.ImageFile.ContentLength <= 2000000)
                    {
                        employee.ImageFile.SaveAs(fileName);
                       
                        bl.CreateEmployee(employee);

                        TempData["Message"] = "Employee Created Successfully";
                        return RedirectToAction("Index");
                    }
                    //TempData["Message"] = "Employee Created Successfully";
                }
                
                else
                {                  
                    TempData["ErrorMessage"] = "Please Upload Valid Profile Image...";
                }

            }
            
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int? Row_Id,int? CountryId,int? StateId)
         {
            BusinessLogicLayer bl = new BusinessLogicLayer();


            
            ViewBag.countryList = new SelectList(CountryList(), "Country_Id", "CountryName");
            ViewBag.stateList = new SelectList(StateListByCountryId(CountryId ?? 0), "State_Id", "StateName");
            ViewBag.cityList = new SelectList(CityListBySatateId(StateId ?? 0), "City_Id", "CityName");

          

            var r = bl.GetAllEmployessDetails();
            Employee e = r.Where(s => s.Row_Id == Row_Id).FirstOrDefault();
            return View(e);
        }


        [HttpPost]
        public ActionResult Edit(Employee employee)
        {
            
            BusinessLogicLayer bl = new BusinessLogicLayer();
            if (ModelState.IsValid)
            {
                
                var r = bl.GetAllEmployessDetails();
                Employee e = r.Where(s => s.Row_Id == employee.Row_Id).FirstOrDefault();

                string OldImagePath = e.ProfileImage;

              // ImageFile = e.ProfileImage;
                if (employee.ImageFile==null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(OldImagePath);
                    string extension = Path.GetExtension(OldImagePath);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                  
                    employee.ProfileImage = "../Image/" + fileName;
                    fileName = Path.Combine(Server.MapPath("../Image/" + fileName));

                    if (extension.ToLower() == ".jpeg" || extension.ToLower() == ".jpg" || extension.ToLower() == ".jfif" || extension.ToLower() == ".png")
                    {
                        if (OldImagePath.Length <= 2000000)
                        {
                            ViewBag.personalImagePath = OldImagePath;

                            //  ???
                           // employee.ImageFile = OldImagePath;

                           employee.ImageFile.SaveAs(fileName);
                            if (bl.EditEmployee(employee))
                            {
                                TempData["Message"] = "Employee Details UpDated Successfully";
                                return RedirectToAction("Index");
                            }
                        }

                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Please Upload Valid Profile Image...'";
                    }

                }


                else
                {
                    string fileName = Path.GetFileNameWithoutExtension(employee.ImageFile.FileName);
                    string extension = Path.GetExtension(employee.ImageFile.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    employee.ProfileImage = "../Image/" + fileName;
                    fileName = Path.Combine(Server.MapPath("../Image/" + fileName));

                    employee.ImageFile.SaveAs(fileName);

                    if (extension.ToLower() == ".jpeg" || extension.ToLower() == ".jpg" || extension.ToLower() == ".jfif" || extension.ToLower() == ".png")
                    {
                        if (employee.ImageFile.ContentLength <= 2000000)
                        {
                            employee.ImageFile.SaveAs(fileName);

                            if (bl.EditEmployee(employee))
                            {
                                TempData["Message"] = "Employee Details UpDated Successfully";
                                return RedirectToAction("Index");
                            }
                        }

                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Please Upload Valid Profile Image...'";
                    }
                }
              
            }
            return View();

        }

        //------------------ Delete through Model PopUp ------------------------
        [HttpPost]
        public JsonResult DeleteEmployeeModel(int EmployeeId)
        {
            //BusinessLogicLayer bl = new BusinessLogicLayer();
            bool result = false;

            BusinessLogicLayer bl = new BusinessLogicLayer();
            var r = bl.GetAllEmployessDetails();
            Employee e = r.Where(s => s.Row_Id == EmployeeId).FirstOrDefault();
            if (e != null)
            {
               if(bl.DeleteEmployee(EmployeeId))
                {
                    e.IsActive = false;
                    result = true;
                }
                e.IsActive = true;
                
               
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        
        #region Hard Delete
        //[HttpGet]
        //public ActionResult Delete(int? Row_Id)
        //{
        //    BusinessLogicLayer bl = new BusinessLogicLayer();
        //    var r = bl.GetAllEmployessDetails();
        //    Employee e = r.Where(s => s.Row_Id == Row_Id).FirstOrDefault();
        //    return View(e);
        //}

        //[HttpPost]
        //[ActionName("Delete")]
        //public ActionResult DeleteConfirm(int? Row_Id)
        //{
        //    BusinessLogicLayer bl = new BusinessLogicLayer();
        //    if (bl.DeleteEmployee(Row_Id))
        //    {
        //       return RedirectToAction("Index");
        //    }
        //    return View();
        //}

        //[HttpGet]
        //public ActionResult Details(int? Row_Id)
        //{
        //    BusinessLogicLayer bl = new BusinessLogicLayer();
        //    var r = bl.GetAllEmployessDetails();
        //    Employee e = r.Where(s => s.Row_Id == Row_Id).FirstOrDefault();
        //    return View(e);
        //}

        #endregion


        //-------------- Bind All Country  -------------------------------
        BusinessLogicLayer bl = null;
        public List<Country> CountryList()
        {
            bl = new BusinessLogicLayer();
            List<Country> countries = bl.GetCountry().ToList();
            return countries;
        }
        

        //----------- Bind Drop Down value ByID  (Create Method)------------------------
        public ActionResult StateList(int CountryId)
        {
            bl = new BusinessLogicLayer();
           

            List<State> selectList = bl.GetStateByCountryID(CountryId).Where(x => x.Country_Id == CountryId).ToList();
            ViewBag.StateList = new SelectList(selectList, "State_Id", "StateName");
            return PartialView("DisplayStates");
        }
        public ActionResult CityList(int StateId)
        {
            bl = new BusinessLogicLayer();
            

            List<City> selectList = bl.GetCityByStateID(StateId).Where(x => x.State_Id == StateId).ToList();
            ViewBag.cityList = new SelectList(selectList, "City_Id", "CityName");
            return PartialView("DisplayCities");
        }





        //----------- Bind Drop Down value ByID  (Edit Method we load data  Base on IDs)  ------------------------
        public List<State> StateListByCountryId(int CountryId)
        {
            bl = new BusinessLogicLayer();


            List<State> selectList = bl.GetStateByCountryID(CountryId).Where(x => x.Country_Id == CountryId).ToList();

            ViewBag.StateList = new SelectList(selectList, "State_Id", "StateName");
            return selectList;
           
        }
        public List<City> CityListBySatateId(int StateId)
        {
            bl = new BusinessLogicLayer();


            List<City> selectList = bl.GetCityByStateID(StateId).Where(x => x.State_Id == StateId).ToList();
            ViewBag.cityList = new SelectList(selectList, "City_Id", "CityName");
            return selectList;
           
        }


        
        #region Remote attribute validation Methodes

        [HttpGet]
        public JsonResult IsMobileNumberExist(string MobileNumber)
        {
            bl = new BusinessLogicLayer();
            var isExists = bl.GetAllEmployessDetails().Any(s => s.MobileNumber == MobileNumber);
            return Json(!isExists, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult IsEmailExist(string EmailAddress)
        {
            bl = new BusinessLogicLayer();
            var isExist = bl.GetAllEmployessDetails().Any(s => s.EmailAddress == EmailAddress);
            return Json(!isExist, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult IsPassportNumberExist(string PassportNumber)
        {
            bl = new BusinessLogicLayer();
            var isExist = bl.GetAllEmployessDetails().Any(s => s.PassportNumber == PassportNumber);
            return Json(!isExist, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult IsPanNumberExist(string PanNumber)
        {
            bl = new BusinessLogicLayer();
            var isExist = bl.GetAllEmployessDetails().Any(s => s.PanNumber == PanNumber);
            return Json(!isExist, JsonRequestBehavior.AllowGet);
        }
        #endregion

       
        #region loder method 
        public JsonResult CallingAjaxFunction()
        {
            System.Threading.Thread.Sleep(2000);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}