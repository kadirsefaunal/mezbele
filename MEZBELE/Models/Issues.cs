using System;
using System.Collections.Generic;

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
        public int Id { get; set; }
        /// <summary>
        /// İşin ait olduğu projenin kimliği.
        /// </summary>
        public int ProjectID { get; set; }
        /// <summary>
        /// Oluşturan kişinin kimliği.
        /// </summary>
        public int Creator { get; set; }
        /// <summary>
        /// İşin adı.
        /// </summary>
        public string IssuesName { get; set; }
        /// <summary>
        /// İşin açıklaması.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// İşin oluşturulma tarihi.
        /// </summary>
        public DateTime CreationDate { get; set; }
        /// <summary>
        /// İşe ait yorumlar.
        /// </summary>
        public virtual List<Comments> Comments { get; set; }
    }
}