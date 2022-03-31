using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Assignment3.Models
{
    public class Classes
    {
        public int classID { get; set; }
        public string teachername { get; set; }
        public string classCode { get; set; }   
        public string className { get; set; }
        public DateTime startClass { get; set; }
        public DateTime endClass { get; set; }
        public int teacherID { get; set; }
    }
}