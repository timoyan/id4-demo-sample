# id4-demo-sample

## Prerequisite

- .Net Core SDK (version please refer to globa.json in root, install please refer to https://dotnet.microsoft.com/download)
- .Net EF Cli (Install via `dotnet tool install -g dotnet-ef`)

## Create solution and project

`dotnet new sln -n id4-netcore-sample`<br/>
`dotnet new web -n id4-server -o ./src/id4-server -lang C# --type project`<br/>
`dotnet sln id4-netcore-sample.sln add ./src/id4-server/id4-server.csproj`

## Install package

`dotnet add src/id4-server/id4-server.csproj package IdentityServer4 -v 3.0.1`<br/>
`dotnet add src/id4-server/id4-server.csproj package Serilog -v 2.8.0`<br/>
`dotnet add src/id4-server/id4-server.csproj package Serilog.AspNetCore -v 3.0.0`<br/>
`dotnet add src/id4-server/id4-server.csproj package Serilog.Sinks.Console -v 3.1.1`<br/>
`dotnet add src/id4-server/id4-server.csproj package Serilog.Sinks.File -v 4.0.0`

## Container SQL Server settings

- Get docker image SQL server
```
docker pull mcr.microsoft.com/mssql/server
```

- Run container with SQL server image
```
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=<YourStrong@Passw0rd>" \
    -p 1433:1433 --name sql1 \
    -d mcr.microsoft.com/mssql/server:latest
```

- Enter container
```
docker exec -it sql1 "bash"
```

- Login SQL server
```
/opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P "<YourStrong@Passw0rd>"
```

- Generated DB Script
```
dotnet ef migrations add InitialIdentityServerPersistedGrantDbMigration -c PersistedGrantDbContext -o Data/Migrations/IdentityServer/PersistedGrantDb
dotnet ef migrations add InitialIdentityServerConfigurationDbMigration -c ConfigurationDbContext -o Data/Migrations/IdentityServer/ConfigurationDb
```
