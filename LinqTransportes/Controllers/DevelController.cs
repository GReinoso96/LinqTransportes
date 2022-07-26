using LinqTransportes.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LinqTransportes.Controllers
{
    public class DevelController : Controller
    {
        DevelDAO DAO = new DevelDAO();

        // GET: Devel
        public ActionResult Index()
        {
            DAO.CreateDB();
            return View();
        }

        public ActionResult Limpiar()
        {
            DAO.ClearDB();
            return Redirect("Index");
        }
    }
}