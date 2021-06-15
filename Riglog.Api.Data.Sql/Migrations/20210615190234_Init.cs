﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Riglog.Api.Data.Sql.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ComputerTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComputerTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OsVersions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OsVersions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IsSuperAdmin = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OsEditions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OsVersionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OsEditions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OsEditions_OsVersions_OsVersionId",
                        column: x => x.OsVersionId,
                        principalTable: "OsVersions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OsTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OsEditionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OsTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OsTypes_OsEditions_OsEditionId",
                        column: x => x.OsEditionId,
                        principalTable: "OsEditions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Computers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ComputerTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OsTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OsEditionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OsVersionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Computers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Computers_ComputerTypes_ComputerTypeId",
                        column: x => x.ComputerTypeId,
                        principalTable: "ComputerTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Computers_OsEditions_OsEditionId",
                        column: x => x.OsEditionId,
                        principalTable: "OsEditions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Computers_OsTypes_OsTypeId",
                        column: x => x.OsTypeId,
                        principalTable: "OsTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Computers_OsVersions_OsVersionId",
                        column: x => x.OsVersionId,
                        principalTable: "OsVersions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ComputerUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ComputerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComputerUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComputerUsers_Computers_ComputerId",
                        column: x => x.ComputerId,
                        principalTable: "Computers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComputerUsers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Computers_ComputerTypeId",
                table: "Computers",
                column: "ComputerTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Computers_IsDeleted",
                table: "Computers",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Computers_Name",
                table: "Computers",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Computers_OsEditionId",
                table: "Computers",
                column: "OsEditionId");

            migrationBuilder.CreateIndex(
                name: "IX_Computers_OsTypeId",
                table: "Computers",
                column: "OsTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Computers_OsVersionId",
                table: "Computers",
                column: "OsVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_ComputerTypes_IsDeleted",
                table: "ComputerTypes",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_ComputerTypes_Name",
                table: "ComputerTypes",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ComputerUsers_ComputerId",
                table: "ComputerUsers",
                column: "ComputerId");

            migrationBuilder.CreateIndex(
                name: "IX_ComputerUsers_IsAdmin",
                table: "ComputerUsers",
                column: "IsAdmin");

            migrationBuilder.CreateIndex(
                name: "IX_ComputerUsers_IsDeleted",
                table: "ComputerUsers",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_ComputerUsers_UserId",
                table: "ComputerUsers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OsEditions_IsDeleted",
                table: "OsEditions",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_OsEditions_OsVersionId",
                table: "OsEditions",
                column: "OsVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_OsTypes_IsDeleted",
                table: "OsTypes",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_OsTypes_Name",
                table: "OsTypes",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OsTypes_OsEditionId",
                table: "OsTypes",
                column: "OsEditionId");

            migrationBuilder.CreateIndex(
                name: "IX_OsVersions_IsDeleted",
                table: "OsVersions",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_IsDeleted",
                table: "Users",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Users_IsSuperAdmin",
                table: "Users",
                column: "IsSuperAdmin");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Phone",
                table: "Users",
                column: "Phone",
                unique: true,
                filter: "[Phone] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComputerUsers");

            migrationBuilder.DropTable(
                name: "Computers");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "ComputerTypes");

            migrationBuilder.DropTable(
                name: "OsTypes");

            migrationBuilder.DropTable(
                name: "OsEditions");

            migrationBuilder.DropTable(
                name: "OsVersions");
        }
    }
}