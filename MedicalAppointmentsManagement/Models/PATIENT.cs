//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
using MedicalAppointmentsManagement.MetadataModel;

namespace MedicalAppointmentsManagement.Models
{
    using System;
    using System.Collections.Generic;

    [MetadataType(typeof(PatientsMetadata))]
    public partial class PATIENT
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PATIENT()
        {
            this.APPOINTMENTs = new HashSet<APPOINTMENT>();
        }
    
        public int patientAMKA { get; set; }
        public string username { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string password { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<APPOINTMENT> APPOINTMENTs { get; set; }
    }
}
