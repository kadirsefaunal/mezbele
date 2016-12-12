using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MEZBELE.Models
{
    public class Processes
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessage = "Lütfen süreç adını girin.")]
        [Display(Name = "Süreç Adı")]
        [MaxLength(200)]
        public string ProcessName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required]
        [MaxLength(200)]
        public Processes ParentProcess { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "Başlangıç Tarihi")]
        [DataType(DataType.DateTime)]
        public DateTime StartingDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "Biiş Tarihi")]
        [DataType(DataType.DateTime)]
        public DateTime DeadLine { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "İşler")]
        public virtual List<Issues> Issues { get; set; }
    }
}