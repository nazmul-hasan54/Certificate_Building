using Application.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CoreInformation.Context
{
    public class ProjectDbContext: IdentityDbContext 
    {
        public ProjectDbContext(DbContextOptions<ProjectDbContext> options): base(options)
        {
            
        }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Certificate> Certificates { get; set; }
        public DbSet<Users> Users { get; set; }
    }
}
