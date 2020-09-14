
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
 
SELECT id,
    FirstName,
    LastName,
    Bio
FROM Author;


SELECT p.Id AS pId, 
       p.Title AS pTitle, 
       p.URL AS URL, 
       p.PublishDateTime AS PublishDateTime,
       a.FirstName AS FirstName, 
       a.LastName AS Lastname, 
       b.Title AS bTitle, 
       a.Id AS aId, b.Id AS bId
FROM Post p
JOIN Author a ON p.AuthorId = a.Id
JOIN Blog b ON p.BlogId = b.id;

SELECT p.Id, 
        p.Title, 
        p.URL, 
        p.PublishDateTime,
        a.FirstName, 
        a.LastName, 
        b.Title AS bTitle, 
        a.Id AS aId, b.Id AS bId
        FROM Post p
        JOIN Author a ON p.AuthorId = a.Id
        JOIN Blog b ON p.BlogId = b.id;


SELECT p.Id AS pId, 
p.Title, 
p.URL as URL, 
p.PublishDateTime,
a.FirstName, 
a.LastName, 
a.Id AS aId
FROM Post p
JOIN Author a ON p.AuthorId = a.Id;