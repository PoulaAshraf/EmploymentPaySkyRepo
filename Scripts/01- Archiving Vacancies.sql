-- Author:		<Author,,Pola Ashraf>
-- Create date: <Create Date,,14/06/2023>
-- Description:	<Description,,Archiving Procedure>
-- =============================================
CREATE PROCEDURE spArchiveVacances
AS
BEGIN
	DECLARE @CURRENT_DATE DATETIME
	SET @CURRENT_DATE = GETDATE()
	SELECT * FROM [dbo].[Vacancy] WHERE [ExpiryDate] < @CURRENT_DATE
	UPDATE [dbo].[Vacancy] 
	SET [IsArchived] = 1 -- ARCHIVED VACANCY
END
GO
