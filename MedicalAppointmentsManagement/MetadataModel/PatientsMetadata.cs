using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MedicalAppointmentsManagement.MetadataModel
{
    public partial class PatientsMetadata
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]

        [Display(Name = "AMKA")]
        //[Required]
        public int patientAMKA { get; set; }


        [Display(Name = "Username")]
        [Required]
        [StringLength(45, MinimumLength = 4, ErrorMessage = "Username must be between 6-45 characters!")]
        public string username { get; set; }

        [Display(Name = "First Name")]
        [Required]
        [StringLength(45, ErrorMessage = "First Name must be fill!")]
        public string name { get; set; }

        [Display(Name = "Last Name")]
        [Required]
        [StringLength(45, ErrorMessage = "Last Name must be fill!")]
        public string surname { get; set; }

        [Display(Name = "Password")]
        [Required]
        [StringLength(45, MinimumLength = 6, ErrorMessage = "Password must be between 6-45 fill!")]
        public string password { get; set; }

        [Display(Name = "Role")]
        [Required]
        [StringLength(45, ErrorMessage = "Password must be between 6-45 fill!")]
        public string role { get; set; }

    }
}