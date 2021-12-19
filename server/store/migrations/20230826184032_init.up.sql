CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

CREATE TABLE IF NOT EXISTS products
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
    created_at     TIMESTAMP WITH TIME ZONE DEFAULT NOW(),
    updated_at     TIMESTAMP WITH TIME ZONE DEFAULT NOW()
);
