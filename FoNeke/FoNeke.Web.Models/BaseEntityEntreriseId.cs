using EMM.FoNeke.Common.Attributes;
using EMM.FoNeke.Common.Entities;
using FoNeke.Web.Models.Directory;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoNeke.Web.Models
{
    public class BaseEntityEntreriseId : BaseEntity
    {
        [FormIgnore]
        public string IdEntreprise { get; set; }

        [FormIgnore]
        [BsonIgnore]
        public Entreprise Entreprise { get; set; }
    }
}
