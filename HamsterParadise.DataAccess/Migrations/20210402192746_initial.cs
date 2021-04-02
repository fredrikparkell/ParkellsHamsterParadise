using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HamsterParadise.DataAccess.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Activities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActivityName = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CageSize = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cages", x => x.Id);
                    table.CheckConstraint("CK_Cage_CageSize", "[CageSize] >= 0 AND [CageSize] < 4");
                });

            migrationBuilder.CreateTable(
                name: "ExerciseAreas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CageSize = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseAreas", x => x.Id);
                    table.CheckConstraint("CK_ExerciseArea_CageSize", "[CageSize] >= 0 AND [CageSize] < 7");
                });

            migrationBuilder.CreateTable(
                name: "Owner",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(65)", maxLength: 65, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Owner", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Simulations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Simulations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Hamsters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    IsFemale = table.Column<bool>(type: "bit", nullable: false),
                    CheckedInTime = table.Column<DateTime>(type: "datetime2(0)", nullable: true),
                    LastExerciseTime = table.Column<DateTime>(type: "datetime2(0)", nullable: true),
                    OwnerId = table.Column<int>(type: "int", nullable: false),
                    CageId = table.Column<int>(type: "int", nullable: true),
                    ExerciseAreaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hamsters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hamsters_Cages_CageId",
                        column: x => x.CageId,
                        principalTable: "Cages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Hamsters_ExerciseAreas_ExerciseAreaId",
                        column: x => x.ExerciseAreaId,
                        principalTable: "ExerciseAreas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Hamsters_Owner_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Owner",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ActivityLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeStamp = table.Column<DateTime>(type: "datetime2(0)", nullable: false),
                    HamsterId = table.Column<int>(type: "int", nullable: false),
                    ActivityId = table.Column<int>(type: "int", nullable: false),
                    SimulationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActivityLogs_Activities_ActivityId",
                        column: x => x.ActivityId,
                        principalTable: "Activities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActivityLogs_Hamsters_HamsterId",
                        column: x => x.HamsterId,
                        principalTable: "Hamsters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActivityLogs_Simulations_SimulationId",
                        column: x => x.SimulationId,
                        principalTable: "Simulations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Activities",
                columns: new[] { "Id", "ActivityName" },
                values: new object[,]
                {
                    { 1, "Arrived" },
                    { 2, "Exercise" },
                    { 3, "Cage" },
                    { 4, "Departure" }
                });

            migrationBuilder.InsertData(
                table: "Cages",
                column: "Id",
                values: new object[]
                {
                    10,
                    9,
                    7,
                    6,
                    8,
                    4,
                    3,
                    2,
                    1,
                    5
                });

            migrationBuilder.InsertData(
                table: "ExerciseAreas",
                column: "Id",
                value: 1);

            migrationBuilder.InsertData(
                table: "Owner",
                columns: new[] { "Id", "Email", "Name" },
                values: new object[,]
                {
                    { 12, "shieldman9000@yopmail.com", "Ludvig Sköld" },
                    { 19, "5muneagle@mail.com", "Simon Hörnfalk" },
                    { 18, "robinlovesvarberg@yopmail.com", "Robin Tranberg" },
                    { 17, "mrlaholm@yopmail.com", "Mattis Kindblom" },
                    { 16, "falkenbergaren2@yopmail.com", "Albin Ahmetaj" },
                    { 15, "falkenbergaren1@yandex.com", "Louis Headlam" },
                    { 14, "innebandyproffset100@protonmail.com", "Andrea Envall" },
                    { 13, "dlk1337@outlook.com", "Madelene Sjösten" },
                    { 11, "llll1ebro@hotmail.com", "Andreas Lind" },
                    { 3, "ninaspelardota2@yopmail.com", "Nils Brufors" },
                    { 9, "jjjjjjnnnn123@yandex.com", "Julia Nilsson" },
                    { 8, "333333svanen333333@yopmail.com", "Olof Svahn" },
                    { 7, "ringhalsen@yandex.com", "Emile Nestor" },
                    { 6, "iceland4l1fe@protonmail.com", "Einar Olafsson" },
                    { 5, "commiev3ganf0l1fe@yopmail.com", "David Lindgren Kamali" },
                    { 4, "annab00kfan111@yopmail.com", "Johan Nilsson" },
                    { 20, "ccccffff00@outlook.com", "Carl Fredrik Ahl" },
                    { 2, "steffeboi2021@protonmail.com", "Stefan Trenh" },
                    { 1, "fredrikparkell5@gmail.com", "Fredrik Parkell" },
                    { 10, "jp0racing@yopmail.com", "Johannes Posse" },
                    { 21, "p9hhjds33@protonmail.com", "Paul Tannenberg" }
                });

            migrationBuilder.InsertData(
                table: "Hamsters",
                columns: new[] { "Id", "Age", "CageId", "CheckedInTime", "ExerciseAreaId", "IsFemale", "LastExerciseTime", "Name", "OwnerId" },
                values: new object[,]
                {
                    { 2, 1, null, null, null, false, null, "Junior", 1 },
                    { 15, 2, null, null, null, false, null, "Klopapier", 20 },
                    { 20, 3, null, null, null, false, null, "Bubba", 19 },
                    { 23, 3, null, null, null, true, null, "Lulu", 18 },
                    { 22, 3, null, null, null, true, null, "Alexandra", 17 },
                    { 19, 1, null, null, null, true, null, "Candy", 16 },
                    { 10, 2, null, null, null, true, null, "Apple", 16 },
                    { 4, 1, null, null, null, false, null, "Lilleman", 15 },
                    { 27, 2, null, null, null, false, null, "Hasse", 14 },
                    { 11, 1, null, null, null, true, null, "Fluffy", 13 },
                    { 30, 1, null, null, null, true, null, "Summer", 12 },
                    { 7, 3, null, null, null, false, null, "Brutus", 12 },
                    { 12, 3, null, null, null, false, null, "Einstein", 11 },
                    { 3, 3, null, null, null, false, null, "Rufus", 10 },
                    { 13, 2, null, null, null, true, null, "Bella", 9 },
                    { 6, 2, null, null, null, true, null, "Greta", 9 },
                    { 28, 3, null, null, null, false, null, "Richard Gere", 8 },
                    { 21, 2, null, null, null, false, null, "Elvis", 8 },
                    { 14, 3, null, null, null, false, null, "Taco", 7 },
                    { 17, 1, null, null, null, true, null, "Mindy", 6 },
                    { 24, 2, null, null, null, true, null, "Miss Vegan", 5 },
                    { 1, 2, null, null, null, false, null, "Nisse", 5 },
                    { 26, 2, null, null, null, true, null, "Blondie", 4 },
                    { 16, 2, null, null, null, true, null, "Anna Book", 4 },
                    { 18, 2, null, null, null, true, null, "Dota", 3 },
                    { 8, 1, null, null, null, false, null, "Pelle", 3 },
                    { 5, 2, null, null, null, true, null, "Vilma", 2 },
                    { 25, 1, null, null, null, false, null, "Falukorv", 1 },
                    { 29, 2, null, null, null, true, null, "Gullis", 20 },
                    { 9, 1, null, null, null, false, null, "Buttons", 21 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActivityLogs_ActivityId",
                table: "ActivityLogs",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityLogs_HamsterId",
                table: "ActivityLogs",
                column: "HamsterId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityLogs_SimulationId",
                table: "ActivityLogs",
                column: "SimulationId");

            migrationBuilder.CreateIndex(
                name: "IX_Hamsters_CageId",
                table: "Hamsters",
                column: "CageId");

            migrationBuilder.CreateIndex(
                name: "IX_Hamsters_ExerciseAreaId",
                table: "Hamsters",
                column: "ExerciseAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_Hamsters_OwnerId",
                table: "Hamsters",
                column: "OwnerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActivityLogs");

            migrationBuilder.DropTable(
                name: "Activities");

            migrationBuilder.DropTable(
                name: "Hamsters");

            migrationBuilder.DropTable(
                name: "Simulations");

            migrationBuilder.DropTable(
                name: "Cages");

            migrationBuilder.DropTable(
                name: "ExerciseAreas");

            migrationBuilder.DropTable(
                name: "Owner");
        }
    }
}
