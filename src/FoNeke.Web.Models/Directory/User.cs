using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMM.FoNeke.Common.Attributes;
using MongoDB.Bson.Serialization.Attributes;

namespace FoNeke.Web.Models.Directory
{
    [BsonDiscriminator("User")]
    public class User : Person
    {
        [FormIgnore] public override string Display => LastName;
        [FormIgnore] public override string Name => FirstName;

    }
}
