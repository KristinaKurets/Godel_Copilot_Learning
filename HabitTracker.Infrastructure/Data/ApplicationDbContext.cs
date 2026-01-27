using HabitTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HabitTracker.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Habit> Habits { get; set; }
    public DbSet<HabitCompletion> HabitCompletions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Habit>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Category).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.CreatedAt).IsRequired();
            
            entity.HasMany(h => h.Completions)
                .WithOne(c => c.Habit)
                .HasForeignKey(c => c.HabitId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<HabitCompletion>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.CompletedDate).IsRequired();
            entity.HasIndex(e => new { e.HabitId, e.CompletedDate });
        });

        SeedData(modelBuilder);
    }

    private void SeedData(ModelBuilder modelBuilder)
    {
        var now = DateTime.UtcNow;
        var today = now.Date;

        modelBuilder.Entity<Habit>().HasData(
            new Habit
            {
                Id = 1,
                Name = "Morning Exercise",
                Category = "Fitness",
                Description = "30 minutes of morning workout",
                CreatedAt = now.AddDays(-30),
                IsActive = true
            },
            new Habit
            {
                Id = 2,
                Name = "Read Books",
                Category = "Learning",
                Description = "Read for at least 20 minutes",
                CreatedAt = now.AddDays(-20),
                IsActive = true
            },
            new Habit
            {
                Id = 3,
                Name = "Drink Water",
                Category = "Health",
                Description = "Drink 8 glasses of water",
                CreatedAt = now.AddDays(-15),
                IsActive = true
            },
            new Habit
            {
                Id = 4,
                Name = "Meditation",
                Category = "Wellness",
                Description = "10 minutes of mindfulness meditation",
                CreatedAt = now.AddDays(-10),
                IsActive = true
            }
        );

        var completions = new List<HabitCompletion>();
        int completionId = 1;

        // Morning Exercise - 7 day streak (including today)
        for (int i = 6; i >= 0; i--)
        {
            completions.Add(new HabitCompletion
            {
                Id = completionId++,
                HabitId = 1,
                CompletedDate = today.AddDays(-i)
            });
        }

        // Read Books - 5 day streak (including today)
        for (int i = 4; i >= 0; i--)
        {
            completions.Add(new HabitCompletion
            {
                Id = completionId++,
                HabitId = 2,
                CompletedDate = today.AddDays(-i)
            });
        }

        // Drink Water - broken streak (last completed 3 days ago)
        for (int i = 10; i >= 3; i--)
        {
            completions.Add(new HabitCompletion
            {
                Id = completionId++,
                HabitId = 3,
                CompletedDate = today.AddDays(-i)
            });
        }

        // Meditation - 3 day streak (including today)
        for (int i = 2; i >= 0; i--)
        {
            completions.Add(new HabitCompletion
            {
                Id = completionId++,
                HabitId = 4,
                CompletedDate = today.AddDays(-i)
            });
        }

        modelBuilder.Entity<HabitCompletion>().HasData(completions);
    }
}

