using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SI2_TP.Models;

namespace SI2_TP.Controllers
{
    public class HomeController : Controller
    {
        private readonly FuncionarioDao _dao = new FuncionarioDao();
        private readonly EmpresaDao _empresaDao = new EmpresaDao();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Funcionario(int num)
        {
            var func = _dao.GetById(num);
            if(func == null) return View("Error");
            return View(func);
        }

        public ActionResult Xslt()
        {
            var xslFilePath = Server.MapPath("~/Content/toTransform.xsl");
            return Content(_empresaDao.GetHtmlTablesFromEmpresas(xslFilePath));
        }

    }
}
