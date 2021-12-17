using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Text;

namespace DemoGennerateSqlParameters.EF
{

    public class CommandMsg
    {
        [Key]
        public string error { get; set; }
    }
    public class DemoContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-0VUF4BP\SQLEXPRESS;Database=kooboo_demo_01;User Id=sa;Password=123;");
        }
        public DbSet<CommandMsg> CommandMsgs { get; set; }
    }


}
