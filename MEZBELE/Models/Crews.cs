using System.Collections.Generic;

namespace MEZBELE.Models
{
    public class Crews
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public string CrewName { get; set; }
        public string CrewAvatar { get; set; }

        public virtual List<Users> Users { get; set; }
        public virtual List<Projects> Projects { get; set; }
    }
}