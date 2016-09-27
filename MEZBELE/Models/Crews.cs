using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MEZBELE.Models
{
    /// <summary>
    /// Ekipler.
    /// </summary>
    public class Crews
    {
        /// <summary>
        /// Ekip kimliği.
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// Ekibi oluşturan kişinin kimliği.
        /// </summary>
        [Display(Name = "Kurucu")]
        public Users Owner { get; set; }
        /// <summary>
        /// Ekip adı.
        /// </summary>
        [Required(ErrorMessage = "Lütfen ekip adını girin.")]
        [Display(Name = "Ekip Adı")]
        [MaxLength(25)]
        public string CrewName { get; set; }
        /// <summary>
        /// Ekip resmi.
        /// </summary>
        [Display(Name = "Ekip Resmi")]
        public string CrewAvatar { get; set; }
        /// <summary>
        /// Ekipte olan üyeler.
        /// </summary>
        [Display(Name = "Üyeler")]
        public virtual List<Users> Users { get; set; }
        /// <summary>
        /// Ekibin projeleri.
        /// </summary>
        [Display(Name = "Projeler")]
        public virtual List<Projects> Projects { get; set; }
    }
}