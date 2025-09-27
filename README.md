# IoTHub

A layered .NET 8 solution with ASP.NET Core Minimal APIs, EF Core, and PostgreSQL.

---

## ⚙️ Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Docker](https://www.docker.com/) (for Postgres) or a local Postgres install
- EF Core CLI:
  ```bash
  dotnet tool update -g dotnet-ef
  ```

---

## 🚀 Quick Start

### 1) Start Postgres (Docker; change host port if 5432 is busy)
```bash
  docker compose up -d
```

### 2) Set dev connection string (stored in user-secrets; not committed)
```bash
    cd src/IoTHub.Api
    dotnet user-secrets init
    dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Host=localhost;Port=5432;Database=iothubdb;Username=postgres;Password=postgres"
```

### 3) Apply DB schema
```bash
    cd ../..
    dotnet ef database update   --project src/IoTHub.Infrastructure   --startup-project src/IoTHub.Api
```

### 4) Run API
```bash
  dotnet run --project src/IoTHub.Api
```

Browse:
- Swagger → [http://localhost:<port>/swagger](http://localhost:5190/swagger)
- Health → [http://localhost:<port>/api/health](http://localhost:5190/api/health)

---

## 🗄️ Database (Postgres via Docker)

Create a `docker-compose.yml` in the repo root:

```yaml
version: "3.9"
services:
  postgres:
    image: postgres:16
    container_name: iothub-postgres
    restart: unless-stopped
    environment:
      POSTGRES_USER: postgres
      POSTGRES_PASSWORD: postgres
      POSTGRES_DB: iothubdb
    ports:
      - "5432:5432" # change left side to 5433 if 5432 is busy
    volumes:
      - iothub_pg_data:/var/lib/postgresql/data

volumes:
  iothub_pg_data:
```

Commands:
```bash
    docker compose up -d         # start DB
    docker compose down          # stop DB (keeps volume)
    docker compose down -v       # stop and delete data volume
    docker ps                    # list running containers
```

---

## 🔌 Configuration

We use ASP.NET configuration + **User Secrets** (dev only):

- **Key:** `ConnectionStrings:DefaultConnection`
- **Example (Postgres):**
  ```
  Host=localhost;Port=5432;Database=iothubdb;Username=postgres;Password=postgres
  ```

Set it once:
```bash
    cd src/IoTHub.Api
    dotnet user-secrets init
    dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Host=localhost;Port=5432;Database=iothubdb;Username=postgres;Password=postgres"
```

⚠️ Do **not** commit credentials in `appsettings*.json`.

---

## 🧱 Migrations Runbook

**Projects**
- DbContext & migrations → `src/IoTHub.Infrastructure`
- Startup (DI/config) → `src/IoTHub.Api`

### Create a new migration
```bash
  dotnet ef migrations add <Name>   --project src/IoTHub.Infrastructure   --startup-project src/IoTHub.Api   --output-dir Persistence/Migrations
```

### Apply migrations
```bash
  dotnet ef database update   --project src/IoTHub.Infrastructure   --startup-project src/IoTHub.Api
```

### List migrations
```bash
  dotnet ef migrations list   --project src/IoTHub.Infrastructure   --startup-project src/IoTHub.Api
```

---

## ❤️ Health Check

- Endpoint: `/api/health`
- Returns **200** when DB is reachable.
- Optional: customize the JSON `ResponseWriter` for detailed output.

---

## 🧪 Running the API

```bash
  dotnet run --project src/IoTHub.Api
```

Stop: `Ctrl + C`

- Swagger UI → `/swagger`
- Health Check → `/api/health`

---
