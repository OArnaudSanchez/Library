FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

COPY Library.API/Library.API.csproj ./Library.API/
COPY Library.Application/Library.Application.csproj ./Library.Application/
COPY Library.Domain/Library.Domain.csproj ./Library.Domain/
COPY Library.Infrastructure/Library.Infrastructure.csproj ./Library.Infrastructure/

RUN dotnet restore Library.API/Library.API.csproj

COPY . .

RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./

EXPOSE 80

ENTRYPOINT ["dotnet", "Library.API.dll"]