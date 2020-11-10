//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
////using MongoDB.Bson.Serialization.Attributes;
//using EMM.FoNeke.Common.Attributes;
//using EMM.FoNeke.Common.Entities;
//using FoNeke.Web.Models.Enums;

//namespace FoNeke.Web.Models
//{
//    public class Notification : BaseEntity
//    {
//        //public List<Parent> Parents { get; set; }

//        //public Student Student { get; set; }

//        //public Teacher Teacher { get; set; }
//        public Notification()
//        {
//            Start = DateTime.Now;
//            IsSentImediat = true;
//            Receivers=new List<Person>();
//            Comments = new List<Comment>();
//            Attachements = new List<Attachement>();
//        }

//        public Person Sender { get; set; }

//        public List<Person> Receivers { get; set; }

//        public List<Comment> Comments { get; set; }
//        public List<Attachement> Attachements { get; set; }

//        [Required]
//        //[BsonDateTimeOptions(Kind = DateTimeKind.Local)]
//        public DateTime? Start { get; set; }

//        //[BsonDateTimeOptions(Kind = DateTimeKind.Local)]
//        public DateTime? End { get; set; }

//        public string Title { get; set; }

//        public NotificationTypeEnum Type { get; set; }

//        public bool IsSentImediat { get; set; }

//        [RequiredIf("IsSentImediat", false, ErrorMessage = "La date d'envoi est obligatoire")]
//        //[BsonDateTimeOptions(Kind = DateTimeKind.Local)]
//        public DateTime? SendDateTime { get; set; }

//        public bool IsRead { get; set; }

//        //[BsonDateTimeOptions(Kind = DateTimeKind.Local)]
//        public DateTime? ReadTime { get; set; }

//        public override string Display {
//            get { return Type.ToString(); }
//        }
//    }
//}
