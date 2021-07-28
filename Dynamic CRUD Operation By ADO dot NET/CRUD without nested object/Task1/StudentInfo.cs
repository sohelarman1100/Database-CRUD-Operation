using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    public class StudentInfo:IData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Result StdRes { get; set; }
        public StudentInfo()
        {

        }
        public StudentInfo(int id,string name, Result res)
        {
            Id = id;
            Name = name;
            StdRes = res;
        }
    }
}
