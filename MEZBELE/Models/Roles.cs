using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MEZBELE.Models
{
    public class Roles
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessage = "Lütfen rol adını girin.")]
        [Display(Name = "Rol Adı")]
        [MaxLength(50)]
        public string Role { get; set; }
    }
}