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
            var obj = new MyORM<Student>(constring);
            
            var stud1 = new Student(6, "arman", 24);
            /*obj.Insert(stud1);

            stud1.Name = "sohel arman"; //updated value for name of object ins;
            obj.Update(stud1);

            obj.Delete(stud1.Id);
            obj.Delete(stud1);

            var res = obj.GetById(7);
            Console.WriteLine("{0}, {1}, {2}", res.Id, res.Name, res.Age);*/

            var rcrd = obj.GetAll();
            for (var i=0; i<rcrd.Count; i++)
            {
                Console.WriteLine("{0}, {1}, {2}", rcrd[i].Name, rcrd[i].Id, rcrd[i].Age);
            }
        }
    }
}
