CREATE FUNCTION [dbo].[Function1]
(
	@tvp [dbo].[udt_Sample] READONLY
)
RETURNS TABLE
AS
RETURN
(
	SELECT * FROM @tvp
);
