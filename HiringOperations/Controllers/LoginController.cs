using Microsoft.AspNetCore.Mvc;
using HiringOperations.Models;
using HiringOperations.BusinessLogic_bl;
using System.Data;
using System.Net;
using System.Net.Mail;
using System.Configuration;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.Data.SqlClient;


namespace HiringOperations.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginViewModel obj)
        {
            if (ModelState.IsValid)
            {
                DataTable dt = new DataTable();
                dt = Admin_bl.Login(obj);
                if (dt.Rows.Count > 0)
                {
                    HttpContext.Session.SetString("UserName", dt.Rows[0]["EmailID"].ToString());
                    HttpContext.Session.SetString("LoginName", dt.Rows[0]["FirstName"].ToString());
                    HttpContext.Session.SetString("Time", System.DateTime.Now.ToShortTimeString());
                    return RedirectToAction("AdminHome", "Login");
                }
                else
                {
                    ViewBag.Message = String.Format("Your Emailid or Password is Incorrect");
                    return View();
                }
            }
            else
            {
                return View();
            }
        }
        [HttpGet]
     [SetSessionsGlobally]
        public IActionResult AdminHome()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AdminHome(AdminModel obj)
        {
            if (ModelState.IsValid)
            {
                bool res = Admin_bl.InsertData(obj);
                if (res == true)
                {
                    return View();
                }
                else
                {
                    return View();
                }
            }
            else
            {
                return View();
            }
        }
        [HttpGet]
        [SetSessionsGlobally]
        public IActionResult Addusers()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Addusers(AdminModel obj)
        {
             if (ModelState.IsValid)
                {
                    bool res = Admin_bl.InsertData(obj);
                    if (res)
                    {
                        ViewBag.Message = "Your Data Inserted Sucessfully..!!";
                    }
                    else
                    {
                        ViewBag.Message = "Data Insertion Failed";
                    }
                    return View(obj);
                }
                else
                {
                    return View(obj);
                }
            }
        [HttpGet]
       [SetSessionsGlobally]
        public IActionResult Addstudents()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Addstudents(studentModel obj, List<IFormFile> PostedFiles)
        {
            foreach (IFormFile PostedFile in PostedFiles)
            {
                string fileName = Path.GetFileName(PostedFile.FileName);
                string type = PostedFile.ContentType;
                byte[] bytes = null;
                using (MemoryStream ms = new MemoryStream())
                {
                    PostedFile.CopyTo(ms);
                    bytes = ms.ToArray();
                }
                var dbconfig = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json").Build();
                string dbconnectionstr = dbconfig["ConnectionStrings:DefaultConnection"];
                using (SqlConnection con = new SqlConnection(dbconnectionstr))
                {

                    SqlCommand cmd = new SqlCommand("sp_StudentHTDinsert", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Hallticket", obj.Hallticket);
                    cmd.Parameters.AddWithValue("@Name", obj.Name);
                    cmd.Parameters.AddWithValue("@Emailid", obj.Emailid);
                    cmd.Parameters.AddWithValue("@Dob", Convert.ToDateTime(obj.Dob));
                    cmd.Parameters.AddWithValue("@Gender", obj.Gender);
                    cmd.Parameters.AddWithValue("@Mobile", obj.Mobile);
                    cmd.Parameters.AddWithValue("@Aadhar", obj.Aadhar);
                    cmd.Parameters.AddWithValue("@SchoolName", obj.SchoolName);
                    cmd.Parameters.AddWithValue("@Tenthpassout", obj.Tenthpassout);
                    cmd.Parameters.AddWithValue("@TenthAggregate", obj.TenthAggregate);
                    cmd.Parameters.AddWithValue("@Intercollegename", obj.Intercollegename);
                    cmd.Parameters.AddWithValue("@TwelthPass", obj.TwelthPass);
                    cmd.Parameters.AddWithValue("@TwelthAggregate", obj.TwelthAggregate);
                    cmd.Parameters.AddWithValue("@EngcollegeName", obj.EngcollegeName);
                    cmd.Parameters.AddWithValue("@Branch", obj.Branch);
                    cmd.Parameters.AddWithValue("@YOP", obj.YOP);
                    cmd.Parameters.AddWithValue("@Totalbacklogs", obj.Totalbacklogs);
                    cmd.Parameters.AddWithValue("@GraduationAggregate", obj.GraduationAggregate);
                    cmd.Parameters.AddWithValue("@Fathersname", obj.Fathersname);
                    cmd.Parameters.AddWithValue("@Fathersoccupation", obj.Fathersoccupation);
                    cmd.Parameters.AddWithValue("@Permanentaddress", obj.Permanentaddress);
                    cmd.Parameters.AddWithValue("@Presentaddress", obj.Presentaddress);
                    cmd.Parameters.AddWithValue("@FathersMobile", obj.FathersMobile);
                    cmd.Parameters.AddWithValue("@MothersName", obj.MothersName);
                    cmd.Parameters.AddWithValue("@Mothersoccupation", obj.Mothersoccupation);
                    cmd.Parameters.AddWithValue("@Names", fileName);
                    cmd.Parameters.AddWithValue("@ContentType", type);
                    cmd.Parameters.AddWithValue("@Data", bytes);
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return View();

        }
        [HttpGet]
        public IActionResult TrainerHome()
        {
            return View(Admin_bl.GetAllData2());
        }
        [HttpGet]


        public IActionResult Logout()
        {
            return RedirectToAction("Login", "Login");
        }
        [HttpGet]
       [SetSessionsGlobally]
        public IActionResult Display()
        {
            return View(Admin_bl.GetAllData2());
        }
        [HttpPost]
        public ActionResult Display(string Hallticket, string Remarks, int Score, string To)
        {
            string LoginName = HttpContext.Session.GetString("LoginName");
            string Status = "";
          //  string To = "";
            string Result = "";
            if (Score >= 60)
            {
                Status = "Waiting For L2 Interview";
                Result = "Dear Student,Congratulations you are Selected In First Round";
                //return RedirectToAction("dummyL2", "Login");
            }
            else
            {
                Result = "Dear Student,  you are Rejected In First Round ";
                Status = "L1 Rejected";
            }
                var dbconfig = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("appsettings.json").Build();
            string dbconnectionstr = dbconfig["ConnectionStrings:DefaultConnection"];
            using (SqlConnection con = new SqlConnection(dbconnectionstr))
            {

                con.Open();
                SqlCommand cmd = new SqlCommand("sp_interview_records_std", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Hallticket", Hallticket);
                cmd.Parameters.AddWithValue("@Remarks", Remarks);
                cmd.Parameters.AddWithValue("@Status", Status);
                cmd.Parameters.AddWithValue("@LoginName", LoginName);
                cmd.Parameters.AddWithValue("@Score", Score);

                MailMessage mail = new MailMessage();
                mail.To.Add(To);
                mail.From = new MailAddress("gollabrijesh2001@gmail.com");
                mail.Subject = "Interview Result";
                string Body = Result;
                mail.Body = Body;
                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential("gollabrijesh2001@gmail.com", "fqdjqilrcbytqvzz"); // Enter seders User name and password       
                smtp.EnableSsl = true;
                smtp.Send(mail);
                int x = cmd.ExecuteNonQuery();
                if (x > 0)
                {
                    return RedirectToAction("Display", "Login");
                }
                return View();
            }
        }
        [HttpGet]
        public IActionResult Displayusers()
        {
            return View(Admin_bl.GetAllData());
        }
        [HttpGet]
        public IActionResult Edit(int? USERID)
        {
            return View(Admin_bl.GetuserByID((int)USERID));
        }
        [HttpPost]
        public IActionResult Edit(AdminModel obj)
        {
            if (ModelState.IsValid)
            {
                bool res = (Admin_bl.UpdateuserData(obj));
                if (res == true)
                {
                    return RedirectToAction("Displayusers", "Login");
                }
                else
                {
                    return View(obj);
                }
            }
            return View();
        }
        public IActionResult Delete(int? USERID)
        {
            bool res = Admin_bl.DeleteData((int)USERID);
            if (res == true)
            {
                return RedirectToAction("Displayusers");
            }
            else
            {
                return View();
            }
            return View();
        }
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ForgotPassword(forget obj)
        {
            if (ModelState.IsValid)
            {
                DataTable dt = new DataTable(); dt = Admin_bl.forget(obj); if (dt.Rows.Count > 0)
                {
                    Random rand = new Random();
                    HttpContext.Session.SetString("OTP", rand.Next(1111, 9999).ToString());


                    bool result = SendEmail(obj.EmailID);
                    if (result == true)
                    {
                        return RedirectToAction("forgetotp", "Login");
                    }
                }
                else
                {
                    return View();
                }
                return View(obj);
            }
            return View();
        }

        [HttpGet]
        public IActionResult forgetotp()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult forgetotp(Sendmail obj)
        {
            if (obj.Otp.Equals(HttpContext.Session.GetString("OTP")))
            {
                return RedirectToAction("Reset", "Login");
            }
            else
            {
                return View();
            }
        }
        public bool SendEmail(string receiver)
        {
            bool chk = false;
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("gollabrijesh2001@gmail.com");
                mail.To.Add(receiver);
                mail.IsBodyHtml = true;
                mail.Subject = "OTP";
                mail.Body = "Your OTP is :" + HttpContext.Session.GetString("OTP");
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                client.Credentials = new NetworkCredential("gollabrijesh2001@gmail.com", "fqdjqilrcbytqvzz");
                client.EnableSsl = true;
                client.Send(mail);
                chk = true;
            }
            catch (Exception)
            {
                throw;
            }
            return chk;
        }
        [HttpGet]
        public ActionResult Reset()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Reset(reset obj)
        {
            if (ModelState.IsValid)
            {
                DataTable dt = new DataTable();
                dt = Admin_bl.reset(obj); if (dt.Rows.Count == 0)
                {
                    return RedirectToAction("Login", "Login");
                }
                else
                {
                    return View(obj);
                }

            }
            else
            {
                return View();
            }
        }
        //[HttpGet]
        //public ActionResult Model()
        //{
        //    return View(Admin_bl.GetAllData2());
        //}
        //[HttpPost]
        //public ActionResult Model(string Hallticket,string Remarks,int Score)
        //{
        //    string LoginName = "";
        //    string Status = "";
        //    if (Score >= 60)
        //    {
        //        Status = "Waiting for L2 Interview";
        //    }
        //    else
        //    {
        //        Status = "L1 Rejected";
        //    }
        //    var dbconfig = new ConfigurationBuilder()
        //         .SetBasePath(Directory.GetCurrentDirectory())
        //         .AddJsonFile("appsettings.json").Build();
        //    string dbconnectionstr = dbconfig["ConnectionStrings:DefaultConnection"];
        //    using (SqlConnection con = new SqlConnection(dbconnectionstr))
        //    {

        //            con.Open();
        //            SqlCommand cmd = new SqlCommand("sp_interview_records_std", con);
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.AddWithValue("@Hallticket",Hallticket);
        //            cmd.Parameters.AddWithValue("@Remarks",Remarks);
        //            cmd.Parameters.AddWithValue("@Status",Status);
        //            cmd.Parameters.AddWithValue("@LoginName",LoginName);
        //            cmd.Parameters.AddWithValue("@Score", Score);

        //            int x = cmd.ExecuteNonQuery();
        //            con.Close();
        //            if (x > 0)
        //            {
        //                return RedirectToAction("Model","Login");
        //            }
        //            return View();
        //        }
        //    }
        [HttpGet]
        public IActionResult L2Module()
        {
            return View(Admin_bl.GetAllData3());
        }
        [HttpPost]
        public ActionResult L2Module(string Hallticket, string Remarks, int Score, string To)
        {
            string LoginName = HttpContext.Session.GetString("LoginName");
            string Status = "";
            //  string To = "";
            string Result = "";
            if (Score >= 60)
            {
                Status = "Waiting For L3 Interview";
                Result = "Dear Student,Congratulations you are Selected In second Round";
                //return RedirectToAction("dummyL2", "Login");
            }
            else
            {
                Result = "Dear Student,  you are Rejected In second Round ";
                Status = "L2 Rejected";
            }
            var dbconfig = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("appsettings.json").Build();
            string dbconnectionstr = dbconfig["ConnectionStrings:DefaultConnection"];
            using (SqlConnection con = new SqlConnection(dbconnectionstr))
            {

                con.Open();
                SqlCommand cmd = new SqlCommand("sp_interview_records_std2", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Hallticket", Hallticket);
                cmd.Parameters.AddWithValue("@Remarks", Remarks);
                cmd.Parameters.AddWithValue("@Status", Status);
                cmd.Parameters.AddWithValue("@LoginName", LoginName);
                cmd.Parameters.AddWithValue("@Score", Score);

                MailMessage mail = new MailMessage();
                mail.To.Add(To);
                mail.From = new MailAddress("gollabrijesh2001@gmail.com");
                mail.Subject = "Interview Result";
                string Body = Result;
                mail.Body = Body;
                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential("gollabrijesh2001@gmail.com", "fqdjqilrcbytqvzz"); // Enter seders User name and password       
                smtp.EnableSsl = true;
                smtp.Send(mail);
                int x = cmd.ExecuteNonQuery();
                if (x > 0)
                {
                    return RedirectToAction("L3Module", "Login");
                }
                return View();
            }
        }

        [HttpGet]
        public IActionResult L3Module()
        {
            return View(Admin_bl.GetAllData4());
        }
        [HttpPost]
        public ActionResult L3Module(string Hallticket, string Remarks, int Score, string To)
        {
            string LoginName = HttpContext.Session.GetString("LoginName");
            string Status = "";
            //  string To = "";
            string Result = "";
            if (Score >= 60)
            {
                Status = "Ready to Onboarding"; 
                Result = "Dear Student,Congratulations you are Selected In Final Round";
                //return RedirectToAction("dummyL2", "Login");
            }
            else
            {
                Result = "Dear Student,  you are Rejected In Final Round ";
                Status = "Final Round Rejected";
            }
            var dbconfig = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("appsettings.json").Build();
            string dbconnectionstr = dbconfig["ConnectionStrings:DefaultConnection"];
            using (SqlConnection con = new SqlConnection(dbconnectionstr))
            {

                con.Open();
                SqlCommand cmd = new SqlCommand("sp_interview_records_std3", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Hallticket", Hallticket);
                cmd.Parameters.AddWithValue("@Remarks", Remarks);
                cmd.Parameters.AddWithValue("@Status", Status);
                cmd.Parameters.AddWithValue("@LoginName", LoginName);
                cmd.Parameters.AddWithValue("@Score", Score);
                MailMessage mail = new MailMessage();
                mail.To.Add(To);
                mail.From = new MailAddress("gollabrijesh2001@gmail.com");
                mail.Subject = "Interview Result";
                string Body = Result;
                mail.Body = Body;
                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential("gollabrijesh2001@gmail.com","fqdjqilrcbytqvzz"); // Enter seders User name and password       
                smtp.EnableSsl = true;
                smtp.Send(mail);
                int x = cmd.ExecuteNonQuery();
                if (x > 0)
                {
                    return RedirectToAction("Onboarding", "Login");
                }
                return View();
            }
        }
        [HttpGet]
        public IActionResult Onboarding()
        {
            return View(Admin_bl.GetAllData5());
        }
    }
} 



    
