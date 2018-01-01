using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Dynamic;
using System.EnterpriseServices;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QualitestProject.Models;
using QualitestProject.Helpers;

namespace QualitestProject.Services.MovieManagement
{
    public class MovieRentManegement
    {
       
        public void AddNewMovie(FormCollection fc, HttpPostedFileBase file)
        {
            QualitestProjDBEntities obj = new QualitestProjDBEntities();
            MovieTable tbl = new MovieTable();
            var allowedExtensions = new[] {
            ".Jpg", ".png", ".jpg", "jpeg"
        };
            try
            {
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

                    var path = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/images/movie/"), myfile);
                    tbl.MoviePicture = "/images/movie/" + myfile;
                    obj.MovieTables.AddOrUpdate(tbl);
                    obj.SaveChanges();
                    file.SaveAs(path);
                }
            }
            catch (Exception)
            {
                tbl.Year = fc["Id"].ToString();
                tbl.MoviePicture = "default.png"; //getting complete url  
                tbl.MovieName = fc["Name"].ToString();
                tbl.Category = fc["Category"];
                tbl.Country = fc["Country"];
                tbl.Length = fc["Length"];
                tbl.MovieBody = fc["Description"];
                tbl.Quality = fc["Quality"];
                tbl.DateAdded = DateTime.Now;
                obj.MovieTables.AddOrUpdate(tbl);
                obj.SaveChanges();
            }
          
        }

        public void DeleteMovie(FormCollection fc)
        {

            using (QualitestProjDBEntities movieEntities = new QualitestProjDBEntities())
            {
                MovieTable tbl = new MovieTable();
                tbl.MovieName = fc["Name"].ToString();
                MovieTable movie = movieEntities.MovieTables.FirstOrDefault(c => c.MovieName == tbl.MovieName);

                try
                {
                    string rootFolderPath = movie.MoviePicture;

                    string[] filesToDelete = rootFolderPath.Split('/');


                    string directoryName = Path.GetDirectoryName("~" + rootFolderPath);

                    string[] fileList = Directory.GetFiles(System.Web.HttpContext.Current.Server.MapPath(directoryName),
                        filesToDelete[3]);
                    foreach (string file in fileList)
                    {
                        System.Diagnostics.Debug.WriteLine(file + "will be deleted");
                        System.IO.File.Delete(file);
                    }

                    movieEntities.MovieTables.Remove(movie);
                    movieEntities.SaveChanges();
                }
                catch (Exception)
                {

                    movieEntities.MovieTables.Remove(movie);
                    movieEntities.SaveChanges();
                }
            }
       
        }

        public void UpdateMovie(MovieTable movie)
        {

            using (QualitestProjDBEntities movEntities = new QualitestProjDBEntities())
            {
              
                var currentMovie = movEntities.MovieTables.FirstOrDefault(c => c.Id == movie.Id);


                currentMovie.MovieName = movie.MovieName;
                currentMovie.MovieBody = movie.MovieBody;
                currentMovie.Category = movie.Category;
                currentMovie.Country = movie.Country;
                currentMovie.Year = movie.Year;
                currentMovie.Quality = movie.Quality;
                currentMovie.Length = movie.Length;
                movEntities.SaveChanges();

            }
           
        }
    }
}