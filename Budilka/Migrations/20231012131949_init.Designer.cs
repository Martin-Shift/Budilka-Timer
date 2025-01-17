﻿// <auto-generated />
using System;
using Budilka.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Budilka.Migrations
{
    [DbContext(typeof(TimerDbContext))]
    [Migration("20231012131949_init")]
    partial class init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.12");

            modelBuilder.Entity("Budilka.DbModels.LiveTimer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("SoundId")
                        .HasColumnType("INTEGER");

                    b.Property<TimeSpan>("TimeSpan")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("SoundId");

                    b.ToTable("Timers");
                });

            modelBuilder.Entity("Budilka.DbModels.SoundFile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Filename")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("RootDirectory")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Sounds");
                });

            modelBuilder.Entity("Budilka.DbModels.LiveTimer", b =>
                {
                    b.HasOne("Budilka.DbModels.SoundFile", "Sound")
                        .WithMany()
                        .HasForeignKey("SoundId");

                    b.Navigation("Sound");
                });
#pragma warning restore 612, 618
        }
    }
}
