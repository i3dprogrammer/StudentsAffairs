using StudentsAffairs.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace StudentsAffairs.DAL
{
    public class StudentsAffairsContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
    }
}