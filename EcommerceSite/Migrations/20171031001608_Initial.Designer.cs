﻿// <auto-generated />
using EcommerceSite.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace EcommerceSite.Migrations
{
    [DbContext(typeof(ItemContext))]
    [Migration("20171031001608_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EcommerceSite.Models.Item", b =>
                {
                    b.Property<int>("ItemNumber")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Desc");

                    b.Property<string>("ImageURL");

                    b.Property<string>("ListingURL");

                    b.Property<string>("Name");

                    b.Property<double>("Price");

                    b.HasKey("ItemNumber");

                    b.ToTable("Items");
                });
#pragma warning restore 612, 618
        }
    }
}
