using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EntityLayer
{
    public class Employee
    {
        public int Row_Id { get; set; }
        public string EmployeeCode { get; set; }


        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use of Alphabets only allowed")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Entered FirstName Must be between 2 -20 characters long.")]
        [Required(ErrorMessage = "Please Enter Your FirstName")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use of Alphabets only allowed")]
        [StringLength(maximumLength: 20, MinimumLength = 2, ErrorMessage = "Entered LastName Must be between 2 -20 characters long.")]
        [Required(ErrorMessage = "Please Enter Your LastName")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [DataType(DataType.EmailAddress, ErrorMessage = "Please enter correct email Format")]
        [Required(ErrorMessage = "Email address is Mandatory")]
        [Remote("IsEmailExist", "Employee", ErrorMessage = "Email already exist")]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Mobile Number is Mandatory")]
        [RegularExpression("^[0-9]{10}$", ErrorMessage = "Please Enter Valid Mobile Number (Only 10 Number Excepted )")]
        [StringLength(maximumLength:10,ErrorMessage ="Enter 10 Number Only")]
        [Remote("IsMobileNumberExist", "Employee", ErrorMessage = "Mobile Number already exist")]
        [Display(Name = "Mobile Number")]
        public string MobileNumber { get; set; }

        [Required(ErrorMessage = "PAN Number is Mandatory")]
        [RegularExpression("^([A-Za-z]){5}([0-9]){4}([A-Za-z]){1}$", ErrorMessage = "Invalid PAN Number")]
        [Remote("IsPanNumberExist", "Employee", ErrorMessage = "Pan Number already Exist")]
        [Display(Name = "PAN Number")]
        public string PanNumber { get; set; }

        [Required(ErrorMessage = "Passport No. is Mandatory")]
        [RegularExpression("^(?!^0+$)[a-zA-Z0-9]{3,20}$", ErrorMessage = "Invalid Entered PassPort Number")]
        [Remote("IsPassportNumberExist", "Employee", ErrorMessage = "Passport Number already Exist")]
        [Display(Name = "Passport Number")]
        public string PassportNumber { get; set; }



        [DataType(DataType.Date)]    // ApplyFormatInEditMode = "true ")
        [Required(ErrorMessage = "Please Select Your Date Of Birth")]
        [Display(Name = "Date Of Birth")]
        public string DateOfBirth { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Please Select Your Date Of Joinee")]
        [Display(Name = "Date Of Joinee")]
        public string DateOfJoinee { get; set; }

        [Required(ErrorMessage = "Please Select Your Country from List")]
        [Display(Name = "Country")]
        public int CountryId { get; set; }

        [Display(Name = "State")]
        public int StateId { get; set; }

        [Display(Name = "City")]
        public int CityId { get; set; }

        //[Required(ErrorMessage = "Please Select Your Profile Image")]
        [Display(Name = "Profile Image")]
        public string ProfileImage { get; set; }

        [Required(ErrorMessage = "Please Select Your Gender")]
        [Display(Name = "Gender")]
        public string Gender { get; set; }

        public Boolean IsActive { get; set; }

        public virtual Country Country { get; set; }
        public virtual State State { get; set; }
        public virtual City City { get; set; }


        
        public HttpPostedFileBase ImageFile { get; set; }

    }
  
 
}