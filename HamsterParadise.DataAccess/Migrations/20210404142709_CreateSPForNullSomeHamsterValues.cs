using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Text;

namespace HamsterParadise.DataAccess.Migrations
{
    public partial class CreateSPForNullSomeHamsterValues : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            StringBuilder hamsterNullStoredProcedure = new StringBuilder();

            hamsterNullStoredProcedure.Append(@"CREATE PROCEDURE NullHamsterForNewSimulation" + Environment.NewLine);
            hamsterNullStoredProcedure.Append("AS" + Environment.NewLine);
            hamsterNullStoredProcedure.Append("BEGIN" + Environment.NewLine);
            hamsterNullStoredProcedure.Append(@"UPDATE [Hamsters] SET [CageId] = null" + Environment.NewLine);
            hamsterNullStoredProcedure.Append(@"UPDATE [Hamsters] SET [ExerciseAreaId] = null" + Environment.NewLine);
            hamsterNullStoredProcedure.Append(@"UPDATE [Hamsters] SET [CheckedInTime] = null" + Environment.NewLine);
            hamsterNullStoredProcedure.Append(@"UPDATE [Hamsters] SET [LastExerciseTime] = null" + Environment.NewLine);
            hamsterNullStoredProcedure.Append("END" + Environment.NewLine);

            migrationBuilder.Sql(hamsterNullStoredProcedure.ToString());
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
