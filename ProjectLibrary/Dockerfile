# Etapa 1: build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copia csproj e restaura dependências
COPY *.sln .
COPY DoroTech.BookStore.API/*.csproj ./DoroTech.BookStore.API/
COPY DoroTech.BookStore.Application/*.csproj ./DoroTech.BookStore.Application/
COPY DoroTech.BookStore.Domain/*.csproj ./DoroTech.BookStore.Domain/
COPY DoroTech.BookStore.Infrastructure/*.csproj ./DoroTech.BookStore.Infrastructure/
RUN dotnet restore

# Copia tudo e publica
COPY . .
WORKDIR /app/ProjectLibrary/DoroTech.BookStore.API
RUN dotnet publish -c Release -o out

# Etapa 2: runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/ProjectLibrary/DoroTech.BookStore.API/out ./

# Porta exposta
EXPOSE 80

# Entry point
ENTRYPOINT ["dotnet", "DoroTech.BookStore.API.dll"]
