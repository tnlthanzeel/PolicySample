using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Facets.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Addedsptocheckuserhasclaim : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var text = @"
                    GO

                    SET ANSI_NULLS ON
                    GO
                    SET QUOTED_IDENTIFIER ON
                    GO
                    CREATE PROCEDURE [dbo].[SP_CheckUserClaim] 
                        @UserId  NVARCHAR(100),
						@ClaimValues NVARCHAR(1000)

                        AS
                            BEGIN  
                            SELECT CASE
								WHEN EXISTS (
									SELECT 1 
									FROM [AspNetUserClaims] AS [a]
									WHERE ([a].[UserId] =  @UserId) AND [a].[ClaimValue] IN (SELECT * FROM STRING_SPLIT (@ClaimValues, ','))) THEN CAST(1 AS bit)
								ELSE CAST(0 AS bit)
								END  AS HasClaim
							END";

            migrationBuilder.Sql(text);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var text = @"DROP PROCEDURE [dbo].[SP_CheckUserClaim];
                         GO";

            migrationBuilder.Sql(text);
        }
    }
}
