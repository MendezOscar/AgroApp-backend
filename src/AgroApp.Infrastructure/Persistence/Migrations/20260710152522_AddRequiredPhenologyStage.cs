using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgroApp.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddRequiredPhenologyStage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "required_phenology_stage",
                table: "task_templates",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "required_phenology_stage",
                table: "task_templates");
        }
    }
}
