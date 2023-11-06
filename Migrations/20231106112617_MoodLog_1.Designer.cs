﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Well_Up_API.Models;

#nullable disable

namespace Well_Up_API.Migrations
{
    [DbContext(typeof(PostgresDbContext))]
    [Migration("20231106112617_MoodLog_1")]
    partial class MoodLog_1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Well_Up_API.Models.Mood", b =>
                {
                    b.Property<int>("MoodId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("MoodId"));

                    b.Property<string>("MoodName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("PositionX")
                        .HasColumnType("integer");

                    b.Property<int>("PositionY")
                        .HasColumnType("integer");

                    b.HasKey("MoodId");

                    b.ToTable("Mood");
                });

            modelBuilder.Entity("Well_Up_API.Models.MoodLog", b =>
                {
                    b.Property<int>("MoodLogId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("MoodLogId"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("MoodId")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("MoodLogId");

                    b.HasIndex("MoodId");

                    b.HasIndex("UserId");

                    b.ToTable("MoodLog");
                });

            modelBuilder.Entity("Well_Up_API.Models.TestModel", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Test");
                });

            modelBuilder.Entity("Well_Up_API.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("UserId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Well_Up_API.Models.MoodLog", b =>
                {
                    b.HasOne("Well_Up_API.Models.Mood", "Mood")
                        .WithMany("MoodLogs")
                        .HasForeignKey("MoodId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Well_Up_API.Models.User", "User")
                        .WithMany("MoodLogs")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Mood");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Well_Up_API.Models.Mood", b =>
                {
                    b.Navigation("MoodLogs");
                });

            modelBuilder.Entity("Well_Up_API.Models.User", b =>
                {
                    b.Navigation("MoodLogs");
                });
#pragma warning restore 612, 618
        }
    }
}
