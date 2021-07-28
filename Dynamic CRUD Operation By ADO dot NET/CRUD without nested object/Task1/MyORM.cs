using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Task1
{
    public class MyORM<T> where T:IData
    {
        private SqlConnection _sqlConnection;

        public MyORM(SqlConnection connection)
        {
            _sqlConnection = connection;
        }

        public MyORM(string connectionString)
            : this(new SqlConnection(connectionString))
        {

        }

        public void Insert(T item)
        {
            var sql = new StringBuilder("Insert into ");
            var type = item.GetType();
            var properties = type.GetProperties();

            sql.Append(type.Name);
            sql.Append('(');
            foreach (var property in properties)
            {
                sql.Append(' ').Append(property.Name).Append(',');
            }
            sql.Remove(sql.Length - 1, 1);

            sql.Append(") values(");

            foreach (var property in properties)
            {
                sql.Append('@').Append(property.Name).Append(',');
            }
            sql.Remove(sql.Length - 1, 1);
            sql.Append(");");

            var query = sql.ToString();

            using SqlCommand command = new SqlCommand(query, _sqlConnection);
            if (_sqlConnection.State == System.Data.ConnectionState.Closed)
                _sqlConnection.Open();
            foreach (var property in properties)
            {
                string s = property.Name;
                command.Parameters.AddWithValue(s,property.GetValue(item));
            }

            command.ExecuteNonQuery();
            //Console.WriteLine("insert operation done!!!");
        }

        public void Update(T item)
        {
            var type = item.GetType();
            //Console.WriteLine("type is {0}", type);
            var properties = type.GetProperties();
            var sql = new StringBuilder("UPDATE ");
            sql.Append(type.Name);
            sql.Append(" SET ");
            foreach(var property in properties)
            {
                sql.Append(property.Name).Append(" = ").Append("@").Append(property.Name).Append(",");
            }
            sql.Remove(sql.Length - 1, 1);
            sql.Append(" WHERE Id=").Append("@val").Append(";");

            var query = sql.ToString();
            //Console.WriteLine(query);

            using SqlCommand command = new SqlCommand(query, _sqlConnection);
            if (_sqlConnection.State == System.Data.ConnectionState.Closed)
                _sqlConnection.Open();
            foreach(var property in properties)
            {
                string s = property.Name;
                command.Parameters.AddWithValue(s, property.GetValue(item));
            }
            command.Parameters.AddWithValue("@val", item.Id);

            command.ExecuteNonQuery();
            //Console.WriteLine("update operation done!!!");

        }

        public void Delete(T item)
        {
            Delete(item.Id);
        }

        public void Delete(int id)
        {
            var type = typeof(T);

            var sql = new StringBuilder("DELETE FROM ");
            sql.Append(type.Name).Append(" WHERE Id = ").Append("@id ;");   //@id ekta variable tai ekhane id na likhe independent vabe je kono kichu likha jabe.

            var query = sql.ToString();
            //Console.WriteLine(query);

            using SqlCommand command = new SqlCommand(query, _sqlConnection);
            command.Parameters.AddWithValue("@id", id);    //parameter er @id er same eikhaneo @id likha lagbe karon eikhane AddWithValue first value hisebe parameter name ke ney o second value hisebe value ke ney
            
            if (_sqlConnection.State == System.Data.ConnectionState.Closed)
                _sqlConnection.Open();
            
            command.ExecuteNonQuery();
        }

        public T GetById(int id)
        {
            var type = typeof(T);
            var properties = type.GetProperties();

            var sql = new StringBuilder("SELECT * FROM ");
            sql.Append(type.Name).Append(" WHERE Id = ").Append("@id;");
            var query = sql.ToString();

            using SqlCommand command = new SqlCommand(query, _sqlConnection);
            command.Parameters.AddWithValue("@id", id);

            if (_sqlConnection.State == System.Data.ConnectionState.Closed)
                _sqlConnection.Open();

            using SqlDataReader rdr = command.ExecuteReader();
            rdr.Read();
            var constructor = type.GetConstructor(new Type[0]);
            var obj = constructor.Invoke(new object[0]);
         
            foreach(var property in properties)
            {
                string col = property.Name;
                property.SetValue(obj, rdr[col]);
            }

            return (T) obj;
        }
        public IList<T> GetAll()
        {
            var type = typeof(T);
            var properties = type.GetProperties();

            var sql = new StringBuilder("SELECT * FROM ");
            sql.Append(type.Name).Append(";");
            var query = sql.ToString();

            using SqlCommand command = new SqlCommand(query, _sqlConnection);
            if (_sqlConnection.State == System.Data.ConnectionState.Closed)
                _sqlConnection.Open();

            using SqlDataReader rdr = command.ExecuteReader();

            List<T> record = new List<T>();
            while(rdr.Read())
            {
                var constructor = type.GetConstructor(new Type[0]);
                var obj = constructor.Invoke(new object[0]);

                foreach (var property in properties)
                {
                    string col = property.Name;
                    property.SetValue(obj, rdr[col]);
                }

                record.Add((T) obj);
            }
            return record;
        }

    }
}
