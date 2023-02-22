using System.Reflection;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MessagePack;

namespace HiringOperations.Models
{

    public class AdminModel
    {
        public int USERID { get; set; }
        [Required(ErrorMessage = "FirstName can't be empty")]
        [RegularExpression(@"^[a-z A-Z]+$", ErrorMessage = "Use letters only please")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "LastName can't be empty")]
        [RegularExpression(@"^[a-z A-Z]+$", ErrorMessage = "Use letters only please")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email can't be empty")]
        [RegularExpression(@"[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}", ErrorMessage = "Invalid email")]
        public string EmailID { get; set; }
        [StringLength(18, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [RegularExpression(@"^((?=.*[a-z])(?=.*[A-Z])(?=.*\d)).+$", ErrorMessage = "Password must contain at least six characters,a capital letter,a symbol,and a number")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Gender can't be empty")]
        public string Gender { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Dob { get; set; }
        public string Role { get; set; }
        public bool Status { get; set; }
        public DateTime InsertionDate { get; set; }


    }
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Enter your EmailID")]
        public string EmailID { get; set; }

        [Required(ErrorMessage = "Enter your Password")]
        public string Password { get; set; }
    }

    public class studentModel
    {
        public string Hallticket { get; set; }
        [Required(ErrorMessage = "Name can't be empty")]
        [RegularExpression(@"^[a-z A-Z]+$", ErrorMessage = "Use letters only please")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Email can't be empty")]
        [RegularExpression(@"[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}", ErrorMessage = "Invalid email")]
        public string Emailid { get; set; }
        [Required(ErrorMessage = "Dob can't be empty")]
        public DateTime Dob { get; set; }
        [Required(ErrorMessage = "Gender can't be empty")]
        public string Gender { get; set; }
        [Required(ErrorMessage = "Mobile can't be empty")]
        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Invalid Mobile Number.")]
        public string Mobile { get; set; }
        [Required(ErrorMessage = "Adhar number can't be empty")]

        public string Aadhar { get; set; }
        public string SchoolName { get; set; }
        public int Tenthpassout { get; set; }
        public string TenthAggregate { get; set; }
        public string Intercollegename { get; set; }
        public int TwelthPass { get; set; }
        public string TwelthAggregate { get; set; }
        public string EngcollegeName { get; set; }
        public string Branch { get; set; }
        public int YOP { get; set; }
        public int Totalbacklogs { get; set; }
        public string GraduationAggregate { get; set; }
        public string Fathersname { get; set; }
        public string Fathersoccupation { get; set; }
        public string Permanentaddress { get; set; }
        public string Presentaddress { get; set; }
        public string FathersMobile { get; set; }
        public string MothersName { get; set; }
        public string Mothersoccupation { get; set; }
        public string Names { get; set; }
        public string ContentType { get; set; }
        public byte[] Data { get; set; }
        public string Status { get; set; }

    }
    public class forget
    {
        [Required(ErrorMessage = "Please Enter your email")]
        public string EmailID { get; set; }
       

    }
    public class Sendmail
    { 
        public string Otp { get; set; }

    }
   
public class reset
    {
        [Required(ErrorMessage = "Please enter your email")]

        public string EmailID { get; set; }
        [Required(ErrorMessage = "Please enter password")]
        [DataType(DataType.Password)][StringLength(100, ErrorMessage = "Password \"{0}\" must have {2} character", MinimumLength = 8)][RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&]{6,}$", ErrorMessage = "Password must contain: Minimum 8 characters atleast 1 UpperCase Alphabet, 1 LowerCase Alphabet, 1 Number and 1 Special Character")] 
        public string Password { get; set; }
        [Display(Name = "Confirm password")][Required(ErrorMessage = "Re-Enter password")][Compare("Password", ErrorMessage = "Confirm password doesn't match, Try again !")][DataType(DataType.Password)]
        public string confirmPassword { get; set; }
    }


    public class Model
    {
        public string Hallticket { get; set; }
        public string Remarks { get; set; }
        public int score { get; set; }
       
    }
    public class MailModel
    {
        public string To { get; set; }
        //public string Subject { get; set; }
        //public string Body { get; set; }  
        //public string From { get; set; }    
    }

}
