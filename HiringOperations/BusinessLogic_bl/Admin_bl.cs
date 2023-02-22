using HiringOperations.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using static System.Reflection.Metadata.BlobBuilder;

namespace HiringOperations.BusinessLogic_bl
{
    public class Admin_bl
    {
        public static bool InsertData(AdminModel obj)
        {
            bool res = false;
            var dbconfig = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json").Build();
            string dbconnectionstr = dbconfig["ConnectionStrings:DefaultConnection"];
            using (SqlConnection con = new SqlConnection(dbconnectionstr))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("sp_AdminData", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FirstName", obj.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", obj.LastName);
                    cmd.Parameters.AddWithValue("@EmailID", obj.EmailID);
                    cmd.Parameters.AddWithValue("@Password", obj.Password);
                    cmd.Parameters.AddWithValue("@Gender", obj.Gender);
                    cmd.Parameters.AddWithValue("@Dob", Convert.ToDateTime(obj.Dob));
                    cmd.Parameters.AddWithValue("@Role", obj.Role);
                    cmd.Parameters.AddWithValue("@Status", obj.Status);



                    int x = cmd.ExecuteNonQuery();
                    if (x > 0)
                    {
                        return res = true;
                    }
                    else
                    {
                        return res = false;
                    }
                }
                catch (Exception)
                {

                }
                finally
                {
                    con.Close();
                }
                return res = true;


            }
        }
        public static DataTable Login(LoginViewModel obj)
        {
            var dbconfig = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json").Build();
            string dbconnectionstr = dbconfig["ConnectionStrings:DefaultConnection"];
            using (SqlConnection con = new SqlConnection(dbconnectionstr))
            {
                SqlCommand cmd = new SqlCommand("sp_AdminLogin", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmailID", obj.EmailID);
                cmd.Parameters.AddWithValue("@Password", obj.Password);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;

            }
        }
        public static bool Insertstudent(studentModel obj)
        {
            bool res = false;
            var dbconfig = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json").Build();
            string dbconnectionstr = dbconfig["ConnectionStrings:DefaultConnection"];
            using (SqlConnection con = new SqlConnection(dbconnectionstr))
            {
                try
                {
                    con.Open();
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



                    int x = cmd.ExecuteNonQuery();
                    if (x > 0)
                    {
                        return res = true;
                    }
                    else
                    {
                        return res = false;
                    }
                }
                catch (Exception)
                {

                }
                finally
                {
                    con.Close();
                }
                return res = true;


            }
        }
        public static List<studentModel> GetAllData2()
        {
            List<studentModel> obj = new List<studentModel>();
            var dbconfig = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json").Build();
            string dbconnectionstr = dbconfig["ConnectionStrings:DefaultConnection"];
            using (SqlConnection con = new SqlConnection(dbconnectionstr))
            {
                SqlDataAdapter da = new SqlDataAdapter("select * from studentinfo", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    obj.Add(
                        new studentModel
                        {
                            Hallticket = dr["Hallticket"].ToString(),
                            Name = dr["Name"].ToString(),
                            Emailid = dr["Emailid"].ToString(),
                            Mobile = dr["Mobile"].ToString(),
                            EngcollegeName = dr["EngcollegeName"].ToString(),
                            YOP = Convert.ToInt32(dr["YOP"].ToString()),
                            Totalbacklogs = Convert.ToInt32(dr["Totalbacklogs"].ToString()),
                            Status = dr["Status"].ToString(),
                        }
                        );
                }

                return obj;

            }
        }
        public static List<studentModel> GetAllData3()
        {
            List<studentModel> obj = new List<studentModel>();
            var dbconfig = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json").Build();
            string dbconnectionstr = dbconfig["ConnectionStrings:DefaultConnection"];
            using (SqlConnection con = new SqlConnection(dbconnectionstr))
            {
                SqlDataAdapter da = new SqlDataAdapter("select * from dummyL1 where Status in('Waiting For L2 Interview','Waiting For L3 Interview','L2 Rejected','Ready to Onboarding','Final Round Rejected')", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    obj.Add(
                        new studentModel
                        {
                            Hallticket = dr["Hallticket"].ToString(),
                            Name = dr["Name"].ToString(),
                            Emailid = dr["Emailid"].ToString(),
                            Mobile = dr["Mobile"].ToString(),
                            EngcollegeName = dr["EngcollegeName"].ToString(),
                            YOP = Convert.ToInt32(dr["YOP"].ToString()),
                            Totalbacklogs = Convert.ToInt32(dr["Totalbacklogs"].ToString()),
                            Status = dr["Status"].ToString(),
                        }
                        );
                }

                return obj;

            }
        }
        public static List<studentModel> GetAllData4()
        {
            List<studentModel> obj = new List<studentModel>();
            var dbconfig = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json").Build();
            string dbconnectionstr = dbconfig["ConnectionStrings:DefaultConnection"];
            using (SqlConnection con = new SqlConnection(dbconnectionstr))
            {
                SqlDataAdapter da = new SqlDataAdapter("select * from dummyL2 where Status in ('Waiting For L3 Interview','Ready to Onboarding','Final Round Rejected')", con);
              
                DataTable dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    obj.Add(
                        new studentModel
                        {
                            Hallticket = dr["Hallticket"].ToString(),
                            Name = dr["Name"].ToString(),
                            Emailid = dr["Emailid"].ToString(),
                            Mobile = dr["Mobile"].ToString(),
                            EngcollegeName = dr["EngcollegeName"].ToString(),
                            YOP = Convert.ToInt32(dr["YOP"].ToString()),
                            Totalbacklogs = Convert.ToInt32(dr["Totalbacklogs"].ToString()),
                            Status = dr["Status"].ToString(),
                        }
                        );
                }

                return obj;

            }
        }
        public static List<studentModel> GetAllData5()
        {
            List<studentModel> obj = new List<studentModel>();
            var dbconfig = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json").Build();
            string dbconnectionstr = dbconfig["ConnectionStrings:DefaultConnection"];
            using (SqlConnection con = new SqlConnection(dbconnectionstr))
            {
                SqlDataAdapter da = new SqlDataAdapter("select * from dummyL3 where Status in ('Ready to Onboarding')", con);

                DataTable dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    obj.Add(
                        new studentModel
                        {
                            Hallticket = dr["Hallticket"].ToString(),
                            Name = dr["Name"].ToString(),
                            Emailid = dr["Emailid"].ToString(),
                            Mobile = dr["Mobile"].ToString(),
                            EngcollegeName = dr["EngcollegeName"].ToString(),
                            YOP = Convert.ToInt32(dr["YOP"].ToString()),
                            Totalbacklogs = Convert.ToInt32(dr["Totalbacklogs"].ToString()),
                            Status = dr["Status"].ToString(),
                        }
                        );
                }

                return obj;

            }
        }
        public static List<AdminModel> GetAllData()
        {
            List<AdminModel> obj = new List<AdminModel>();
            var dbconfig = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json").Build();
            string dbconnectionstr = dbconfig["ConnectionStrings:DefaultConnection"];
            using (SqlConnection con = new SqlConnection(dbconnectionstr))
            {
                SqlDataAdapter da = new SqlDataAdapter("select * from Admin ", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    obj.Add(
                        new AdminModel
                        {
                            USERID = Convert.ToInt32(dr["USERID"].ToString()),
                            FirstName = dr["FirstName"].ToString(),
                            LastName = dr["LastName"].ToString(),
                            EmailID = dr["EmailID"].ToString(),
                            Password = dr["Password"].ToString(),
                            Gender = dr["Gender"].ToString(),
                            Dob = Convert.ToDateTime(dr["Dob"].ToString()),
                            Role = dr["Role"].ToString(),
                            Status = Convert.ToBoolean(dr["Status"].ToString()),
                            
                        }
                        );
                }
                return obj;

            }
        }
        public static AdminModel GetuserByID(int USERID)
        {
            AdminModel obj = null;
            var dbconfig = new ConfigurationBuilder()
                         .SetBasePath(Directory.GetCurrentDirectory())
                         .AddJsonFile("appsettings.json").Build();
            string dbconnectionstr = dbconfig["ConnectionStrings:DefaultConnection"];
            using (SqlConnection con = new SqlConnection(dbconnectionstr))
            {
                SqlCommand cmd = new SqlCommand("sp_getuserdatabyId", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@USERID", USERID);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                DataSet ds = new DataSet();
                da.Fill(ds);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    obj = new AdminModel();
                    obj.USERID = Convert.ToInt32(ds.Tables[0].Rows[i]["USERID"].ToString());
                    obj.FirstName = ds.Tables[0].Rows[i]["FirstName"].ToString();
                    obj.LastName = ds.Tables[0].Rows[i]["LastName"].ToString();
                    obj.EmailID = ds.Tables[0].Rows[i]["EmailID"].ToString();
                    obj.Password = ds.Tables[0].Rows[i]["Password"].ToString();
                    obj.Gender = ds.Tables[0].Rows[i]["Gender"].ToString();
                    obj.Dob = Convert.ToDateTime(ds.Tables[0].Rows[i]["Dob"].ToString());
                    obj.Role = ds.Tables[0].Rows[i]["Role"].ToString();
                    obj.Status = Convert.ToBoolean(ds.Tables[0].Rows[i]["Status"].ToString());
                   

                }
                return obj;
            }
        }

        public static bool UpdateuserData(AdminModel obj)
        {
            bool res = false;
            var dbconfig = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json").Build();
            string dbconnectionstr = dbconfig["ConnectionStrings:DefaultConnection"];
            using (SqlConnection con = new SqlConnection(dbconnectionstr))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("sp_updateusers", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    
                    cmd.Parameters.AddWithValue("@FirstName", obj.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", obj.LastName);
                    cmd.Parameters.AddWithValue("@EmailID", obj.EmailID);
                    cmd.Parameters.AddWithValue("@Password", obj.Password);
                    cmd.Parameters.AddWithValue("@Gender", obj.Gender);
                    cmd.Parameters.AddWithValue("@Dob", Convert.ToDateTime(obj.Dob));
                    cmd.Parameters.AddWithValue("@Role", obj.Role);
                    cmd.Parameters.AddWithValue("@Status", obj.Status);
                    cmd.Parameters.AddWithValue("@USERID", obj.USERID);

                    int x = cmd.ExecuteNonQuery();
                    if (x > 0)
                    {
                        return res = true;
                    }
                    else
                    {
                        return res = false;
                    }
                }
                catch (Exception)
                {

                }
                finally
                {
                    con.Close();
                }
                return res = true;
            }
        }
        public static DataTable forget(forget obj)
        {
            var dbconfig = new ConfigurationBuilder()
                       .SetBasePath(Directory.GetCurrentDirectory())
                       .AddJsonFile("appsettings.json").Build();
            string dbconnectionstr = dbconfig["ConnectionStrings:DefaultConnection"];
            using (SqlConnection con = new SqlConnection(dbconnectionstr))
            {
                SqlCommand cmd = new SqlCommand("sp_loginmvcforget", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmailID", obj.EmailID);
              
                SqlDataAdapter d = new SqlDataAdapter();
                d.SelectCommand = cmd;
                DataTable dt = new DataTable();
                d.Fill(dt);
                return dt;
            }
        }
        public static bool DeleteData(int USERID)
        {
            bool res = false;
            var dbconfig = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json").Build();
            string dbconnectionstr = dbconfig["ConnectionStrings:DefaultConnection"];
            using (SqlConnection con = new SqlConnection(dbconnectionstr))
            {
                try
                {

                    con.Open();
                    SqlCommand cmd = new SqlCommand("sp_deleteusers", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@USERID", USERID);

                    int x = cmd.ExecuteNonQuery();
                    if (x > 0)
                    {
                        return res = true;
                    }
                    else
                    {
                        return res = false;
                    }
                }
                catch (Exception)
                {

                }
                finally
                {
                    con.Close();
                }
                return res = true;


            }
        }
        public static DataTable reset(reset obj)
        {
            var dbconfig = new ConfigurationBuilder()
                      .SetBasePath(Directory.GetCurrentDirectory())
                      .AddJsonFile("appsettings.json").Build();
            string dbconnectionstr = dbconfig["ConnectionStrings:DefaultConnection"];
            using (SqlConnection con = new SqlConnection(dbconnectionstr))
            {
                SqlCommand cmd = new SqlCommand("sp_resetpassword2", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmailID", obj.EmailID);
                cmd.Parameters.AddWithValue("@Password", obj.Password);
                SqlDataAdapter dd = new SqlDataAdapter();
                dd.SelectCommand = cmd;
                DataTable d = new DataTable();
                dd.Fill(d);
                return d;
            }
        }
    }
}
