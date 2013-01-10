using System;
using System.Collections.Generic;
using System.Data;

using System.Data.Objects;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using webf.Models.EntityModel;

namespace webf.Controllers
{ 
    public class RegistrationNewUserController : Controller
    {
        private FlexDBEntities db = new FlexDBEntities();
        private int stepsize = 5, currentPage = 1;

        private int framesAmount(int lenghOfdata, int _frameLenght)
        {
            int operationValue = 0;

            if (lenghOfdata < _frameLenght) lenghOfdata = _frameLenght;
            else
            {
                operationValue = ((lenghOfdata / _frameLenght) * _frameLenght);
                lenghOfdata = operationValue < lenghOfdata ?
                    (operationValue + _frameLenght) : lenghOfdata;
            }
            return lenghOfdata / _frameLenght;
        }

        //
        // GET: /RegistrationNewUser/
        
        public ViewResult Index()
        {
            
            ViewBag.UsersAmount = db.UserProfile.Count();
            ViewBag.CurrentPage = currentPage;
            ViewBag.RowsPerPage = stepsize;
            ViewBag.StepsAmount = framesAmount(ViewBag.UsersAmount, stepsize);
            ViewBag.FramesAmount = framesAmount(ViewBag.StepsAmount,10);

            
            using (ObjectResult<Users_page_Results> result = db.Users_page(currentPage, stepsize))
            {
                return View(result.ToList());    
            }
            
        }

        
        public ActionResult Nav(int Page)
        {
            using (ObjectResult<Users_page_Results> result = db.Users_page(Page, stepsize))
            {
                return PartialView("Partials/ListTable", result.ToList());
            }
        }

        //
        // GET: /RegistrationNewUser/Details/5

        public ViewResult Details(Guid id)
        {
            UserProfile userprofile = db.UserProfile.Single(u => u.UserProfileID == id);
            return View(userprofile);
        }

        //
        // GET: /RegistrationNewUser/Create

        public ActionResult Create()
        {
            ViewBag.LoginID = new SelectList(db.aspnet_Users, "UserId", "UserName");
            ViewBag.KeyID = new SelectList(db.KeyTable, "Id", "HostIdentity");
            ViewBag.DegreeID = new SelectList(db.MilitaryDegree, "DegreeID", "DegreeName");
            ViewBag.Post_ID = new SelectList(db.Posts, "Post_ID", "PostName");
            ViewBag.RegionID = new SelectList(db.Region, "RegionID", "RegionName");
            ViewBag.ObeysTo = new SelectList(db.UserProfile, "UserProfileID", "LastName");
            ViewBag.WorkArea_ID = new SelectList(db.WorkArea, "WorkArea_Id", "WA_Name");
            return View();
        } 

        //
        // POST: /RegistrationNewUser/Create

