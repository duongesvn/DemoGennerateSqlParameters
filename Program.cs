using DemoGennerateSqlParameters.EF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace DemoGennerateSqlParameters
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var tempData = new TempClass
            {
                RowGuid = null,
                Name = "Thanh Dương",
                CreatedDate = DateTime.Now,
                Cost = (decimal)1.2,
                IsActive = true
            };
            var lsData = new List<TempClass> { tempData };

            var parameters = SqlParameterGen.GennerateH.genSqlParameter(tempData);
            var tbl01 = SqlParameterGen.GennerateH.genSqlParameterDataTable(lsData,"tempData","dbo.ABC");
            parameters.Add(tbl01);

            string sql = @"SP_Demo 
	                        @RowGuid
	                        ,@Name
	                        ,@CreatedDate
	                        ,@Cost
	                        ,@IsActive
	                        ,@tempData";
            //using (var db = new DemoContext())
            //{
            //    var rawSql = new RawSqlString(sql);

            //    var rs = await db.CommandMsgs.FromSqlRaw(sql, parameters.ToArray()).FirstOrDefaultAsync();
            //    var a = 1;
            //}
            
            Console.WriteLine("Hello World!");
        }
    }

    public class TempClass
    {
        public Guid? RowGuid { get; set; }
        public string Name { get; set; }
        public DateTime? CreatedDate { get; set; }
        public decimal? Cost { get; set; }
        public bool? IsActive { get; set; }
        [NotMapped]
        public string NotMap { get; set; }
    }
}
