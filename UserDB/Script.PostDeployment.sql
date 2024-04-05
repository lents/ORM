if not exists (SELECT 1 FROM [dbo].[User])
begin
	insert into [dbo].[User](FirstName, LastName)
	values ('Tim', 'Black'),
	('Sandy', 'Marble'),
	('Tom', 'Smith')
end