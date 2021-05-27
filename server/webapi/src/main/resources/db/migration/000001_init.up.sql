CREATE TABLE employee
(
    id            BIGSERIAL PRIMARY KEY,
    fio           TEXT NOT NULL,
    department    TEXT NOT NULL,
    photoFileName TEXT NOT NULL,
    created       timestamp DEFAULT now()
);

CREATE TABLE department
(
    id      BIGSERIAL PRIMARY KEY,
    name    TEXT NOT NULL,
    created timestamp DEFAULT now()
);
