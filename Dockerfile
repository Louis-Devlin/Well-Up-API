FROM mcr.microsoft.com/dotnet/aspnet:7.0-alpine AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0-alpine AS build

WORKDIR /src
COPY . .

# Add a COPY instruction to copy the MLSentimentModel.zip file
COPY ML/MLSentimentModel.zip /src/ML/

RUN dotnet restore Well-Up-API.csproj
RUN dotnet build Well-Up-API.csproj -c Debug -o /app

FROM build AS publish
RUN dotnet publish Well-Up-API.csproj -c Debug -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .

# The rest of your Dockerfile remains the same
CMD ASPNETCORE_URLS=http://*:$PORT dotnet Well-Up-API.dll