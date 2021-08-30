﻿// <auto-generated />
using System;
using Bowling.Models.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Bowling.Models.Migrations
{
    [DbContext(typeof(BowlingContext))]
    [Migration("20210828222629_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.9");

            modelBuilder.Entity("Bowling.Models.Frame", b =>
                {
                    b.Property<int>("FrameId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("FrameSequence")
                        .HasColumnType("INTEGER");

                    b.Property<int>("GameId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Roll1")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Roll2")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Roll3")
                        .HasColumnType("INTEGER");

                    b.HasKey("FrameId");

                    b.HasIndex("GameId");

                    b.ToTable("Frames");

                    b.HasData(
                        new
                        {
                            FrameId = 1,
                            FrameSequence = 0,
                            GameId = 1,
                            Roll1 = 10
                        },
                        new
                        {
                            FrameId = 2,
                            FrameSequence = 0,
                            GameId = 1,
                            Roll1 = 10
                        },
                        new
                        {
                            FrameId = 3,
                            FrameSequence = 0,
                            GameId = 1,
                            Roll1 = 10
                        },
                        new
                        {
                            FrameId = 4,
                            FrameSequence = 0,
                            GameId = 1,
                            Roll1 = 10
                        },
                        new
                        {
                            FrameId = 5,
                            FrameSequence = 0,
                            GameId = 1,
                            Roll1 = 10
                        },
                        new
                        {
                            FrameId = 6,
                            FrameSequence = 0,
                            GameId = 1,
                            Roll1 = 10
                        },
                        new
                        {
                            FrameId = 7,
                            FrameSequence = 0,
                            GameId = 1,
                            Roll1 = 10
                        },
                        new
                        {
                            FrameId = 8,
                            FrameSequence = 0,
                            GameId = 1,
                            Roll1 = 10
                        },
                        new
                        {
                            FrameId = 9,
                            FrameSequence = 0,
                            GameId = 1,
                            Roll1 = 10
                        },
                        new
                        {
                            FrameId = 10,
                            FrameSequence = 0,
                            GameId = 1,
                            Roll1 = 10,
                            Roll2 = 10,
                            Roll3 = 10
                        });
                });

            modelBuilder.Entity("Bowling.Models.Game", b =>
                {
                    b.Property<int>("GameId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("GameStarted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasDefaultValueSql("date('now')");

                    b.Property<int>("LastFrame")
                        .HasColumnType("INTEGER");

                    b.HasKey("GameId");

                    b.ToTable("Games");

                    b.HasData(
                        new
                        {
                            GameId = 1,
                            LastFrame = 0
                        });
                });

            modelBuilder.Entity("Bowling.Models.Frame", b =>
                {
                    b.HasOne("Bowling.Models.Game", "Game")
                        .WithMany("Frames")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Game");
                });

            modelBuilder.Entity("Bowling.Models.Game", b =>
                {
                    b.Navigation("Frames");
                });
#pragma warning restore 612, 618
        }
    }
}
