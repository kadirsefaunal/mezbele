using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MEZBELE.Models
{
    /// <summary>
    /// İşler.
    /// </summary>
    public class Issues
    {
        /// <summary>
        /// İş kimliği.
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// İşin aktif olma durumu.
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// İşin ait olduğu projenin kimliği.
        /// </summary>
        [Display(Name = "Proje")]
        public Projects Project { get; set; }
        /// <summary>
        /// Oluşturan kişinin kimliği.
        /// </summary>
        [Display(Name = "Oluşturan")]
        public int CreatorId { get; set; }
        /// <summary>
        /// İşin adı.
        /// </summary>
        [Required(ErrorMessage = "Lütfen iş adını girin.")]
        [Display(Name = "İş Adı")]
        [MaxLength(200)]
        public string IssuesName { get; set; }
        /// <summary>
        /// İşin açıklaması.
        /// </summary>
        [Display(Name = "İş Açıklaması")]
        [DataType(DataType.MultilineText)]
        [MaxLength(500)]
        public string Description { get; set; }
        /// <summary>
        /// İşin oluşturulma tarihi.
        /// </summary>
        [Display(Name = "Oluşturma Tarihi")]
        [DataType(DataType.DateTime)]
        public DateTime CreationDate { get; set; }
        /// <summary>
        /// İşe ait yorumlar.
        /// </summary>
        [Display(Name = "Yorumlar")]
        public virtual List<Comments> Comments { get; set; }
        /// <summary>
        /// Görevliler.
        /// </summary>
        [Display(Name = "Görevliler")]
        public virtual List<Users> Attendants { get; set; }

    }
}