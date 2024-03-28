# Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:7.0-alpine AS build
WORKDIR /src

# Copy the .csproj files and restore dependencies
COPY Well-Up-API.csproj .
RUN dotnet restore

# Copy the entire project and build
COPY . .

# Add a dummy file to invalidate the cache for the subsequent COPY instruction

RUN dotnet build -c Release -o /app

# Stage 2: Publish the application
FROM build AS publish
RUN dotnet publish -c Release -o /app

# Stage 3: Create the final image
FROM mcr.microsoft.com/dotnet/aspnet:7.0-alpine AS final
WORKDIR /app
COPY --from=publish /app .

# Create the ML directory and copy the zip file
COPY MLSentimentModel.zip /app/ML/

# The rest of your Dockerfile remains the same
CMD ASPNETCORE_URLS=http://*:$PORT dotnet Well-Up-API.dll