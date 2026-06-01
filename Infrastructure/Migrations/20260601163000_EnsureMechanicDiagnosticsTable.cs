using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    [Migration("20260601163000_EnsureMechanicDiagnosticsTable")]
    public partial class EnsureMechanicDiagnosticsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                CREATE TABLE IF NOT EXISTS `MechanicDiagnostics` (
                    `MechanicDiagnosticId` int NOT NULL AUTO_INCREMENT,
                    `ServiceOrderId` int NOT NULL,
                    `MechanicPersonId` int NOT NULL,
                    `WorkshopChiefPersonId` int NULL,
                    `Status` int NOT NULL DEFAULT 1,
                    `Findings` longtext NOT NULL,
                    `RecommendedWork` longtext NOT NULL,
                    `WorkshopChiefComment` longtext NULL,
                    `SubmittedAt` datetime(6) NOT NULL,
                    `ReviewedAt` datetime(6) NULL,
                    `CreatedAt` datetime(6) NOT NULL,
                    PRIMARY KEY (`MechanicDiagnosticId`),
                    KEY `IX_MechanicDiagnostics_MechanicPersonId` (`MechanicPersonId`),
                    KEY `IX_MechanicDiagnostics_ServiceOrderId_MechanicPersonId_Status` (`ServiceOrderId`, `MechanicPersonId`, `Status`),
                    KEY `IX_MechanicDiagnostics_WorkshopChiefPersonId` (`WorkshopChiefPersonId`),
                    CONSTRAINT `FK_MechanicDiagnostics_Persons_MechanicPersonId`
                        FOREIGN KEY (`MechanicPersonId`) REFERENCES `Persons` (`PersonId`) ON DELETE RESTRICT,
                    CONSTRAINT `FK_MechanicDiagnostics_Persons_WorkshopChiefPersonId`
                        FOREIGN KEY (`WorkshopChiefPersonId`) REFERENCES `Persons` (`PersonId`) ON DELETE RESTRICT,
                    CONSTRAINT `FK_MechanicDiagnostics_ServiceOrders_ServiceOrderId`
                        FOREIGN KEY (`ServiceOrderId`) REFERENCES `ServiceOrders` (`ServiceOrderId`) ON DELETE RESTRICT
                ) CHARACTER SET utf8mb4;
                """);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Repair migration: intentionally does not drop production or development diagnostics.
        }
    }
}
