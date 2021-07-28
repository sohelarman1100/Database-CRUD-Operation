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
                if (property.PropertyType.IsClass && !property.PropertyType.IsValueType && !property.PropertyType.IsPrimitive && property.PropertyType.FullName != "System.String")
                {
                    var typ = property.PropertyType;
                    var cons = typ.GetConstructor(new Type[0]);
                    var nwObj = cons.Invoke(new object[0]);
                    string para = "";
                    para += (property.Name);
                    para += (".");

                    //Console.WriteLine("property name= {0}", property.Name);
                    var nstdelement = property.PropertyType.GetProperties();

                    foreach (var nstdprp in nstdelement)
                    {
                        string para1 = para;
                        para1 += (nstdprp.Name);
                        var val = GetPropertyValue(item, para1);   //retrive nested object value from reflection property

                        // storing value of nested obj in nwobj
                        Type tp = nwObj.GetType();
                        PropertyInfo prop = tp.GetProperty(nstdprp.Name);
                        prop.SetValue(nwObj, val, null);
                    }
                    var ormObj = new MyORM<IData>(_sqlConnection);
                    IData obj = (IData)nwObj;
                    
                    //calling nested object
                    ormObj.Insert(obj);
                }
                else
                { 
                    sql.Append(' ').Append(property.Name).Append(','); 
                }
            }
            sql.Remove(sql.Length - 1, 1);

            sql.Append(") values(");

            foreach (var property in properties)
            {
                if (property.PropertyType.IsClass && !property.PropertyType.IsValueType && !property.PropertyType.IsPrimitive && property.PropertyType.FullName != "System.String")
                {

                }
                else
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
                if (property.PropertyType.IsClass && !property.PropertyType.IsValueType && !property.PropertyType.IsPrimitive && property.PropertyType.FullName != "System.String")
                {

                }
                else
                {
                    string s = property.Name;
                    command.Parameters.AddWithValue(s, property.GetValue(item));
                }
            }

            command.ExecuteNonQuery();
        }

        
        public void Update(T item)
        {
            var type = item.GetType();
            var properties = type.GetProperties();
            var sql = new StringBuilder("UPDATE ");
            sql.Append(type.Name);
            sql.Append(" SET ");
            foreach(var property in properties)
            {
                if (property.PropertyType.IsClass && !property.PropertyType.IsValueType && !property.PropertyType.IsPrimitive && property.PropertyType.FullName != "System.String")
                {
                    var typ = property.PropertyType;
                    var cons = typ.GetConstructor(new Type[0]);
                    var nwObj = cons.Invoke(new object[0]);
                    string para = "";
                    para += (property.Name);
                    para += (".");

                    var nstdelement = property.PropertyType.GetProperties();

                    foreach (var nstdprp in nstdelement)
                    {
                        string para1 = para;
                        para1 += (nstdprp.Name);
                        var val = GetPropertyValue(item, para1);   //retrive nested object value from reflection property

                        // storing value of nested obj in nwobj
                        Type tp = nwObj.GetType();
                        PropertyInfo prop = tp.GetProperty(nstdprp.Name);
                        prop.SetValue(nwObj, val, null);
                    }
                    
                    var ormObj = new MyORM<IData>(_sqlConnection);
                    IData obj = (IData)nwObj;
                    ormObj.Update(obj);
                }
                else
                    sql.Append(property.Name).Append(" = ").Append("@").Append(property.Name).Append(",");
            }
            sql.Remove(sql.Length - 1, 1);
            sql.Append(" WHERE ID=").Append("@val").Append(";");

            var query = sql.ToString();
            using SqlCommand command = new SqlCommand(query, _sqlConnection);
            if (_sqlConnection.State == System.Data.ConnectionState.Closed)
                _sqlConnection.Open();
            foreach(var property in properties)
            {
                if (property.PropertyType.IsClass && !property.PropertyType.IsValueType && !property.PropertyType.IsPrimitive && property.PropertyType.FullName != "System.String")
                {

                }
                else
                {
                    string s = property.Name;
                    command.Parameters.AddWithValue(s, property.GetValue(item));
                }
            }
            command.Parameters.AddWithValue("@val", item.ID);

            command.ExecuteNonQuery();
        }

        public void Delete(T item)
        {
            Type t = item.GetType();
            Delete(item.ID,t);
        }

        public void Delete(int id,Type t)
        {
            var type = t;
            var properties = type.GetProperties();
            Console.WriteLine("t type = {0}", type);
            foreach (var property in properties)
            {
                if (property.PropertyType.IsClass && !property.PropertyType.IsValueType && !property.PropertyType.IsPrimitive && property.PropertyType.FullName != "System.String")
                {
                    var typ = property.PropertyType;
                    var ormObj = new MyORM<IData>(_sqlConnection);
                    ormObj.Delete(id,typ);
                }
            }

            var sql = new StringBuilder("DELETE FROM ");
            sql.Append(type.Name).Append(" WHERE ID = ").Append("@id ;");  
            var query = sql.ToString();
            using SqlCommand command = new SqlCommand(query, _sqlConnection);
            command.Parameters.AddWithValue("@id", id);   
            if (_sqlConnection.State == System.Data.ConnectionState.Closed)
                _sqlConnection.Open();
            
            command.ExecuteNonQuery();
        }

        public T GetById(int id,Type t)
        {
            var type = t;
            var cons = type.GetConstructor(new Type[0]);
            var demoObj = cons.Invoke(new object[0]);
            IData nstobj = (IData)demoObj;
            var properties = type.GetProperties();
        
            foreach (var property in properties)
            {
                if (property.PropertyType.IsClass && !property.PropertyType.IsValueType && !property.PropertyType.IsPrimitive && property.PropertyType.FullName != "System.String")               
                {
                    var typ = property.PropertyType;
                    //Console.WriteLine("prop type = {0}", typ);
                    //Console.WriteLine(property.Name);

                    var ormObj = new MyORM<IData>(_sqlConnection);
                    nstobj = ormObj.GetById(id, typ);
                }
            }

            var sql = new StringBuilder("SELECT * FROM ");
            sql.Append(type.Name).Append(" WHERE ID = ").Append("@id;");
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
                if (property.PropertyType.IsClass && !property.PropertyType.IsValueType && !property.PropertyType.IsPrimitive && property.PropertyType.FullName != "System.String")
                {
                    property.SetValue(obj,nstobj);
                }
                else
                {
                    string col = property.Name;
                    property.SetValue(obj, rdr[col]);
                }
            }

            return (T) obj;
        }
       
        public static object GetPropertyValue(object src, string propName)
        {
            if (propName.Contains("."))
            {
                var temp = propName.Split(new char[] { '.' }, 2);
                return GetPropertyValue(GetPropertyValue(src, temp[0]), temp[1]);
            }
            else
            {
                var prop = src.GetType().GetProperty(propName);
                return prop != null ? prop.GetValue(src, null) : null;
            }
        }
    }
}
