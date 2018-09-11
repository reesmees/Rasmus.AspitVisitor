namespace Rasmus.AspitVisitor.DataAccess.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Answer
    {
        public int ID { get; set; }

        public int connectedQuestion { get; set; }

        public int visitor { get; set; }

        [Required]
        public string answerBody { get; set; }

        public virtual Question Question { get; set; }

        public virtual Visitor Visitor1 { get; set; }
    }
}
