# topNamesGivenPerYear
This is a C# ASP.NET Core 7 project. I implemented an API with a single GET request which call an stored procedure on a Postgresql db.

Voici l’énoncé : 

* Créer un projet C# .NET dont l’objectif sera de porter une API REST (.net 6 ou 7)
* Initialiser une base de données Postgresql avec une seule table correspondant aux données intégrées dans le CSV ci-joint (le nom des champs, les index seront vérifiés)
* Créer une méthode d’API en GET qui prendra 2 paramètres optionnels dont on vérifiera le contenu pour éviter de créer des failles de sécurité. 
* Paramètre 1 : Query => 5 caractères max, chaine de caractère A-Z 
* Paramètre 2 : Sexe => « M » ou « F » seulement , chaine de caractère
* La réponse devra donner le top 3 des prénoms les plus données par année, filtrés si les paramètres sont envoyés par query (est ce que le prénom comprend cette chaine en tout ou partie ?) et par sexe (M ou F)
* L’API consommera les données de cette table à une procédure stockée dans Postgres. La procédure stockée doit être minutieusement préparée pour être efficiente et optimisée.
* Il faut documenter la méthode d’API pour qu’elle soit compatible avec la génération de documentation automatisée Swagger
* Générer la documentation d’api avec Swagger
* Créer un dump de la base de données créée et nous la transmettre 
* Créer un README pour présenter le projet et expliquer comment lancer le projet localement (Import de la base, Lancement du projet)

## DATABASE 
First go to appsettings.json and change the connection string to match your Postgres database.
Make sure to replace {user} by yours.
Then run those commands

```bash
psql -U {user} -c "CREATE DATABASE test_technique" 
psql -c "CREATE TABLE name (id serial PRIMARY KEY, sexe varchar(1) NOT NULL, annee integer NOT NULL, prenoms varchar(100) NOT NULL);" -U {user} test_technique
psql -c "\copy name(sexe, annee, prenoms) FROM './database/liste_des_prenoms.csv' DELIMITER ';' CSV HEADER;" -U {user} test_technique
psql -U {user} -f ./database/procedure.sql -d test_technique
```

## RUN the app 
```bash
dotnet watch run
```

## Routes
GET /NameGiven
2 optionnal params :
    -name (5 char max, A-Z)
    -sex (M or F)

Example :
* http://localhost:5072/NameGiven?name=Chris&sex=M
* http://localhost:5072/NameGiven

A postman collec is provided at ./Names Given Collection.postman_collection.json

Or if you prefer the SwaggerUI : http://localhost:5072/swagger/index.html
