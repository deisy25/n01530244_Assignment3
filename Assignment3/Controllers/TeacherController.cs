using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Assignment3.Models;
using System.Diagnostics;
using System.Dynamic;


namespace Assignment3.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher
        public ActionResult Index()
        {

            return View();
        }


        //get: /Teacher/List
        public ActionResult List(string SearchKey=null)
        {
            TeacherDataController controller=new TeacherDataController();
            IEnumerable<Teacher> Teachers = controller.ListTeacher(SearchKey);
            return View(Teachers);
        }

        //get: /Teacher/Show/{id}
        public ActionResult Show(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher newTeacher = controller.FindTeacher(id);
            return View(newTeacher);
        }
    }
}