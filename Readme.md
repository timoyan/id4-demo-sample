# id4-demo-sample

## Prerequisite

- .Net Core SDK (version please refer to globa.json in root, install please refer to <https://dotnet.microsoft.com/download)>
- .Net EF Cli (Install via `dotnet tool install -g dotnet-ef`)

## Create solution and project

`dotnet new sln -n id4-netcore-sample`\
`dotnet new web -n id4-server -o ./src/id4-server -lang C# --type project`\
`dotnet sln id4-demo-sample.sln add ./src/id4-server/id4-server.csproj`

## Install package

`dotnet add src/id4-server/id4-server.csproj package IdentityServer4 -v 3.0.1`\
`dotnet add src/id4-server/id4-server.csproj package Serilog -v 2.8.0`\
`dotnet add src/id4-server/id4-server.csproj package Serilog.AspNetCore -v 3.0.0`\
`dotnet add src/id4-server/id4-server.csproj package Serilog.Sinks.Console -v 3.1.1`\
`dotnet add src/id4-server/id4-server.csproj package Serilog.Sinks.File -v 4.0.0`

## Container SQL Server settings

- Get docker image SQL server

``` bash
docker pull mcr.microsoft.com/mssql/server
```

- Run container with SQL server image

``` bash
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=<YourStrong@Passw0rd>"\
    -p 1433:1433 --name sql1
    -d mcr.microsoft.com/mssql/server:latest
```

- Enter container

``` bash
docker exec -it sql1 "bash"
```

- Login SQL server

``` bash
/opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P "<YourStrong@Passw0rd>"
```

- Azure Data Studio for mac

<https://docs.microsoft.com/en-us/sql/azure-data-studio/download?view=sql-server-ver15>

- Generated DB Script

``` bash
dotnet ef migrations add InitialIdentityServerPersistedGrantDbMigration -c PersistedGrantDbContext -o Data/Migrations/IdentityServer/PersistedGrantDb
dotnet ef migrations add InitialIdentityServerConfigurationDbMigration -c ConfigurationDbContext -o Data/Migrations/IdentityServer/ConfigurationDb
```

## Create api project

`dotnet new webapi -n api -o ./src/api -lang C# --type project`\
`dotnet sln id4-demo-sample.sln add ./src/api/api.csproj`

Access <https://localhost:5004/WeatherForecast> to validate service status.

## Create client project

`dotnet new console -n console-client -o ./src/console-client -lang C# --type project`\
`dotnet sln id4-demo-sample.sln add ./src/console-client/console-client.csproj`
