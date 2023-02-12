﻿// <auto-generated />
using System;
using ConstradeApi.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ConstradeApi.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230212142528_AddingIndexesInPersonAndUserTable")]
    partial class AddingIndexesInPersonAndUserTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ConstradeApi.Entity.Address", b =>
                {
                    b.Property<int>("Address_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Address_id"));

                    b.Property<string>("Barangay")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("House_number")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("Postal_code")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("Province")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("Updated_at")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Address_id");

                    b.ToTable("address");
                });

            modelBuilder.Entity("ConstradeApi.Entity.Favorites", b =>
                {
                    b.Property<int>("FavoriteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("favorite_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("FavoriteId"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("date");

                    b.Property<int>("ProductId")
                        .HasColumnType("integer")
                        .HasColumnName("product_id");

                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.HasKey("FavoriteId");

                    b.HasIndex("ProductId");

                    b.HasIndex("UserId");

                    b.ToTable("favorites");
                });

            modelBuilder.Entity("ConstradeApi.Entity.Follow", b =>
                {
                    b.Property<int>("FollowId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("follow_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("FollowId"));

                    b.Property<DateTime>("DateFollowed")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("date_followed");

                    b.Property<int>("FollowByUserId")
                        .HasColumnType("integer")
                        .HasColumnName("follow_by_user_id");

                    b.Property<int>("FollowedByUserId")
                        .HasColumnType("integer")
                        .HasColumnName("followed_by_user_id");

                    b.HasKey("FollowId");

                    b.HasIndex("FollowByUserId");

                    b.HasIndex("FollowedByUserId");

                    b.ToTable("follow");
                });

            modelBuilder.Entity("ConstradeApi.Entity.ImageList", b =>
                {
                    b.Property<int>("ImageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("image_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ImageId"));

                    b.Property<string>("ImageURL")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("imageURL");

                    b.Property<int>("ProductId")
                        .HasColumnType("integer")
                        .HasColumnName("product_id");

                    b.HasKey("ImageId");

                    b.HasIndex("ProductId");

                    b.ToTable("image_list");
                });

            modelBuilder.Entity("ConstradeApi.Entity.Person", b =>
                {
                    b.Property<int>("Person_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Person_id"));

                    b.Property<int?>("AddressRef_id")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("Birthdate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Person_id");

                    b.HasIndex("AddressRef_id");

                    b.HasIndex("FirstName", "LastName");

                    b.ToTable("person");
                });

            modelBuilder.Entity("ConstradeApi.Entity.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("product_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ProductId"));

                    b.Property<decimal>("Cash")
                        .HasColumnType("numeric")
                        .HasColumnName("cash");

                    b.Property<string>("Condition")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("condition");

                    b.Property<int>("CountFavorite")
                        .HasColumnType("integer")
                        .HasColumnName("count_favorite");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("date_created");

                    b.Property<string>("DeliveryMethod")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("delivery_method");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)")
                        .HasColumnName("description");

                    b.Property<string>("GameGenre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("game_genre");

                    b.Property<string>("Item")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("item");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("location");

                    b.Property<string>("ModelNumber")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("model_number");

                    b.Property<string>("Platform")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("platform");

                    b.Property<int>("PosterUserId")
                        .HasColumnType("integer")
                        .HasColumnName("poster_user_id");

                    b.Property<string>("PreferTrade")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("prefere_trade");

                    b.Property<string>("ProductStatus")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("product_status");

                    b.Property<string>("SerialNumber")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("serial_number");

                    b.Property<string>("ThumbnailUrl")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("thumbnail_url");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("title");

                    b.HasKey("ProductId");

                    b.HasIndex("PosterUserId");

                    b.ToTable("product");
                });

            modelBuilder.Entity("ConstradeApi.Entity.ProductComment", b =>
                {
                    b.Property<int>("ProductCommentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("product_comment_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ProductCommentId"));

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("comment");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("date_created");

                    b.Property<int>("ProductId")
                        .HasColumnType("integer")
                        .HasColumnName("product_id");

                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.HasKey("ProductCommentId");

                    b.HasIndex("ProductId");

                    b.HasIndex("UserId");

                    b.ToTable("product_comment");
                });

            modelBuilder.Entity("ConstradeApi.Entity.ProductView", b =>
                {
                    b.Property<int>("ProductViewId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("product_view_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ProductViewId"));

                    b.Property<int>("ProductId")
                        .HasColumnType("integer")
                        .HasColumnName("product_id");

                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.HasKey("ProductViewId");

                    b.HasIndex("ProductId");

                    b.HasIndex("UserId");

                    b.ToTable("product_view");
                });

            modelBuilder.Entity("ConstradeApi.Entity.Review", b =>
                {
                    b.Property<int>("ReviewId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ReviewId"));

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("date_created");

                    b.Property<short>("Rate")
                        .HasColumnType("smallint")
                        .HasColumnName("rate");

                    b.Property<int>("TransactionRefId")
                        .HasColumnType("integer")
                        .HasColumnName("transaction_ref_id");

                    b.HasKey("ReviewId");

                    b.HasIndex("TransactionRefId");

                    b.ToTable("reviews");
                });

            modelBuilder.Entity("ConstradeApi.Entity.SendMoneyTransaction", b =>
                {
                    b.Property<int>("SendMoneyTransactionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("send_money_transaction");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("SendMoneyTransactionId"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric")
                        .HasColumnName("amount");

                    b.Property<DateTime>("DateSend")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("date_send");

                    b.Property<int>("ReceiverWalletId")
                        .HasColumnType("integer")
                        .HasColumnName("receiver_wallet_id");

                    b.Property<int>("SenderWalletId")
                        .HasColumnType("integer")
                        .HasColumnName("sender_wallet_id");

                    b.HasKey("SendMoneyTransactionId");

                    b.HasIndex("ReceiverWalletId");

                    b.HasIndex("SenderWalletId");

                    b.ToTable("send_money_transaction");
                });

            modelBuilder.Entity("ConstradeApi.Entity.Subscription", b =>
                {
                    b.Property<int>("SubscriptionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("subscription_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("SubscriptionId"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric")
                        .HasColumnName("amount");

                    b.Property<DateTime>("DateEnd")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("date_end");

                    b.Property<DateTime>("DateStart")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("date_start");

                    b.Property<string>("SubscriptionType")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("subscription_type");

                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.HasKey("SubscriptionId");

                    b.HasIndex("UserId");

                    b.ToTable("subscription");
                });

            modelBuilder.Entity("ConstradeApi.Entity.SubscriptionHistory", b =>
                {
                    b.Property<int>("SubscriptionHistoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("subscription_history_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("SubscriptionHistoryId"));

                    b.Property<DateTime>("DateUpdate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("date_updated");

                    b.Property<decimal>("NewAmount")
                        .HasColumnType("numeric")
                        .HasColumnName("new_amount");

                    b.Property<DateTime>("NewDateEnd")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("new_date_end");

                    b.Property<DateTime>("NewDateStart")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("new_date_start");

                    b.Property<string>("NewSubscriptionType")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("new_subscription_type");

                    b.Property<decimal>("PreviousAmount")
                        .HasColumnType("numeric")
                        .HasColumnName("previous_amount");

                    b.Property<DateTime>("PreviousDateEnd")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("previous_date_end");

                    b.Property<DateTime>("PreviousDateStart")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("previous_date_start");

                    b.Property<string>("PreviousSubscriptionType")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("previous_subscription_type");

                    b.Property<int>("SubscriptionId")
                        .HasColumnType("integer")
                        .HasColumnName("subscription_id");

                    b.HasKey("SubscriptionHistoryId");

                    b.HasIndex("SubscriptionId");

                    b.ToTable("subscription_history");
                });

            modelBuilder.Entity("ConstradeApi.Entity.TopUpTransaction", b =>
                {
                    b.Property<int>("TopUpTransactionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("top_up_transaction");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("TopUpTransactionId"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric")
                        .HasColumnName("amount");

                    b.Property<DateTime>("DateTopUp")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("date_topup");

                    b.Property<int>("WalletId")
                        .HasColumnType("integer")
                        .HasColumnName("wallet_id");

                    b.HasKey("TopUpTransactionId");

                    b.HasIndex("WalletId");

                    b.ToTable("top_up_transaction");
                });

            modelBuilder.Entity("ConstradeApi.Entity.Transaction", b =>
                {
                    b.Property<int>("TransactionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("transaction_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("TransactionId"));

                    b.Property<int>("BuyerUserId")
                        .HasColumnType("integer")
                        .HasColumnName("buyer_user_id");

                    b.Property<DateTime>("DateTransaction")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("date_transaction");

                    b.Property<bool>("GetWanted")
                        .HasColumnType("boolean")
                        .HasColumnName("get_wanted");

                    b.Property<bool>("InAppTransaction")
                        .HasColumnType("boolean")
                        .HasColumnName("in_app_transaction");

                    b.Property<bool>("IsReviewed")
                        .HasColumnType("boolean")
                        .HasColumnName("is_reviewed");

                    b.Property<int>("ProductId")
                        .HasColumnType("integer")
                        .HasColumnName("product_id");

                    b.Property<int>("SellerUserId")
                        .HasColumnType("integer")
                        .HasColumnName("seller_user_id");

                    b.HasKey("TransactionId");

                    b.HasIndex("BuyerUserId");

                    b.HasIndex("ProductId");

                    b.HasIndex("SellerUserId");

                    b.ToTable("transactions");
                });

            modelBuilder.Entity("ConstradeApi.Entity.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("UserId"));

                    b.Property<string>("AuthProviderType")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("auth_provider_type");

                    b.Property<int>("CountPost")
                        .HasColumnType("integer")
                        .HasColumnName("count_post");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("date_created");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("email");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("image_url");

                    b.Property<DateTime>("LastActiveAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("last_active_at");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("password");

                    b.Property<int>("PersonRefId")
                        .HasColumnType("integer")
                        .HasColumnName("person_ref_id");

                    b.Property<string>("UserStatus")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("user_status");

                    b.Property<string>("UserType")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("user_type");

                    b.HasKey("UserId");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("PersonRefId");

                    b.ToTable("user");
                });

            modelBuilder.Entity("ConstradeApi.Entity.Wallet", b =>
                {
                    b.Property<int>("WalletId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("wallet_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("WalletId"));

                    b.Property<decimal>("Balance")
                        .HasColumnType("numeric")
                        .HasColumnName("balance");

                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.HasKey("WalletId");

                    b.HasIndex("UserId");

                    b.ToTable("user_wallet");
                });

            modelBuilder.Entity("ConstradeApi.Entity.Favorites", b =>
                {
                    b.HasOne("ConstradeApi.Entity.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ConstradeApi.Entity.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ConstradeApi.Entity.Follow", b =>
                {
                    b.HasOne("ConstradeApi.Entity.User", "User1")
                        .WithMany()
                        .HasForeignKey("FollowByUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ConstradeApi.Entity.User", "User2")
                        .WithMany()
                        .HasForeignKey("FollowedByUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User1");

                    b.Navigation("User2");
                });

            modelBuilder.Entity("ConstradeApi.Entity.ImageList", b =>
                {
                    b.HasOne("ConstradeApi.Entity.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("ConstradeApi.Entity.Person", b =>
                {
                    b.HasOne("ConstradeApi.Entity.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressRef_id");

                    b.Navigation("Address");
                });

            modelBuilder.Entity("ConstradeApi.Entity.Product", b =>
                {
                    b.HasOne("ConstradeApi.Entity.User", "User")
                        .WithMany()
                        .HasForeignKey("PosterUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ConstradeApi.Entity.ProductComment", b =>
                {
                    b.HasOne("ConstradeApi.Entity.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ConstradeApi.Entity.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ConstradeApi.Entity.ProductView", b =>
                {
                    b.HasOne("ConstradeApi.Entity.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ConstradeApi.Entity.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ConstradeApi.Entity.Review", b =>
                {
                    b.HasOne("ConstradeApi.Entity.Transaction", "Transaction")
                        .WithMany()
                        .HasForeignKey("TransactionRefId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Transaction");
                });

            modelBuilder.Entity("ConstradeApi.Entity.SendMoneyTransaction", b =>
                {
                    b.HasOne("ConstradeApi.Entity.Wallet", "Wallet2")
                        .WithMany()
                        .HasForeignKey("ReceiverWalletId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ConstradeApi.Entity.Wallet", "Wallet1")
                        .WithMany()
                        .HasForeignKey("SenderWalletId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Wallet1");

                    b.Navigation("Wallet2");
                });

            modelBuilder.Entity("ConstradeApi.Entity.Subscription", b =>
                {
                    b.HasOne("ConstradeApi.Entity.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ConstradeApi.Entity.SubscriptionHistory", b =>
                {
                    b.HasOne("ConstradeApi.Entity.Subscription", "Subscription")
                        .WithMany()
                        .HasForeignKey("SubscriptionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Subscription");
                });

            modelBuilder.Entity("ConstradeApi.Entity.TopUpTransaction", b =>
                {
                    b.HasOne("ConstradeApi.Entity.Wallet", "Wallet")
                        .WithMany()
                        .HasForeignKey("WalletId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Wallet");
                });

            modelBuilder.Entity("ConstradeApi.Entity.Transaction", b =>
                {
                    b.HasOne("ConstradeApi.Entity.User", "Buyer")
                        .WithMany()
                        .HasForeignKey("BuyerUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ConstradeApi.Entity.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ConstradeApi.Entity.User", "Seller")
                        .WithMany()
                        .HasForeignKey("SellerUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Buyer");

                    b.Navigation("Product");

                    b.Navigation("Seller");
                });

            modelBuilder.Entity("ConstradeApi.Entity.User", b =>
                {
                    b.HasOne("ConstradeApi.Entity.Person", "Person")
                        .WithMany()
                        .HasForeignKey("PersonRefId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Person");
                });

            modelBuilder.Entity("ConstradeApi.Entity.Wallet", b =>
                {
                    b.HasOne("ConstradeApi.Entity.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}
