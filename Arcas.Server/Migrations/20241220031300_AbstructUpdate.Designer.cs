﻿// <auto-generated />
using Arcas.Server.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Arcas.Server.Migrations
{
    [DbContext(typeof(ArcasDbContext))]
    [Migration("20241220031300_AbstructUpdate")]
    partial class AbstructUpdate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("Arcas.Server.Models.AbstractBook", b =>
                {
                    b.Property<string>("ISBN")
                        .HasMaxLength(13)
                        .HasColumnType("varchar(13)")
                        .HasColumnName("ISBN");

                    b.Property<string>("author")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("author");

                    b.Property<int>("category")
                        .HasColumnType("int")
                        .HasColumnName("category");

                    b.Property<string>("publishHouse")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("publishHouse");

                    b.Property<string>("title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("title");

                    b.HasKey("ISBN");

                    b.ToTable("AbstractBook");
                });

            modelBuilder.Entity("Arcas.Server.Models.Comment", b =>
                {
                    b.Property<int>("commentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("commentID");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("commentID"));

                    b.Property<string>("AbstractBookISBN")
                        .HasColumnType("varchar(13)");

                    b.Property<int>("ConcreteBookbookID")
                        .HasColumnType("int");

                    b.Property<string>("ISBN")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("varchar(13)")
                        .HasColumnName("ISBN");

                    b.Property<int>("bookID")
                        .HasColumnType("int")
                        .HasColumnName("bookID");

                    b.Property<string>("commentContent")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("commentContent");

                    b.Property<int>("commentatorID")
                        .HasColumnType("int")
                        .HasColumnName("commentatorID");

                    b.Property<int>("likeNum")
                        .HasColumnType("int")
                        .HasColumnName("likeNum");

                    b.Property<int>("userID")
                        .HasColumnType("int");

                    b.HasKey("commentID");

                    b.HasIndex("AbstractBookISBN");

                    b.HasIndex("ConcreteBookbookID");

                    b.HasIndex("userID");

                    b.ToTable("Comment");
                });

            modelBuilder.Entity("Arcas.Server.Models.ConcreteBook", b =>
                {
                    b.Property<int>("bookID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("bookID");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("bookID"));

                    b.Property<string>("ISBN")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("varchar(13)")
                        .HasColumnName("ISBN");

                    b.Property<int>("bookState")
                        .HasColumnType("int")
                        .HasColumnName("bookState");

                    b.Property<string>("cover")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("cover");

                    b.Property<int>("userID")
                        .HasColumnType("int")
                        .HasColumnName("userID");

                    b.HasKey("bookID");

                    b.HasIndex("ISBN");

                    b.HasIndex("userID");

                    b.ToTable("ConcreteBook");
                });

            modelBuilder.Entity("Arcas.Server.Models.ExchangeDetail", b =>
                {
                    b.Property<int>("exchangeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("exchangeID");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("exchangeID"));

                    b.Property<int>("ConcreteBookbookID")
                        .HasColumnType("int");

                    b.Property<int>("bookID")
                        .HasColumnType("int")
                        .HasColumnName("bookID");

                    b.Property<string>("content")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("content");

                    b.Property<string>("detailsA")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("detailsA");

                    b.Property<string>("detailsB")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("detailsB");

                    b.Property<int>("ida")
                        .HasColumnType("int")
                        .HasColumnName("IDA");

                    b.Property<int>("idb")
                        .HasColumnType("int")
                        .HasColumnName("IDB");

                    b.Property<bool>("isApproved")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("isApproved");

                    b.Property<string>("trackingID")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)")
                        .HasColumnName("trackingID");

                    b.HasKey("exchangeID");

                    b.HasIndex("ConcreteBookbookID");

                    b.HasIndex("ida");

                    b.HasIndex("idb");

                    b.ToTable("ExchangeDetail");
                });

            modelBuilder.Entity("Arcas.Server.Models.User", b =>
                {
                    b.Property<int>("userID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("userID");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("userID"));

                    b.Property<string>("nickname")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)")
                        .HasColumnName("nickname");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)")
                        .HasColumnName("password");

                    b.Property<string>("telephone")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("varchar(11)")
                        .HasColumnName("telephone");

                    b.Property<string>("token")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)")
                        .HasColumnName("token");

                    b.Property<string>("username")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)")
                        .HasColumnName("username");

                    b.HasKey("userID");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Arcas.Server.Models.Comment", b =>
                {
                    b.HasOne("Arcas.Server.Models.AbstractBook", "AbstractBook")
                        .WithMany("Comments")
                        .HasForeignKey("AbstractBookISBN");

                    b.HasOne("Arcas.Server.Models.ConcreteBook", "ConcreteBook")
                        .WithMany("Comments")
                        .HasForeignKey("ConcreteBookbookID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Arcas.Server.Models.User", "User")
                        .WithMany("Comments")
                        .HasForeignKey("userID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AbstractBook");

                    b.Navigation("ConcreteBook");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Arcas.Server.Models.ConcreteBook", b =>
                {
                    b.HasOne("Arcas.Server.Models.AbstractBook", "AbstractBook")
                        .WithMany("ConcreteBooks")
                        .HasForeignKey("ISBN")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Arcas.Server.Models.User", "User")
                        .WithMany("ConcreteBooks")
                        .HasForeignKey("userID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AbstractBook");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Arcas.Server.Models.ExchangeDetail", b =>
                {
                    b.HasOne("Arcas.Server.Models.ConcreteBook", "ConcreteBook")
                        .WithMany("ExchangeDetails")
                        .HasForeignKey("ConcreteBookbookID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Arcas.Server.Models.User", "UserA")
                        .WithMany("ExchangeDetailsA")
                        .HasForeignKey("ida")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Arcas.Server.Models.User", "UserB")
                        .WithMany("ExchangeDetailsB")
                        .HasForeignKey("idb")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ConcreteBook");

                    b.Navigation("UserA");

                    b.Navigation("UserB");
                });

            modelBuilder.Entity("Arcas.Server.Models.AbstractBook", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("ConcreteBooks");
                });

            modelBuilder.Entity("Arcas.Server.Models.ConcreteBook", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("ExchangeDetails");
                });

            modelBuilder.Entity("Arcas.Server.Models.User", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("ConcreteBooks");

                    b.Navigation("ExchangeDetailsA");

                    b.Navigation("ExchangeDetailsB");
                });
#pragma warning restore 612, 618
        }
    }
}
