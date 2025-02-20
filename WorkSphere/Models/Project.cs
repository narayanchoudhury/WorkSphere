using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorkSphere.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string ProjectName { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
    }

}