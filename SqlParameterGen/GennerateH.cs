using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoGennerateSqlParameters.SqlParameterGen
{
    
    public class GennerateH
    {
        public static List<SqlParameter> genSqlParameter<T>(T obj) where T:new()
        {
            var parameters = new List<SqlParameter>();
            var tObj = obj.GetType();
            var props = tObj.GetProperties();

            foreach (var prop in props)
            {
                var hasNotMappedAttr = prop.CustomAttributes.Any(c => c.AttributeType == typeof(NotMappedAttribute));

                if (!hasNotMappedAttr)
                {
                    var _pr = new SqlParameter();
                    _pr.ParameterName = "@" + prop.Name;
                    var value = prop.GetValue(obj);
                    _pr.Value = value == null ? "" : value;

                    parameters.Add(_pr);
                }
                
            }

            return parameters;
        }
        public static List<SqlParameter> genSqlParameterWithPaged<T>(T obj, int page = 1, int pageSize = 50) where T : new()
        {
            var parameters = genSqlParameter(obj);

            int offset = (page - 1) * pageSize;
            var skip = new SqlParameter("@Skip", offset);
            var take = new SqlParameter("@Take", pageSize);

            parameters.AddRange(new[] {skip,take});

            return parameters;
        }

        public static SqlParameter genSqlParameterDataTable<T>(IEnumerable<T> data, string parameterName, string typeName) where T : new()
        {
            var _pr = new SqlParameter();
            _pr.ParameterName = "@" + parameterName;
            _pr.TypeName = typeName;

            var props = data.First().GetType().GetProperties().Where(prop => {
                var hasNotMappedAttr = prop.CustomAttributes.Any(c => c.AttributeType == typeof(NotMappedAttribute));
                return !hasNotMappedAttr;
            });

            DataTable dataTable = new DataTable();

            foreach (var prop in props)
            {
                var type = prop.PropertyType;
                if (type == typeof(Nullable<>) || type.Name == typeof(Nullable<>).Name)
                {
                    var underlyingType = Nullable.GetUnderlyingType(type);
                    type = underlyingType ?? type;
                }

                dataTable.Columns.Add(prop.Name, type);
            }

            foreach (var item in data)
            {
                var tblRow = dataTable.NewRow();
                foreach(var prop in props)
                {
                    var value = prop.GetValue(item);
                    tblRow[prop.Name] = value==null?DBNull.Value:value;
                }
                dataTable.Rows.Add(tblRow);
            }

            _pr.Value = dataTable;
            return _pr;
        }
    }
}
