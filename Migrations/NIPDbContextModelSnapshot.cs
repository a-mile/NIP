using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using NIP.Models;

namespace NIP.Migrations
{
    [DbContext(typeof(NIPDbContext))]
    partial class NIPDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1");

            modelBuilder.Entity("NIP.Models.Business", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("City");

                    b.Property<string>("KRS");

                    b.Property<string>("NIP");

                    b.Property<string>("Name");

                    b.Property<int>("Number");

                    b.Property<string>("REGON");

                    b.Property<string>("Street");

                    b.Property<string>("ZIPCode");

                    b.HasKey("Id");

                    b.ToTable("Businesses");
                });

            modelBuilder.Entity("NIP.Models.Log", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BusinessId");

                    b.Property<DateTime>("Date");

                    b.Property<string>("Headers");

                    b.HasKey("Id");

                    b.HasIndex("BusinessId");

                    b.ToTable("Logs");
                });

            modelBuilder.Entity("NIP.Models.Log", b =>
                {
                    b.HasOne("NIP.Models.Business", "Business")
                        .WithMany("Logs")
                        .HasForeignKey("BusinessId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
