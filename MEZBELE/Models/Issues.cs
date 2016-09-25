using System;
using System.Collections.Generic;

namespace MEZBELE.Models
{
    public class Issues
    {
        public int Id { get; set; }
        public int ProjectID { get; set; }
        public int Creator { get; set; }
        public string IssuesName { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }

        public virtual List<Comments> Comments { get; set; }
    }
}