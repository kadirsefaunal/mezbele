using System.Collections.Generic;

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
        public int Id { get; set; }
        /// <summary>
        /// Ekibi oluşturan kişinin kimliği.
        /// </summary>
        public int OwnerId { get; set; }
        /// <summary>
        /// Ekip adı.
        /// </summary>
        public string CrewName { get; set; }
        /// <summary>
        /// Ekip resmi.
        /// </summary>
        public string CrewAvatar { get; set; }
        /// <summary>
        /// Ekipte olan üyeler.
        /// </summary>
        public virtual List<Users> Users { get; set; }
        /// <summary>
        /// Ekibin projeleri.
        /// </summary>
        public virtual List<Projects> Projects { get; set; }
    }
}