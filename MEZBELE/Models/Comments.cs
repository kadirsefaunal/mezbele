using System;

namespace MEZBELE.Models
{
    public class Comments
    {
        public int Id { get; set; }
        public int IssuesId { get; set; }
        public int UserId { get; set; }
        public string Comment { get; set; }
        public DateTime CreationDate { get; set; }
    }
}