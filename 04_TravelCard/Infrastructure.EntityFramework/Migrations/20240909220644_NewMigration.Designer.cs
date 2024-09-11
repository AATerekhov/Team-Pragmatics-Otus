﻿// <auto-generated />
using System;
using Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.EntityFramework.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20240909220644_NewMigration")]
    partial class NewMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entities.Travel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("travel_ID");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("Deleted")
                        .HasColumnType("boolean")
                        .HasColumnName("deleted");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("description");

                    b.Property<string>("FinishPoint")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("finish_point");

                    b.Property<bool>("IsPrivate")
                        .HasColumnType("boolean")
                        .HasColumnName("is_private");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("start_date");

                    b.Property<string>("StartPoint")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("start_point");

                    b.Property<Guid>("UserID")
                        .HasColumnType("uuid")
                        .HasColumnName("user_ID");

                    b.HasKey("Id");

                    b.HasIndex("UserID");

                    b.ToTable("Travels");
                });

            modelBuilder.Entity("Domain.Entities.TravelPoint", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("tp_ID");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("Deleted")
                        .HasColumnType("boolean")
                        .HasColumnName("deleted");

                    b.Property<string>("PointDesc")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("point_desc");

                    b.Property<string>("PointMap")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("point_map");

                    b.Property<int>("TravelId")
                        .HasColumnType("integer")
                        .HasColumnName("travel_ID");

                    b.Property<double>("WaitingTimeCountMinutes")
                        .HasColumnType("double precision")
                        .HasColumnName("waiting_time");

                    b.HasKey("Id");

                    b.HasIndex("TravelId");

                    b.ToTable("Travel_point");
                });

            modelBuilder.Entity("Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("user_ID");

                    b.Property<DateTime>("DateRegistration")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("Deleted")
                        .HasColumnType("boolean")
                        .HasColumnName("deleted");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int?>("TravelId")
                        .HasColumnType("integer")
                        .HasColumnName("travel_ID");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Domain.Entities.Travel", b =>
                {
                    b.HasOne("Domain.Entities.User", "User")
                        .WithMany("Travels")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Entities.TravelPoint", b =>
                {
                    b.HasOne("Domain.Entities.Travel", "Travel")
                        .WithMany("TravelPoints")
                        .HasForeignKey("TravelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Travel");
                });

            modelBuilder.Entity("Domain.Entities.Travel", b =>
                {
                    b.Navigation("TravelPoints");
                });

            modelBuilder.Entity("Domain.Entities.User", b =>
                {
                    b.Navigation("Travels");
                });
#pragma warning restore 612, 618
        }
    }
}