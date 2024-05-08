-- ------------------------------------------------------
-- ---------------- Migration ---------------------------
-- ------------------------------------------------------


-- ------------------------------------------------------
-- DDL
-- ------------------------------------------------------
CREATE TABLE IF NOT EXISTS Cinema (
  id INTEGER PRIMARY KEY AUTOINCREMENT, 
  name VARCHAR(64), 
  location TEXT);

CREATE TABLE IF NOT EXISTS Movie (
  id INTEGER PRIMARY KEY AUTOINCREMENT, 
  title VARCHAR(128), 
  releaseDate DATE,
  genre VARCHAR(64),
  runtime int,
  synopsis TEXT,
  director VARCHAR(64),
  rating VARCHAR(8),
  princessTheatreMovieId VARCHAR(16) NOT NULL);

CREATE TABLE IF NOT EXISTS MovieCinema (
  id INTEGER PRIMARY KEY AUTOINCREMENT, 
  movieId INT NOT NULL, 
  cinemaId INT NOT NULL, 
  showtime DATE,
  ticketPrice DECIMAL(4,2),
  CONSTRAINT FK_Movie_MovieCinema FOREIGN KEY (movieId) REFERENCES Movie(id),
  CONSTRAINT FK_Cinema_MovieCinema FOREIGN KEY (cinemaId) REFERENCES Cinema(id)
);

