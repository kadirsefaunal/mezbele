using System;

namespace MEZBELE.Models
{
    /// <summary>
    /// Yorumlar.
    /// </summary>
    public class Comments
    {
        /// <summary>
        /// Yorum kimliği.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Yorumun ait olduğu işin kimliği.
        /// </summary>
        public int IssuesId { get; set; }
        /// <summary>
        /// Yorumu yapan kişinin kimliği.
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// Yorum.
        /// </summary>
        public string Comment { get; set; }
        /// <summary>
        /// Yorumun oluşturulma tarihi.
        /// </summary>
        public DateTime CreationDate { get; set; }
    }
}