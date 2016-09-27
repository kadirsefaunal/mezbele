using System;
using System.ComponentModel.DataAnnotations;

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
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// Yorumun ait olduğu işin kimliği.
        /// </summary>
        [Display(Name = "İlgili İş")]
        public Issues Issue { get; set; }
        /// <summary>
        /// Yorumu yapan kişinin kimliği.
        /// </summary>
        [Display(Name = "Yorumcu")]
        public Users Commenter { get; set; }
        /// <summary>
        /// Yorum.
        /// </summary>
        [Required(ErrorMessage = "Lütfen yorumunuzu yazın.")]
        [Display(Name = "Yorum")]
        [DataType(DataType.MultilineText)]
        [MaxLength(500)]
        public string Comment { get; set; }
        /// <summary>
        /// Yorumun oluşturulma tarihi.
        /// </summary>
        [Display(Name = "Tarih")]
        [DataType(DataType.DateTime)]
        public DateTime CreationDate { get; set; }
    }
}