        [HttpPost]
        public ActionResult Create(UserProfile userprofile, HttpPostedFileBase ImgFile)
        {
            if (ModelState.IsValid)
            {
                byte[] imgBuffer = null;

                

                if (ImgFile != null)
                {
                    float imageWdth = float.Parse(Request["width"], CultureInfo.InvariantCulture);
                    float imageHeight = float.Parse(Request["height"], CultureInfo.InvariantCulture);
                    float imageX1 = float.Parse(Request["cor_x1"], CultureInfo.InvariantCulture);
                    float imageY1 = float.Parse(Request["cor_y1"], CultureInfo.InvariantCulture);
                    float imageX2 = float.Parse(Request["cor_x2"], CultureInfo.InvariantCulture);
                    float imageY2 = float.Parse(Request["cor_y2"], CultureInfo.InvariantCulture);
                    /*imgBuffer = new byte[ImgFile.ContentLength];*/
                    using (MemoryStream memStream = new MemoryStream())
                    {
                        using (Stream memStrm = ImgFile.InputStream)
                        {
                            Bitmap bmp = new Bitmap(memStrm);
                            Bitmap bmpCrop = bmp.Clone(new RectangleF(imageX1, imageY1, imageWdth, imageHeight),
                                                       bmp.PixelFormat);
                            bmpCrop.Save(memStream, ImageFormat.Jpeg);
                            /*memStrm.Read(imgBuffer, 0, imgBuffer.Length);*/
                        }
                        userprofile.Photo = memStream.GetBuffer();
                    }
                }
                
                userprofile.UserProfileID = Guid.NewGuid();
                db.UserProfile.AddObject(userprofile);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            

            ViewBag.LoginID = new SelectList(db.aspnet_Users, "UserId", "UserName", userprofile.LoginID);
            ViewBag.KeyID = new SelectList(db.KeyTable, "Id", "HostIdentity", userprofile.KeyID);
            ViewBag.DegreeID = new SelectList(db.MilitaryDegree, "DegreeID", "DegreeName", userprofile.DegreeID);
            ViewBag.Post_ID = new SelectList(db.Posts, "Post_ID", "PostName", userprofile.Post_ID);
            ViewBag.ObeysTo = new SelectList(db.UserProfile, "UserProfileID", "LastName", userprofile.ObeysTo);
            ViewBag.WorkArea_ID = new SelectList(db.WorkArea, "WorkArea_Id", "WA_Name", userprofile.WorkArea_ID);
            return View(userprofile);
        }
        
        //
        // GET: /RegistrationNewUser/Edit/5
 
        public ActionResult Edit(Guid id)
        {
            UserProfile userprofile = db.UserProfile.Single(u => u.UserProfileID == id);
            

            ViewBag.LoginID = new SelectList(db.aspnet_Users, "UserId", "UserName", userprofile.LoginID);
            ViewBag.KeyID = new SelectList(db.KeyTable, "Id", "HostIdentity", userprofile.KeyID);
            ViewBag.DegreeID = new SelectList(db.MilitaryDegree, "DegreeID", "DegreeName", userprofile.DegreeID);
            ViewBag.Post_ID = new SelectList(db.Posts, "Post_ID", "PostName", userprofile.Post_ID);
            ViewBag.ObeysTo = new SelectList(db.UserProfile.OrderBy(us=>us.LastName), "UserProfileID", "FullName", userprofile.ObeysTo);
            ViewBag.WorkArea_ID = new SelectList(db.WorkArea, "WorkArea_Id", "WA_Name", userprofile.WorkArea_ID);
            return View(userprofile);
        }

        //
        // POST: /RegistrationNewUser/Edit/5

        [HttpPost]
        public ActionResult Edit(UserProfile userprofile, HttpPostedFileBase ImgFile)
        {
            if (ModelState.IsValid)
            {

                if (ImgFile != null)
                {
                    float imageWdth = float.Parse(Request["width"], CultureInfo.InvariantCulture);
                    float imageHeight = float.Parse(Request["height"], CultureInfo.InvariantCulture);
                    float imageX1 = float.Parse(Request["cor_x1"], CultureInfo.InvariantCulture);
                    float imageY1 = float.Parse(Request["cor_y1"], CultureInfo.InvariantCulture);
                    float imageX2 = float.Parse(Request["cor_x2"], CultureInfo.InvariantCulture);
                    float imageY2 = float.Parse(Request["cor_y2"], CultureInfo.InvariantCulture);
                    /*imgBuffer = new byte[ImgFile.ContentLength];*/
                    using (MemoryStream memStream = new MemoryStream())
                    {
                        using (Stream memStrm = ImgFile.InputStream)
                        {
                            Bitmap bmp = new Bitmap(memStrm);
                            Bitmap bmpCrop = bmp.Clone(new RectangleF(imageX1, imageY1, imageWdth, imageHeight),
                                                       bmp.PixelFormat);
                            bmpCrop.Save(memStream, ImageFormat.Jpeg);
                            /*memStrm.Read(imgBuffer, 0, imgBuffer.Length);*/
                        }
                        userprofile.Photo = memStream.GetBuffer();
                    }
                }
                db.UserProfile.Attach(userprofile);
                db.ObjectStateManager.ChangeObjectState(userprofile, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LoginID = new SelectList(db.aspnet_Users, "UserId", "UserName", userprofile.LoginID);
            ViewBag.KeyID = new SelectList(db.KeyTable, "Id", "HostIdentity", userprofile.KeyID);
            ViewBag.DegreeID = new SelectList(db.MilitaryDegree, "DegreeID", "DegreeName", userprofile.DegreeID);
            ViewBag.Post_ID = new SelectList(db.Posts, "Post_ID", "PostName", userprofile.Post_ID);
            ViewBag.ObeysTo = new SelectList(db.UserProfile, "UserProfileID", "LastName", userprofile.ObeysTo);
            ViewBag.WorkArea_ID = new SelectList(db.WorkArea, "WorkArea_Id", "WA_Name", userprofile.WorkArea_ID);
            return View(userprofile);
        }

        //
        // GET: /RegistrationNewUser/Delete/5
 
        public ActionResult Delete(Guid id)
        {
            UserProfile userprofile = db.UserProfile.Single(u => u.UserProfileID == id);
            return View(userprofile);
        }

        //
        // POST: /RegistrationNewUser/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {            
            UserProfile userprofile = db.UserProfile.Single(u => u.UserProfileID == id);
            /*находим всех подчиненных*/
            List<UserProfile> slaves = db.UserProfile.Where(us => us.ObeysTo == id).ToList();
            
            foreach (UserProfile slave in slaves)
            {
                slave.ObeysTo = slave.UserProfileID;  /*подчиненные уже подчиняються сами себе*/
                db.ObjectStateManager.ChangeObjectState(slave, EntityState.Modified);
            }
            
            db.UserProfile.DeleteObject(userprofile);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult ShowPhoto(Guid id)
        {
            UserProfile profile = db.UserProfile.FirstOrDefault(user => user.UserProfileID.Equals(id));
            if (profile.Photo != null)
            {
                return File(profile.Photo, "img/jpeg");
            }

            return null;
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}