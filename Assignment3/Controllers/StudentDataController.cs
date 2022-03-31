using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Assignment3.Models;
using MySql.Data.MySqlClient;

namespace Assignment3.Controllers
{
    public class StudentDataController : ApiController
    {
        // The database context class which allows us to access our MySQL Database.
        private SchoolDBContext school = new SchoolDBContext();

        //This Controller Will access the students table of our school database.
        /// <summary>
        /// Returns a list of students in the system
        /// </summary>
        /// <example>GET api/StudentData/ListStudent</example>
        /// <returns>
        /// A list of Students (first names and last names)
        /// </returns>
        [HttpGet]
        public IEnumerable<Student> ListStudent()
        {
            //Create an instance of a connection
            MySqlConnection Conn = school.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from students";

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create an empty list of student Names
            List<Student> studentNames = new List<Student>();

            //Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index
                Student studentList = new Student();
                studentList.studentID = Convert.ToInt32(ResultSet["studentid"]);
                studentList.studentName = ResultSet["studentfName"].ToString() + " " + ResultSet["studentlName"].ToString();

                //Add the student Name to the List
                studentNames.Add(studentList);
            }

            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();

            //Return the final list of student names
            return studentNames;
        }

        public Student FindStudent(int id)
        {
            Student newStudent = new Student();
            //Create an instance of a connection
            MySqlConnection Conn = school.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from students left join studentsxclasses on studentsxclasses.studentid=students.studentid join classes on classes.classid=studentsxclasses.classid where studentsxclasses.studentid = " + id;

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            while (ResultSet.Read())
            {
                int studentID = Convert.ToInt32(ResultSet["studentid"]);
                string studentfname = ResultSet["studentfname"].ToString();
                string studentlname = ResultSet["studentlname"].ToString();
                string studentNumber = ResultSet["studentnumber"].ToString();
                DateTime enrolDate = (DateTime)ResultSet["enroldate"];
                string className = ResultSet["classname"].ToString();
                

                newStudent.studentID = studentID;
                newStudent.studentName = studentfname + " " + studentlname;
                newStudent.studentNumber = studentNumber;
                newStudent.enroldate = enrolDate;
                newStudent.className = className;   
               

            }

            return newStudent;
        }
    }
}
