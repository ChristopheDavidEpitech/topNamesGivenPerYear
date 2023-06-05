# topNamesGivenPerYear



psql -U {user} -c "CREATE DATABASE test_technique" 
psql -c "CREATE TABLE name (id serial PRIMARY KEY, sexe varchar(1) NOT NULL, annee integer NOT NULL, prenoms varchar(100) NOT NULL);" -U {user} test_technique
psql -c "\copy name(sexe, annee, prenoms) FROM './liste_des_prenoms.csv' DELIMITER ';' CSV HEADER;" -U {user} test_technique
psql -U {user} -f ./procedure.sql -d test_technique


