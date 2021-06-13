create table users
(
    id        UUID NOT NULL PRIMARY KEY,
    firstname VARCHAR(255),
    lastname  VARCHAR(255),
    username  VARCHAR(255),
    email     VARCHAR(255),
    password  VARCHAR(255),
    role      VARCHAR(255)
);

-- create table role
-- (
--     id   SERIAL      NOT NULL PRIMARY KEY,
--     role VARCHAR(20) NOT NULL
-- );

-- CREATE TABLE user_role
-- (
--     userId INTEGER NOT NULL,
--     roleId INTEGER NOT NULL,
--     PRIMARY KEY (userId, roleId),
--     FOREIGN KEY (userId) references users (id),
--     FOREIGN KEY (roleId) references role (id)
-- );

INSERT INTO users (id, firstname, lastname, username, email, password, role)
VALUES ('49ae31fc-79e9-4577-a4b9-47ac32ebcddd', 'admin', 'admin', 'admin', 'admin@gmail.com', '$2a$10$QRW1d.EKZPnxV.D7hj4@gd67#5JLgiFCD', 'ADMIN');
