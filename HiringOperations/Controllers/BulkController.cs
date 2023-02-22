using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
namespace HiringOperations.Controllers
{
    public class BulkController : Controller
    {
        private IHostEnvironment Environment;
        private IConfiguration Configuration;
        public BulkController(IHostEnvironment _environment, IConfiguration _configuration)
        {
            Environment = _environment;
            Configuration = _configuration;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(IFormFile postedFile)
        {
            if (postedFile != null)
            {
                //Create a Folder.
                string path = Path.Combine(this.Environment.ContentRootPath, "Uploads");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }



                //Save the uploaded Excel file.
                string fileName = Path.GetFileName(postedFile.FileName);
                string filePath = Path.Combine(path, fileName);
                using (FileStream stream = new FileStream(filePath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }



                //Read the connection string for the Excel file.
                string conString = this.Configuration.GetConnectionString("ExcelConString");
                DataTable dt = new DataTable();
                conString = string.Format(conString, filePath);



                using (OleDbConnection connExcel = new OleDbConnection(conString))
                {
                    using (OleDbCommand cmdExcel = new OleDbCommand())
                    {
                        using (OleDbDataAdapter odaExcel = new OleDbDataAdapter())
                        {
                            cmdExcel.Connection = connExcel;



                            //Get the name of First Sheet.
                            connExcel.Open();
                            DataTable dtExcelSchema;
                            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                            string sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                            connExcel.Close();



                            //Read Data from First Sheet.
                            connExcel.Open();
                            cmdExcel.CommandText = "SELECT * From [" + sheetName + "]";
                            odaExcel.SelectCommand = cmdExcel;
                            odaExcel.Fill(dt);
                            connExcel.Close();
                        }
                    }
                }
                //Insert the Data read from the Excel file to Database Table.
                conString = this.Configuration.GetConnectionString("DefaultConnection");
                using (SqlConnection con = new SqlConnection(conString))
                {
                    using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                    {
                        //Set the database table name.
                        sqlBulkCopy.DestinationTableName = "studentinfo";
                        //[OPTIONAL]: Map the Excel columns with that of the database table.
                        sqlBulkCopy.ColumnMappings.Add("Hallticket", "Hallticket");
                        sqlBulkCopy.ColumnMappings.Add("Name", "Name");
                        sqlBulkCopy.ColumnMappings.Add("Emailid", "Emailid");
                        sqlBulkCopy.ColumnMappings.Add("Dob", "Dob");
                        sqlBulkCopy.ColumnMappings.Add("Gender", "Gender");
                        sqlBulkCopy.ColumnMappings.Add("Mobile", "Mobile");
                        sqlBulkCopy.ColumnMappings.Add("Aadhar", "Aadhar");
                        sqlBulkCopy.ColumnMappings.Add("SchoolName", "SchoolName");
                        sqlBulkCopy.ColumnMappings.Add("Tenthpassout", "Tenthpassout");
                        sqlBulkCopy.ColumnMappings.Add("TenthAggregate", "TenthAggregate");
                        sqlBulkCopy.ColumnMappings.Add("Intercollegename", "Intercollegename");
                        sqlBulkCopy.ColumnMappings.Add("TwelthPass", "TwelthPass");
                        sqlBulkCopy.ColumnMappings.Add("TwelthAggregate", "TwelthAggregate");
                        sqlBulkCopy.ColumnMappings.Add("EngcollegeName", "EngcollegeName");
                        sqlBulkCopy.ColumnMappings.Add("Branch", "Branch");
                        sqlBulkCopy.ColumnMappings.Add("YOP", "YOP");
                        sqlBulkCopy.ColumnMappings.Add("Totalbacklogs", "Totalbacklogs");
                        sqlBulkCopy.ColumnMappings.Add("GraduationAggregate", "GraduationAggregate");
                        sqlBulkCopy.ColumnMappings.Add("Fathersname", "Fathersname");
                        sqlBulkCopy.ColumnMappings.Add("Fathersoccupation", "Fathersoccupation");
                        sqlBulkCopy.ColumnMappings.Add("Permanentaddress", "Permanentaddress");
                        sqlBulkCopy.ColumnMappings.Add("Presentaddress", "Presentaddress");
                        sqlBulkCopy.ColumnMappings.Add("FathersMobile", "FathersMobile");
                        sqlBulkCopy.ColumnMappings.Add("MothersName", "MothersName");
                        sqlBulkCopy.ColumnMappings.Add("Mothersoccupation", "Mothersoccupation");
                        sqlBulkCopy.ColumnMappings.Add("Names", "Names");
                        sqlBulkCopy.ColumnMappings.Add("ContentType", "ContentType");
                        sqlBulkCopy.ColumnMappings.Add("Data", "Data");
                        con.Open();
                        sqlBulkCopy.WriteToServer(dt);
                        con.Close();
                    }
                }
            }
            return View();
        }
    }
}