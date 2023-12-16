CREATE DATABASE LibraryDbs

use LibraryDbs

CREATE TABLE Books (
    BookId INT IDENTITY(1,1) PRIMARY KEY,
    Title NVARCHAR(255),
    Author NVARCHAR(255),
    Genre NVARCHAR(255),
    Quantity INT
)
INSERT INTO Books (Title, Author, Genre, Quantity)
VALUES
    ('Book 1', 'Author 1', 'Genre 1', 10),
    ('Book 2', 'Author 2', 'Genre 2', 15),
    ('Book 3', 'Author 3', 'Genre 3', 20);

	select * from Books