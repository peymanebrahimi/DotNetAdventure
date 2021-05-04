using System;
using System.Linq;

namespace Linq.ConsoleApp1
{
    // https://www.tutorialsteacher.com/linq/linq-joining-operator-groupjoin


    public class GroupJoinExample1
    {
        public void GroupJoinInMethodSyntax()
        {
            var groupJoin = Data.StandardList.GroupJoin(Data.StudentList,  //inner sequence
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
            var groupJoin = from std in Data.StandardList
                            join s in Data.StudentList
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