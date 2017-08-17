﻿// <auto-generated />
using Data.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace Data.DataAccess.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Data.Entities.Article", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Author");

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<string>("Description");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("Name");

                    b.Property<string>("Path");

                    b.Property<string>("Route");

                    b.Property<int>("SubSectionId");

                    b.HasKey("Id");

                    b.HasIndex("SubSectionId");

                    b.ToTable("Article");
                });

            modelBuilder.Entity("Data.Entities.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ArticleId");

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("UserComment");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("ArticleId");

                    b.HasIndex("UserId");

                    b.ToTable("Comment");
                });

            modelBuilder.Entity("Data.Entities.Section", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Active");

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<string>("Description");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("Name");

                    b.Property<int>("Order");

                    b.HasKey("Id");

                    b.ToTable("Section");
                });

            modelBuilder.Entity("Data.Entities.SubSection", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Active");

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<string>("Description");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("Name");

                    b.Property<int>("Order");

                    b.Property<int>("SectionId");

                    b.HasKey("Id");

                    b.HasIndex("SectionId");

                    b.ToTable("SubSection");
                });

            modelBuilder.Entity("Data.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ContactNumber");

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<string>("Email");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Data.Entities.Article", b =>
                {
                    b.HasOne("Data.Entities.SubSection", "SubSection")
                        .WithMany("Articles")
                        .HasForeignKey("SubSectionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Data.Entities.Comment", b =>
                {
                    b.HasOne("Data.Entities.Article", "Article")
                        .WithMany("Comments")
                        .HasForeignKey("ArticleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Data.Entities.User", "User")
                        .WithMany("Comments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Data.Entities.SubSection", b =>
                {
                    b.HasOne("Data.Entities.Section", "Section")
                        .WithMany("Childs")
                        .HasForeignKey("SectionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
