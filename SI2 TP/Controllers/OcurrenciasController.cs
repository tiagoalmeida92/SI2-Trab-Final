using System.Web.Mvc;
using SI2_TP.Models;

namespace SI2_TP.Controllers
{
    public class OcorrenciasController : Controller
    {
        //
        // GET: /Ocurrencias/
        private readonly OcorrenciasDao _dao = new OcorrenciasDao();

        public ActionResult Index(int idFuncio)
        {

            return View(_dao.GetAll(idFuncio));
        }

    }
}
