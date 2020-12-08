﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SdnMonitorData;

namespace SdnMonitorData.Migrations
{
    [DbContext(typeof(SdnListContext))]
    partial class SdnListContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("Models.sdnList", b =>
                {
                    b.Property<string>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("publshInformationid")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("id");

                    b.HasIndex("publshInformationid");

                    b.ToTable("sdnLists");
                });

            modelBuilder.Entity("Models.sdnListPublshInformation", b =>
                {
                    b.Property<string>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Publish_Date")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Record_Count")
                        .HasColumnType("int");

                    b.Property<bool>("Record_CountSpecified")
                        .HasColumnType("bit");

                    b.HasKey("id");

                    b.ToTable("sdnListPublshInformation");
                });

            modelBuilder.Entity("Models.sdnListSdnEntry", b =>
                {
                    b.Property<int>("uid")
                        .HasColumnType("int");

                    b.Property<string>("InternalData")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("firstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("lastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("remarks")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("sdnListid")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("sdnType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("vesselInfoid")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("uid");

                    b.HasIndex("sdnListid");

                    b.HasIndex("vesselInfoid");

                    b.ToTable("sdnListSdnEntry");
                });

            modelBuilder.Entity("Models.sdnListSdnEntryAddress", b =>
                {
                    b.Property<int>("uid")
                        .HasColumnType("int");

                    b.Property<string>("address1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("address2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("address3")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("city")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("postalCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("sdnListSdnEntryuid")
                        .HasColumnType("int");

                    b.Property<string>("stateOrProvince")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("uid");

                    b.HasIndex("sdnListSdnEntryuid");

                    b.ToTable("sdnListSdnEntryAddress");
                });

            modelBuilder.Entity("Models.sdnListSdnEntryAka", b =>
                {
                    b.Property<int>("uid")
                        .HasColumnType("int");

                    b.Property<string>("category")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("firstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("lastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("sdnListSdnEntryuid")
                        .HasColumnType("int");

                    b.Property<string>("type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("uid");

                    b.HasIndex("sdnListSdnEntryuid");

                    b.ToTable("sdnListSdnEntryAka");
                });

            modelBuilder.Entity("Models.sdnListSdnEntryCitizenship", b =>
                {
                    b.Property<int>("uid")
                        .HasColumnType("int");

                    b.Property<string>("country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("mainEntry")
                        .HasColumnType("bit");

                    b.Property<int?>("sdnListSdnEntryuid")
                        .HasColumnType("int");

                    b.HasKey("uid");

                    b.HasIndex("sdnListSdnEntryuid");

                    b.ToTable("sdnListSdnEntryCitizenship");
                });

            modelBuilder.Entity("Models.sdnListSdnEntryDateOfBirthItem", b =>
                {
                    b.Property<int>("uid")
                        .HasColumnType("int");

                    b.Property<string>("dateOfBirth")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("mainEntry")
                        .HasColumnType("bit");

                    b.Property<int?>("sdnListSdnEntryuid")
                        .HasColumnType("int");

                    b.HasKey("uid");

                    b.HasIndex("sdnListSdnEntryuid");

                    b.ToTable("sdnListSdnEntryDateOfBirthItem");
                });

            modelBuilder.Entity("Models.sdnListSdnEntryID", b =>
                {
                    b.Property<int>("uid")
                        .HasColumnType("int");

                    b.Property<string>("expirationDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("idCountry")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("idNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("idType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("issueDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("sdnListSdnEntryuid")
                        .HasColumnType("int");

                    b.HasKey("uid");

                    b.HasIndex("sdnListSdnEntryuid");

                    b.ToTable("sdnListSdnEntryID");
                });

            modelBuilder.Entity("Models.sdnListSdnEntryNationality", b =>
                {
                    b.Property<int>("uid")
                        .HasColumnType("int");

                    b.Property<string>("country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("mainEntry")
                        .HasColumnType("bit");

                    b.Property<int?>("sdnListSdnEntryuid")
                        .HasColumnType("int");

                    b.HasKey("uid");

                    b.HasIndex("sdnListSdnEntryuid");

                    b.ToTable("sdnListSdnEntryNationality");
                });

            modelBuilder.Entity("Models.sdnListSdnEntryPlaceOfBirthItem", b =>
                {
                    b.Property<int>("uid")
                        .HasColumnType("int");

                    b.Property<bool>("mainEntry")
                        .HasColumnType("bit");

                    b.Property<string>("placeOfBirth")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("sdnListSdnEntryuid")
                        .HasColumnType("int");

                    b.HasKey("uid");

                    b.HasIndex("sdnListSdnEntryuid");

                    b.ToTable("sdnListSdnEntryPlaceOfBirthItem");
                });

            modelBuilder.Entity("Models.sdnListSdnEntryVesselInfo", b =>
                {
                    b.Property<string>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("callSign")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("grossRegisteredTonnage")
                        .HasColumnType("int");

                    b.Property<bool>("grossRegisteredTonnageSpecified")
                        .HasColumnType("bit");

                    b.Property<int>("tonnage")
                        .HasColumnType("int");

                    b.Property<bool>("tonnageSpecified")
                        .HasColumnType("bit");

                    b.Property<string>("vesselFlag")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("vesselOwner")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("vesselType")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("sdnListSdnEntryVesselInfo");
                });

            modelBuilder.Entity("Models.sdnList", b =>
                {
                    b.HasOne("Models.sdnListPublshInformation", "publshInformation")
                        .WithMany()
                        .HasForeignKey("publshInformationid");

                    b.Navigation("publshInformation");
                });

            modelBuilder.Entity("Models.sdnListSdnEntry", b =>
                {
                    b.HasOne("Models.sdnList", null)
                        .WithMany("sdnEntry")
                        .HasForeignKey("sdnListid");

                    b.HasOne("Models.sdnListSdnEntryVesselInfo", "vesselInfo")
                        .WithMany()
                        .HasForeignKey("vesselInfoid");

                    b.Navigation("vesselInfo");
                });

            modelBuilder.Entity("Models.sdnListSdnEntryAddress", b =>
                {
                    b.HasOne("Models.sdnListSdnEntry", null)
                        .WithMany("addressList")
                        .HasForeignKey("sdnListSdnEntryuid");
                });

            modelBuilder.Entity("Models.sdnListSdnEntryAka", b =>
                {
                    b.HasOne("Models.sdnListSdnEntry", null)
                        .WithMany("akaList")
                        .HasForeignKey("sdnListSdnEntryuid");
                });

            modelBuilder.Entity("Models.sdnListSdnEntryCitizenship", b =>
                {
                    b.HasOne("Models.sdnListSdnEntry", null)
                        .WithMany("citizenshipList")
                        .HasForeignKey("sdnListSdnEntryuid");
                });

            modelBuilder.Entity("Models.sdnListSdnEntryDateOfBirthItem", b =>
                {
                    b.HasOne("Models.sdnListSdnEntry", null)
                        .WithMany("dateOfBirthList")
                        .HasForeignKey("sdnListSdnEntryuid");
                });

            modelBuilder.Entity("Models.sdnListSdnEntryID", b =>
                {
                    b.HasOne("Models.sdnListSdnEntry", null)
                        .WithMany("idList")
                        .HasForeignKey("sdnListSdnEntryuid");
                });

            modelBuilder.Entity("Models.sdnListSdnEntryNationality", b =>
                {
                    b.HasOne("Models.sdnListSdnEntry", null)
                        .WithMany("nationalityList")
                        .HasForeignKey("sdnListSdnEntryuid");
                });

            modelBuilder.Entity("Models.sdnListSdnEntryPlaceOfBirthItem", b =>
                {
                    b.HasOne("Models.sdnListSdnEntry", null)
                        .WithMany("placeOfBirthList")
                        .HasForeignKey("sdnListSdnEntryuid");
                });

            modelBuilder.Entity("Models.sdnList", b =>
                {
                    b.Navigation("sdnEntry");
                });

            modelBuilder.Entity("Models.sdnListSdnEntry", b =>
                {
                    b.Navigation("addressList");

                    b.Navigation("akaList");

                    b.Navigation("citizenshipList");

                    b.Navigation("dateOfBirthList");

                    b.Navigation("idList");

                    b.Navigation("nationalityList");

                    b.Navigation("placeOfBirthList");
                });
#pragma warning restore 612, 618
        }
    }
}