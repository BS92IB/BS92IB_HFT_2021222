using BS92IB_HFT_2021222.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS92IB_HFT_2021222.Data
{
    public class NavyDbContext : DbContext
    {
        public NavyDbContext()
        {
            Database.EnsureCreated();
        }

        public virtual DbSet<Fleet> Fleets { get; set; }
        public virtual DbSet<Ship> Ships { get; set; }
        public virtual DbSet<Armament> Armaments { get; set; }
        public virtual DbSet<Weapon> Weapons { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseInMemoryDatabase("navyDb")
                    .UseLazyLoadingProxies();
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Fleet dd6 = new Fleet { Id = 1, Name = "IJN Destroyer Division 6" };

            // helper to create armaments and weapon definitions along with the ship
            SeedDataBuilder dataBuilder = new SeedDataBuilder();

            Ship[] ships = new Ship[]
            {
                dataBuilder.BuildAkatsukiClass(dd6.Id, "Akatsuki"),
                dataBuilder.BuildAkatsukiClass(dd6.Id, "Hibiki"),
                dataBuilder.BuildAkatsukiClass(dd6.Id, "Ikazuchi"),
                dataBuilder.BuildAkatsukiClass(dd6.Id, "Inazuma")
            };

            modelBuilder.Entity<Fleet>().HasData(dd6);
            modelBuilder.Entity<Ship>().HasData(ships);
            modelBuilder.Entity<Armament>().HasData(dataBuilder.Armaments);
            modelBuilder.Entity<Weapon>().HasData(dataBuilder.Weapons);



            modelBuilder.Entity<Ship>(entity =>
            {
                // Ship <=N===1=> Fleet
                entity
                    .HasOne(ship => ship.Fleet)
                    .WithMany(fleet => fleet.Ships)
                    .HasForeignKey(ship => ship.FleetId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Armament>(entity =>
            {
                // Armament <=N===1=> Ship
                entity
                    .HasOne(a => a.Ship)
                    .WithMany(s => s.Armaments)
                    .HasForeignKey(a => a.ShipId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                // Armament ======1=> Weapon
                entity
                    .HasOne(a => a.Weapon)
                    .WithMany()
                    .HasForeignKey(a => a.WeaponId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });
        }
    }
}
