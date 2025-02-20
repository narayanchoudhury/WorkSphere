using System;

namespace WorkSphere.Models
{
    public class ProjectAssign
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string AssignedUserIds { get; set; } // Comma-separated user IDs
        public DateTime FromDateTime { get; set; }
        public DateTime ToDateTime { get; set; }
    }

}
