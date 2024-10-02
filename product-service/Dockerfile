# Use the official .NET Core SDK as a parent image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy the project file and restore any dependencies (use .csproj for the project name)
COPY ./WebApi.Product-Service/*.csproj ./WebApi.Product-Service/
COPY ./Domain.Product-Service/*.csproj ./Domain.Product-Service/
COPY ./Infraestructure.Product-Service/*.csproj ./Infraestructure.Product-Service/
RUN cd ./WebApi.Product-Service && dotnet restore

# Copy the rest of the application code
COPY . .

# Publish the application
RUN cd ./WebApi.Product-Service && dotnet publish -c Release -o out

# Build the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/WebApi.Product-Service/out ./

# Expose the port your application will run on
EXPOSE 8000

# Start the application
ENTRYPOINT ["dotnet", "WebApi.Product-Service.dll"]