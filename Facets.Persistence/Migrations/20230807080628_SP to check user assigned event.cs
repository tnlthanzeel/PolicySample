using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Facets.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SPtocheckuserassignedevent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var text = @"
                        GO
                        /****** Object:  StoredProcedure [dbo].[SP_GetUserEvents]    Script Date: 08/07/2022 13:37:40 ******/
                        SET ANSI_NULLS ON
                        GO
                        SET QUOTED_IDENTIFIER ON
                        GO
                        CREATE PROCEDURE [dbo].[SP_GetUserEvents] 
                        @UserProfileId  NVARCHAR(100)

                        AS
                            BEGIN  
								SELECT [u].[EventId]
								FROM [UserAssignedEvent] AS [u]
								WHERE [u].[UserProfileId] = @UserProfileId
	                        END";

            migrationBuilder.Sql(text);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
