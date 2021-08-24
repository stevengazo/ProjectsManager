using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectsControl.Models.StoredProcedures
{
    public static class SP_Project
    {
		/// <summary>
		/// Return the code for the stored procedure to search in the table projects
		/// </summary>
		/// <returns>Sql Query</returns>
        public static string GetSPSearchProjects()
        {
            return @"-- =============================================
					-- Author:		Steven Gazo
					-- Create date: 23/8/21
					-- Description:	Search a projects by the specific information
					-- =============================================
					Create PROCEDURE [dbo].[SearchProjects]
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
								SET @_sqlcommand = @_sqlcommand + (' WHERE MONTH(OCDate) = '''+@_Month+'''') ;
								SET @_band= 1;
								SET @_exec=1;
							end
							ELSE
							begin
								SET @_sqlcommand = @_sqlcommand + (' AND (MONTH(OCDate) MONTH(OCDate) = '''+@_Month+'''') ;
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
								SET @_sqlcommand = @_sqlcommand + ('WHERE (TypeOfJob LIKE CONCAT(''%'',''' + @_ProjectType + ''',''%''))');
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
								EXEC (@_sqlcommand)
							end
						else
							begin
								select * from projects where ProjectId is null
							end
					END
";
        }
    }
}
