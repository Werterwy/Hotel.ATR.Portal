using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotel.ATR.Portal.Migrations
{
    public partial class addRoomDateTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DateTime",
                table: "Rooms",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateTime",
                table: "Rooms");
        }
    }
}
