using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Text;

namespace HamsterParadise.DataAccess.Migrations
{
    /// <summary>
    /// This migration contains a MigrationBuilder that adds 
    /// a (in this case two) stored procedure to the database
    /// </summary>
    public partial class CreateSPForUpdatingCageSize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            StringBuilder cagesStoredProcedure = new StringBuilder();

            cagesStoredProcedure.Append(@"CREATE PROCEDURE NullCageCageSize" + Environment.NewLine);
            cagesStoredProcedure.Append("AS" + Environment.NewLine);
            cagesStoredProcedure.Append("BEGIN" + Environment.NewLine);
            cagesStoredProcedure.Append(@"UPDATE [Cages] SET [CageSize] = 0" + Environment.NewLine);
            cagesStoredProcedure.Append("END" + Environment.NewLine);

            migrationBuilder.Sql(cagesStoredProcedure.ToString());

            StringBuilder exerciseAreaStoredProcedure = new StringBuilder();

            exerciseAreaStoredProcedure.Append(@"CREATE PROCEDURE NullExerciseAreaCageSize" + Environment.NewLine);
            exerciseAreaStoredProcedure.Append("AS" + Environment.NewLine);
            exerciseAreaStoredProcedure.Append("BEGIN" + Environment.NewLine);
            exerciseAreaStoredProcedure.Append(@"UPDATE [ExerciseAreas] SET [CageSize] = 0" + Environment.NewLine);
            exerciseAreaStoredProcedure.Append("END" + Environment.NewLine);

            migrationBuilder.Sql(exerciseAreaStoredProcedure.ToString());
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
