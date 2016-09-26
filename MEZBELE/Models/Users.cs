using System.Collections.Generic;

namespace MEZBELE.Models
{
    /// <summary>
    /// Kullanıcılar.
    /// </summary>
    public class Users
    {
        /// <summary>
        /// Kullanıcı kimliği.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Kullanıcı adı.
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// Kullanıcı parolası.
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Kulannıcı ismi.
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Kullanıcı soyismi.
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// Kullanıcı e-posta adresi.
        /// </summary>
        public string EMail { get; set; }
        /// <summary>
        /// Kullanıcı resmi.
        /// </summary>
        public string UserAvatar { get; set; }
        /// <summary>
        /// Kullanıcının dahil olduğu ekipler.
        /// </summary>
        public virtual List<Crews> Crews { get; set; }
        /// <summary>
        /// Kullanıcının dahil olduğu projeler.
        /// </summary>
        public virtual List<Projects> Projects { get; set; }
        /// <summary>
        /// Kullanıcının yorumları.
        /// </summary>
        public virtual List<Comments> Comments { get; set; }
    }
}