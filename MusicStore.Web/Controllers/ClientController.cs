using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MusicStore.Web.App_Start;
using MusicStore.Data;
using MusicStore.Data.Service.Facade;
using MusicStore.Web.Models;
using System.Collections.Generic;

namespace MusicStore.Web.Controllers
{
    public class ClientController : Controller
    {
        private ClientFacade dbModel = new ClientFacade();

        // GET: Client
        [Filters.AutenticacionAdmin]
        public ActionResult Index()
        {
            var oDataClient = dbModel.ListClientorDetailFacade("0");
            List<Client> oClientDtoList = new List<Client>();
            foreach (var itemClient in oDataClient)
            {
                Client oClientDto = new Client();
                oClientDto.Id = itemClient.Id;
                oClientDto.Name = itemClient.Name;
                oClientDto.Direction = itemClient.Direction;
                oClientDto.Mail = itemClient.Mail;
                oClientDto.Phone = itemClient.Phone;
                oClientDto.Photo = itemClient.Photo;
                oClientDtoList.Add(oClientDto);
            }
            return View(oClientDtoList);
        }

        // GET: Client/Details/5
        [Filters.AutenticacionAdmin]
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var Client = dbModel.ListClientorDetailFacade(id);
            if (Client.Count() == 0)
            {
                return HttpNotFound();
            }

            ViewBag.ClientName = singleton.GetCurrentSingleton().ClientName;
            return View(Client);
        }

        // GET: Client/Create
        [Filters.AutenticacionAdmin]
        public ActionResult Create()
        {
            return View();
        }

        [Filters.AutenticacionAdmin]
        public JsonResult Upload(string Id, string edit)
        {

            var activeRegistro = dbModel.ListClientorDetailFacade(Id).FirstOrDefault();
            if (activeRegistro != null)
            {
                //Poner por defecto no foto 
                if (string.IsNullOrEmpty(edit))
                {
                    var foto = "default_avatar_male.jpg";
                    //Actualizo el registro con la foto cargada
                    dbModel.InsertorUpdateClientFacade(activeRegistro.Id, activeRegistro.Name, activeRegistro.Mail, activeRegistro.Direction, activeRegistro.Phone, foto);

                }
                if (Request.Files.Count > 0)
                {
                    for (int i = 0; i < Request.Files.Count; i++)
                    {
                        HttpPostedFileBase file = Request.Files[i]; //Uploaded file
                                                                    //Use the following properties to get file's name, size and MIMEType
                        if (file != null)
                        {

                            string key = Request.Files.GetKey(i);

                            switch (key)
                            {
                                case "avatar":
                                    var fileInfo = file.FileName.Split('.');
                                    var foto = Id + "." + fileInfo[1];
                                    //Actualizo el registro con la foto cargada
                                    dbModel.InsertorUpdateClientFacade(activeRegistro.Id, activeRegistro.Name, activeRegistro.Mail, activeRegistro.Direction, activeRegistro.Phone, foto);
                                    //To save file, use SaveAs method
                                    file.SaveAs(Server.MapPath("~/Content/Uploads/") + foto);
                                    //File will be saved in application root
                                    break;
                            }
                        }
                    }
                }
            }
            return Json("ok", JsonRequestBehavior.AllowGet);
        }

        [HttpPost, ValidateHeaderAntiForgeryToken]
        [Filters.AutenticacionAdmin]
        public JsonResult RegistrarClient(
                    string Id, string Name, string Mail, string Direction, string Phone, string Photo)
        {
            string result;

            var oRegistro = dbModel.ListClientorDetailFacade(Id);
            if (oRegistro.Count() == 0)
            {
                //Guardar registro en tabla SongSet
                dbModel.InsertorUpdateClientFacade(Id, Name, Mail, Direction, Phone, Photo);
                result = "save";
            }
            else
            {
                //Cedula registrada
                result = "exist";
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost, ValidateHeaderAntiForgeryToken]
        [Filters.AutenticacionAdmin]
        public JsonResult EditarClient(
                   string Id, string Name, string Mail, string Direction, string Phone)
        {
            string result;

            //Update registro 
            dbModel.InsertorUpdateClientFacade(Id, Name, Mail, Direction, Phone, string.Empty);
            result = "save";

            return Json(result, JsonRequestBehavior.AllowGet);
        }


        // GET: Client/Edit/5
        [Filters.AutenticacionAdmin]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var Client = dbModel.ListClientorDetailFacade(id);
            if (Client == null)
            {
                return HttpNotFound();
            }
            ViewBag.AlbumSet = singleton.GetCurrentSingleton().DescAlbumSet;
            return View(Client);
        }


        // GET: Client/Delete/5
        [Filters.AutenticacionAdmin]
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var Client = dbModel.ListClientorDetailFacade(id);
            if (Client == null)
            {
                return HttpNotFound();
            }
            return View(Client);
        }
    }
}
