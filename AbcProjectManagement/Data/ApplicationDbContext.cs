using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using AbcProjectManagement.Models;
using System.Linq;
using System.Xml;

namespace AbcProjectManagement.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        
        public DbSet<AbcProjectManagement.Models.ProjectsModel> ProjectModel { get; set; }
        
    }
    
}
