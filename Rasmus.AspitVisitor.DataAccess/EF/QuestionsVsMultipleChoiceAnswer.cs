namespace Rasmus.AspitVisitor.DataAccess.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class QuestionsVsMultipleChoiceAnswer
    {
        public int ID { get; set; }

        public int question { get; set; }

        public int answer { get; set; }

        public virtual MultipleChoiceAnswer MultipleChoiceAnswer { get; set; }

        public virtual Question Question1 { get; set; }
    }
}
