namespace Rasmus.AspitVisitor.DataAccess.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Visit
    {
        public int ID { get; set; }

        public int visitor { get; set; }

        public int visitedDepartment { get; set; }

        public DateTime visitStartTime { get; set; }

        public DateTime visitEndTime { get; set; }

        public bool visitorWantsToStudyAtAspit { get; set; }

        public virtual AspitDepartment AspitDepartment { get; set; }

        public virtual Visitor Visitor1 { get; set; }
    }
}
