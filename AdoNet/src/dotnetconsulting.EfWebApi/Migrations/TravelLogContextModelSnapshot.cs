using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using dotnetconsulting.EfWebApi.DAL;

namespace dotnetconsulting.EfWebApi.Migrations
{
    [DbContext(typeof(TravelLogContext))]
    partial class TravelLogContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("dotnetconsulting.EfWebApi.DAL.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Author");

                    b.Property<int?>("TravelDayId");

                    b.HasKey("Id");

                    b.HasIndex("TravelDayId");

                    b.ToTable("Comment");
                });

            modelBuilder.Entity("dotnetconsulting.EfWebApi.DAL.TravelDay", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Text");

                    b.Property<string>("Title");

                    b.Property<int?>("VlogId");

                    b.HasKey("Id");

                    b.HasIndex("VlogId");

                    b.ToTable("TravelDay");
                });

            modelBuilder.Entity("dotnetconsulting.EfWebApi.DAL.TravelVlog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("End");

                    b.Property<string>("Name");

                    b.Property<DateTime>("Start");

                    b.HasKey("Id");

                    b.ToTable("TravelLogs");
                });

            modelBuilder.Entity("dotnetconsulting.EfWebApi.DAL.Comment", b =>
                {
                    b.HasOne("dotnetconsulting.EfWebApi.DAL.TravelDay")
                        .WithMany("Comments")
                        .HasForeignKey("TravelDayId");
                });

            modelBuilder.Entity("dotnetconsulting.EfWebApi.DAL.TravelDay", b =>
                {
                    b.HasOne("dotnetconsulting.EfWebApi.DAL.TravelVlog", "Vlog")
                        .WithMany("TravelDays")
                        .HasForeignKey("VlogId");
                });
        }
    }
}
