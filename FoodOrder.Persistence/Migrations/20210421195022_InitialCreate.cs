using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodOrder.Persistence.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.CreateTable(
                name: "suppliers",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "public.uuid_generate_v4()"),
                    Name = table.Column<string>(type: "character varying", maxLength: 100, nullable: true),
                    WebUrl = table.Column<string>(type: "character varying", maxLength: 500, nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "NOW()"),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_supplier_id", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "UUID_GENERATE_V4()"),
                    FirstName = table.Column<string>(type: "character varying", maxLength: 100, nullable: true),
                    LastName = table.Column<string>(type: "character varying", maxLength: 100, nullable: true),
                    Email = table.Column<string>(type: "character varying", maxLength: 100, nullable: true),
                    Password = table.Column<string>(type: "character varying", maxLength: 250, nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "NOW()"),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_id", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "public.uuid_generate_v4()"),
                    CreatedUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    SupplierId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying", maxLength: 100, nullable: true),
                    Description = table.Column<string>(type: "character varying", maxLength: 1000, nullable: true),
                    ExpireDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "NOW()"),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_order_id", x => x.Id);
                    table.ForeignKey(
                        name: "fk_supplier_order_id",
                        column: x => x.SupplierId,
                        principalSchema: "public",
                        principalTable: "suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_user_order_id",
                        column: x => x.CreatedUserId,
                        principalSchema: "public",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "order_items",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "public.uuid_generate_v4()"),
                    CreatedUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    OrderId = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<string>(type: "character varying", maxLength: 1000, nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "NOW()"),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_orderItem_id", x => x.Id);
                    table.ForeignKey(
                        name: "fk_orderitems_order_id",
                        column: x => x.OrderId,
                        principalSchema: "public",
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_orderitems_user_id",
                        column: x => x.CreatedUserId,
                        principalSchema: "public",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_order_items_CreatedUserId",
                schema: "public",
                table: "order_items",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_order_items_OrderId",
                schema: "public",
                table: "order_items",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CreatedUserId",
                schema: "public",
                table: "Orders",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_SupplierId",
                schema: "public",
                table: "Orders",
                column: "SupplierId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "order_items",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Orders",
                schema: "public");

            migrationBuilder.DropTable(
                name: "suppliers",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "public");
        }
    }
}
