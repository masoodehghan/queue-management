using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QueueManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueStatusInProgresss : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_QueueItems_QueueId",
                table: "QueueItems");

            migrationBuilder.CreateIndex(
                name: "IX_QueueItems_QueueId",
                table: "QueueItems",
                column: "QueueId",
                unique: true,
                filter: "[Status] = 1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_QueueItems_QueueId",
                table: "QueueItems");

            migrationBuilder.CreateIndex(
                name: "IX_QueueItems_QueueId",
                table: "QueueItems",
                column: "QueueId",
                unique: true,
                filter: "Status = 1");
        }
    }
}
