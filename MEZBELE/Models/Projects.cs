using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MEZBELE.Models
{
    /// <summary>
    /// Projeler.
    /// </summary>
    public class Projects
    {
        /// <summary>
        /// Proje kimliği.
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// Proje sahibinin kimliği. (Kullanıcı veya Ekip olabilir.)
        /// </summary>
        public int OwnerID { get; set; }
        /// <summary>
        /// Projenin bireysel olma durumu.
        /// </summary>
        [Display(Name = "Bireysel Mi?")]
        public bool IsIndividual { get; set; }
        /// <summary>
        /// Projenin gizli olma durumu.
        /// </summary>
        [Display(Name = "Özel Mi?")]
        public bool IsPrivate { get; set; }
        /// <summary>
        /// Projenin adı.
        /// </summary>
        [Required(ErrorMessage = "Lütfen proje adını girin.")]
        [Display(Name = "Proje Adı")]
        [MaxLength(50)]
        public string Name { get; set; }
        /// <summary>
        /// Projenin açıklaması.
        /// </summary>
        [Required(ErrorMessage = "Lütfen proje açıklamasını girin.")]
        [Display(Name = "Proje Açıklaması")]
        [DataType(DataType.MultilineText)]
        [MaxLength(250)]
        public string Description { get; set; }
        /// <summary>
        /// Projenin oluşturulma tarihi.
        /// </summary>
        [Display(Name = "Oluşturulma Tarihi")]
        [DataType(DataType.DateTime)]
        public DateTime CreationDate { get; set; }
        /// <summary>
        /// Projenin güncellenme tarihi.
        /// </summary>
        [Display(Name = "Güncellenme Tarihi")]
        [DataType(DataType.DateTime)]
        public DateTime ChangeDate { get; set; }
        /// <summary>
        /// Projenin işleri.
        /// </summary>
        [Display(Name = "İşler")]
        public virtual List<Issues> Issues { get; set; }
        /// <summary>
        /// Projeye dahil olan kullanıcılar.
        /// </summary>
        [Display(Name = "Üyeler")]
        public virtual List<Users> Users { get; set; }
        /// <summary>
        /// Projeye dahil olan ekipler.
        /// </summary>
        [Display(Name = "Ekipler")]
        public virtual List<Crews> Crews { get; set; }
    }
}