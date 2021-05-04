using System.Collections.Generic;

namespace Linq.ConsoleApp1
{
    public class Data
    {
        public static readonly IList<Student> StudentList = new List<Student>() {
            new Student() { StudentID = 1, StudentName = "John", StandardID =1 },
            new Student() { StudentID = 2, StudentName = "Moin", StandardID =1 },
            new Student() { StudentID = 3, StudentName = "Bill", StandardID =2 },
            new Student() { StudentID = 4, StudentName = "Ram",  StandardID =2 },
            new Student() { StudentID = 5, StudentName = "Ron" }
        };

        public static readonly IList<Standard> StandardList = new List<Standard>() {
            new Standard(){ StandardID = 1, StandardName="Standard 1"},
            new Standard(){ StandardID = 2, StandardName="Standard 2"},
            new Standard(){ StandardID = 3, StandardName="Standard 3"}
        };
    }
}
