﻿// <auto-generated />
using System;
using Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.EntityFramework.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("description");

                    b.Property<string>("FinishPoint")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("finish_point");

                    b.Property<string>("StartPoint")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("start_point");

                    b.HasKey("Id");

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
                        .HasMaxLength(80)
                        .HasColumnType("character varying(80)")
                        .HasColumnName("point_desc");

                    b.Property<string>("PointMap")
                        .IsRequired()
                        .HasMaxLength(90)
                        .HasColumnType("character varying(90)")
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
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("user_ID");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("Deleted")
                        .HasColumnType("boolean")
                        .HasColumnName("deleted");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("login");

                    b.Property<int?>("TravelId")
                        .HasColumnType("integer")
                        .HasColumnName("travel_ID");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TravelUser", b =>
                {
                    b.Property<int>("TravelsId")
                        .HasColumnType("integer");

                    b.Property<int>("UsersId")
                        .HasColumnType("integer");

                    b.HasKey("TravelsId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("TravelUser");
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

            modelBuilder.Entity("TravelUser", b =>
                {
                    b.HasOne("Domain.Entities.Travel", null)
                        .WithMany()
                        .HasForeignKey("TravelsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.Travel", b =>
                {
                    b.Navigation("TravelPoints");
                });
#pragma warning restore 612, 618
        }
    }
}
