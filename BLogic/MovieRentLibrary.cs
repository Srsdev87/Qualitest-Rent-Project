using QualitestProject.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Services.Description;


namespace BLogic
{
    class MovieRentLibrary
    {
       public void AddNewMovie(PortCollection fc, HttpPostedFileBase file)
        {
            QualitestProjDBEntities obj = new QualitestProjDBEntities();
            MovieTable tbl = new MovieTable();
            var allowedExtensions = new[] {
            ".Jpg", ".png", ".jpg", "jpeg"
        };
            tbl.Year = fc["Id"].ToString();
            tbl.MoviePicture = file.ToString(); //getting complete url  
            tbl.MovieName = fc["Name"].ToString();
            tbl.Category = fc["Category"];
            tbl.Country = fc["Country"];
            tbl.Length = fc["Length"];
            tbl.MovieBody = fc["Description"];
            tbl.Quality = fc["Quality"];
            tbl.DateAdded = DateTime.Now;

            var fileName = Path.GetFileName(file.FileName); //getting only file name(ex-ganesh.jpg)  
            var ext = Path.GetExtension(file.FileName); //getting the extension(ex-.jpg)  
            if (allowedExtensions.Contains(ext)) //check what type of extension  
            {
                string name = Path.GetFileNameWithoutExtension(fileName); //getting file name without extension  
                name = tbl.MovieName;
                string myfile = name + "_" + tbl.Year + ext; //appending the name with id  
                                                             // store the file inside ~/project folder(Img)  

                var path = Path.Combine(Server.MapPath("~/images/movie/"), myfile);
                tbl.MoviePicture = "/images/movie/" + myfile;
                obj.MovieTables.AddOrUpdate(tbl);
                obj.SaveChanges();
                file.SaveAs(path);
               


            }
          

        }
    }
}
