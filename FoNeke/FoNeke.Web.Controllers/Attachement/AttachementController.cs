using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using FoNeke.Web.Repositories.Interfaces;

namespace FoNeke.Web.Controllers.Attachement
{
    public class AttachementController : Controller
    {
        public IAttachementRepository AttachementRepository { get; set; }

        [HttpPost]
        public async Task<ActionResult> Upload()
        {
            for (var i = 0; i < Request.Files.Count; i++)
            {
                var file = Request.Files[i];
                if (file == null || file.ContentLength <= 0) continue;

                await AttachementRepository.SaveOrUpdate(new EMM.FoNeke.Common.Entities.Attachement
                {
                    Name = Path.GetFileNameWithoutExtension(file.FileName),
                    Stream = file.InputStream,
                    ContentType = file.ContentType,
                    DocumentId = Request.UrlReferrer.Segments[3],
                    Extention = Path.GetExtension(file.FileName)
                });
            }
            return Json("good");
        }
    }
}
