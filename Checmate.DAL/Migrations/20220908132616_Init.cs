using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Checkmate.DAL.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nickname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EncodedPassword = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Salt = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    Elo = table.Column<int>(type: "int", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tournaments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MinPlayers = table.Column<int>(type: "int", nullable: false),
                    MaxPlayers = table.Column<int>(type: "int", nullable: false),
                    MinElo = table.Column<int>(type: "int", nullable: true),
                    MaxElo = table.Column<int>(type: "int", nullable: true),
                    Category = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Round = table.Column<int>(type: "int", nullable: false),
                    WomenOnly = table.Column<bool>(type: "bit", nullable: false),
                    RegistrationEndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tournaments", x => x.Id);
                    table.CheckConstraint("CK_MINMAX_ELO", "MinElo <= MaxElo");
                    table.CheckConstraint("CK_MINMAX_PLAYERS", "MinPlayers <= MaxPlayers");
                    table.CheckConstraint("CK_OKMAXVALUE_MAXPLAYERS", "MaxPlayers <= 32");
                    table.CheckConstraint("CK_OKMAXVALUE_MINELO", "MinElo <= 3000");
                    table.CheckConstraint("CK_OKMINVALUE_MINPLAYERS", "MinPlayers >= 2");
                });

            migrationBuilder.CreateTable(
                name: "MemberTournament",
                columns: table => new
                {
                    MembersId = table.Column<int>(type: "int", nullable: false),
                    TournamentsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberTournament", x => new { x.MembersId, x.TournamentsId });
                    table.ForeignKey(
                        name: "FK_MemberTournament_Members_MembersId",
                        column: x => x.MembersId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MemberTournament_Tournaments_TournamentsId",
                        column: x => x.TournamentsId,
                        principalTable: "Tournaments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Members",
                columns: new[] { "Id", "BirthDate", "Elo", "Email", "EncodedPassword", "Gender", "Nickname", "Role", "Salt" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 9, 8, 15, 26, 16, 388, DateTimeKind.Local).AddTicks(6075), 3000, "admin@mail.be", new byte[] { 82, 179, 174, 76, 113, 222, 238, 87, 17, 47, 25, 115, 51, 205, 97, 85, 151, 94, 116, 1, 69, 185, 121, 178, 156, 28, 21, 160, 247, 44, 90, 44, 161, 67, 158, 74, 25, 59, 165, 43, 29, 180, 236, 114, 136, 158, 61, 50, 71, 222, 102, 236, 25, 94, 11, 215, 29, 7, 250, 229, 233, 214, 21, 120 }, 0, "Admin", 0, new Guid("5d542a52-4544-41a3-a3b5-835897b55272") },
                    { 2, new DateTime(2022, 9, 8, 15, 26, 16, 388, DateTimeKind.Local).AddTicks(6129), 1200, "leo@mail.be", new byte[] { 211, 238, 49, 97, 146, 253, 134, 17, 182, 72, 94, 10, 121, 166, 199, 173, 226, 141, 121, 248, 229, 141, 93, 252, 97, 236, 196, 17, 224, 199, 226, 236, 234, 153, 110, 95, 67, 180, 33, 160, 107, 20, 91, 124, 15, 4, 228, 153, 176, 76, 59, 176, 20, 216, 46, 72, 177, 129, 33, 4, 65, 6, 47, 248 }, 0, "Leo", 1, new Guid("9d46a9a7-7f72-4da7-8eec-74fc979b106c") },
                    { 3, new DateTime(2022, 9, 8, 15, 26, 16, 388, DateTimeKind.Local).AddTicks(6134), 2500, "andre@mail.be", new byte[] { 121, 226, 141, 81, 230, 6, 175, 138, 251, 205, 65, 6, 233, 184, 136, 184, 214, 209, 25, 129, 73, 188, 131, 61, 141, 118, 163, 138, 86, 55, 89, 36, 177, 48, 101, 243, 179, 164, 13, 224, 63, 144, 15, 139, 254, 35, 48, 19, 17, 214, 228, 149, 81, 122, 199, 177, 39, 40, 192, 121, 218, 223, 233, 122 }, 0, "André", 1, new Guid("78ce23ec-39fd-435c-9c6d-6e35ef06c44f") },
                    { 13, new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1500, "romuald@mail.be", new byte[] { 99, 71, 124, 84, 229, 229, 184, 240, 212, 242, 193, 170, 224, 175, 184, 103, 142, 159, 88, 90, 252, 161, 201, 93, 137, 227, 245, 21, 224, 143, 219, 136, 193, 72, 159, 201, 29, 195, 159, 50, 89, 121, 142, 76, 72, 189, 215, 89, 163, 135, 0, 173, 183, 44, 30, 159, 249, 35, 45, 121, 2, 167, 180, 191 }, 0, "Romuald", 1, new Guid("0a1185a7-0fde-4184-a77a-11079d6f5baf") },
                    { 14, new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2500, "sarah@mail.be", new byte[] { 125, 146, 148, 91, 93, 226, 49, 201, 133, 254, 58, 71, 148, 7, 31, 90, 191, 227, 172, 51, 197, 125, 24, 47, 33, 157, 5, 21, 138, 157, 114, 91, 253, 185, 131, 24, 117, 100, 86, 22, 175, 140, 206, 86, 164, 39, 80, 118, 250, 154, 55, 252, 80, 205, 108, 128, 18, 178, 215, 255, 147, 150, 24, 74 }, 1, "Sarah", 1, new Guid("f3859aca-ef60-439a-a3f4-1b7e522353df") },
                    { 15, new DateTime(1950, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1000, "dana@mail.be", new byte[] { 62, 199, 7, 146, 79, 168, 250, 250, 243, 146, 166, 212, 17, 125, 14, 79, 68, 34, 3, 29, 134, 196, 81, 87, 23, 191, 84, 94, 173, 232, 99, 8, 215, 232, 144, 52, 117, 7, 6, 104, 192, 133, 103, 52, 100, 138, 233, 84, 184, 214, 100, 175, 172, 207, 3, 182, 54, 223, 57, 212, 242, 179, 94, 79 }, 1, "Dana", 1, new Guid("015120eb-9ae0-45ff-8409-0f92e4a42ed5") },
                    { 16, new DateTime(1920, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2999, "bob@mail.be", new byte[] { 76, 19, 45, 162, 27, 46, 210, 155, 163, 104, 183, 22, 87, 205, 182, 38, 56, 191, 192, 219, 65, 2, 203, 129, 50, 84, 163, 209, 82, 169, 132, 12, 134, 172, 159, 21, 46, 204, 252, 123, 39, 12, 229, 5, 119, 56, 83, 185, 131, 176, 80, 227, 237, 84, 185, 153, 141, 251, 252, 44, 223, 66, 197, 7 }, 0, "Bob", 1, new Guid("4380c495-2041-49c2-829b-f93b875763e4") }
                });

            migrationBuilder.InsertData(
                table: "Tournaments",
                columns: new[] { "Id", "Category", "CreationDate", "Location", "MaxElo", "MaxPlayers", "MinElo", "MinPlayers", "Name", "RegistrationEndDate", "Round", "Status", "UpdateDate", "WomenOnly" },
                values: new object[] { 1, 4, new DateTime(2022, 9, 8, 15, 26, 16, 388, DateTimeKind.Local).AddTicks(6524), "Jardin d'Eden", 3000, 32, 1000, 2, "Tournoi originel", new DateTime(2023, 11, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 0, new DateTime(2022, 9, 8, 15, 26, 16, 388, DateTimeKind.Local).AddTicks(6529), true });

            migrationBuilder.CreateIndex(
                name: "IX_Members_Email",
                table: "Members",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Members_Nickname",
                table: "Members",
                column: "Nickname",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Members_Salt",
                table: "Members",
                column: "Salt",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MemberTournament_TournamentsId",
                table: "MemberTournament",
                column: "TournamentsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MemberTournament");

            migrationBuilder.DropTable(
                name: "Members");

            migrationBuilder.DropTable(
                name: "Tournaments");
        }
    }
}
