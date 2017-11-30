CREATE   PROCEDURE [dbo].[usp_ProcTVP]
	@list [dbo].[udt_Sample] READONLY
AS
BEGIN
	SET NOCOUNT ON;

	INSERT [dbo].[Names]
	OUTPUT [Inserted].*
		SELECT * FROM @list;
END
