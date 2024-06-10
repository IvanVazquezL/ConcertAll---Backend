create or alter procedure usp_ListConcertsByGenre --usp_ListConcertsByGenre 'POP'
	@genrename nvarchar(50)
as
begin
	SELECT [c].[Id],
		[c].[Title],
		[c].[Description],
		[c].[Place],
		[c].[UnitPrice],
		[g].[Name] 'Genre',
		[c].[GenreId],
		CONVERT(VARCHAR, [c].[DateEvent], 3) 'DateEvent',
		CONVERT(VARCHAR, [c].[DateEvent], 8) 'TimeEvent',
		[c].[ImageUrl],
		[c].[TicketsQuantity],
		[c].[Finalized],
		CASE
			WHEN [c].[Status] = CAST(1 AS bit)
			THEN N'Active'
			ELSE N'Inactive'
		END 'STATUS'
	FROM [Musicals].[Concert] AS [c]
	INNER JOIN [Musicals].[Genre] AS [g] ON [c].[GenreId] = [g].[Id]
	WHERE (g.Name LIKE '%'+@genrename+'%')
end
go