﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PrintShareSolution.Data.EF;

namespace PrintShareSolution.Data.Migrations
{
    [DbContext(typeof(PrinterShareDbContext))]
    [Migration("20210128102834_changeSomething")]
    partial class changeSomething
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("AppRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("AppUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("AppUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "RoleId");

                    b.ToTable("AppUserRoles");

                    b.HasData(
                        new
                        {
                            UserId = new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                            RoleId = new Guid("8d04dce2-969a-435d-bba4-df3f325983dc")
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("AppUserTokens");
                });

            modelBuilder.Entity("PrintShareSolution.Data.Entities.AppConfig", b =>
                {
                    b.Property<string>("Key")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Key");

                    b.ToTable("AppConfigs");

                    b.HasData(
                        new
                        {
                            Key = "HomeTitle",
                            Value = "This is home page of PrinterShareSolution"
                        },
                        new
                        {
                            Key = "HomeKeyword",
                            Value = "This is keyword of PrinterShareSolution"
                        },
                        new
                        {
                            Key = "HomeDescription",
                            Value = "This is description of PrinterShareSolution"
                        });
                });

            modelBuilder.Entity("PrintShareSolution.Data.Entities.AppRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("AppRoles");

                    b.HasData(
                        new
                        {
                            Id = new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                            ConcurrencyStamp = "c423d2db-fb72-4eb5-8d58-68c47fa4b66f",
                            Description = "Administrator role",
                            Name = "admin",
                            NormalizedName = "admin"
                        });
                });

            modelBuilder.Entity("PrintShareSolution.Data.Entities.AppUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("AppUsers");

                    b.HasData(
                        new
                        {
                            Id = new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "2fa9f919-3177-4054-a7c7-052f21e9f494",
                            Email = "quanglehoi@gmail.com",
                            EmailConfirmed = true,
                            FullName = "Lê Hội Quang",
                            LockoutEnabled = false,
                            NormalizedEmail = "quanglehoi@gmail.com",
                            NormalizedUserName = "admin",
                            PasswordHash = "AQAAAAEAACcQAAAAEEZnBelQLZ/O1HBkbMwYSSEFx+Xc4eVxNh1qkH+VgHSWNOqWnfJ1Sj7DDQDOH9u8Mw==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "",
                            TwoFactorEnabled = false,
                            UserName = "admin"
                        });
                });

