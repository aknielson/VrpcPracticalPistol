using System.Data.Entity;
using DomainClasses;

namespace DataModel
{
    public partial class VrpcPracticalPistolContext : DbContext
    {
        public VrpcPracticalPistolContext()
            : base("name=VrpcPracticalPistolContext")
        {
        }

        public virtual DbSet<Competitor> Competitors { get; set; }
        public virtual DbSet<CompetitorStage> CompetitorStages { get; set; }
        public virtual DbSet<Match> Matches { get; set; }
        public virtual DbSet<MatchStageTime> MatchStageTimes { get; set; }
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<Stage> Stages { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Competitor>()
                .HasMany(e => e.CompetitorStages)
                .WithOptional(e => e.Competitor)
                .HasForeignKey(e => e.Competitor_Id);

            modelBuilder.Entity<CompetitorStage>()
                .HasMany(e => e.MatchStageTimes)
                .WithOptional(e => e.CompetitorStage)
                .HasForeignKey(e => e.CompetitorStage_Id);

            modelBuilder.Entity<Match>()
                .HasMany(e => e.Competitors)
                .WithOptional(e => e.Match)
                .HasForeignKey(e => e.Match_Id);

            modelBuilder.Entity<Match>()
                .HasMany(e => e.CompetitorStages)
                .WithOptional(e => e.Match)
                .HasForeignKey(e => e.Match_Id);

            modelBuilder.Entity<Match>()
                .HasMany(e => e.Stages)
                .WithOptional(e => e.Match)
                .HasForeignKey(e => e.Match_Id);

            modelBuilder.Entity<Member>()
                .HasMany(e => e.Competitors)
                .WithOptional(e => e.Member)
                .HasForeignKey(e => e.Member_Id);

            modelBuilder.Entity<Member>()
                .HasMany(e => e.Stages)
                .WithOptional(e => e.Member)
                .HasForeignKey(e => e.Designer_Id);

            modelBuilder.Entity<Stage>()
                .HasMany(e => e.CompetitorStages)
                .WithOptional(e => e.Stage)
                .HasForeignKey(e => e.Stage_Id);
        }
    }
}
