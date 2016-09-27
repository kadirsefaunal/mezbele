using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// Kullanıcı adı.
        /// </summary>
        [Required(ErrorMessage = "Lütfen kullanıcı adınızı girin.")]
        [Display(Name = "Kullanıcı Adı")]
        [MaxLength(50)]
        public string UserName { get; set; }
        /// <summary>
        /// Kullanıcı parolası.
        /// </summary>
        [Required(ErrorMessage = "Lütfen parolanızı girin.")]
        [Display(Name = "Parola")]
        [DataType(DataType.Password)]
        [MinLength(6)]
        public string Password { get; set; }
        /// <summary>
        /// Kulannıcı ismi.
        /// </summary>
        [Display(Name = "İsim")]
        [MinLength(3)]
        [MaxLength(50)]
        public string FirstName { get; set; }
        /// <summary>
        /// Kullanıcı soyismi.
        /// </summary>
        [Display(Name = "Soyisim")]
        [MinLength(2)]
        [MaxLength(50)]
        public string LastName { get; set; }
        /// <summary>
        /// Kullanıcı e-posta adresi.
        /// </summary>
        [Display(Name = "E-posta Adresi")]
        [DataType(DataType.EmailAddress)]
        [MaxLength(250)]
        public string EMail { get; set; }
        /// <summary>
        /// Kullanıcı resmi.
        /// </summary>
        [Display(Name = "Kullanıcı Resmi")]
        public string UserAvatar { get; set; }
        /// <summary>
        /// Kullanıcının dahil olduğu ekipler.
        /// </summary>
        [Display(Name = "Ekipler")]
        public virtual List<Crews> Crews { get; set; }
        /// <summary>
        /// Kullanıcının dahil olduğu projeler.
        /// </summary>
        [Display(Name = "Projeler")]
        public virtual List<Projects> Projects { get; set; }
        /// <summary>
        /// Kullanıcının yorumları.
        /// </summary>
        [Display(Name = "Yorumlar")]
        public virtual List<Comments> Comments { get; set; }
    }
}