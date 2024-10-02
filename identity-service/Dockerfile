# Use the official .NET Core SDK as a parent image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy the project file and restore any dependencies (use .csproj for the project name)
COPY ./WebApi.Identity-Service/*.csproj ./WebApi.Identity-Service/
COPY ./Domain.Identity-Service/*.csproj ./Domain.Identity-Service/
COPY ./Infraestructure.Identity-Service/*.csproj ./Infraestructure.Identity-Service/
RUN cd ./WebApi.Identity-Service && dotnet restore

# Copy the rest of the application code
COPY . .

# Publish the application
RUN cd ./WebApi.Identity-Service && dotnet publish -c Release -o out

# Build the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/WebApi.Identity-Service/out ./

# Expose the port your application will run on
EXPOSE 5194

# Start the application
ENTRYPOINT ["dotnet", "WebApi.Identity-Service.dll"]