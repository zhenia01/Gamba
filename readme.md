# Gamba
## Applications
- [Backend](./backend) — Gamba's backend. Written in .NET 7

  _To work properly, fill in the **`Gamba.WebAPI/appsettings.Development.json`** file. Use the **`appsettings.json`** file as an example._

- [Frontend](./frontend) — Gamba's backend. Written in React

  _To work properly, fill in the **`.env`** file. Use the **`.env.example`** file as an example._

## Requirements
- [NodeJS](https://nodejs.org/en/) (16.x.x);
- [NPM](https://www.npmjs.com/) (8.x.x);
- [PostgreSQL](https://www.postgresql.org/) (14.2);
- [.NET](https://dotnet.microsoft.com/en-us/download/dotnet/) (7.x);
- run **`npx simple-git-hooks ../simple-git-hooks.js`** in the **`/frontend`** folder

## Docker
1. You can build and run all application containers via **`docker-compose -f docker-compose.apps.yml up -d`** command.
2. You can pull and run all 3rd-party services (Postgres) via **`docker-compose -f docker-compose.services.yml up -d`** command.

## Start
1. Fill ENVs
2. **`npx simple-git-hooks ../simple-git-hooks.js`** in the **`/frontend`** folder
3. **`cd frontend && npm run start`** 
4. **`cd backend && dotnet build && cd Gamba.WebAPI && dotnet run`**