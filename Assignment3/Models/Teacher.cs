using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Assignment3.Models
{
    public class Teacher
    {
        public int teacherId { get; set; }
        public string teacherFName { get; set; }
        public string teacherLName { get; set; }
        public decimal teacherSalary { get; set; }
        public string teacherNumber { get; set; }
        public DateTime teacherHire { get; set; }
       
       // public List<String> className { get; set; }
       public string className { get; set; }
        public int classId { get; set; }
    }

}