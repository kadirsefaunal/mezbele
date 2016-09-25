using System.Collections.Generic;

namespace MEZBELE.Models
{
    public class Users
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EMail { get; set; }
        public string UserAvatar { get; set; }

        public virtual List<Crews> Crews { get; set; }
        public virtual List<Projects> Projects { get; set; }
        public virtual List<Comments> Comments { get; set; }
    }
}