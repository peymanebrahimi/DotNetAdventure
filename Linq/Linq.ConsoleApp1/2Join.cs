using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq.ConsoleApp1
{
    public class JoinExample1
    {
        public void JoinMethodSyntax()
        {
            var innerJoin = Data.StudentList.Join(// outer sequence 
                Data.StandardList,  // inner sequence 
                student => student.StandardID,    // outerKeySelector
                standard => standard.StandardID,  // innerKeySelector
                (student, standard) => new  // result selector
                {
                    StudentName = student.StudentName,
                    StandardName = standard.StandardName
                });
        }
        /*
         * John - Standard 1
           Moin - Standard 1
           Bill - Standard 2
           Ram - Standard 2
         */


        public void JoinQuerySyntax()
        {
            var innerJoin = from s in Data.StudentList // outer sequence
                            join st in Data.StandardList //inner sequence 
                    on s.StandardID equals st.StandardID // key selector 
                            select new
                            { // result selector 
                                StudentName = s.StudentName,
                                StandardName = st.StandardName
                            };
        }
        /*
         * John - Standard 1
           Moin - Standard 1
           Bill - Standard 2
           Ram - Standard 2
         */
    }
}
