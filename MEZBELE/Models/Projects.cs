using System;
using System.Collections.Generic;

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
        public int Id { get; set; }
        /// <summary>
        /// Proje sahibinin kimliği.
        /// </summary>
        public int OwnerID { get; set; }
        /// <summary>
        /// Projenin bireysel olma durumu.
        /// </summary>
        public bool IsIndividual { get; set; }
        /// <summary>
        /// Projenin gizli olma durumu.
        /// </summary>
        public bool IsPrivate { get; set; }
        /// <summary>
        /// Projenin adı.
        /// </summary>
        public string ProjectName { get; set; }
        /// <summary>
        /// Projenin açıklaması.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Projenin oluşturulma tarihi.
        /// </summary>
        public DateTime CreationDate { get; set; }
        /// <summary>
        /// Projenin güncellenme tarihi.
        /// </summary>
        public DateTime ChangeDate { get; set; }
        /// <summary>
        /// Projenin işleri.
        /// </summary>
        public virtual List<Issues> Issues { get; set; }
        /// <summary>
        /// Projeye dahil olan kullanıcılar.
        /// </summary>
        public virtual List<Users> Users { get; set; }
        /// <summary>
        /// Projeye dahil olan ekipler.
        /// </summary>
        public virtual List<Crews> Crews { get; set; }
    }
}