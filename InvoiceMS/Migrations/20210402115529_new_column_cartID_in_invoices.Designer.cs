﻿// <auto-generated />
using System;
using InvoiceMS.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace InvoiceMS.Migrations
{
    [DbContext(typeof(InvoiceMsDbContext))]
    [Migration("20210402115529_new_column_cartID_in_invoices")]
    partial class new_column_cartID_in_invoices
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("InvoiceMS.Models.Entities.Invoice", b =>
                {
                    b.Property<int>("InvoiceID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CountOfCommodities")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("ShoppingCartID")
                        .HasColumnType("int");

                    b.Property<decimal>("Total")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("InvoiceID");

                    b.ToTable("Invoices");
                });

            modelBuilder.Entity("InvoiceMS.Models.Entities.Payment", b =>
                {
                    b.Property<int>("PaymentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("InvoiceID")
                        .HasColumnType("int");

                    b.Property<DateTime>("PaidAt")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("PaymentAmount")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("PaymentID");

                    b.HasIndex("InvoiceID");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("InvoiceMS.Models.Entities.Payment", b =>
                {
                    b.HasOne("InvoiceMS.Models.Entities.Invoice", "Invoice")
                        .WithMany()
                        .HasForeignKey("InvoiceID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Invoice");
                });
#pragma warning restore 612, 618
        }
    }
}
