# id4-server sample

## Prerequisite

.Net Core SDK (version please refer to globa.json in root)

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

