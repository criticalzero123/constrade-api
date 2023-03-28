﻿// <auto-generated />
using System;
using ConstradeApi.VerificationEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ConstradeApi.Migrations.VerificationData
{
    [DbContext(typeof(VerificationDataContext))]
    [Migration("20230328011708_AddingAddedByInProductPrices")]
    partial class AddingAddedByInProductPrices
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ConstradeApi.VerificationEntity.AdminAccounts", b =>
                {
                    b.Property<int>("AdminAccountId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("AdminAccountId"));

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("AdminAccountId");

                    b.ToTable("admin_accounts");
                });

            modelBuilder.Entity("ConstradeApi.VerificationEntity.ProductPrices", b =>
                {
                    b.Property<int>("ProductPricesId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ProductPricesId"));

                    b.Property<int>("AddedBy")
                        .HasColumnType("integer");

                    b.Property<string>("Genre")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("OriginUrl")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("origin_url");

                    b.Property<string>("Platform")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("ReleaseDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("release_date");

                    b.Property<decimal>("Value")
                        .HasColumnType("numeric");

                    b.HasKey("ProductPricesId");

                    b.HasIndex("AddedBy");

                    b.ToTable("product_prices");
                });

            modelBuilder.Entity("ConstradeApi.VerificationEntity.ValidIdRequest", b =>
                {
                    b.Property<int>("ValidIdRequestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("valid_id_request_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ValidIdRequestId"));

                    b.Property<DateTime>("RequestDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("request_date");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("status");

                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("username");

                    b.Property<string>("ValidIdNumber")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("valid_id_number");

                    b.Property<int>("ValidationType")
                        .HasColumnType("integer")
                        .HasColumnName("validation_type");

                    b.HasKey("ValidIdRequestId");

                    b.ToTable("valid_id_request");
                });

            modelBuilder.Entity("ConstradeApi.VerificationEntity.ValidIdentification", b =>
                {
                    b.Property<int>("ValidIdentificationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ValidIdentificationId"));

                    b.Property<DateTime>("DateExpiration")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("date_expiration");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("ValidIdNumber")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("valid_id_number");

                    b.Property<int>("ValidIdType")
                        .HasColumnType("integer")
                        .HasColumnName("valid_id_type");

                    b.HasKey("ValidIdentificationId");

                    b.ToTable("valid_identification");
                });

            modelBuilder.Entity("ConstradeApi.VerificationEntity.ProductPrices", b =>
                {
                    b.HasOne("ConstradeApi.VerificationEntity.AdminAccounts", "AdminAccount")
                        .WithMany()
                        .HasForeignKey("AddedBy")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AdminAccount");
                });
#pragma warning restore 612, 618
        }
    }
}
