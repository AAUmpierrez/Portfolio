using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLogic.Migrations
{
    /// <inheritdoc />
    public partial class AssignesByUserID_on_ticket_entity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AssignedByUserId",
                table: "Tickets",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_AssignedByUserId",
                table: "Tickets",
                column: "AssignedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Users_AssignedByUserId",
                table: "Tickets",
                column: "AssignedByUserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Users_AssignedByUserId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_AssignedByUserId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "AssignedByUserId",
                table: "Tickets");
        }
    }
}
