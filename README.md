# topNamesGivenPerYear




docker compose up -d

docker-compose exec postgres psql -U postgres -d postgres

CREATE TABLE name (id serial PRIMARY KEY, sexe varchar(1) NOT NULL, annee integer NOT NULL, prenoms varchar(100) NOT NULL);

COPY name(sexe, annee, prenoms) FROM '/var/docker-entrypoint-initdb.d/liste_des_prenoms.csv' DELIMITER ';' CSV HEADER;

