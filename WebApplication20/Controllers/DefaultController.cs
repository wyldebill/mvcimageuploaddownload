using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication20.Models;

namespace WebApplication20.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Default
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ComplexObject()
        {
            return View();
        }


        [Route("ComplexObject")]
        [HttpPost]
        public ActionResult ComplexObject(MyComplexObject model)
        {
            MyComplexObjectForPartial newModel = new MyComplexObjectForPartial();
            newModel.UserName = "Bobo";
            newModel.UserAge = 77;
            newModel.Married = true;
            return PartialView("_Partial", newModel);
        }

    }
}