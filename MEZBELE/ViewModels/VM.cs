using MEZBELE.Models;
using System.Collections.Generic;

namespace MEZBELE.ViewModels
{
    /// <summary>
    /// ViewModel.
    /// </summary>
    public class VM
    {
        /// <summary>
        /// Sistemdeki kullanıcı.
        /// </summary>
        public Kullanici Kullanici { get; set; }
        /// <summary>
        /// Sistemdeki projeler.
        /// </summary>
        public List<Proje> Projeler { get; set; }
        /// <summary>
        /// Aktif proje.
        /// </summary>
        public Proje AktifProje { get; set; }
        /// <summary>
        /// Aktif projenin yöneticisi.
        /// </summary>
        public Kullanici Yonetici { get; set; }
        /// <summary>
        /// Aktif projenin çalışanları.
        /// </summary>
        public List<Kullanici> Calisanlar { get; set; }
        /// <summary>
        /// Aktif projedeki roller.
        /// </summary>
        public List<Rol> Roller { get; set; }
        /// <summary>
        /// Aktif süreç.
        /// </summary>
        public Surec AktifSurec { get; set; }
        /// <summary>
        /// Aktif iş.
        /// </summary>
        public Is AktifIs { get; set; }
        /// <summary>
        /// Aktif işe atanan kişi.
        /// </summary>
        public IsKullanici AtananKisi { get; set; }
        /// <summary>
        /// Projenin müşterisi.
        /// </summary>
        public Kullanici Musteri { get; set; }
        /// <summary>
        /// İşler.
        /// </summary>
        public List<Is> Isler { get; set; }
    }
}