CREATE OR REPLACE PROCEDURE sp_ranked_names(
    IN query_name TEXT DEFAULT NULL,
    IN query_gender TEXT DEFAULT NULL
)
LANGUAGE plpgsql
AS $$
BEGIN
    DROP TABLE IF EXISTS temp_names_occurrences;
    CREATE TEMPORARY TABLE temp_names_occurrences (
        annee INTEGER,	
        prenoms VARCHAR,
        nombre INTEGER
    );

    INSERT INTO temp_names_occurrences (annee, prenoms, nombre)
    SELECT annee, prenoms, COUNT(*) AS nombre
    FROM name
	WHERE (prenoms ILIKE '%' || query_name || '%')
          AND (sexe = query_gender)
    GROUP BY annee, prenoms;

    DROP TABLE IF EXISTS ranked_names;
    CREATE TEMPORARY TABLE ranked_names (
        annee INTEGER,
        prenoms VARCHAR,
        nombre INTEGER,
        rank INTEGER
    );

    INSERT INTO ranked_names (annee, prenoms, nombre, rank)
    SELECT annee, prenoms, nombre, RANK() OVER (PARTITION BY annee ORDER BY nombre DESC)
	FROM temp_names_occurrences;
	
	DROP TABLE IF EXISTS temp_names_occurrences;
	
END;	
$$;