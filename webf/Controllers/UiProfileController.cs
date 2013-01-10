using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webf.Models.EntityModel;
using webf.SvcDependencies;

namespace webf.Controllers
{
    public class UiProfileController : Controller
    {
        //
        // GET: /UiProfile/
        private IDbServices<FlexDBEntities> _dbServices;

        public UiProfileController(IDbServices<FlexDBEntities> dbServices)
        {
            _dbServices = dbServices;
        }
        [HttpGet]
        public ActionResult UserRegistrationBlank()
        {
            Guid newUserId = Guid.Empty;
            if(Session["NewUserLoginID"]!=null)
               newUserId = (Guid)Session["NewUserLoginID"];
            
            RegForm _blank = new RegForm();
            _blank.LoginID = newUserId;
            return View(_blank);
        }

        [HttpPost]
        public ActionResult UserRegistrationBlank(RegForm obj)
        {
            if (ModelState.IsValid)
            {
                
                
                UserProfile profile = new UserProfile();
                profile.LastName = obj.LastName;
                profile.FirstName = obj.FirstName;
                profile.ParentName = obj.ParentName;
                profile.DateOfBirth = obj.BirthDate;
                
                profile.DegreeID = obj.SelectedDegreeID;
                profile.LoginID = obj.LoginID;
                

                byte[] imgBuffer = null;
                if (obj.ImgFile != null)
                {
                    imgBuffer = new byte[obj.ImgFile.ContentLength];
                    using (Stream memStrm = obj.ImgFile.InputStream)
                    {
                        memStrm.Read(imgBuffer, 0, imgBuffer.Length);
                    }
                }
                
                profile.Photo = imgBuffer;

                if (_dbServices.saveModeltoDB(typeof(UserProfile), profile))

                    return RedirectToAction("UiResultUpdatable", "UiProfile", new { id = profile.UserProfileID });
                else
                    return RedirectToAction("Index", "Home");
            }

            return View(obj);
        }

        [HttpGet]
        public ActionResult UiResultUpdatable()//Guid id)
        {

            Guid newUserId = Guid.Parse("13474517-c22f-4829-8a99-8df51864c313");
            //Guid newUserId = id;//13474517-c22f-4829-8a99-8df51864c313 
            
            UserProfile query = (from c in _dbServices.DataBase.UserProfile
                             .Include("MilitaryDegree").Include("Contacts")
                         where c.UserProfileID.Equals(newUserId)
                         select c).FirstOrDefault();
            
            RegForm regForm = new RegForm();
            
            regForm.CurrentAccounID = newUserId;
            ViewBag.Image = File(query.Photo, "image/jpg");
            
            regForm.createBinding(query);
            return View(regForm);
        }

        [HttpGet]
        public ActionResult ShowPhoto(Guid id)
        {
            UserProfile profile = _dbServices.DataBase
                .UserProfile.FirstOrDefault(user => user.UserProfileID.Equals(id));
            if(profile!=null)
            {
                return File(profile.Photo, "img/jpeg");
            }
            
            return null;
        }



        

    }
}
