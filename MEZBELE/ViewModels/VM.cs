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
    }
}