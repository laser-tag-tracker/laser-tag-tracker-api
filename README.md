# Laser Tag Tracker API

Backend WebAPI for the Laser Tag Tracker App.

## Environnement variable used

| Name | Description | Exemple Value |
| ---- | ----------- | ------------- |
| `DATABASE_URL` | Connection string to the PostgresSQL database | postgres://user:password@host:port/database |

The swagger API docs can be found at `/index.html`.

## Run

Check that you have the .NET 5 SDK installed.

```bash
git clone https://github.com/laser-tag-tracker/laser-tag-tracker-api
cd LaserTagTrackerApi
dotnet run
```

## Deploy with Docker

```bash
git clone https://github.com/laser-tag-tracker/laser-tag-tracker-api
cd LaserTagTrackerApi
docker build -t LaserTagTrackerApi .
docker run -d -p 8080:80 LaserTagTrackerApi
```

## Build with

- .NET 5
- ASP .NET Core
- Entity Framework Core
- Swagger
- PostgreSQL
- Docker
