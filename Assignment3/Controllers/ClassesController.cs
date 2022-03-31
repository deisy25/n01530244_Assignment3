using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Assignment3.Models;

namespace Assignment3.Controllers
{
    public class ClassesController : Controller
    {
        // GET: Classes
        public ActionResult Index()
        {
            return View();
        }

        //GET: /Classes/List
        public ActionResult List()
        {
            ClassesDataController controller=new ClassesDataController();
            IEnumerable<Classes> classes = controller.ListClass();
            return View(classes); 
        }


        //get: /Classes/Show/{id}
        public ActionResult Show(int id)
        {
            ClassesDataController controller= new ClassesDataController();
            Classes newclass = controller.FindClass(id);
            return View(newclass);
        }
    }
}