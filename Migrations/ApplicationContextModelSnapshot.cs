﻿// <auto-generated />
using System;
using CryptoExchange;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CryptoExchange.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.25")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CryptoExchange.Entities.Bank", b =>
                {
                    b.Property<int>("CurrencyId")
                        .HasColumnType("integer");

                    b.Property<int>("ByBitId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("CurrencyId");

                    b.ToTable("Bank");
                });

            modelBuilder.Entity("CryptoExchange.Entities.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CountryId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("CryptoExchange.Entities.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Countries");
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

                    b.ToTable("CurrencyNetworks");
                });

            modelBuilder.Entity("CryptoExchange.Entities.Network", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ChainId")
                        .HasColumnType("integer");

                    b.Property<int>("ChainProtocol")
                        .HasColumnType("integer");

                    b.Property<string>("ExplorerUrl")
                        .HasColumnType("text");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Symbol")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Url")
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

                    b.Property<int>("ChannelType")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("now()");

                    b.Property<int>("CurrencyId")
                        .HasColumnType("integer");

                    b.Property<int>("PaymentStatus")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("CurrencyId");

                    b.HasIndex("UserId");

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

                    b.Property<string>("PrivateKey")
                        .HasColumnType("text");

                    b.Property<decimal>("ToAmount")
                        .HasColumnType("numeric");

                    b.Property<string>("TxHash")
                        .HasColumnType("text");

                    b.Property<string>("WalletAddress")
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

                    b.Property<string>("Ip")
                        .IsRequired()
                        .HasColumnType("text");

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

            modelBuilder.Entity("CryptoExchange.Entities.Withdrawal", b =>
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

                    b.Property<decimal>("Rate")
                        .HasColumnType("numeric");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<string>("Telegram")
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<string>("WhatsApp")
                        .HasColumnType("text");

                    b.Property<int>("WithdrawalType")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Withdrawals");
                });

            modelBuilder.Entity("CryptoExchange.Entities.WithdrawalCash", b =>
                {
                    b.Property<Guid>("WithdrawalId")
                        .HasColumnType("uuid");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("CityId")
                        .HasColumnType("integer");

                    b.Property<int>("CurrencyId")
                        .HasColumnType("integer");

                    b.Property<string>("GoogleMapsUrl")
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("WithdrawalId");

                    b.HasIndex("CityId");

                    b.HasIndex("CurrencyId");

                    b.ToTable("WithdrawalsCash");
                });

            modelBuilder.Entity("CryptoExchange.Entities.Bank", b =>
                {
                    b.HasOne("CryptoExchange.Entities.Currency", "Currency")
                        .WithOne("Bank")
                        .HasForeignKey("CryptoExchange.Entities.Bank", "CurrencyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Currency");
                });

            modelBuilder.Entity("CryptoExchange.Entities.City", b =>
                {
                    b.HasOne("CryptoExchange.Entities.Country", "Country")
                        .WithMany("Cities")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Country");
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
                        .HasForeignKey("CurrencyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CryptoExchange.Entities.User", "User")
                        .WithMany("Payments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Currency");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CryptoExchange.Entities.PaymentData", b =>
                {
                    b.HasOne("CryptoExchange.Entities.Currency", "Currency")
                        .WithMany("PaymentDatas")
                        .HasForeignKey("CurrencyId");

                    b.HasOne("CryptoExchange.Entities.Network", "Network")
                        .WithMany("PaymentDatas")
                        .HasForeignKey("NetworkId");

                    b.HasOne("CryptoExchange.Entities.Payment", "Payment")
                        .WithOne("PaymentData")
                        .HasForeignKey("CryptoExchange.Entities.PaymentData", "PaymentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Currency");

                    b.Navigation("Network");

                    b.Navigation("Payment");
                });

            modelBuilder.Entity("CryptoExchange.Entities.Withdrawal", b =>
                {
                    b.HasOne("CryptoExchange.Entities.User", "User")
                        .WithMany("Withdrawals")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("CryptoExchange.Entities.WithdrawalCash", b =>
                {
                    b.HasOne("CryptoExchange.Entities.City", "City")
                        .WithMany()
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CryptoExchange.Entities.Currency", "Currency")
                        .WithMany()
                        .HasForeignKey("CurrencyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CryptoExchange.Entities.Withdrawal", "Withdrawal")
                        .WithOne("DeliveryData")
                        .HasForeignKey("CryptoExchange.Entities.WithdrawalCash", "WithdrawalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");

                    b.Navigation("Currency");

                    b.Navigation("Withdrawal");
                });

            modelBuilder.Entity("CryptoExchange.Entities.Country", b =>
                {
                    b.Navigation("Cities");
                });

            modelBuilder.Entity("CryptoExchange.Entities.Currency", b =>
                {
                    b.Navigation("Bank");

                    b.Navigation("Networks");

                    b.Navigation("PaymentDatas");
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
                    b.Navigation("Payments");

                    b.Navigation("Withdrawals");
                });

            modelBuilder.Entity("CryptoExchange.Entities.Withdrawal", b =>
                {
                    b.Navigation("DeliveryData");
                });
#pragma warning restore 612, 618
        }
    }
}
