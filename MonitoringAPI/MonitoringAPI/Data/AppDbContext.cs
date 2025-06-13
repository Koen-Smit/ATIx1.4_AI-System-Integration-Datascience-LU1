using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

public class AppDbContext : IdentityDbContext<
    ApplicationUser,
    ApplicationRole,
    int,
    IdentityUserClaim<int>,
    ApplicationUserRole,
    IdentityUserLogin<int>,
    IdentityRoleClaim<int>,
    IdentityUserToken<int>>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    public DbSet<Trash> Trash { get; set; }
    public DbSet<Camera> Cameras { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ApplicationUserRole>()
        .HasKey(ur => new { ur.UserId, ur.RoleId });

        modelBuilder.Entity<ApplicationUserRole>()
            .HasOne(ur => ur.User)
            .WithMany(u => u.UserRoles)
            .HasForeignKey(ur => ur.UserId);

        modelBuilder.Entity<ApplicationUserRole>()
            .HasOne(ur => ur.Role)
            .WithMany(r => r.UserRoles)
            .HasForeignKey(ur => ur.RoleId);

        modelBuilder.Entity<Camera>()
            .HasKey(c => c.Id);

        modelBuilder.Entity<Trash>()
            .HasOne(t => t.Camera)
            .WithMany(c => c.TrashRecords)
            .HasForeignKey(t => t.CameraId)
            .OnDelete(DeleteBehavior.Cascade);





        modelBuilder.Entity<Camera>().HasData(
            new Camera
            {
                Id = 1,
                Naam = "Camera Noord",
                Latitude = 51.9239,
                Longitude = 4.4699,
                Postcode = "3011AA"
            },
            new Camera
            {
                Id = 2,
                Naam = "Camera Zuid",
                Latitude = 51.9225,
                Longitude = 4.4790,
                Postcode = "3011AB"
            },
            new Camera
            {
                Id = 3,
                Naam = "Camera Oost",
                Latitude = 51.9240,
                Longitude = 4.4700,
                Postcode = "3011AC"
            },
            new Camera
            {
                Id = 4,
                Naam = "Camera West",
                Latitude = 51.9250,
                Longitude = 4.4680,
                Postcode = "3011AD"
            }
        );

        modelBuilder.Entity<Trash>().HasData(
            new Trash
            {
                Id = 1,
                DateCollected = new DateTime(2025, 6, 8),
                DagCategorie = "Werkdag",
                TypeAfval = "Plastic",
                WindRichting = "NO",
                Temperatuur = 17.2,
                WeerOmschrijving = "Licht bewolkt",
                Confidence = 0.85,
                CameraId = 1
            },
            new Trash
            {
                Id = 2,
                DateCollected = new DateTime(2025, 6, 9),
                DagCategorie = "Weekend",
                TypeAfval = "Metaal",
                WindRichting = "ZW",
                Temperatuur = 19.5,
                WeerOmschrijving = "Zonnig",
                Confidence = 0.92,
                CameraId = 1
            },
            new Trash
            {
                Id = 3,
                DateCollected = new DateTime(2025, 6, 10),
                DagCategorie = "Werkdag",
                TypeAfval = "Papier",
                WindRichting = "Z",
                Temperatuur = 20.0,
                WeerOmschrijving = "Zonnig",
                Confidence = 0.90,
                CameraId = 2
            },
            new Trash
            {
                Id = 4,
                DateCollected = new DateTime(2025, 6, 11),
                DagCategorie = "Weekend",
                TypeAfval = "Glas",
                WindRichting = "O",
                Temperatuur = 18.5,
                WeerOmschrijving = "Bewolkt",
                Confidence = 0.88,
                CameraId = 2
            },
            new Trash
            {
                Id = 5,
                DateCollected = new DateTime(2025, 6, 12),
                DagCategorie = "Werkdag",
                TypeAfval = "Grofvuil",
                WindRichting = "NW",
                Temperatuur = 16.0,
                WeerOmschrijving = "Regenachtig",
                Confidence = 0.80,
                CameraId = 3
            },
            new Trash
            {
                Id = 6,
                DateCollected = new DateTime(2025, 6, 13),
                DagCategorie = "Weekend",
                TypeAfval = "Textiel",
                WindRichting = "Z",
                Temperatuur = 21.0,
                WeerOmschrijving = "Zonnig",
                Confidence = 0.95,
                CameraId = 3
            },
            new Trash
            {
                Id = 7,
                DateCollected = new DateTime(2025, 6, 14),
                DagCategorie = "Werkdag",
                TypeAfval = "Kruidenierswaren",
                WindRichting = "O",
                Temperatuur = 19.0,
                WeerOmschrijving = "Licht bewolkt",
                Confidence = 0.87,
                CameraId = 4
            },
            new Trash
            {
                Id = 8,
                DateCollected = new DateTime(2025, 6, 15),
                DagCategorie = "Weekend",
                TypeAfval = "Hout",
                WindRichting = "ZW",
                Temperatuur = 22.0,
                WeerOmschrijving = "Zonnig",
                Confidence = 0.93,
                CameraId = 4
            },
            new Trash
            {
                Id = 9,
                DateCollected = new DateTime(2025, 6, 16),
                DagCategorie = "Werkdag",
                TypeAfval = "Snoeiafval",
                WindRichting = "NO",
                Temperatuur = 18.0,
                WeerOmschrijving = "Regenachtig",
                Confidence = 0.82,
                CameraId = 1
            },
            new Trash
            {
                Id = 10,
                DateCollected = new DateTime(2025, 6, 17),
                DagCategorie = "Weekend",
                TypeAfval = "Batterijen",
                WindRichting = "Z",
                Temperatuur = 20.5,
                WeerOmschrijving = "Licht bewolkt",
                Confidence = 0.89,
                CameraId = 2
            },
            new Trash
            {
                Id = 11,
                DateCollected = new DateTime(2025, 6, 18),
                DagCategorie = "Werkdag",
                TypeAfval = "Elektronica",
                WindRichting = "O",
                Temperatuur = 17.5,
                WeerOmschrijving = "Zonnig",
                Confidence = 0.91,
                CameraId = 3
            },
            new Trash
            {
                Id = 12,
                DateCollected = new DateTime(2025, 6, 19),
                DagCategorie = "Weekend",
                TypeAfval = "Kunststof",
                WindRichting = "NW",
                Temperatuur = 16.5,
                WeerOmschrijving = "Bewolkt",
                Confidence = 0.84,
                CameraId = 4
            },
            new Trash
            {
                Id = 13,
                DateCollected = new DateTime(2025, 6, 20),
                DagCategorie = "Werkdag",
                TypeAfval = "Groente- en fruitafval",
                WindRichting = "Z",
                Temperatuur = 19.0,
                WeerOmschrijving = "Zonnig",
                Confidence = 0.86,
                CameraId = 1
            },
            new Trash
            {
                Id = 14,
                DateCollected = new DateTime(2025, 6, 21),
                DagCategorie = "Weekend",
                TypeAfval = "Restafval",
                WindRichting = "NO",
                Temperatuur = 21.5,
                WeerOmschrijving = "Licht bewolkt",
                Confidence = 0.94,
                CameraId = 2
            },
            new Trash
            {
                Id = 15,
                DateCollected = new DateTime(2025, 6, 22),
                DagCategorie = "Werkdag",
                TypeAfval = "Papier en karton",
                WindRichting = "ZW",
                Temperatuur = 18.0,
                WeerOmschrijving = "Regenachtig",
                Confidence = 0.83,
                CameraId = 3
            },
            new Trash
            {
                Id = 16,
                DateCollected = new DateTime(2025, 6, 23),
                DagCategorie = "Weekend",
                TypeAfval = "Metaal en kunststof",
                WindRichting = "O",
                Temperatuur = 20.0,
                WeerOmschrijving = "Zonnig",
                Confidence = 0.90,
                CameraId = 4
            },
            new Trash
            {
                Id = 17,
                DateCollected = new DateTime(2025, 6, 24),
                DagCategorie = "Werkdag",
                TypeAfval = "Grofvuil en hout",
                WindRichting = "NW",
                Temperatuur = 17.0,
                WeerOmschrijving = "Licht bewolkt",
                Confidence = 0.88,
                CameraId = 1
            },
            new Trash
            {
                Id = 18,
                DateCollected = new DateTime(2025, 6, 25),
                DagCategorie = "Weekend",
                TypeAfval = "Textiel en kleding",
                WindRichting = "Z",
                Temperatuur = 19.5,
                WeerOmschrijving = "Zonnig",
                Confidence = 0.92,
                CameraId = 2
            },
            new Trash
            {
                Id = 19,
                DateCollected = new DateTime(2025, 6, 26),
                DagCategorie = "Werkdag",
                TypeAfval = "Snoeiafval en takken",
                WindRichting = "O",
                Temperatuur = 18.5,
                WeerOmschrijving = "Regenachtig",
                Confidence = 0.85,
                CameraId = 3
            },
            new Trash
            {
                Id = 20,
                DateCollected = new DateTime(2025, 6, 27),
                DagCategorie = "Weekend",
                TypeAfval = "Batterijen en accu's",
                WindRichting = "ZW",
                Temperatuur = 20.5,
                WeerOmschrijving = "Licht bewolkt",
                Confidence = 0.93,
                CameraId = 4
            }
        );
    }
}
