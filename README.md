## Criando o Projeto

* ```dotnet new sln --name solution-name```
* ```dotnet new webapi -o solution-name.template-project --no-https true```
* ```dotnet sln solution-name.sln add ./solution-name.template-project/solution-name.template-project.csproj```
* ```dotnet new gitignore```

## Executando o Projeto

* ```dotnet run --project solution-name.template-project```

## Executando o Container do Redis

* ```docker run -p 6379:6379 -d redis```