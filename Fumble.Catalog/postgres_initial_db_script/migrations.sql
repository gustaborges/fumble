CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;


DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20231119185952_InitialCreate') THEN
    CREATE TABLE "Categories" (
        "Id" uuid NOT NULL DEFAULT (gen_random_uuid()),
        "Name" character varying(60) NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        CONSTRAINT "PK_Categories" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20231119185952_InitialCreate') THEN
    CREATE TABLE "Products" (
        "Id" uuid NOT NULL DEFAULT (gen_random_uuid()),
        "Name" character varying(200) NOT NULL,
        "Price" numeric(12,2) NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        CONSTRAINT "PK_Products" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20231119185952_InitialCreate') THEN
    CREATE TABLE "CategoryProduct" (
        "CategoryId" uuid NOT NULL,
        "ProductsId" uuid NOT NULL,
        CONSTRAINT "PK_CategoryProduct" PRIMARY KEY ("CategoryId", "ProductsId"),
        CONSTRAINT "FK_CategoryProduct_Categories_CategoryId" FOREIGN KEY ("CategoryId") REFERENCES "Categories" ("Id") ON DELETE CASCADE,
        CONSTRAINT "FK_CategoryProduct_Products_ProductsId" FOREIGN KEY ("ProductsId") REFERENCES "Products" ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20231119185952_InitialCreate') THEN
    CREATE INDEX "IX_CategoryProduct_ProductsId" ON "CategoryProduct" ("ProductsId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20231119185952_InitialCreate') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20231119185952_InitialCreate', '6.0.25');
    END IF;
END $EF$;
COMMIT;

START TRANSACTION;


DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20231119222701_Alter_Products_Category_To_Categories') THEN
    ALTER TABLE "CategoryProduct" DROP CONSTRAINT "FK_CategoryProduct_Categories_CategoryId";
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20231119222701_Alter_Products_Category_To_Categories') THEN
    ALTER TABLE "CategoryProduct" RENAME COLUMN "CategoryId" TO "CategoriesId";
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20231119222701_Alter_Products_Category_To_Categories') THEN
    ALTER TABLE "CategoryProduct" ADD CONSTRAINT "FK_CategoryProduct_Categories_CategoriesId" FOREIGN KEY ("CategoriesId") REFERENCES "Categories" ("Id") ON DELETE CASCADE;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20231119222701_Alter_Products_Category_To_Categories') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20231119222701_Alter_Products_Category_To_Categories', '6.0.25');
    END IF;
END $EF$;
COMMIT;

START TRANSACTION;


DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20231119225411_Alter_Categories_Unique_Name') THEN
    CREATE UNIQUE INDEX "UQ_Categories_Name" ON "Categories" ("Name");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20231119225411_Alter_Categories_Unique_Name') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20231119225411_Alter_Categories_Unique_Name', '6.0.25');
    END IF;
END $EF$;
COMMIT;

