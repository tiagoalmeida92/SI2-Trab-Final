using System;
using System.Web.Mvc;
using SI2_TP.Models;

namespace SI2_TP.Controllers
{
    public class OcorrenciasController : Controller
    {
      
        private readonly OcorrenciaDao _dao = new OcorrenciaDao();
        private readonly FuncionarioDao _funcDao = new FuncionarioDao();

          //
        // GET: /Ocurrencias/

        public ActionResult Index(int? idFuncio)
        {
           
            if(!idFuncio.HasValue) 
                return RedirectToAction("Index", "Home");
            var func = _funcDao.GetById(idFuncio.Value);
            ViewBag.Admin = func.Admin;
            if(func.Admin)
            {
                return View(_dao.GetAll());
            }
            return View(_dao.GetAll(idFuncio.Value));
        }

        public ActionResult Details(int id)
        {
            return View(new Ocorrencia
                            {
                                id = id,
                                Trabalhos = _dao.GetTrabalhos(id)
                            });
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Ocorrencia ocorrencia)
        {
            ocorrencia.dataHoraAct = DateTime.Now;
            ocorrencia.dataHoraEnt = DateTime.Now;
            ocorrencia.estado = Estado.Inicial;
            _dao.Insert(ocorrencia);
            return RedirectToAction("Index","Home");
        }

        public ActionResult Edit(int id)
        {
            var ocorrencia = _dao.GetById(id);
            return View(ocorrencia);
        }

        [HttpPost]
        public ActionResult Edit(Ocorrencia ocorrencia)
        {
            //_dao.Update(ocorrencia);
            return RedirectToAction("Index","Home");
        }

        public ActionResult NonConcluded(DateTime? date)
        {
            if(!date.HasValue) return View();
            var ocorrencias = _dao.GetAllNaoConcluidas(date.Value);
            return View("Index", ocorrencias);
        }

        public ActionResult AddArea(int idOcorr)
        {
            return View(idOcorr);

        }

        [HttpPost]
        public ActionResult AddArea(int idOcorr, int areaIntervencao, string desc)
        {
            _dao.InsertTrabalhoOnAreaInterv(areaIntervencao, idOcorr, desc);
            return RedirectToAction("AddArea", new {idOcorr});
        }

        public ActionResult ShowAvailableWorkers(int idOcorr, int areaInterv)
        {
            
            return View(_funcDao.GetAvailableWorkers(areaInterv));

        }

        [HttpPost]
        public ActionResult AddWorker(int idOcorr, int areaInterv, int idFunc)
        {
            _dao.InsertAfecto(idOcorr, areaInterv, idFunc);
            return RedirectToAction("ShowAvailableWorkers");
        }
    }
}
