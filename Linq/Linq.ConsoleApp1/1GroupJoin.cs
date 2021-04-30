using System;
using System.Collections.Generic;
using System.Linq;

namespace Linq.ConsoleApp1
{
    // https://www.tutorialsteacher.com/linq/linq-joining-operator-groupjoin
    public class Student
    {
        public int StudentID { get; set; }
        public string StudentName { get; set; }
        public int StandardID { get; set; }
    }

    public class Standard
    {
        public int StandardID { get; set; }
        public string StandardName { get; set; }
    }

    public class GroupJoinExample1
    {
        private readonly IList<Student> _studentList = new List<Student>() {
            new Student() { StudentID = 1, StudentName = "John", StandardID =1 },
            new Student() { StudentID = 2, StudentName = "Moin", StandardID =1 },
            new Student() { StudentID = 3, StudentName = "Bill", StandardID =2 },
            new Student() { StudentID = 4, StudentName = "Ram",  StandardID =2 },
            new Student() { StudentID = 5, StudentName = "Ron" }
        };

        private readonly IList<Standard> _standardList = new List<Standard>() {
            new Standard(){ StandardID = 1, StandardName="Standard 1"},
            new Standard(){ StandardID = 2, StandardName="Standard 2"},
            new Standard(){ StandardID = 3, StandardName="Standard 3"}
        };

        public void GroupJoinInMethodSyntax()
        {
            var groupJoin = _standardList.GroupJoin(_studentList,  //inner sequence
                std => std.StandardID, //outerKeySelector 
                s => s.StandardID,     //innerKeySelector
                (std, studentsGroup) => new // resultSelector 
                {
                    Students = studentsGroup,
                    StandarFulldName = std.StandardName
                });

            foreach (var item in groupJoin)
            {
                Console.WriteLine(item.StandarFulldName);

                foreach (var stud in item.Students)
                    Console.WriteLine(stud.StudentName);
            }
        }
        /*
           Standard 1:
           John,
           Moin,
           Standard 2:
           Bill,
           Ram,
           Standard 3:
         */

        public void GroupJoinInQuerySyntax()
        {
            var groupJoin = from std in _standardList
                            join s in _studentList
                                on std.StandardID equals s.StandardID
                                into studentGroup
                            select new
                            {
                                Students = studentGroup,
                                StandardName = std.StandardName
                            };

            foreach (var item in groupJoin)
            {
                Console.WriteLine(item.StandardName);

                foreach (var stud in item.Students)
                    Console.WriteLine(stud.StudentName);
            }
        }
        /*
           Standard 1:
           John,
           Moin,
           Standard 2:
           Bill,
           Ram,
           Standard 3:
         */
    }
}