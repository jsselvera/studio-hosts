//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SS.DATA.EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class OwnerStudio
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OwnerStudio()
        {
            this.Reservations = new HashSet<Reservation>();
        }
    
        public int OwnerStudioID { get; set; }
        public string StudioName { get; set; }
        public string OwnerID { get; set; }
        public string StudioPhoto { get; set; }
        public string StudioAbout { get; set; }
        public bool ProducerAvailable { get; set; }
        public System.DateTime DateAdded { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public byte ReservationLimit { get; set; }
        public string StudioRules { get; set; }
        public string MapLink { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}