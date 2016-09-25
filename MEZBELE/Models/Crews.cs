using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MEZBELE.Models
{
    public class Crews
    {
        public int ID { get; set; }
        public int OwnerID { get; set; }
        public string CrewName { get; set; }
        public string CrewAvatar { get; set; }

        public virtual List<Users> Users { get; set; }
        public virtual List<Projects> Projects { get; set; }
    }
}