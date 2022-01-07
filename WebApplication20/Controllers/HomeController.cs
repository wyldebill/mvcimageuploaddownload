using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication20.Models;

namespace WebApplication20.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet]
        public ActionResult ViewUpload()
        {
            return View();
        }
        [HttpGet]
        public ActionResult LoadImage(int imageId)
        {

            byte[] image = null;
            SqlConnection conn = new SqlConnection(@"Data Source=(LocalDb)\MSSQLLocalDB;Initial Catalog=testdb;Integrated Security=true;");
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;

            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "Select * from  Images where Id=@imageId";
            cmd.Parameters.Add(new SqlParameter("@imageId", imageId));


            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                image = (byte[])reader["imgdata"];
            }
            conn.Close();
            FileContentResult file = File(image, "image/png");

            return file;
        }

        [HttpGet]
        public ActionResult Upload()
        {
            return View();
        }

        [Route("Upload")]
        [HttpPost]
        public ActionResult Upload(ContentViewModel model)
        {
            // the request has the file that was uploaded in it.
            HttpPostedFileBase file = Request.Files["ImageData"];

            // but we need an array of bytes to store it in sql db, so convert it here
            byte[] imageBytes = null;
            BinaryReader reader = new BinaryReader(file.InputStream);
            imageBytes = reader.ReadBytes((int)file.ContentLength);



            SqlConnection conn = new SqlConnection(@"Data Source=(LocalDb)\MSSQLLocalDB;Initial Catalog=testdb;Integrated Security=true;");
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;

            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "Insert into Images (imgdata) values (@data)";

            // setup the array of bytes in the sql query here using a sqlparameter
            cmd.Parameters.Add("@data", SqlDbType.Image, imageBytes.Length).Value = imageBytes;

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();


            return View(model);
        }

     
    }
}