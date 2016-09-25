using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MEZBELE.Models
{
    public class Comments
    {
        public int ID { get; set; }
        public int IssuesID { get; set; }
        public int UserID { get; set; }
        public string Comment { get; set; }
        public DateTime CreationDate { get; set; }
    }
}