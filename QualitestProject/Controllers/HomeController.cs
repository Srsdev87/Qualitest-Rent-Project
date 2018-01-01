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
using QualitestProject.Services.MovieManagement;

namespace QualitestProject.Controllers
{
    public class HomeController : Controller
    {
        private static readonly MovieRentManegement MovieRentManegement = new MovieRentManegement();
        private static readonly UsersManegement UserManagement = new UsersManegement();
      
        public ActionResult Index()
        {
            using (QualitestProjDBEntities moviEntities = new QualitestProjDBEntities())
            {
                List<object> myModel = new List<object>();
                myModel.Add(moviEntities.MovieTables.ToList());
                myModel.Add(moviEntities.UsersTables.ToList());
                myModel.Add(moviEntities.RentedMoviesTables.ToList());
                return View(myModel);
            }

        }

        public ActionResult Management()
        {
            using (QualitestProjDBEntities moviEntities = new QualitestProjDBEntities())
            {
                List<object> myModel = new List<object>();
                myModel.Add(moviEntities.MovieTables.ToList());
                myModel.Add(moviEntities.UsersTables.ToList());
                return View(myModel);
            }
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Sergey Svanidze.";

            return View();
        }

        public ActionResult logIn()
        {
            return View();
        }

        public ActionResult Registration()
        {
            ViewBag.Message = "Your Register page.";
            return View();
        }

        [HttpGet]
        public string DecryptToken(string token)
        {
            string tokentodecrypt = token.Replace(" ", "+");
            return Encryptor.Decrypt(tokentodecrypt);
        }//decrypting the token to a username (log in validation)

        [HttpGet]
        public string IsAdminCheck(string username)
        {
            using (QualitestProjDBEntities userEntities = new QualitestProjDBEntities())
            {
                var userdetails =
                    userEntities.UsersTables.FirstOrDefault(usr => usr.Username == username);
                string stirngforsplit = userdetails.Picture;
                string[] splitted = stirngforsplit.Split('/');
                string[] getridoffileextension = splitted[3].Split('.');
                return getridoffileextension[0];
            }
        }//checking if the logged in user is admin

        [HttpGet]
        public string submitlogIn(string username,string password)
        {
            ViewBag.Message = "Your Login page.";
            if (username != null && password != null)
            {
            
                if (UserLoginSecurity.LogIn(username, password))
                {
                    DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                    long ms = (long)(DateTime.UtcNow - epoch).TotalMilliseconds;
                    string tokenString = username + ":" + ms;
                   
                    return Encryptor.Encrypt(tokenString);
                }
                else
                {
                    return "error";
                }
            }
            else
            {
                return null;
            }
        }//submitting log in form, encrypt the username + datetime to a Token

        [HttpPost]
        public ActionResult AddNewMovie(FormCollection fc, HttpPostedFileBase file)
        {
            MovieRentManegement.AddNewMovie(fc,file);
            return RedirectToAction("Management");
        }//add a movie

        public ActionResult DeleteMovie(FormCollection fc)
        {
            MovieRentManegement.DeleteMovie(fc);
           
            return RedirectToAction("Management");
        }//delete a movie

        [HttpPost]
        public ActionResult UpdateMovie(MovieTable movie)
        {
          MovieRentManegement.UpdateMovie(movie);
            ViewBag.Message = "Movie Added successfully";

            return RedirectToAction("Management");
        }//update movie details

 
        [HttpPost]
        public ActionResult AddNewUser(FormCollection fc, HttpPostedFileBase file1)
        {
            UserManagement.AddNewUser(fc, file1);
            return RedirectToAction("Index");
        }//add new user

        [HttpPost]
        public ActionResult UpdateUser(UsersTable user)
        {
            UserManagement.UpdateUser(user);
            ViewBag.Message = "Movie Added successfully";

            return RedirectToAction("Management");
        }//update user details

        public ActionResult DeleteUser(FormCollection fc)
        {

            UserManagement.DeleteUser(fc);
            return RedirectToAction("Management");
        }//delete user

        [HttpGet]
        public string ShowMovieDetails(string moviename)
        {
            using (QualitestProjDBEntities movEntities = new QualitestProjDBEntities())
            {
                var moviedetails =
                    movEntities.MovieTables.FirstOrDefault(mv => mv.MovieName == moviename);
                string stirngforsplit = moviedetails.MoviePicture + ":" + moviedetails.MovieName + ":" + moviedetails.MovieBody + ":" +
                           moviedetails.Year + ":" + moviedetails.Country + ":" + moviedetails.Category + ":" +
                           moviedetails.Quality + ":" + moviedetails.Length+":"+ moviedetails.Id;
                return stirngforsplit;
            }
        }

        [HttpGet]
        public string ShowUserDetails(string username)
        {
            using (QualitestProjDBEntities userEntities = new QualitestProjDBEntities())
            {
                var userdetails =
                    userEntities.UsersTables.FirstOrDefault(usr => usr.Username == username);
                string stirngforsplit = userdetails.Picture + ":" + userdetails.Name + ":" + userdetails.LastName + ":" +
                                        userdetails.Username + ":" + userdetails.Email + ":" + userdetails.Isadmin+":"+userdetails.Id;
                return stirngforsplit;
            }
        }


        [HttpGet]
        public string ShowMoviePic(string moviename)
        {
            using (QualitestProjDBEntities movEntities = new QualitestProjDBEntities())
            {
                var moviepic =
                    movEntities.Database.SqlQuery<string>("SELECT moviepicture from movietable where moviename like '" +
                                                     moviename + "'").ToList();

                List<string> moviepicture = new List<string>();

                moviepicture.Add(moviepic[0]);
                return moviepicture[0];
            }
        }

        [HttpGet]
        public string ShowUserPic(string username)
        {
            using (QualitestProjDBEntities userEntities = new QualitestProjDBEntities())
            {
                var userpic =
                    userEntities.Database.SqlQuery<string>("SELECT picture from userstable where username like '" +
                                                     username + "'").ToList();

                List<string> userpicture = new List<string>();

                userpicture.Add(userpic[0]);
                return userpicture[0];
            }
        }

        [HttpPost]
        public void SetRentOrder(string username, string moviename)
        {
            QualitestProjDBEntities obj = new QualitestProjDBEntities();
            RentedMoviesTable tbl = new RentedMoviesTable();
            tbl.MovieName = moviename;
            tbl.UserName = username;
            obj.RentedMoviesTables.AddOrUpdate(tbl);
            obj.SaveChanges();
        }

        [HttpGet]
        public string GetUserOrders(string username)
        {
            using (QualitestProjDBEntities entities = new QualitestProjDBEntities())
            {
                var movname =
                    entities.Database.SqlQuery<string>("SELECT MovieName from RentedMoviesTable where UserName like '" +
                                               username + "'").ToList();
                List<string> listofmovies = new List<string>();
                foreach (var mov in movname)
                {
                    listofmovies.Add(mov);
                }
                string stringOfMovies = string.Join(",", listofmovies.ToArray());
             
                return stringOfMovies;
            }

        }




    }
}