﻿// <auto-generated />
using dotnetconsulting.Samples.Domains;
using dotnetconsulting.Samples.EFContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace dotnetconsulting.Samples.EFContext.Migrations
{
    [DbContext(typeof(SamplesContext1))]
    [Migration("20171102070206_Next01")]
    partial class Next01
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("dnc")
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("dotnetconsulting.Samples.Domains.Session", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Abstract")
                        .IsRequired()
                        .HasColumnName("ContentDescription")
                        .HasMaxLength(300);

                    b.Property<DateTime>("Begin")
                        .HasColumnType("time");

                    b.Property<DateTime>("Created")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("getdate()");

                    b.Property<int>("Difficulty");

                    b.Property<int>("Duration");

                    b.Property<DateTime>("End");

                    b.Property<int?>("EventId");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<int>("SpeakerId");

                    b.Property<int>("TechEventId");

                    b.Property<string>("Title");

                    b.Property<DateTime?>("Updated")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(null);

                    b.HasKey("Id");

                    b.HasIndex("TechEventId");

                    b.HasIndex("Title");

                    b.ToTable("Sessions");
                });

            modelBuilder.Entity("dotnetconsulting.Samples.Domains.Speaker", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("Homepage");

                    b.Property<string>("Infos")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue("(Keine Infos)");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<string>("Name");

                    b.Property<DateTime?>("Updated")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(null);

                    b.HasKey("Id");

                    b.ToTable("Speakers");
                });

            modelBuilder.Entity("dotnetconsulting.Samples.Domains.SpeakerSession", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<bool>("IsDeleted");

                    b.Property<int>("SessionId");

                    b.Property<int>("SpeakerId");

                    b.Property<string>("Tag");

                    b.Property<DateTime?>("Updated");

                    b.HasKey("Id");

                    b.HasIndex("SessionId");

                    b.HasIndex("SpeakerId");

                    b.ToTable("SpeakerSessions");
                });

            modelBuilder.Entity("dotnetconsulting.Samples.Domains.TechEvent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Begin");

                    b.Property<DateTime>("Created");

                    b.Property<DateTime>("End");

                    b.Property<string>("ImageUrl");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Name");

                    b.Property<decimal>("Price");

                    b.Property<DateTime?>("Updated");

                    b.Property<string>("Venue");

                    b.Property<int?>("VenueSetupId");

                    b.Property<string>("WebSite");

                    b.HasKey("Id");

                    b.HasIndex("VenueSetupId")
                        .IsUnique()
                        .HasFilter("[VenueSetupId] IS NOT NULL");

                    b.ToTable("TechEvents");
                });

            modelBuilder.Entity("dotnetconsulting.Samples.Domains.VenueSetup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<string>("Description");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("Updated");

                    b.HasKey("Id");

                    b.ToTable("VenueSetup");
                });

            modelBuilder.Entity("dotnetconsulting.Samples.Domains.Session", b =>
                {
                    b.HasOne("dotnetconsulting.Samples.Domains.TechEvent", "TechEvent")
                        .WithMany("Sessions")
                        .HasForeignKey("TechEventId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("dotnetconsulting.Samples.Domains.SpeakerSession", b =>
                {
                    b.HasOne("dotnetconsulting.Samples.Domains.Session", "Session")
                        .WithMany("SpeakerSessions")
                        .HasForeignKey("SessionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("dotnetconsulting.Samples.Domains.Speaker", "Speaker")
                        .WithMany("SpeakerSessions")
                        .HasForeignKey("SpeakerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("dotnetconsulting.Samples.Domains.TechEvent", b =>
                {
                    b.HasOne("dotnetconsulting.Samples.Domains.VenueSetup", "VenueSetup")
                        .WithOne("TechEvent")
                        .HasForeignKey("dotnetconsulting.Samples.Domains.TechEvent", "VenueSetupId");
                });
#pragma warning restore 612, 618
        }
    }
}
