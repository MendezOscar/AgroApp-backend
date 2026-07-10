using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgroApp.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddSoilAnalysisCropSaleAndPestDiagnosis : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "diagnosis_condition",
                table: "crop_images",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "crop_sales",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    crop_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    sold_at = table.Column<DateOnly>(type: "date", nullable: false),
                    quantity_kg = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    price_per_kg = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    buyer = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    notes = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_crop_sales", x => x.id);
                    table.ForeignKey(
                        name: "fk_crop_sales_crops_crop_id",
                        column: x => x.crop_id,
                        principalTable: "crops",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_crop_sales_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "soil_analyses",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    plot_id = table.Column<Guid>(type: "uuid", nullable: false),
                    analyzed_at = table.Column<DateOnly>(type: "date", nullable: false),
                    ph = table.Column<decimal>(type: "numeric(4,2)", precision: 4, scale: 2, nullable: true),
                    nitrogen_pct = table.Column<decimal>(type: "numeric(5,2)", precision: 5, scale: 2, nullable: true),
                    phosphorus_pct = table.Column<decimal>(type: "numeric(5,2)", precision: 5, scale: 2, nullable: true),
                    potassium_pct = table.Column<decimal>(type: "numeric(5,2)", precision: 5, scale: 2, nullable: true),
                    organic_matter_pct = table.Column<decimal>(type: "numeric(5,2)", precision: 5, scale: 2, nullable: true),
                    notes = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_soil_analyses", x => x.id);
                    table.ForeignKey(
                        name: "fk_soil_analyses_plots_plot_id",
                        column: x => x.plot_id,
                        principalTable: "plots",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_crop_sales_crop_id",
                table: "crop_sales",
                column: "crop_id");

            migrationBuilder.CreateIndex(
                name: "ix_crop_sales_user_id",
                table: "crop_sales",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_soil_analyses_plot_id",
                table: "soil_analyses",
                column: "plot_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "crop_sales");

            migrationBuilder.DropTable(
                name: "soil_analyses");

            migrationBuilder.DropColumn(
                name: "diagnosis_condition",
                table: "crop_images");
        }
    }
}
