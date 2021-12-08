-- CREATE TABLE employee
-- (
--     id            BIGSERIAL PRIMARY KEY,
--     fio           TEXT NOT NULL,
--     department    TEXT NOT NULL,
--     photoFileName TEXT NOT NULL,
--     created       timestamp DEFAULT now()
-- );
--
-- CREATE TABLE department
-- (
--     id      BIGSERIAL PRIMARY KEY,
--     name    TEXT NOT NULL,
--     created timestamp DEFAULT now()
-- );

CREATE TABLE product
(
    id           BIGSERIAL PRIMARY KEY,
    name         TEXT NOT NULL
--     category     TEXT NOT NULL,
--     image        TEXT NOT NULL,
--     price        MONEY,
--     brand        TEXT NOT NULL,
--     rating       NUMERIC,
--     numReviews   INTEGER,
--     countInStock INTEGER
);
