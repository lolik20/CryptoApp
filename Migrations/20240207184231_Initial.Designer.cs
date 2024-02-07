﻿// <auto-generated />
using System;
using CryptoExchange;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CryptoExchange.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20240207184231_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.25")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CryptoExchange.Entities.BalanceTransaction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric");

                    b.Property<DateTime>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("now()");

                    b.Property<int>("CurrencyId")
                        .HasColumnType("integer");

                    b.Property<int>("OperationType")
                        .HasColumnType("integer");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("CurrencyId");

                    b.HasIndex("UserId");

                    b.ToTable("BalanceTransactions");
                });

            modelBuilder.Entity("CryptoExchange.Entities.Currency", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Currencies");
                });

            modelBuilder.Entity("CryptoExchange.Entities.CurrencyNetwork", b =>
                {
                    b.Property<int>("CurrencyId")
                        .HasColumnType("integer");

                    b.Property<int>("NetworkId")
                        .HasColumnType("integer");

                    b.Property<string>("ContractAddress")
                        .HasColumnType("text");

                    b.HasKey("CurrencyId", "NetworkId");

                    b.HasIndex("NetworkId");

                    b.ToTable("CurrencyNetwork");
                });

            modelBuilder.Entity("CryptoExchange.Entities.Network", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ImageUrl")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Symbol")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Networks");
                });

            modelBuilder.Entity("CryptoExchange.Entities.Payment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric");

                    b.Property<DateTime>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("now()");

                    b.Property<int?>("CurrencyId")
                        .HasColumnType("integer");

                    b.Property<Guid>("MerchantId")
                        .HasColumnType("uuid");

                    b.Property<int?>("NetworkId")
                        .HasColumnType("integer");

                    b.Property<int>("PaymentStatus")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CurrencyId");

                    b.HasIndex("MerchantId");

                    b.HasIndex("NetworkId");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("CryptoExchange.Entities.PaymentData", b =>
                {
                    b.Property<Guid>("PaymentId")
                        .HasColumnType("uuid");

                    b.Property<int?>("CurrencyId")
                        .HasColumnType("integer");

                    b.Property<int?>("NetworkId")
                        .HasColumnType("integer");

                    b.Property<string>("ToAddress")
                        .HasColumnType("text");

                    b.Property<string>("TxHash")
                        .HasColumnType("text");

                    b.HasKey("PaymentId");

                    b.HasIndex("CurrencyId");

                    b.HasIndex("NetworkId");

                    b.ToTable("PaymentsData");
                });

            modelBuilder.Entity("CryptoExchange.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("now()");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("WebsiteUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CryptoExchange.Entities.UserBalance", b =>
                {
                    b.Property<int>("CurrencyId")
                        .HasColumnType("integer");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<decimal>("Value")
                        .HasColumnType("numeric");

                    b.HasKey("CurrencyId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("Balances");
                });

            modelBuilder.Entity("CryptoExchange.Entities.BalanceTransaction", b =>
                {
                    b.HasOne("CryptoExchange.Entities.Currency", "Currency")
                        .WithMany("Transactions")
                        .HasForeignKey("CurrencyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CryptoExchange.Entities.User", "User")
                        .WithMany("Transactions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Currency");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CryptoExchange.Entities.CurrencyNetwork", b =>
                {
                    b.HasOne("CryptoExchange.Entities.Currency", "Currency")
                        .WithMany("Networks")
                        .HasForeignKey("CurrencyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CryptoExchange.Entities.Network", "Network")
                        .WithMany("Currencies")
                        .HasForeignKey("NetworkId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Currency");

                    b.Navigation("Network");
                });

            modelBuilder.Entity("CryptoExchange.Entities.Payment", b =>
                {
                    b.HasOne("CryptoExchange.Entities.Currency", "Currency")
                        .WithMany()
                        .HasForeignKey("CurrencyId");

                    b.HasOne("CryptoExchange.Entities.User", "User")
                        .WithMany("Payments")
                        .HasForeignKey("MerchantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CryptoExchange.Entities.Network", "Network")
                        .WithMany()
                        .HasForeignKey("NetworkId");

                    b.Navigation("Currency");

                    b.Navigation("Network");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CryptoExchange.Entities.PaymentData", b =>
                {
                    b.HasOne("CryptoExchange.Entities.Currency", null)
                        .WithMany("PaymentDatas")
                        .HasForeignKey("CurrencyId");

                    b.HasOne("CryptoExchange.Entities.Network", null)
                        .WithMany("PaymentDatas")
                        .HasForeignKey("NetworkId");

                    b.HasOne("CryptoExchange.Entities.Payment", "Payment")
                        .WithOne("PaymentData")
                        .HasForeignKey("CryptoExchange.Entities.PaymentData", "PaymentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Payment");
                });

            modelBuilder.Entity("CryptoExchange.Entities.UserBalance", b =>
                {
                    b.HasOne("CryptoExchange.Entities.Currency", "Currency")
                        .WithMany("Balances")
                        .HasForeignKey("CurrencyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CryptoExchange.Entities.User", "User")
                        .WithMany("Balances")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Currency");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CryptoExchange.Entities.Currency", b =>
                {
                    b.Navigation("Balances");

                    b.Navigation("Networks");

                    b.Navigation("PaymentDatas");

                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("CryptoExchange.Entities.Network", b =>
                {
                    b.Navigation("Currencies");

                    b.Navigation("PaymentDatas");
                });

            modelBuilder.Entity("CryptoExchange.Entities.Payment", b =>
                {
                    b.Navigation("PaymentData");
                });

            modelBuilder.Entity("CryptoExchange.Entities.User", b =>
                {
                    b.Navigation("Balances");

                    b.Navigation("Payments");

                    b.Navigation("Transactions");
                });
#pragma warning restore 612, 618
        }
    }
}
