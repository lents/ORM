CREATE PROCEDURE [dbo].[spUser_GetByFilter]
	@Filter nvarchar(50)
AS
BEGIN
  SELECT Id, FirstName, LastName 
  FROM [dbo].[User] 
  WHERE FirstName LIKE '%'+@Filter+'%' OR LastName LIKE '%'+@Filter+'%'
END