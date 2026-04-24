
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VVSummerBlastBackendAPI.Data;

#nullable disable

namespace VVSummerBlastBackendAPI.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.25")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("Kosar", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("FelhasznaloId")
                        .HasColumnType("int");

                    b.Property<int>("Mennyiseg")
                        .HasColumnType("int");

                    b.Property<int>("TermekVariansId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FelhasznaloId");

                    b.HasIndex("TermekVariansId");

                    b.ToTable("Kosarak");
                });

            modelBuilder.Entity("VVSummerBlastBackendAPI.Models.Eloado", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Bio")
                        .HasColumnType("longtext");

                    b.Property<string>("KepUrl")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Leiras")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("varchar(1000)");

                    b.Property<string>("Mufaj")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Nev")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("SpotifyUrl")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Eloadok");
                });

            modelBuilder.Entity("VVSummerBlastBackendAPI.Models.Esemeny", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("EloadoId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Kezdes")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Leiras")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<int>("SzinpadId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Vege")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("EloadoId");

                    b.HasIndex("SzinpadId");

                    b.ToTable("Esemenyek");
                });

            modelBuilder.Entity("VVSummerBlastBackendAPI.Models.Felhasznalo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.ToTable("felhasznalok");
                });

            modelBuilder.Entity("VVSummerBlastBackendAPI.Models.Hostel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Ar")
                        .HasColumnType("int");

                    b.Property<string>("KepUrl")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Leiras")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Link")
                        .HasColumnType("longtext");

                    b.Property<string>("Nev")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Hostelek");
                });

            modelBuilder.Entity("VVSummerBlastBackendAPI.Models.Jegy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("ErvenyessegKezdete")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("EsemenyId")
                        .HasColumnType("int");

                    b.Property<string>("Reszletek")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("TermekId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EsemenyId");

                    b.HasIndex("TermekId");

                    b.ToTable("Jegyek");
                });

            modelBuilder.Entity("VVSummerBlastBackendAPI.Models.Kemping", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Ar")
                        .HasColumnType("int");

                    b.Property<string>("KepUrl")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Leiras")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Nev")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Kempingek");
                });

            modelBuilder.Entity("VVSummerBlastBackendAPI.Models.Meret", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Megnevezes")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.HasKey("Id");

                    b.ToTable("Meretek");
                });

            modelBuilder.Entity("VVSummerBlastBackendAPI.Models.Rendeles", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("FelhasznaloId")
                        .HasColumnType("int");

                    b.Property<DateTime>("RendelesIdeje")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Statusz")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<decimal>("Vegosszeg")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("FelhasznaloId");

                    b.ToTable("Rendelesek");
                });

            modelBuilder.Entity("VVSummerBlastBackendAPI.Models.RendelesTetel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Egysegar")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Mennyiseg")
                        .HasColumnType("int");

                    b.Property<int>("RendelesId")
                        .HasColumnType("int");

                    b.Property<int>("TermekVariansId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RendelesId");

                    b.HasIndex("TermekVariansId");

                    b.ToTable("RendelesTetelek");
                });

            modelBuilder.Entity("VVSummerBlastBackendAPI.Models.Szinpad", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Helyszin")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)");

                    b.Property<string>("Nev")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Szinpadok");
                });

            modelBuilder.Entity("VVSummerBlastBackendAPI.Models.Termek", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Ar")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("KepUrl")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Nev")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Tipus")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Termekek");
                });

            modelBuilder.Entity("VVSummerBlastBackendAPI.Models.TermekVarians", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Keszlet")
                        .HasColumnType("int");

                    b.Property<int>("MeretId")
                        .HasColumnType("int");

                    b.Property<int>("TermekId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MeretId");

                    b.HasIndex("TermekId");

                    b.ToTable("TermekVariansok");
                });

            modelBuilder.Entity("Kosar", b =>
                {
                    b.HasOne("VVSummerBlastBackendAPI.Models.Felhasznalo", "Felhasznalo")
                        .WithMany()
                        .HasForeignKey("FelhasznaloId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VVSummerBlastBackendAPI.Models.TermekVarians", "TermekVarians")
                        .WithMany("Kosarak")
                        .HasForeignKey("TermekVariansId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Felhasznalo");

                    b.Navigation("TermekVarians");
                });

            modelBuilder.Entity("VVSummerBlastBackendAPI.Models.Esemeny", b =>
                {
                    b.HasOne("VVSummerBlastBackendAPI.Models.Eloado", "Eloado")
                        .WithMany("Esemenyek")
                        .HasForeignKey("EloadoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VVSummerBlastBackendAPI.Models.Szinpad", "Szinpad")
                        .WithMany("Esemenyek")
                        .HasForeignKey("SzinpadId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Eloado");

                    b.Navigation("Szinpad");
                });

            modelBuilder.Entity("VVSummerBlastBackendAPI.Models.Jegy", b =>
                {
                    b.HasOne("VVSummerBlastBackendAPI.Models.Esemeny", "Esemeny")
                        .WithMany("Jegyek")
                        .HasForeignKey("EsemenyId");

                    b.HasOne("VVSummerBlastBackendAPI.Models.Termek", "Termek")
                        .WithMany()
                        .HasForeignKey("TermekId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Esemeny");

                    b.Navigation("Termek");
                });

            modelBuilder.Entity("VVSummerBlastBackendAPI.Models.Rendeles", b =>
                {
                    b.HasOne("VVSummerBlastBackendAPI.Models.Felhasznalo", "Felhasznalo")
                        .WithMany()
                        .HasForeignKey("FelhasznaloId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Felhasznalo");
                });

            modelBuilder.Entity("VVSummerBlastBackendAPI.Models.RendelesTetel", b =>
                {
                    b.HasOne("VVSummerBlastBackendAPI.Models.Rendeles", "Rendeles")
                        .WithMany("RendelesTetelek")
                        .HasForeignKey("RendelesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VVSummerBlastBackendAPI.Models.TermekVarians", "TermekVarians")
                        .WithMany("RendelesTetelek")
                        .HasForeignKey("TermekVariansId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Rendeles");

                    b.Navigation("TermekVarians");
                });

            modelBuilder.Entity("VVSummerBlastBackendAPI.Models.TermekVarians", b =>
                {
                    b.HasOne("VVSummerBlastBackendAPI.Models.Meret", "Meret")
                        .WithMany()
                        .HasForeignKey("MeretId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VVSummerBlastBackendAPI.Models.Termek", "Termek")
                        .WithMany("Variansok")
                        .HasForeignKey("TermekId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Meret");

                    b.Navigation("Termek");
                });

            modelBuilder.Entity("VVSummerBlastBackendAPI.Models.Eloado", b =>
                {
                    b.Navigation("Esemenyek");
                });

            modelBuilder.Entity("VVSummerBlastBackendAPI.Models.Esemeny", b =>
                {
                    b.Navigation("Jegyek");
                });

            modelBuilder.Entity("VVSummerBlastBackendAPI.Models.Rendeles", b =>
                {
                    b.Navigation("RendelesTetelek");
                });

            modelBuilder.Entity("VVSummerBlastBackendAPI.Models.Szinpad", b =>
                {
                    b.Navigation("Esemenyek");
                });

            modelBuilder.Entity("VVSummerBlastBackendAPI.Models.Termek", b =>
                {
                    b.Navigation("Variansok");
                });

            modelBuilder.Entity("VVSummerBlastBackendAPI.Models.TermekVarians", b =>
                {
                    b.Navigation("Kosarak");

                    b.Navigation("RendelesTetelek");
                });
#pragma warning restore 612, 618
        }
    }
}

