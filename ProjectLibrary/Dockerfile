# Etapa 1: build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copia os projetos
COPY DoroTech.BookStore.API/*.csproj ./DoroTech.BookStore.API/
COPY DoroTech.BookStore.Application/*.csproj ./DoroTech.BookStore.Application/
COPY DoroTech.BookStore.Domain/*.csproj ./DoroTech.BookStore.Domain/
COPY DoroTech.BookStore.Infrastructure/*.csproj ./DoroTech.BookStore.Infrastructure/

# Restore individual
RUN dotnet restore DoroTech.BookStore.API/DoroTech.BookStore.API.csproj
RUN dotnet restore DoroTech.BookStore.Application/DoroTech.BookStore.Application.csproj
RUN dotnet restore DoroTech.BookStore.Domain/DoroTech.BookStore.Domain.csproj
RUN dotnet restore DoroTech.BookStore.Infrastructure/DoroTech.BookStore.Infrastructure.csproj


# Copia tudo e publica
COPY . .
WORKDIR /app/DoroTech.BookStore.API
RUN dotnet publish DoroTech.BookStore.API.csproj -c Release -o out
 

# Etapa 2: runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/DoroTech.BookStore.API/out ./


# Porta exposta
EXPOSE 80

# Entry point
ENTRYPOINT ["dotnet", "DoroTech.BookStore.API.dll"]
