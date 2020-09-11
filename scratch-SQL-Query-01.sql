SELECT p.Id,
       p.Title,
       p.URL as Link,
       p.PublishDateTime,
       p.AuthorId,
       p.BlogId,
       a.FirstName,
       a.LastName
  FROM Post p 
  LEFT JOIN Author a on AuthorId = a.Id
  LEFT JOIN Blog b on BlogId = b.Id
 WHERE a.id = 2;

Select Post.Title from Post where AuthorId = 3;

SELECT * from Post where Post.AuthorId = 3;

SELECT * FROM Post;

-- "SELECT p.Id AS AuthorId,
--    p.FirstName,
--    p.LastName,
--    p.Bio,
--    t.Id AS AuthorId,
--    t.Name
--FROM Post p 
--    LEFT JOIN Author a on p.Id = a.AuthorId
--WHERE a.id = @id";


SELECT id,
    FirstName,
    LastName,
    Bio
FROM Author;