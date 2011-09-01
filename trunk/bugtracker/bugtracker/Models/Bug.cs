using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace bugtracker.Models
{
    public class Bug
    {
        public int ID { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Text)]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required]
        [StringLength(400, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 20)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Description")]
        public string Description { get; set; }


        [Required]
        [Range(0, 5)]
        [DataType(DataType.Text)]
        [Display(Name = "Criticality")]
        public int Criticality { get; set; }

        [Required]
        [Range(0, 5)]
        [DataType(DataType.Text)]
        [Display(Name = "Criticality")]
        public int PriorityID { get; set; }
        public int StatusID { get; set; }
        public int BugTypeID { get; set; }


    }

    public class BugDBContext : DbContext
    {
        public DbSet<Bug> Bugs { get; set; }
        public DbSet<BugType> BugTypes { get; set; }
    }

    public class BugType
    {
        public int ID { get; set; }
        public String Name { get; set; }
    }

}