namespace Quizzie.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class QuizzieDBContext : DbContext
    {
        public QuizzieDBContext()
            : base("name=QuizzieDB")
        {
        }

        public virtual DbSet<Quiz> Quizs { get; set; }
        public virtual DbSet<QuizCreator> QuizCreators { get; set; }
        public virtual DbSet<QuizQuestion> QuizQuestions { get; set; }
        public virtual DbSet<QuizQuestionAnswer> QuizQuestionAnswers { get; set; }
        public virtual DbSet<QuizResult> QuizResults { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Quiz>()
                .HasMany(e => e.QuizQuestions)
                .WithRequired(e => e.Quiz)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Quiz>()
                .HasMany(e => e.QuizResults)
                .WithRequired(e => e.Quiz)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<QuizCreator>()
                .HasMany(e => e.Quizs)
                .WithRequired(e => e.QuizCreator)
                .HasForeignKey(e => e.CreatedBy)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<QuizQuestion>()
                .HasMany(e => e.QuizQuestionAnswers)
                .WithRequired(e => e.QuizQuestion)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<QuizResult>()
                .HasOptional(e => e.QuizResult1)
                .WithRequired(e => e.QuizResult2);
        }
    }
}
