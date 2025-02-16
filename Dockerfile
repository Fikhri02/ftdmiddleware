# Use a .NET 9.0 SDK image (if available)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src


# Copy the solution file
COPY FTDMiddleware.sln ./

# Copy the project files for both projects
COPY FTDMiddlewareDataAccess/FTDMiddlewareDataAccess.csproj FTDMiddlewareDataAccess/
COPY FTDMiddlewareApi/FTDMiddlewareApi.csproj FTDMiddlewareApi/

# Restore dependencies for the entire solution
RUN dotnet restore

# Copy all the source code into the container
COPY . .

# Publish the API project 
WORKDIR /src/FTDMiddlewareApi
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# Expose the port 
EXPOSE 80

# Start the application
ENTRYPOINT ["dotnet", "FTDMiddlewareApi.dll"]
