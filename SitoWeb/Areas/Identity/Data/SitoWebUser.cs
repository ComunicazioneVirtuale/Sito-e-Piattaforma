using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace SitoWeb.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the SitoWebUser class
    public class SitoWebUser : IdentityUser
    {
        [PersonalData]
        [Column(TypeName ="varchar(16)")]
        public string CodiceFiscale { get; set; }

        //campo autocalcolato
        [PersonalData]
        [Column(TypeName = "varchar(50)")]
        public string Nome { get; set; }
        [PersonalData]
        [Column(TypeName = "varchar(50)")]
        public string Cognome { get; set; }
        [PersonalData]
        [Column(TypeName = "date")]
        public DateTime DataDiNascita { get; set; }

    }
}
