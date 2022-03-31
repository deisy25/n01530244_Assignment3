using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Assignment3.Models
{
    public class Student
    {
        public int studentID { get; set; }
        public string studentName { get; set; }
        public string studentNumber { get; set; }
        public DateTime enroldate { get; set; }
 
        //public List<Classes> ClassName { get; set;}
        public string className { get; set; }
    }
}