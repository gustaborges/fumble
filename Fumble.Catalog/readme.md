

## Database migrations

**Create a new database migration**

```
dotnet ef migrations add {MigrationName} --project=Fumble.Catalog.Database.Migrations --startup-project=Fumble.Catalog.Api --
```

**Generate database migration script**

```
dotnet ef migrations script --project Fumble.Catalog.Database.Migrations --startup-project Fumble.Catalog.Api --output ./postgres_initial_db_script/migrations.sql --idempotent
```

## Running via Docker

**Database only**
```
docker compose -f ./compose.db.yml up
```

**Database + CatalogApi**
```
docker compose -f ./compose.yml -f ./compose.db.yml up
```