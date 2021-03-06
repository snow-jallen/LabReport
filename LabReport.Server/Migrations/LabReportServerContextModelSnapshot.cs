﻿// <auto-generated />
using System;
using LabReport.Server.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LabReport.Server.Migrations
{
    [DbContext(typeof(LabReportServerContext))]
    partial class LabReportServerContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.1-servicing-10028")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("LabReport.Shared.ReportItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("HostName");

                    b.Property<string>("ImageVersionContent");

                    b.Property<string>("MacNumber");

                    b.Property<DateTime>("ReportTime");

                    b.Property<string>("SerialNumber");

                    b.HasKey("Id");

                    b.ToTable("ReportItem");
                });
#pragma warning restore 612, 618
        }
    }
}
