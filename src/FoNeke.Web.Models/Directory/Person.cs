using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EMM.FoNeke.Common.Attributes;
using EMM.FoNeke.Common.Entities;
using FoNeke.Web.Models.Addressing;
using FoNeke.Web.Models.Enums;
using MongoDB.Bson.Serialization.Attributes;

namespace FoNeke.Web.Models.Directory
{
    [BsonDiscriminator("Person")]
    public class Person : BaseEntityEntreriseId
    {
        #region Etat civil


        public CivilityEnum Civility { get; set; }

        [Required]
        [LocalizedDisplayName("Person_FirstName")]
        public string FirstName { get; set; }

        [Required]
        [LocalizedDisplayName("Person_LastName")]
        public string LastName { get; set; }

        public virtual Address Address { get; set; }
        public virtual Address Address2 { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string Nationality { get; set; }

        #endregion Etat civil

        #region RH
        
        public DateTime DateIn { get; set; }

        public DateTime? DateOut { get; set; }

        public string NomPersonnePrevenir1 { get; set; }

        public string NomPersonnePrevenir2 { get; set; }

        public string TelephonePersonnePrevenir1 { get; set; }

        public string TelephonePersonnePrevenir2 { get; set; }

        public string MobilePersonnePrevenir1 { get; set; }

        public string MobilePersonnePrevenir2 { get; set; }

        #endregion RH

        #region Contact

        [Required]
        [EmailAddress]
        [LocalizedDisplayName("Person_Email")]
        public string Email { get; set; }

        public string CellPhoneNumber { get; set; }

        public string HousePhoneNumber { get; set; }

        public string WorkPhoneNumber { get; set; }

        #endregion Contact

        [Required]
        [LocalizedDisplayName("Person_Roles")]
        public List<RoleEnum> Roles { get; set; }

        [FormIgnore] public override string Display => LastName;
        [FormIgnore] public override string Name => FirstName;

    }

    public enum RoleEnum
    {
        SuperAdmin,
        AdminSoc,
        User
    }

}
