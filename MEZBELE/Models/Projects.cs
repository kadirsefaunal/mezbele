using System;
using System.Collections.Generic;

namespace MEZBELE.Models
{
    public class Projects
    {
        public int Id { get; set; }
        public int OwnerID { get; set; }
        public bool IsIndividual { get; set; }
        public bool IsPrivate { get; set; }
        public string ProjectName { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ChangeDate { get; set; }

        public virtual List<Issues> Issues { get; set; }
        public virtual List<Users> Users { get; set; }
        public virtual List<Crews> Crews { get; set; }
    }
}