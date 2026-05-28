using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddOperationalWorkflowAndSeeders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeliveryDate",
                table: "ServiceOrders",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "EstimatedTotal",
                table: "ServiceOrders",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "ClientPersonId",
                table: "Payments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeliveryDate",
                table: "Payments",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RejectedReason",
                table: "Payments",
                type: "text",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "VerifiedAt",
                table: "Payments",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VerifiedByReceptionistPersonId",
                table: "Payments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "OrderServices",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "OrderServices",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "WorkshopServiceId",
                table: "OrderServices",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "StockSubmissions",
                columns: table => new
                {
                    StockSubmissionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    WarehouseChiefPersonId = table.Column<int>(type: "int", nullable: false),
                    InventoryManagerPersonId = table.Column<int>(type: "int", nullable: true),
                    ProductName = table.Column<string>(type: "varchar(160)", maxLength: 160, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ReferenceCode = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SupplierId = table.Column<int>(type: "int", nullable: false),
                    SupplierPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ProfitPercentage = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    SalePrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    MinimumStock = table.Column<int>(type: "int", nullable: false),
                    PartCategoryId = table.Column<int>(type: "int", nullable: true),
                    PartBrandId = table.Column<int>(type: "int", nullable: true),
                    CategoryName = table.Column<string>(type: "varchar(80)", maxLength: 80, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BrandName = table.Column<string>(type: "varchar(80)", maxLength: 80, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "text", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    WarehouseComment = table.Column<string>(type: "text", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    InventoryManagerComment = table.Column<string>(type: "text", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ReviewedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    AddedToInventoryAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockSubmissions", x => x.StockSubmissionId);
                    table.ForeignKey(
                        name: "FK_StockSubmissions_PartBrands_PartBrandId",
                        column: x => x.PartBrandId,
                        principalTable: "PartBrands",
                        principalColumn: "PartBrandId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StockSubmissions_PartCategories_PartCategoryId",
                        column: x => x.PartCategoryId,
                        principalTable: "PartCategories",
                        principalColumn: "PartCategoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StockSubmissions_Persons_InventoryManagerPersonId",
                        column: x => x.InventoryManagerPersonId,
                        principalTable: "Persons",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StockSubmissions_Persons_WarehouseChiefPersonId",
                        column: x => x.WarehouseChiefPersonId,
                        principalTable: "Persons",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StockSubmissions_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "SupplierId",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "WorkshopServices",
                columns: table => new
                {
                    WorkshopServiceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(120)", maxLength: 120, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "text", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Category = table.Column<string>(type: "varchar(80)", maxLength: 80, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LaborPercentage = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    PartsSubtotal = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    LaborAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    FinalPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkshopServices", x => x.WorkshopServiceId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "InventoryHistory",
                columns: table => new
                {
                    InventoryHistoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PartId = table.Column<int>(type: "int", nullable: false),
                    StockSubmissionId = table.Column<int>(type: "int", nullable: true),
                    QuantityChange = table.Column<int>(type: "int", nullable: false),
                    ResultingStock = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Action = table.Column<string>(type: "varchar(80)", maxLength: 80, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Comment = table.Column<string>(type: "text", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryHistory", x => x.InventoryHistoryId);
                    table.ForeignKey(
                        name: "FK_InventoryHistory_Parts_PartId",
                        column: x => x.PartId,
                        principalTable: "Parts",
                        principalColumn: "PartId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InventoryHistory_StockSubmissions_StockSubmissionId",
                        column: x => x.StockSubmissionId,
                        principalTable: "StockSubmissions",
                        principalColumn: "StockSubmissionId",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AdditionalServiceRequests",
                columns: table => new
                {
                    AdditionalServiceRequestId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ServiceOrderId = table.Column<int>(type: "int", nullable: false),
                    MechanicPersonId = table.Column<int>(type: "int", nullable: false),
                    WorkshopChiefPersonId = table.Column<int>(type: "int", nullable: true),
                    ClientPersonId = table.Column<int>(type: "int", nullable: false),
                    RequestType = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    WorkshopServiceId = table.Column<int>(type: "int", nullable: true),
                    PartId = table.Column<int>(type: "int", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: true),
                    TechnicalComment = table.Column<string>(type: "text", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    WorkshopChiefComment = table.Column<string>(type: "text", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClientComment = table.Column<string>(type: "text", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EstimatedPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    WorkshopChiefReviewedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    ClientReviewedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    AddedToOrderAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdditionalServiceRequests", x => x.AdditionalServiceRequestId);
                    table.ForeignKey(
                        name: "FK_AdditionalServiceRequests_Parts_PartId",
                        column: x => x.PartId,
                        principalTable: "Parts",
                        principalColumn: "PartId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AdditionalServiceRequests_Persons_ClientPersonId",
                        column: x => x.ClientPersonId,
                        principalTable: "Persons",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AdditionalServiceRequests_Persons_MechanicPersonId",
                        column: x => x.MechanicPersonId,
                        principalTable: "Persons",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AdditionalServiceRequests_Persons_WorkshopChiefPersonId",
                        column: x => x.WorkshopChiefPersonId,
                        principalTable: "Persons",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AdditionalServiceRequests_ServiceOrders_ServiceOrderId",
                        column: x => x.ServiceOrderId,
                        principalTable: "ServiceOrders",
                        principalColumn: "ServiceOrderId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AdditionalServiceRequests_WorkshopServices_WorkshopServiceId",
                        column: x => x.WorkshopServiceId,
                        principalTable: "WorkshopServices",
                        principalColumn: "WorkshopServiceId",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "WorkshopServiceParts",
                columns: table => new
                {
                    WorkshopServicePartId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    WorkshopServiceId = table.Column<int>(type: "int", nullable: false),
                    PartId = table.Column<int>(type: "int", nullable: false),
                    QuantityRequired = table.Column<int>(type: "int", nullable: false),
                    UnitSalePrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    LineTotal = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkshopServiceParts", x => x.WorkshopServicePartId);
                    table.ForeignKey(
                        name: "FK_WorkshopServiceParts_Parts_PartId",
                        column: x => x.PartId,
                        principalTable: "Parts",
                        principalColumn: "PartId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkshopServiceParts_WorkshopServices_WorkshopServiceId",
                        column: x => x.WorkshopServiceId,
                        principalTable: "WorkshopServices",
                        principalColumn: "WorkshopServiceId",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "OrderStatuses",
                keyColumn: "OrderStatusId",
                keyValue: 1,
                column: "Name",
                value: "Created");

            migrationBuilder.UpdateData(
                table: "OrderStatuses",
                keyColumn: "OrderStatusId",
                keyValue: 2,
                column: "Name",
                value: "PendingAssignment");

            migrationBuilder.UpdateData(
                table: "OrderStatuses",
                keyColumn: "OrderStatusId",
                keyValue: 3,
                column: "Name",
                value: "Assigned");

            migrationBuilder.UpdateData(
                table: "OrderStatuses",
                keyColumn: "OrderStatusId",
                keyValue: 4,
                column: "Name",
                value: "InProgress");

            migrationBuilder.UpdateData(
                table: "OrderStatuses",
                keyColumn: "OrderStatusId",
                keyValue: 5,
                column: "Name",
                value: "PendingClientApproval");

            migrationBuilder.InsertData(
                table: "OrderStatuses",
                columns: new[] { "OrderStatusId", "Name" },
                values: new object[,]
                {
                    { 6, "WaitingForPayment" },
                    { 7, "PaymentUnderReview" },
                    { 8, "Paid" },
                    { 9, "ReadyForDelivery" },
                    { 10, "Delivered" },
                    { 11, "Cancelled" }
                });

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodId",
                keyValue: 1,
                column: "Name",
                value: "Efectivo");

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodId",
                keyValue: 2,
                column: "Name",
                value: "Transferencia");

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodId",
                keyValue: 3,
                column: "Name",
                value: "Tarjeta débito");

            migrationBuilder.InsertData(
                table: "PaymentMethods",
                columns: new[] { "PaymentMethodId", "Name" },
                values: new object[,]
                {
                    { 4, "Tarjeta crédito" },
                    { 5, "Nequi" },
                    { 6, "Daviplata" }
                });

            migrationBuilder.UpdateData(
                table: "PaymentStatuses",
                keyColumn: "PaymentStatusId",
                keyValue: 2,
                column: "Name",
                value: "PendingReceptionVerification");

            migrationBuilder.UpdateData(
                table: "PaymentStatuses",
                keyColumn: "PaymentStatusId",
                keyValue: 3,
                column: "Name",
                value: "Approved");

            migrationBuilder.UpdateData(
                table: "PaymentStatuses",
                keyColumn: "PaymentStatusId",
                keyValue: 4,
                column: "Name",
                value: "Rejected");

            migrationBuilder.InsertData(
                table: "PaymentStatuses",
                columns: new[] { "PaymentStatusId", "Name" },
                values: new object[] { 5, "Refunded" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "RoleName" },
                values: new object[,]
                {
                    { 5, "WorkshopChief" },
                    { 6, "WarehouseChief" },
                    { 7, "InventoryManager" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Payments_ClientPersonId",
                table: "Payments",
                column: "ClientPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_VerifiedByReceptionistPersonId",
                table: "Payments",
                column: "VerifiedByReceptionistPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderServices_WorkshopServiceId",
                table: "OrderServices",
                column: "WorkshopServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_AdditionalServiceRequests_ClientPersonId",
                table: "AdditionalServiceRequests",
                column: "ClientPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_AdditionalServiceRequests_MechanicPersonId",
                table: "AdditionalServiceRequests",
                column: "MechanicPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_AdditionalServiceRequests_PartId",
                table: "AdditionalServiceRequests",
                column: "PartId");

            migrationBuilder.CreateIndex(
                name: "IX_AdditionalServiceRequests_ServiceOrderId",
                table: "AdditionalServiceRequests",
                column: "ServiceOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_AdditionalServiceRequests_WorkshopChiefPersonId",
                table: "AdditionalServiceRequests",
                column: "WorkshopChiefPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_AdditionalServiceRequests_WorkshopServiceId",
                table: "AdditionalServiceRequests",
                column: "WorkshopServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryHistory_PartId",
                table: "InventoryHistory",
                column: "PartId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryHistory_StockSubmissionId",
                table: "InventoryHistory",
                column: "StockSubmissionId");

            migrationBuilder.CreateIndex(
                name: "IX_StockSubmissions_InventoryManagerPersonId",
                table: "StockSubmissions",
                column: "InventoryManagerPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_StockSubmissions_PartBrandId",
                table: "StockSubmissions",
                column: "PartBrandId");

            migrationBuilder.CreateIndex(
                name: "IX_StockSubmissions_PartCategoryId",
                table: "StockSubmissions",
                column: "PartCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_StockSubmissions_SupplierId",
                table: "StockSubmissions",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_StockSubmissions_WarehouseChiefPersonId",
                table: "StockSubmissions",
                column: "WarehouseChiefPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkshopServiceParts_PartId",
                table: "WorkshopServiceParts",
                column: "PartId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkshopServiceParts_WorkshopServiceId_PartId",
                table: "WorkshopServiceParts",
                columns: new[] { "WorkshopServiceId", "PartId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkshopServices_Name",
                table: "WorkshopServices",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderServices_WorkshopServices_WorkshopServiceId",
                table: "OrderServices",
                column: "WorkshopServiceId",
                principalTable: "WorkshopServices",
                principalColumn: "WorkshopServiceId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Persons_ClientPersonId",
                table: "Payments",
                column: "ClientPersonId",
                principalTable: "Persons",
                principalColumn: "PersonId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Persons_VerifiedByReceptionistPersonId",
                table: "Payments",
                column: "VerifiedByReceptionistPersonId",
                principalTable: "Persons",
                principalColumn: "PersonId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderServices_WorkshopServices_WorkshopServiceId",
                table: "OrderServices");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Persons_ClientPersonId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Persons_VerifiedByReceptionistPersonId",
                table: "Payments");

            migrationBuilder.DropTable(
                name: "AdditionalServiceRequests");

            migrationBuilder.DropTable(
                name: "InventoryHistory");

            migrationBuilder.DropTable(
                name: "WorkshopServiceParts");

            migrationBuilder.DropTable(
                name: "StockSubmissions");

            migrationBuilder.DropTable(
                name: "WorkshopServices");

            migrationBuilder.DropIndex(
                name: "IX_Payments_ClientPersonId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_VerifiedByReceptionistPersonId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_OrderServices_WorkshopServiceId",
                table: "OrderServices");

            migrationBuilder.DeleteData(
                table: "OrderStatuses",
                keyColumn: "OrderStatusId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "OrderStatuses",
                keyColumn: "OrderStatusId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "OrderStatuses",
                keyColumn: "OrderStatusId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "OrderStatuses",
                keyColumn: "OrderStatusId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "OrderStatuses",
                keyColumn: "OrderStatusId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "OrderStatuses",
                keyColumn: "OrderStatusId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "PaymentStatuses",
                keyColumn: "PaymentStatusId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 7);

            migrationBuilder.DropColumn(
                name: "DeliveryDate",
                table: "ServiceOrders");

            migrationBuilder.DropColumn(
                name: "EstimatedTotal",
                table: "ServiceOrders");

            migrationBuilder.DropColumn(
                name: "ClientPersonId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "DeliveryDate",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "RejectedReason",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "VerifiedAt",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "VerifiedByReceptionistPersonId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "OrderServices");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "OrderServices");

            migrationBuilder.DropColumn(
                name: "WorkshopServiceId",
                table: "OrderServices");

            migrationBuilder.UpdateData(
                table: "OrderStatuses",
                keyColumn: "OrderStatusId",
                keyValue: 1,
                column: "Name",
                value: "Pending");

            migrationBuilder.UpdateData(
                table: "OrderStatuses",
                keyColumn: "OrderStatusId",
                keyValue: 2,
                column: "Name",
                value: "InProgress");

            migrationBuilder.UpdateData(
                table: "OrderStatuses",
                keyColumn: "OrderStatusId",
                keyValue: 3,
                column: "Name",
                value: "Completed");

            migrationBuilder.UpdateData(
                table: "OrderStatuses",
                keyColumn: "OrderStatusId",
                keyValue: 4,
                column: "Name",
                value: "Cancelled");

            migrationBuilder.UpdateData(
                table: "OrderStatuses",
                keyColumn: "OrderStatusId",
                keyValue: 5,
                column: "Name",
                value: "Voided");

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodId",
                keyValue: 1,
                column: "Name",
                value: "Cash");

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodId",
                keyValue: 2,
                column: "Name",
                value: "Card");

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodId",
                keyValue: 3,
                column: "Name",
                value: "BankTransfer");

            migrationBuilder.UpdateData(
                table: "PaymentStatuses",
                keyColumn: "PaymentStatusId",
                keyValue: 2,
                column: "Name",
                value: "Completed");

            migrationBuilder.UpdateData(
                table: "PaymentStatuses",
                keyColumn: "PaymentStatusId",
                keyValue: 3,
                column: "Name",
                value: "Refunded");

            migrationBuilder.UpdateData(
                table: "PaymentStatuses",
                keyColumn: "PaymentStatusId",
                keyValue: 4,
                column: "Name",
                value: "Failed");
        }
    }
}
