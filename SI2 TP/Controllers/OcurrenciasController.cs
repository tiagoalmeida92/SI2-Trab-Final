using System.Web.Mvc;
using SI2_TP.Models;

namespace SI2_TP.Controllers
{
    public class OcorrenciasController : Controller
    {
        //
        // GET: /Ocurrencias/
        private readonly OcorrenciaDao _dao = new OcorrenciaDao();

        public ActionResult Index(int idFuncio)
        {

            return View(_dao.GetAll(idFuncio));
        }

        public ActionResult Details(int idOcorrencia)
        {
            var ocorrencia = _dao.GetById(idOcorrencia);
            return View(ocorrencia);
        }

    }
}
