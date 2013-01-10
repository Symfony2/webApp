
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web.Mvc;
using Bundles;
using OpenPGPzzz;
using webf.Models.EMsgModel;

namespace webf.Controllers
{
    public class MessageSenderController : Controller
    {
        //
        // GET: /MessageSender/

        public ActionResult EMessageTemplateSendAct()
        {
            var model = new EMsgSender();
            return View(model);
        }

        [HttpPost]
        public ActionResult EMessageTemplateSendAct(EMsgSender incomingFileModel)
        {
            if (!ModelState.IsValid)
            {
                return View(incomingFileModel);
            }
            var model = new EMsgSender();
            

            return View(model);
        }

    }
}
