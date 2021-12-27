-- CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

CREATE TABLE product
(
    id             UUID                     DEFAULT uuid_generate_v4() NOT NULL,
    brand          VARCHAR                                             NOT NULL,
    category       VARCHAR                                             NOT NULL,
    description    VARCHAR                                             NOT NULL,
    image          VARCHAR                                             NOT NULL,
    price          numeric(15, 6),
    rating         REAL,
    num_reviews    INTEGER,
    count_in_stock INTEGER,
    created_at     TIMESTAMP DEFAULT now(),
    updated_at     TIMESTAMP DEFAULT now()
);
