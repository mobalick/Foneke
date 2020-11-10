using System;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;
using MongoRepository;
using EMM.FoNeke.Common.Attributes;

namespace EMM.FoNeke.Common.Entities
{
    public class BaseEntity : Entity, IComparable<BaseEntity>
    {
        [FormIgnore]
        public override string Id { get; set; }

        [FormIgnore]
        public virtual string Name { get; set; }

        [FormIgnore]
        public string CreatedBy { get; set; }

        [FormIgnore]
        public string ModifiedBy { get; set; }

        [FormIgnore]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? CreationDate { get; set; }

        [FormIgnore]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? ModificationDate { get; set; }

        [FormIgnore] public virtual string Display => Name;

        public int CompareTo(BaseEntity other)
        {
            return String.Compare(Id, other.Id, StringComparison.Ordinal);
        }

        [FormIgnore]
        public string DisplaySaved {
            get { return Display; }
            set { value = Display;}
        }
    }

}