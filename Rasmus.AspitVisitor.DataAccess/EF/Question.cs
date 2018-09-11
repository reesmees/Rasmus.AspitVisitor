namespace Rasmus.AspitVisitor.DataAccess.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Question
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Question()
        {
            Answers = new HashSet<Answer>();
            QuestionsVsMultipleChoiceAnswers = new HashSet<QuestionsVsMultipleChoiceAnswer>();
        }

        public int ID { get; set; }

        public int connectedQuestionaire { get; set; }

        [Required]
        public string questionBody { get; set; }

        public bool isMultipleChoice { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Answer> Answers { get; set; }

        public virtual Questionaire Questionaire { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QuestionsVsMultipleChoiceAnswer> QuestionsVsMultipleChoiceAnswers { get; set; }
    }
}
