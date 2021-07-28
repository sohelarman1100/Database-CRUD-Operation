using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    public class Student:IData
    {
        public int ID { get; set; }
        public StudentInfo stdInfo { get; set; }
        public Student()
        {

        }
        public Student(int Id, StudentInfo info)
        {
            this.ID = Id;
            stdInfo = info;
        }
    }
}
