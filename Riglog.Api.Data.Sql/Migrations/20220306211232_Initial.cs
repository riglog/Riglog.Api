using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Riglog.Api.Data.Sql.Migrations
{
    public partial class Initial : Migration
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
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComputerTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OsFamilies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OsFamilies", x => x.Id);
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
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OsDistributions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OsFamilyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OsDistributions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OsDistributions_OsFamilies_OsFamilyId",
                        column: x => x.OsFamilyId,
                        principalTable: "OsFamilies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OsEditions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OsDistributionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OsEditions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OsEditions_OsDistributions_OsDistributionId",
                        column: x => x.OsDistributionId,
                        principalTable: "OsDistributions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OsVersions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OsDistributionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OsVersions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OsVersions_OsDistributions_OsDistributionId",
                        column: x => x.OsDistributionId,
                        principalTable: "OsDistributions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Computers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ComputerTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OsFamilyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OsDistributionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OsVersionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OsEditionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                        name: "FK_Computers_OsDistributions_OsDistributionId",
                        column: x => x.OsDistributionId,
                        principalTable: "OsDistributions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Computers_OsEditions_OsEditionId",
                        column: x => x.OsEditionId,
                        principalTable: "OsEditions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Computers_OsFamilies_OsFamilyId",
                        column: x => x.OsFamilyId,
                        principalTable: "OsFamilies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Computers_OsVersions_OsVersionId",
                        column: x => x.OsVersionId,
                        principalTable: "OsVersions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ComputerUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ComputerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                        onDelete: ReferentialAction.Cascade);
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
                name: "IX_Computers_OsDistributionId",
                table: "Computers",
                column: "OsDistributionId");

            migrationBuilder.CreateIndex(
                name: "IX_Computers_OsEditionId",
                table: "Computers",
                column: "OsEditionId");

            migrationBuilder.CreateIndex(
                name: "IX_Computers_OsFamilyId",
                table: "Computers",
                column: "OsFamilyId");

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
                name: "IX_OsDistributions_IsDeleted",
                table: "OsDistributions",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_OsDistributions_Name_OsFamilyId",
                table: "OsDistributions",
                columns: new[] { "Name", "OsFamilyId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OsDistributions_OsFamilyId",
                table: "OsDistributions",
                column: "OsFamilyId");

            migrationBuilder.CreateIndex(
                name: "IX_OsEditions_IsDeleted",
                table: "OsEditions",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_OsEditions_Name_OsDistributionId",
                table: "OsEditions",
                columns: new[] { "Name", "OsDistributionId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OsEditions_OsDistributionId",
                table: "OsEditions",
                column: "OsDistributionId");

            migrationBuilder.CreateIndex(
                name: "IX_OsFamilies_IsDeleted",
                table: "OsFamilies",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_OsFamilies_Name",
                table: "OsFamilies",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OsVersions_IsDeleted",
                table: "OsVersions",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_OsVersions_Name_OsDistributionId",
                table: "OsVersions",
                columns: new[] { "Name", "OsDistributionId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OsVersions_OsDistributionId",
                table: "OsVersions",
                column: "OsDistributionId");

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
                name: "OsEditions");

            migrationBuilder.DropTable(
                name: "OsVersions");

            migrationBuilder.DropTable(
                name: "OsDistributions");

            migrationBuilder.DropTable(
                name: "OsFamilies");
        }
    }
}
