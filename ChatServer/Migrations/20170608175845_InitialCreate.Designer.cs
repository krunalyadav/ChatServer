using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using ChatServer.Models;

namespace ChatServer.Migrations
{
    [DbContext(typeof(ChatDbContext))]
    [Migration("20170608175845_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ChatServer.Models.ChatLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("FromUserId");

                    b.Property<string>("Message");

                    b.Property<int>("ToUserId");

                    b.HasKey("Id");

                    b.HasIndex("FromUserId");

                    b.HasIndex("ToUserId");

                    b.ToTable("ChatLog");
                });

            modelBuilder.Entity("ChatServer.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("Key");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("ChatServer.Models.ChatLog", b =>
                {
                    b.HasOne("ChatServer.Models.User", "FromUser")
                        .WithMany("FromUserChatLog")
                        .HasForeignKey("FromUserId");

                    b.HasOne("ChatServer.Models.User", "ToUser")
                        .WithMany("ToUserChatLog")
                        .HasForeignKey("ToUserId");
                });
        }
    }
}
