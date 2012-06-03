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
        private IDbServices _dbServices;

        public UiProfileController(IDbServices dbServices)
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
                Contacts contacts = new Contacts();
                contacts.Email = obj.Email;
                contacts.PhoneNumHome = obj.PhoneHomeNum;
                contacts.PhoneNumMobile = obj.PhoneMobile;
                contacts.PhoneNumWorkGTS = obj.PhoneGTS;
                contacts.PhoneNumWorkOS = obj.PhoneOS;
                
                UserProfile profile = new UserProfile();
                profile.LastName = obj.LastName;
                profile.FirstName = obj.FirstName;
                profile.ParentName = obj.ParentName;
                profile.DateOfBirth = obj.BirthDate;
                profile.Post = obj.Post;
                profile.DegreeID = obj.SelectedDegreeID;
                profile.LoginID = obj.LoginID;
                profile.Contacts = contacts;
                profile.UserProfileID = contacts.ContactID = Guid.NewGuid();

                byte[] imgBuffer = new byte[obj.ImgFile.ContentLength];
                using (Stream memStrm = obj.ImgFile.InputStream)
                {
                    memStrm.Read(imgBuffer, 0, imgBuffer.Length);
                }
                profile.Photo = imgBuffer;

                if (_dbServices.saveModeltoDB(typeof(UserProfile), profile))

                    return RedirectToAction("List", "Home");
                else
                    return RedirectToAction("Index", "Home");
            }

            return View(obj);
        }

        [HttpGet]
        public ActionResult UiResultUpdatable()
        {
            Guid newUserId = Guid.Empty;
            if(Session["NewUserLoginID"]!=null)
               newUserId = (Guid)Session["NewUserLoginID"];
            
            File()
            return View();
        }

    }
}
