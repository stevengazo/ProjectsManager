using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectsControl.Models.StoredProcedures
{
    public static class SP_Project
    {
		
		public static void BuildStoredProcedures(MigrationBuilder migrationBuilder)
        {
			string spSearchIncidents = $@"-- =============================================
										-- Author:		Steven Gazo
										-- Create date: 02-03-2022
										-- Description:	Search in the table Asistances and return and specifics rows
										-- =============================================
										Create PROCEDURE SearchAsistances
											-- Add the parameters for the stored procedure here
											@_EmployeeId varchar(max)= null,
											@_ProjectId varchar(max)= null,
											@_DateToSearch varchar(max) = null,
											@_WeekId varchar(max) =  null
										AS
										BEGIN
											-- SET NOCOUNT ON added to prevent extra result sets from
											-- interfering with SELECT statements.
											SET NOCOUNT ON;

											-- Insert statements for procedure here
											DECLARE @_sqlCommand varchar(max) = 'SELECT Asistances.* FROM ASISTANCES';
											DECLARE @_flagParameters binary = 0 ;
											IF( @_EmployeeId IS NOT NULL)
											BEGIN
												if(@_flagParameters = 0)
													BEGIN
														SET @_flagParameters = 1;
														set @_sqlCommand  = @_sqlCommand + ' ' + ' WHERE EmployeeId = ''' + @_EmployeeId  + '''';
													END
												ELSE 
													BEGIN
														SET @_flagParameters = 1;
														set @_sqlCommand  = @_sqlCommand + ' ' + ' AND EmployeeId = ''' + @_EmployeeId  + '''';
													END	
											END
											IF( @_ProjectId IS NOT NULL )
											BEGIN
												if(@_flagParameters = 0)
												BEGIN
														SET @_flagParameters = 1;
														set @_sqlCommand  = @_sqlCommand + ' ' + ' WHERE ProjectId =  ''' + @_ProjectId + '''';
												END
												ELSE
													BEGIN
														SET @_flagParameters = 1;
														set @_sqlCommand  = @_sqlCommand + ' ' + ' AND ProjectId =  ''' + @_ProjectId + '''';
													END	
											END
											IF( @_WeekId IS NOT NULL)
											BEGIN
												IF( @_flagParameters = 0)
													BEGIN
														SET @_flagParameters = 1;
														set @_sqlCommand  = @_sqlCommand + ' ' + ' WHERE WeekId =  ''' + @_WeekId + '''';
													END
												ELSE
													BEGIN
														SET @_flagParameters = 1;
														set @_sqlCommand  = @_sqlCommand + ' ' + ' AND WeekId =  ''' + @_WeekId + '''';
													END
											END
											IF( @_DateToSearch IS NOT NULL)
											BEGIN
												IF( @_flagParameters = 0)
												BEGIN
														SET @_flagParameters = 1;
														set @_sqlCommand  = @_sqlCommand + ' ' + ' WHERE	CAST(DateOfBegin AS date) = CAST( ''' + @_DateToSearch+ ''' AS date) ';
														
												END
												ELSE
												BEGIN
														SET @_flagParameters = 1;
														set @_sqlCommand  = @_sqlCommand + ' ' + ' AND	CAST(DateOfBegin AS date) = CAST( ''' + @_DateToSearch+ ''' AS date) ';
												END
											END
											PRINT(@_sqlCommand)
											IF( @_flagParameters = 1)
											BEGIN
												EXEC (@_sqlCommand);
											END	
										END
										GO
										";
			string SP2 = @"SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Steven Gazo
-- Create date: 22 November 2021
-- Description:	Pagination of the Projects in the database. Also, the projects are order by the numberProject
-- =============================================
CREATE PROCEDURE GetProjectsByPage
	-- Add the parameters for the stored procedure here
	@_PageNumber int = 1 ,
	@_QuantityOfDevices int = 10
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here   
    if(@_PageNumber = 1)
    BEGIN
        SELECT * 
        FROM Projects
        ORDER BY NumberOfProject DESC
        OFFSET 0 ROWS 
        FETCH NEXT @_QuantityOfDevices ROWS ONLY;
    END
    else
    BEGIN
        SELECT * 
        FROM Projects
        ORDER BY NumberOfProject Desc
        OFFSET @_QuantityOfDevices * (@_PageNumber - 1) ROWS 
        FETCH NEXT @_QuantityOfDevices ROWS ONLY;
    END
END
GO
";
			string SP1 = @"-- =============================================
-- Author:		Steven Gazo
-- Create date: 23/8/21
-- Description:	Search a projects by the specific information
-- =============================================
Create PROCEDURE SearchProjects
	-- Add the parameters for the stored procedure here
	 @_Month varchar(5)= null,
	 @_Year varchar(5)= null,
	 @_ProjectName varchar(max) =null,
	 @_ProjectStatus varchar(max) = null,
	 @_ProjectType varchar(max) =null,
	 @_ProjectNumber varchar(50) =null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	
	DECLARE	 @_sqlcommand varchar(max)= 'SELECT * FROM Projects';
	DECLARE	 @_band binary = 0;
	DECLARE	 @_exec binary = 0;
	IF @_ProjectName IS NOT NULL
	BEGIN
		IF @_band =0
		begin
			SET @_sqlcommand = @_sqlcommand + (' WHERE (ProjectName LIKE CONCAT(''%'',''' + @_ProjectName+ ''',''%''))');		
			SET @_band= 1;
			SET @_exec=1;
		end
	END
	IF @_ProjectNumber IS NOT NULL
	BEGIN
		IF @_band =0
		begin
			SET @_sqlcommand = @_sqlcommand + (' WHERE (NumberOfProject LIKE CONCAT(''%'',''' + @_ProjectNumber + ''',''%''))');
			SET @_band= 1;
			SET @_exec=1;
		end
		ELSE
		begin
			SET @_sqlcommand = @_sqlcommand + (' AND (NumberOfProject LIKE CONCAT(''%'',''' + @_ProjectNumber + ''',''%''))');
			SET @_exec=1;
		end
	END
	IF @_Month IS NOT NULL
	BEGIN
		IF @_band =0
		begin
			SET @_sqlcommand = @_sqlcommand + (' WHERE MONTH(OCDate) = '+@_Month) ;
			SET @_band= 1;
			SET @_exec=1;
		end
		ELSE
		begin
			SET @_sqlcommand = @_sqlcommand + (' AND MONTH(OCDate) = '+@_Month) ;
			SET @_exec=1;
		end
	END
	IF @_Year IS NOT NULL
	BEGIN
		IF @_band =0
		begin
			SET @_sqlcommand = @_sqlcommand + (' WHERE YEAR(OCDate)=' + @_Year);
			SET @_band= 1;
			SET @_exec=1;
		end
		ELSE
		begin
			SET @_sqlcommand = @_sqlcommand + (' AND YEAR(OCDate)=' + @_Year);
			SET @_band= 1;
			SET @_exec=1;
		end
	END
	IF @_ProjectStatus IS NOT NULL
	BEGIN
		IF @_band =0
		begin
			SET @_sqlcommand = @_sqlcommand + (' WHERE (Estatus LIKE CONCAT(''%'',''' + @_ProjectStatus + ''',''%''))');
			SET @_band= 1;
			SET @_exec=1;
		end
		ELSE
		begin
			SET @_sqlcommand = @_sqlcommand + (' AND (Estatus LIKE CONCAT(''%'',''' + @_ProjectStatus + ''',''%''))');
			SET @_exec=1;
		end
	END
	IF @_ProjectType IS NOT NULL
	BEGIN
		IF @_band =0
		begin
			SET @_sqlcommand = @_sqlcommand + (' WHERE (TypeOfJob LIKE CONCAT(''%'',''' + @_ProjectType + ''',''%''))');
			SET @_band= 1;
			SET @_exec=1;
		end
		ELSE
		begin
			SET @_sqlcommand = @_sqlcommand + (' AND (TypeOfJob LIKE CONCAT(''%'',''' + @_ProjectType + ''',''%''))');	
			SET @_exec=1;
		end
	END
	if @_exec = 1
		begin
			print(@_sqlcommand)
			EXEC (@_sqlcommand)
		end
	else
		begin

			select * from projects where ProjectId is null
		end
END

					";

			migrationBuilder.Sql(SP1);
			migrationBuilder.Sql(spSearchIncidents);
			migrationBuilder.Sql(SP2);	
		}

    }
}
