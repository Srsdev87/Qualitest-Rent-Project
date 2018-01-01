using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QualitestProject.Models;

namespace QualitestProject.Services.MovieManagement
{
    public class UsersManegement
    {
        public void AddNewUser(FormCollection fc, HttpPostedFileBase file1)
        {

            QualitestProjDBEntities obj = new QualitestProjDBEntities();
            UsersTable tbl = new UsersTable();
            var allowedExtensions = new[] {
            ".Jpg", ".png", ".jpg", "jpeg"
        };
            try
            {
                tbl.Isadmin = fc["Id"];
                tbl.Picture = file1.ToString(); //getting complete url  
                tbl.Username = fc["UserName"];
                tbl.Name = fc["UName"];
                tbl.LastName = fc["LName"];
                tbl.Email = fc["Mail"];
                tbl.Password = fc["Pass"];


                var fileName = Path.GetFileName(file1.FileName); //getting only file name(ex-ganesh.jpg)  
                var ext = Path.GetExtension(file1.FileName); //getting the extension(ex-.jpg)  
                if (allowedExtensions.Contains(ext)) //check what type of extension  
                {
                    string name = Path.GetFileNameWithoutExtension(fileName); //getting file name without extension  
                    name = tbl.Username;
                    string myfile = name + "_" + tbl.Isadmin + ext; //appending the name with id  
                                                                    // store the file inside ~/project folder(Img)  

                    var path = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/images/user/"), myfile);
                    tbl.Picture = "/images/user/" + myfile;
                    obj.UsersTables.AddOrUpdate(tbl);
                    obj.SaveChanges();
                    file1.SaveAs(path);

                }
            }
            catch (Exception)
            {
                tbl.Isadmin = fc["Id"];
               
                tbl.Username = fc["UserName"];
                tbl.Name = fc["UName"];
                tbl.LastName = fc["LName"];
                tbl.Email = fc["Mail"];
                tbl.Password = fc["Pass"];
                tbl.Picture = "default.png";
                obj.UsersTables.AddOrUpdate(tbl);
                obj.SaveChanges();
            }
           
        }

        public void UpdateUser(UsersTable user)
        {
            using (QualitestProjDBEntities userEntities = new QualitestProjDBEntities())
            {
                var currentUser = userEntities.UsersTables.FirstOrDefault(c => c.Id == user.Id);


                currentUser.Username = user.Username;
                currentUser.Name = user.Name;
                currentUser.LastName = user.LastName;
                currentUser.Email = user.Email;
                currentUser.Password = user.Password;
                currentUser.Isadmin = user.Isadmin;

                userEntities.SaveChanges();

            }
       
        }

        public void DeleteUser(FormCollection fc)
        {

            using (QualitestProjDBEntities userEntities = new QualitestProjDBEntities())
            {
                UsersTable tbl = new UsersTable();
                tbl.Username = fc["UName"].ToString();
                UsersTable user = userEntities.UsersTables.FirstOrDefault(c => c.Username == tbl.Username);

                try
                {
                    string rootFolderPath = user.Picture;

                    string[] filesToDelete = rootFolderPath.Split('/');


                    string directoryName = Path.GetDirectoryName("~" + rootFolderPath);

                    string[] fileList = Directory.GetFiles(System.Web.HttpContext.Current.Server.MapPath(directoryName),
                        filesToDelete[3]);
                    foreach (string file in fileList)
                    {
                        System.Diagnostics.Debug.WriteLine(file + "will be deleted");
                        System.IO.File.Delete(file);
                    }

                    userEntities.UsersTables.Remove(user);
                    userEntities.SaveChanges();
                }
                catch (Exception)
                {
                    userEntities.UsersTables.Remove(user);
                    userEntities.SaveChanges();
                }
            }
        
        }
    }
}