using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    public class Result : IData
    {
        public int Id { get; set; }
        public double CGPA { get; set; }
        public Result()
        {

        }
        public Result(int id, double cg)
        {
            Id = id;
            CGPA = cg;
        }
    }
}
