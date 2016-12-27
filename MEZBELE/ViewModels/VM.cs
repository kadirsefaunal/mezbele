using MEZBELE.Models;
using System.Collections.Generic;

namespace MEZBELE.ViewModels
{
    /// <summary>
    ///
    /// </summary>
    public class VM
    {
        /// <summary>
        ///
        /// </summary>
        public Kullanici Kullanici { get; set; }
        /// <summary>
        ///
        /// </summary>
        public List<Proje> Projeler { get; set; }
        /// <summary>
        ///
        /// </summary>
        public Proje AktifProje { get; set; }
        /// <summary>
        ///
        /// </summary>
        public Kullanici Yonetici { get; set; }
        /// <summary>
        ///
        /// </summary>
        public List<Kullanici> Calisanlar { get; set; }
        /// <summary>
        ///
        /// </summary>
        public List<Rol> Roller { get; set; }
        /// <summary>
        ///
        /// </summary>
        public Surec AktifSurec { get; set; }
        /// <summary>
        ///
        /// </summary>
        public Is AktifIs { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public IsKullanici AtananKisi { get; set; }
    }
}