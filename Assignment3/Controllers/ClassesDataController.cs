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
    public class ClassesDataController : ApiController
    {
        // The database context class which allows us to access our MySQL Database.
        private SchoolDBContext school = new SchoolDBContext();

        //This Controller Will access the students table of our school database.
        /// <summary>
        /// Returns a list of Class in the system
        /// </summary>
        /// <example>GET api/ClassesData/ListClass</example>
        /// <returns>
        /// A list of Class
        /// </returns>
        [HttpGet]
        public IEnumerable<Classes> ListClass()
        {
            //Create an instance of a connection
            MySqlConnection Conn = school.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from classes";

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create an empty list of student Names
            List<Classes> ClassList = new List<Classes>();

            //Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index
                Classes ListedClass = new Classes();
                ListedClass.classID = Convert.ToInt32(ResultSet["classid"]);
                ListedClass.classCode = ResultSet["classcode"].ToString();
                ListedClass.className = ResultSet["classname"].ToString();
                //Add the student Name to the List
                ClassList.Add(ListedClass);
            }

            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();

            //Return the final list of Class
            return ClassList;
        }

        /// <summary>
        /// Find a classes from the sql database through id
        /// </summary>
        /// <param name="id">the id for the class as primary key</param>
        /// <returns>the detail of class according to id</returns>
        [HttpGet]
        public Classes FindClass(int id)
        {
            Classes newClass = new Classes();
            //Create an instance of a connection
            MySqlConnection Conn = school.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from classes left join teachers on teachers.teacherid=classes.teacherid where classes.classid = " + id;

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index
                int classesID = Convert.ToInt32(ResultSet["classid"]);
                string classCode=ResultSet["classcode"].ToString();
                string className = ResultSet["classname"].ToString();
                string teacherfname = ResultSet["teacherfname"].ToString();
                string teacherlname=ResultSet["teacherlname"].ToString() ;
                DateTime startClass = (DateTime)ResultSet["startdate"];
                DateTime endClass = (DateTime)ResultSet["finishdate"];


                newClass.classID = classesID;
                newClass.classCode = classCode; 
                newClass.className = className; 
                newClass.teachername= teacherfname + " " + teacherlname;
                newClass.startClass = startClass;   
                newClass.endClass = endClass;
                newClass.teacherID= Convert.ToInt32(ResultSet["teacherid"]);
            }

            return newClass;
        }
    }
}
