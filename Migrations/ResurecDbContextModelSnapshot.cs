﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using resurec.DbContexts;

#nullable disable

namespace resurec.Migrations
{
    [DbContext(typeof(ResurecDbContext))]
    partial class ResurecDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.0");

            modelBuilder.Entity("resurec.DTOs.RecordingDTO", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<float>("CpuTemperature")
                        .HasColumnType("REAL");

                    b.Property<float>("CpuUsage")
                        .HasColumnType("REAL");

                    b.Property<TimeSpan>("Duration")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("TEXT");

                    b.Property<float>("GpuTemperature")
                        .HasColumnType("REAL");

                    b.Property<float>("GpuUsage")
                        .HasColumnType("REAL");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<float>("RamUsage")
                        .HasColumnType("REAL");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Recordings");
                });
#pragma warning restore 612, 618
        }
    }
}
