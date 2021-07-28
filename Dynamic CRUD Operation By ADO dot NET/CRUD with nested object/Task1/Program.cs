using System;
using System.Data.SqlClient;

namespace Task1
{
    class Program
    {
        static void Main(string[] args)
        {
            using SqlConnection constring = new SqlConnection();
            constring.ConnectionString = "Server=DESKTOP-AG2EEU5\\SQLEXPRESS;Database=demo;User Id=aspnetb5;Password=123456;";
            var obj = new MyORM<IData>(constring);

            var resObj = new Result(6, 3.5);
            var infObj = new StudentInfo(6, "rakib", resObj);
            var studObj = new Student(6, infObj);
            //obj.Insert(studObj);


            resObj.CGPA = 3.17;
            infObj.Name = "Arman";
            obj.Update(studObj);

            /*obj.Delete(studObj.ID,studObj.GetType());
            obj.Delete(studObj);

            var res = obj.GetById(studObj.ID,studObj.GetType());
            Console.WriteLine("column values of table [{0}] is:", res.GetType().Name);
            printById(res);*/
        }
        public static void printById(object obj)
        {
      
            var properties = obj.GetType().GetProperties();
            foreach (var prop in properties)
            {
                if (prop.PropertyType.IsClass && !prop.PropertyType.IsValueType && !prop.PropertyType.IsPrimitive && prop.PropertyType.FullName != "System.String")
                {
                    Console.WriteLine("column values of table [{0}] is:", prop.Name);
                    var nstdobj = obj.GetType().GetProperty(prop.Name).GetValue(obj, null);
                    printById(nstdobj);
                }
                else
                {
                    Console.WriteLine("value of {0} is: {1}", prop.Name, prop.GetValue(obj));
                }
            }
        }
    }
}
