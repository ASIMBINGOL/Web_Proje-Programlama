﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Web_Proje.Models;

#nullable disable

namespace Web_Proje.Migrations
{
    [DbContext(typeof(KuaforContext))]
    partial class KuaforContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.0");

            modelBuilder.Entity("CalisanIslemler", b =>
                {
                    b.Property<int>("calisansCalisanID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("islemlersIslemID")
                        .HasColumnType("INTEGER");

                    b.HasKey("calisansCalisanID", "islemlersIslemID");

                    b.HasIndex("islemlersIslemID");

                    b.ToTable("CalisanIslemler");
                });

            modelBuilder.Entity("Web_Proje.Models.Admin", b =>
                {
                    b.Property<int>("AdminId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("AdminEmail")
                        .HasColumnType("TEXT");

                    b.Property<string>("AdminSifre")
                        .HasColumnType("TEXT");

                    b.HasKey("AdminId");

                    b.ToTable("Admin");
                });

            modelBuilder.Entity("Web_Proje.Models.Calisan", b =>
                {
                    b.Property<int>("CalisanID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Aciklama")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("CalisanAd")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("CalisanEmail")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("CalisanSoyad")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("CalisanTelefon")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("IslemID")
                        .HasColumnType("INTEGER");

                    b.HasKey("CalisanID");

                    b.ToTable("Calisanlar");
                });

            modelBuilder.Entity("Web_Proje.Models.CalismaMesai", b =>
                {
                    b.Property<int>("CalismaMesaiID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CalisanID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Gun")
                        .HasColumnType("INTEGER");

                    b.Property<TimeSpan>("SaatBaslangic")
                        .HasColumnType("TEXT");

                    b.Property<TimeSpan>("SaatBitis")
                        .HasColumnType("TEXT");

                    b.HasKey("CalismaMesaiID");

                    b.HasIndex("CalisanID");

                    b.ToTable("CalismaSaatleri");
                });

            modelBuilder.Entity("Web_Proje.Models.Islemler", b =>
                {
                    b.Property<int>("IslemID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("IslemAdi")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Ucret")
                        .HasColumnType("INTEGER");

                    b.HasKey("IslemID");

                    b.ToTable("Islemler");
                });

            modelBuilder.Entity("Web_Proje.Models.Musteri", b =>
                {
                    b.Property<int>("MUsteriID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("KayitTarihi")
                        .HasColumnType("TEXT");

                    b.Property<string>("MUsteriAd")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("MUsteriEmail")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("MUsteriSifre")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("MUsteriSoyad")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("MUsteriTelefon")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("MUsteriID");

                    b.ToTable("Musteri");
                });

            modelBuilder.Entity("Web_Proje.Models.Randevu", b =>
                {
                    b.Property<int>("RandevuID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CalisanID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("IslemID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MusteriID")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("OnayDurumu")
                        .HasColumnType("INTEGER");

                    b.Property<TimeSpan>("Saat")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Tarih")
                        .HasColumnType("TEXT");

                    b.HasKey("RandevuID");

                    b.ToTable("Randevular");
                });

            modelBuilder.Entity("CalisanIslemler", b =>
                {
                    b.HasOne("Web_Proje.Models.Calisan", null)
                        .WithMany()
                        .HasForeignKey("calisansCalisanID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Web_Proje.Models.Islemler", null)
                        .WithMany()
                        .HasForeignKey("islemlersIslemID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Web_Proje.Models.CalismaMesai", b =>
                {
                    b.HasOne("Web_Proje.Models.Calisan", "Calisan")
                        .WithMany()
                        .HasForeignKey("CalisanID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Calisan");
                });
#pragma warning restore 612, 618
        }
    }
}