            modelBuilder.Entity("PrintShareSolution.Data.Entities.BlockList", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("BlackListFilePath")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("UserBlocked")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("BlockLists");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            BlackListFilePath = "C://BackList.txt",
                            UserBlocked = "DKFAJ56",
                            UserId = new Guid("69bd714f-9576-45ba-b5b7-f00649be00de")
                        });
                });

            modelBuilder.Entity("PrintShareSolution.Data.Entities.HistoryOfUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("ActionHistory")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("PrinterId")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("UserReceive")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("HistoryOfUsers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ActionHistory = 0,
                            DateTime = new DateTime(2021, 1, 28, 17, 28, 33, 161, DateTimeKind.Local).AddTicks(3538),
                            FileName = "C://xxx.docx",
                            PrinterId = 1,
                            UserId = new Guid("69bd714f-9576-45ba-b5b7-f00649be00de")
                        });
                });

            modelBuilder.Entity("PrintShareSolution.Data.Entities.ListPrinterOfUser", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("PrinterId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "PrinterId");

                    b.HasIndex("PrinterId");

                    b.ToTable("ListPrinterOfUser");

                    b.HasData(
                        new
                        {
                            UserId = new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                            PrinterId = 1
                        },
                        new
                        {
                            UserId = new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                            PrinterId = 2
                        });
                });

            modelBuilder.Entity("PrintShareSolution.Data.Entities.OrderPrintFile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("FilePath")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<long>("FileSize")
                        .HasColumnType("bigint");

                    b.Property<int>("PrinterId")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("PrinterId");

                    b.HasIndex("UserId");

                    b.ToTable("OrderPrintFiles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DateTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FileName = "xxx.docx",
                            FilePath = "C://xxx.docx",
                            FileSize = 0L,
                            PrinterId = 1,
                            UserId = new Guid("69bd714f-9576-45ba-b5b7-f00649be00de")
                        },
                        new
                        {
                            Id = 2,
                            DateTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FileName = "xxx.docx",
                            FilePath = "C://xxx.docx",
                            FileSize = 0L,
                            PrinterId = 2,
                            UserId = new Guid("69bd714f-9576-45ba-b5b7-f00649be00de")
                        });
                });

            modelBuilder.Entity("PrintShareSolution.Data.Entities.OrderSendFile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("FilePath")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<long>("FileSize")
                        .HasColumnType("bigint");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("UserNameReceive")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("OrderSendFiles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DateTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FileName = "xxx.docx",
                            FilePath = "C://xxx.docx",
                            FileSize = 0L,
                            UserId = new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                            UserNameReceive = "KhaiTb"
                        },
                        new
                        {
                            Id = 2,
                            DateTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FileName = "xxx.docx",
                            FilePath = "C://xxx.docx",
                            FileSize = 0L,
                            UserId = new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                            UserNameReceive = "KhaiTb"
                        });
                });

            modelBuilder.Entity("PrintShareSolution.Data.Entities.Printer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.HasKey("Id");

                    b.ToTable("Printers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "P1",
                            Status = 0
                        },
                        new
                        {
                            Id = 2,
                            Name = "P2",
                            Status = 0
                        });
                });

            modelBuilder.Entity("PrintShareSolution.Data.Entities.BlockList", b =>
                {
                    b.HasOne("PrintShareSolution.Data.Entities.AppUser", "AppUser")
                        .WithMany("BlockIds")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AppUser");
                });

            modelBuilder.Entity("PrintShareSolution.Data.Entities.HistoryOfUser", b =>
                {
                    b.HasOne("PrintShareSolution.Data.Entities.AppUser", "AppUser")
                        .WithMany("HistoryOfUsers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AppUser");
                });

            modelBuilder.Entity("PrintShareSolution.Data.Entities.ListPrinterOfUser", b =>
                {
                    b.HasOne("PrintShareSolution.Data.Entities.Printer", "Printer")
                        .WithMany("ListPrinterOfUsers")
                        .HasForeignKey("PrinterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PrintShareSolution.Data.Entities.AppUser", "AppUser")
                        .WithMany("ListPrinterOfUsers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AppUser");

                    b.Navigation("Printer");
                });

            modelBuilder.Entity("PrintShareSolution.Data.Entities.OrderPrintFile", b =>
                {
                    b.HasOne("PrintShareSolution.Data.Entities.Printer", "Printer")
                        .WithMany("OrderPrintFiles")
                        .HasForeignKey("PrinterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PrintShareSolution.Data.Entities.AppUser", "AppUser")
                        .WithMany("OrderPrintFiles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AppUser");

                    b.Navigation("Printer");
                });

            modelBuilder.Entity("PrintShareSolution.Data.Entities.OrderSendFile", b =>
                {
                    b.HasOne("PrintShareSolution.Data.Entities.AppUser", "AppUser")
                        .WithMany("OrderSendFiles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AppUser");
                });

            modelBuilder.Entity("PrintShareSolution.Data.Entities.AppUser", b =>
                {
                    b.Navigation("BlockIds");

                    b.Navigation("HistoryOfUsers");

                    b.Navigation("ListPrinterOfUsers");

                    b.Navigation("OrderPrintFiles");

                    b.Navigation("OrderSendFiles");
                });

            modelBuilder.Entity("PrintShareSolution.Data.Entities.Printer", b =>
                {
                    b.Navigation("ListPrinterOfUsers");

                    b.Navigation("OrderPrintFiles");
                });
#pragma warning restore 612, 618
        }
    }
}
