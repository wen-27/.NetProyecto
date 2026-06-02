using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    [Migration("20260602123000_EnsureVehiclePlateColumn")]
    public partial class EnsureVehiclePlateColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                SET @plate_column_exists = (
                    SELECT COUNT(*)
                    FROM INFORMATION_SCHEMA.COLUMNS
                    WHERE TABLE_SCHEMA = DATABASE()
                      AND TABLE_NAME = 'Vehicles'
                      AND COLUMN_NAME = 'Plate'
                );

                SET @add_plate_sql = IF(
                    @plate_column_exists = 0,
                    'ALTER TABLE `Vehicles` ADD COLUMN `Plate` varchar(10) NOT NULL DEFAULT '''';',
                    'SELECT 1;'
                );

                PREPARE add_plate_statement FROM @add_plate_sql;
                EXECUTE add_plate_statement;
                DEALLOCATE PREPARE add_plate_statement;

                UPDATE `Vehicles`
                SET `Plate` = LEFT(`VIN`, 10)
                WHERE `Plate` = '';

                SET @plate_index_exists = (
                    SELECT COUNT(*)
                    FROM INFORMATION_SCHEMA.STATISTICS
                    WHERE TABLE_SCHEMA = DATABASE()
                      AND TABLE_NAME = 'Vehicles'
                      AND INDEX_NAME = 'IX_Vehicles_Plate'
                );

                SET @add_plate_index_sql = IF(
                    @plate_index_exists = 0,
                    'CREATE UNIQUE INDEX `IX_Vehicles_Plate` ON `Vehicles` (`Plate`);',
                    'SELECT 1;'
                );

                PREPARE add_plate_index_statement FROM @add_plate_index_sql;
                EXECUTE add_plate_index_statement;
                DEALLOCATE PREPARE add_plate_index_statement;
                """);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Repair migration: keep Plate because application code depends on it.
        }
    }
}
