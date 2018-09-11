namespace Rasmus.AspitVisitor.DataAccess.EF
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class AspitVisitorContext : DbContext
    {
        public AspitVisitorContext()
            : base("name=AspitVisitorContext")
        {
        }

        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<Answer> Answers { get; set; }
        public virtual DbSet<AspitDepartment> AspitDepartments { get; set; }
        public virtual DbSet<MultipleChoiceAnswer> MultipleChoiceAnswers { get; set; }
        public virtual DbSet<Questionaire> Questionaires { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<QuestionsVsMultipleChoiceAnswer> QuestionsVsMultipleChoiceAnswers { get; set; }
        public virtual DbSet<Visitor> Visitors { get; set; }
        public virtual DbSet<Visit> Visits { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<Admin>()
                .HasMany(e => e.Questionaires)
                .WithRequired(e => e.Admin)
                .HasForeignKey(e => e.adminCreator)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Answer>()
                .Property(e => e.answerBody)
                .IsUnicode(false);

            modelBuilder.Entity<AspitDepartment>()
                .Property(e => e.departmentName)
                .IsUnicode(false);

            modelBuilder.Entity<AspitDepartment>()
                .Property(e => e.streeName)
                .IsUnicode(false);

            modelBuilder.Entity<AspitDepartment>()
                .Property(e => e.city)
                .IsUnicode(false);

            modelBuilder.Entity<AspitDepartment>()
                .HasMany(e => e.Visits)
                .WithRequired(e => e.AspitDepartment)
                .HasForeignKey(e => e.visitedDepartment)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MultipleChoiceAnswer>()
                .Property(e => e.choiceBody)
                .IsUnicode(false);

            modelBuilder.Entity<MultipleChoiceAnswer>()
                .HasMany(e => e.QuestionsVsMultipleChoiceAnswers)
                .WithRequired(e => e.MultipleChoiceAnswer)
                .HasForeignKey(e => e.answer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Questionaire>()
                .HasMany(e => e.Questions)
                .WithRequired(e => e.Questionaire)
                .HasForeignKey(e => e.connectedQuestionaire)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Question>()
                .Property(e => e.questionBody)
                .IsUnicode(false);

            modelBuilder.Entity<Question>()
                .HasMany(e => e.Answers)
                .WithRequired(e => e.Question)
                .HasForeignKey(e => e.connectedQuestion)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Question>()
                .HasMany(e => e.QuestionsVsMultipleChoiceAnswers)
                .WithRequired(e => e.Question1)
                .HasForeignKey(e => e.question)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Visitor>()
                .Property(e => e.firstName)
                .IsUnicode(false);

            modelBuilder.Entity<Visitor>()
                .Property(e => e.lastName)
                .IsUnicode(false);

            modelBuilder.Entity<Visitor>()
                .Property(e => e.municipality)
                .IsUnicode(false);

            modelBuilder.Entity<Visitor>()
                .HasMany(e => e.Answers)
                .WithRequired(e => e.Visitor1)
                .HasForeignKey(e => e.visitor)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Visitor>()
                .HasMany(e => e.Visits)
                .WithRequired(e => e.Visitor1)
                .HasForeignKey(e => e.visitor)
                .WillCascadeOnDelete(false);
        }
    }
}
