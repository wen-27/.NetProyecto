using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class AddMechanicDiagnostics : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MechanicDiagnostics",
                columns: table => new
                {
                    MechanicDiagnosticId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ServiceOrderId = table.Column<int>(type: "int", nullable: false),
                    MechanicPersonId = table.Column<int>(type: "int", nullable: false),
                    WorkshopChiefPersonId = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    Findings = table.Column<string>(type: "longtext", nullable: false),
                    RecommendedWork = table.Column<string>(type: "longtext", nullable: false),
                    WorkshopChiefComment = table.Column<string>(type: "longtext", nullable: true),
                    SubmittedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ReviewedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MechanicDiagnostics", x => x.MechanicDiagnosticId);
                    table.ForeignKey(
                        name: "FK_MechanicDiagnostics_Persons_MechanicPersonId",
                        column: x => x.MechanicPersonId,
                        principalTable: "Persons",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MechanicDiagnostics_Persons_WorkshopChiefPersonId",
                        column: x => x.WorkshopChiefPersonId,
                        principalTable: "Persons",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MechanicDiagnostics_ServiceOrders_ServiceOrderId",
                        column: x => x.ServiceOrderId,
                        principalTable: "ServiceOrders",
                        principalColumn: "ServiceOrderId",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_MechanicDiagnostics_MechanicPersonId",
                table: "MechanicDiagnostics",
                column: "MechanicPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_MechanicDiagnostics_ServiceOrderId_MechanicPersonId_Status",
                table: "MechanicDiagnostics",
                columns: new[] { "ServiceOrderId", "MechanicPersonId", "Status" });

            migrationBuilder.CreateIndex(
                name: "IX_MechanicDiagnostics_WorkshopChiefPersonId",
                table: "MechanicDiagnostics",
                column: "WorkshopChiefPersonId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "MechanicDiagnostics");
        }
    }
}
