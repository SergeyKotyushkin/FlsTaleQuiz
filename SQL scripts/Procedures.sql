IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE NAME = 'sp_GetRandomQuestion')
BEGIN
	DROP PROCEDURE sp_GetRandomQuestion;
	PRINT 'Procedure sp_GetRandomQuestion dropped';
END
GO

IF EXISTS(SELECT TOP 1 1 FROM sys.procedures WHERE NAME = 'sp_GetAnswersByIds')
BEGIN
	DROP PROCEDURE sp_GetAnswersByIds;
	PRINT 'Procedure sp_GetAnswersByIds dropped';
END
GO

IF type_id('type_int_ID_Table') IS NOT NULL
    DROP TYPE type_int_ID_Table;
GO

CREATE TYPE type_int_ID_Table AS TABLE (
    ID	INT NOT NULL
)
GO

CREATE PROCEDURE sp_GetRandomQuestion(
	@ExcludedIdsTable type_int_ID_Table READONLY
)
AS
BEGIN
	SELECT
		q.QID, q.Title, q.Img, a.AID, a.Title, a.Valid
	FROM (SELECT TOP 1
			*
		FROM Questions
		WHERE QID NOT IN (SELECT
				ID
			FROM @ExcludedIdsTable)
		ORDER BY NEWID()) q
	INNER JOIN Answers a
		ON q.QID = a.QID
END
GO
PRINT 'Procedure sp_GetRandomQuestion created';
GO

CREATE PROCEDURE sp_GetAnswersByIds(
	@AnswersIdsTable type_int_ID_Table READONLY
)
AS
BEGIN
	SELECT
		AID
	   ,Title
	   ,Valid
	FROM Answers
	WHERE AID IN (SELECT
			ID
		FROM @AnswersIdsTable)
END
GO
PRINT 'Procedure sp_GetAnswersByIds created';
GO