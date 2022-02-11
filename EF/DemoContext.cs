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
            optionsBuilder.UseSqlServer(@"Server=192.168.137.201;Database=VIETNHAT_AM;uid=dev;password=esvn2020@;MultipleActiveResultSets=true");
        }
        public virtual DbSet<CommandMsg> CommandMsgs { get; set; }
    }


}
