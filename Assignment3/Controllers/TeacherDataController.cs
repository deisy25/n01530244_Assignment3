using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Assignment3.Models;
using MySql.Data.MySqlClient;
using System.Dynamic;

namespace Assignment3.Controllers
{
    public class TeacherDataController : ApiController
    {
        // The database context class which allows us to access our MySQL Database.
        private SchoolDBContext school = new SchoolDBContext();

        //This Controller Will access the teacher table of our school database.
        /// <summary>
        /// Returns a list of Teacher in the system
        /// </summary>
        /// <example>GET api/TeacherData/ListTeacher</example>
        /// <returns>
        /// A list of teacher (first names and last names)
        /// </returns>
        [HttpGet]
        [Route("api/TeacherData/ListTeacher/{SearchKey}")]
        public IEnumerable<Teacher> ListTeacher(string SearchKey=null)
        {
            //Create an instance of a connection
            MySqlConnection Conn = school.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from teachers where lower(teacherfname) like lower(@key) or lower(teacherlname) like lower(@key) or lower(concat(teacherfname, ' ', teacherlname)) like lower(@key)";
            cmd.Parameters.AddWithValue("@key", "%" + SearchKey + "%");

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create an empty list of Teacher Names
            List<Teacher> teacherNames = new List<Teacher>();

            //Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index
                Teacher teacherList = new Teacher();
                teacherList.teacherId = (int)ResultSet["teacherid"];
                teacherList.teacherFName = ResultSet["teacherfname"].ToString();
                teacherList.teacherLName = ResultSet["teacherlname"].ToString();

                //Add the teacher Name to the List
                teacherNames.Add(teacherList);
            }

            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();

            //Return the final list of Teacher names
            return teacherNames;
        }

        /// <summary>
        /// find teacher in the system that given an id
        /// </summary>
        /// <param name="id">the teacher primary key</param>
        /// <returns>An teacher objects</returns>
        public Teacher FindTeacher(int id)
        {
            Teacher newTeacher = new Teacher();
            //Create an instance of a connection
            MySqlConnection Conn = school.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from teachers left join classes on teachers.teacherid=classes.teacherid where teachers.teacherid = " + id;

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            while (ResultSet.Read())
            {

                //Access Column information by the DB column name as an index
                int teacherId = (int)ResultSet["teacherId"];
                string teacherFName = ResultSet["teacherfname"].ToString();
                string teacherLName = ResultSet["teacherlname"].ToString();
                decimal teacherSalary = (decimal)ResultSet["salary"];
                string teacherNumber = ResultSet["employeenumber"].ToString();
                DateTime teacherHire = (DateTime)ResultSet["hiredate"];
                string className = ResultSet["classname"].ToString();
              
                newTeacher.teacherId = teacherId;
                newTeacher.teacherFName = teacherFName;
                newTeacher.teacherLName = teacherLName;
                newTeacher.teacherSalary = teacherSalary;
                newTeacher.teacherNumber = teacherNumber;
                newTeacher.teacherHire = teacherHire;
                newTeacher.className = className;
                newTeacher.classId = (int)ResultSet["classid"];
             
            }

            return newTeacher;
        }
    }
}