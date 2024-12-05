﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace avwx_metar_service.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("avwx_metar_service.Cloud", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Altitude")
                        .HasColumnType("int")
                        .HasAnnotation("Relational:JsonPropertyName", "altitude");

                    b.Property<int?>("MetarId")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasAnnotation("Relational:JsonPropertyName", "type");

                    b.HasKey("Id");

                    b.HasIndex("MetarId");

                    b.ToTable("Cloud");

                    b.HasAnnotation("Relational:JsonPropertyName", "clouds");
                });

            modelBuilder.Entity("avwx_metar_service.Metar", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Raw")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasAnnotation("Relational:JsonPropertyName", "raw");

                    b.Property<string>("Station")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasAnnotation("Relational:JsonPropertyName", "station");

                    b.HasKey("Id");

                    b.ToTable("Metars");
                });

            modelBuilder.Entity("avwx_metar_service.WxCode", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("MetarId")
                        .HasColumnType("int");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasAnnotation("Relational:JsonPropertyName", "repr");

                    b.HasKey("Id");

                    b.HasIndex("MetarId");

                    b.ToTable("WxCode");

                    b.HasAnnotation("Relational:JsonPropertyName", "wx_codes");
                });

            modelBuilder.Entity("avwx_metar_service.Cloud", b =>
                {
                    b.HasOne("avwx_metar_service.Metar", null)
                        .WithMany("Clouds")
                        .HasForeignKey("MetarId");
                });

            modelBuilder.Entity("avwx_metar_service.Metar", b =>
                {
                    b.OwnsOne("avwx_metar_service.Altimeter", "Altimeter", b1 =>
                        {
                            b1.Property<int>("MetarId")
                                .HasColumnType("int");

                            b1.Property<int>("Value")
                                .HasColumnType("int")
                                .HasAnnotation("Relational:JsonPropertyName", "value");

                            b1.HasKey("MetarId");

                            b1.ToTable("Metars");

                            b1.HasAnnotation("Relational:JsonPropertyName", "altimeter");

                            b1.WithOwner()
                                .HasForeignKey("MetarId");
                        });

                    b.OwnsOne("avwx_metar_service.Temperature", "Dewpoint", b1 =>
                        {
                            b1.Property<int>("MetarId")
                                .HasColumnType("int");

                            b1.Property<int>("Value")
                                .HasColumnType("int")
                                .HasAnnotation("Relational:JsonPropertyName", "value");

                            b1.HasKey("MetarId");

                            b1.ToTable("Metars");

                            b1.HasAnnotation("Relational:JsonPropertyName", "dewpoint");

                            b1.WithOwner()
                                .HasForeignKey("MetarId");
                        });

                    b.OwnsOne("avwx_metar_service.Temperature", "Temperature", b1 =>
                        {
                            b1.Property<int>("MetarId")
                                .HasColumnType("int");

                            b1.Property<int>("Value")
                                .HasColumnType("int")
                                .HasAnnotation("Relational:JsonPropertyName", "value");

                            b1.HasKey("MetarId");

                            b1.ToTable("Metars");

                            b1.HasAnnotation("Relational:JsonPropertyName", "temperature");

                            b1.WithOwner()
                                .HasForeignKey("MetarId");
                        });

                    b.OwnsOne("avwx_metar_service.Time", "Time", b1 =>
                        {
                            b1.Property<int>("MetarId")
                                .HasColumnType("int");

                            b1.Property<DateTime>("Dt")
                                .HasColumnType("datetime(6)")
                                .HasAnnotation("Relational:JsonPropertyName", "dt");

                            b1.HasKey("MetarId");

                            b1.ToTable("Metars");

                            b1.HasAnnotation("Relational:JsonPropertyName", "time");

                            b1.WithOwner()
                                .HasForeignKey("MetarId");
                        });

                    b.OwnsOne("avwx_metar_service.Visibility", "Visibility", b1 =>
                        {
                            b1.Property<int>("MetarId")
                                .HasColumnType("int");

                            b1.Property<int>("Value")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasDefaultValue(-1)
                                .HasAnnotation("Relational:JsonPropertyName", "value");

                            b1.HasKey("MetarId");

                            b1.ToTable("Metars");

                            b1.HasAnnotation("Relational:JsonPropertyName", "visibility");

                            b1.WithOwner()
                                .HasForeignKey("MetarId");
                        });

                    b.OwnsOne("avwx_metar_service.WindDirection", "WindDirection", b1 =>
                        {
                            b1.Property<int>("MetarId")
                                .HasColumnType("int");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("longtext")
                                .HasAnnotation("Relational:JsonPropertyName", "repr");

                            b1.HasKey("MetarId");

                            b1.ToTable("Metars");

                            b1.HasAnnotation("Relational:JsonPropertyName", "wind_direction");

                            b1.WithOwner()
                                .HasForeignKey("MetarId");
                        });

                    b.OwnsOne("avwx_metar_service.WindSpeed", "WindSpeed", b1 =>
                        {
                            b1.Property<int>("MetarId")
                                .HasColumnType("int");

                            b1.Property<int>("Value")
                                .HasColumnType("int")
                                .HasAnnotation("Relational:JsonPropertyName", "value");

                            b1.HasKey("MetarId");

                            b1.ToTable("Metars");

                            b1.HasAnnotation("Relational:JsonPropertyName", "wind_speed");

                            b1.WithOwner()
                                .HasForeignKey("MetarId");
                        });

                    b.Navigation("Altimeter")
                        .IsRequired();

                    b.Navigation("Dewpoint")
                        .IsRequired();

                    b.Navigation("Temperature")
                        .IsRequired();

                    b.Navigation("Time")
                        .IsRequired();

                    b.Navigation("Visibility")
                        .IsRequired();

                    b.Navigation("WindDirection")
                        .IsRequired();

                    b.Navigation("WindSpeed")
                        .IsRequired();
                });

            modelBuilder.Entity("avwx_metar_service.WxCode", b =>
                {
                    b.HasOne("avwx_metar_service.Metar", null)
                        .WithMany("WxCodes")
                        .HasForeignKey("MetarId");
                });

            modelBuilder.Entity("avwx_metar_service.Metar", b =>
                {
                    b.Navigation("Clouds");

                    b.Navigation("WxCodes");
                });
#pragma warning restore 612, 618
        }
    }
}
