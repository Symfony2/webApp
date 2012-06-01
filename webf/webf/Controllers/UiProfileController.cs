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

        public ActionResult UserRegistrationBlank()
        {
            RegForm _blank = new RegForm();
            ViewData.Model = _blank;
            return View();
        }

        [HttpPost]
        public ActionResult UserRegistrationBlank(RegForm obj)
        {
            if (ModelState.IsValid)
            {
                UserProfile profile = new UserProfile();
                byte[] imgBuffer = new byte[obj.ImgFile.ContentLength];
                using (Stream memStrm = obj.ImgFile.InputStream)
                {
                    memStrm.Read(imgBuffer, 0, imgBuffer.Length);
                }
                _dbServices.saveModeltoDB<UserProfile>();
            }

            return View();
        }

    }
}
