using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ConestogaCarpool.Models
{
    public partial class ConestogaCarpoolContext : DbContext
    {
        public ConestogaCarpoolContext()
        {
        }

        public ConestogaCarpoolContext(DbContextOptions<ConestogaCarpoolContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Driver> Driver { get; set; }
        public virtual DbSet<LicenceClass> LicenceClass { get; set; }
        public virtual DbSet<Post> Post { get; set; }
        public virtual DbSet<PostStatus> PostStatus { get; set; }
        public virtual DbSet<Request> Request { get; set; }
        public virtual DbSet<RequestStatus> RequestStatus { get; set; }
        public virtual DbSet<Review> Review { get; set; }
        public virtual DbSet<Ride> Ride { get; set; }
        public virtual DbSet<RideStatus> RideStatus { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserImage> UserImage { get; set; }
        public virtual DbSet<Vehicle> Vehicle { get; set; }
        public virtual DbSet<VehicleImage> VehicleImage { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Driver>(entity =>
            {
                entity.Property(e => e.DriverId).HasColumnName("driverId");

                entity.Property(e => e.Experience).HasColumnName("experience");

                entity.Property(e => e.LicenceClassId).HasColumnName("licenceClassId");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.LicenceClass)
                    .WithMany(p => p.Driver)
                    .HasForeignKey(d => d.LicenceClassId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Driver_LicenceClass");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Driver)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Driver_User");
            });

            modelBuilder.Entity<LicenceClass>(entity =>
            {
                entity.Property(e => e.LicenceClassId).HasColumnName("licenceClassId");

                entity.Property(e => e.LicenceClass1)
                    .IsRequired()
                    .HasColumnName("licenceClass")
                    .HasMaxLength(5)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.Property(e => e.PostId).HasColumnName("postId");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("date");

                entity.Property(e => e.Destination)
                    .IsRequired()
                    .HasColumnName("destination")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.DriverId).HasColumnName("driverId");

                entity.Property(e => e.Location)
                    .IsRequired()
                    .HasColumnName("location")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.PostStatusId).HasColumnName("postStatusId");

                entity.Property(e => e.Time).HasColumnName("time");

                entity.Property(e => e.VehicleId).HasColumnName("vehicleId");

                entity.HasOne(d => d.Driver)
                    .WithMany(p => p.Post)
                    .HasForeignKey(d => d.DriverId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Post_Driver");

                entity.HasOne(d => d.PostStatus)
                    .WithMany(p => p.Post)
                    .HasForeignKey(d => d.PostStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Post_PostStatus");

                entity.HasOne(d => d.Vehicle)
                    .WithMany(p => p.Post)
                    .HasForeignKey(d => d.VehicleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Post_Vehicle");
            });

            modelBuilder.Entity<PostStatus>(entity =>
            {
                entity.Property(e => e.PostStatusId).HasColumnName("postStatusId");

                entity.Property(e => e.PostStatusDescription)
                    .IsRequired()
                    .HasColumnName("postStatusDescription")
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Request>(entity =>
            {
                entity.Property(e => e.RequestId).HasColumnName("requestId");

                entity.Property(e => e.PassengerId).HasColumnName("passengerId");

                entity.Property(e => e.PostId).HasColumnName("postId");

                entity.Property(e => e.RequestStatusId).HasColumnName("requestStatusId");

                entity.HasOne(d => d.Passenger)
                    .WithMany(p => p.Request)
                    .HasForeignKey(d => d.PassengerId)
                    .HasConstraintName("FK_Request_User");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Request)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Request_Post");

                entity.HasOne(d => d.RequestStatus)
                    .WithMany(p => p.Request)
                    .HasForeignKey(d => d.RequestStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Request_RequestStatus");
            });

            modelBuilder.Entity<RequestStatus>(entity =>
            {
                entity.Property(e => e.RequestStatusId).HasColumnName("requestStatusId");

                entity.Property(e => e.RequestStatusDescription)
                    .IsRequired()
                    .HasColumnName("requestStatusDescription")
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.Property(e => e.ReviewId).HasColumnName("reviewId");

                entity.Property(e => e.Comment)
                    .IsRequired()
                    .HasColumnName("comment")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.DriverId).HasColumnName("driverId");

                entity.Property(e => e.PassengerId).HasColumnName("passengerId");

                entity.Property(e => e.Rating).HasColumnName("rating");

                entity.Property(e => e.RideId).HasColumnName("rideId");

                entity.HasOne(d => d.Driver)
                    .WithMany(p => p.Review)
                    .HasForeignKey(d => d.DriverId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Review_Driver");

                entity.HasOne(d => d.Passenger)
                    .WithMany(p => p.Review)
                    .HasForeignKey(d => d.PassengerId)
                    .HasConstraintName("FK_Review_User");

                entity.HasOne(d => d.Ride)
                    .WithMany(p => p.Review)
                    .HasForeignKey(d => d.RideId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Review_Ride");
            });

            modelBuilder.Entity<Ride>(entity =>
            {
                entity.Property(e => e.RideId).HasColumnName("rideId");

                entity.Property(e => e.PostId).HasColumnName("postId");

                entity.Property(e => e.RequestId).HasColumnName("requestId");

                entity.Property(e => e.RideStatusId).HasColumnName("rideStatusId");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Ride)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ride_Post");

                entity.HasOne(d => d.Request)
                    .WithMany(p => p.Ride)
                    .HasForeignKey(d => d.RequestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ride_Request");

                entity.HasOne(d => d.RideStatus)
                    .WithMany(p => p.Ride)
                    .HasForeignKey(d => d.RideStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ride_RideStatus");
            });

            modelBuilder.Entity<RideStatus>(entity =>
            {
                entity.Property(e => e.RideStatusId).HasColumnName("rideStatusId");

                entity.Property(e => e.RideStatusDescription)
                    .IsRequired()
                    .HasColumnName("rideStatusDescription")
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Email)
                    .HasName("UQ__User__AB6E61644B3A3960")
                    .IsUnique();

                entity.HasIndex(e => e.Username)
                    .HasName("UQ__User__F3DBC572B7962F55")
                    .IsUnique();

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("firstName")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("lastName")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(255);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("username")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.VerifiedEmail)
                    .HasColumnName("verifiedEmail")
                    .HasMaxLength(3)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UserImage>(entity =>
            {
                entity.Property(e => e.UserImageId).HasColumnName("userImageId");

                entity.Property(e => e.Image)
                    .IsRequired()
                    .HasColumnName("image");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserImage)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_UserImage_User");
            });

            modelBuilder.Entity<Vehicle>(entity =>
            {
                entity.Property(e => e.VehicleId).HasColumnName("vehicleId");

                entity.Property(e => e.Colour)
                    .IsRequired()
                    .HasColumnName("colour")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Make)
                    .IsRequired()
                    .HasColumnName("make")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Model)
                    .IsRequired()
                    .HasColumnName("model")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Plate)
                    .HasColumnName("plate")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.Property(e => e.Year).HasColumnName("year");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Vehicle)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Vehicle_User");
            });

            modelBuilder.Entity<VehicleImage>(entity =>
            {
                entity.Property(e => e.VehicleImageId).HasColumnName("vehicleImageId");

                entity.Property(e => e.Image)
                    .IsRequired()
                    .HasColumnName("image");

                entity.Property(e => e.VehicleId).HasColumnName("vehicleId");

                entity.HasOne(d => d.Vehicle)
                    .WithMany(p => p.VehicleImage)
                    .HasForeignKey(d => d.VehicleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VehicleImage_Vehicle");
            });
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
