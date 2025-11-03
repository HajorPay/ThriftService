# ==========================
# STAGE 1: BUILD
# ==========================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution and project files
COPY HajorPay.ThriftService.sln ./
COPY src/HajorPay.ThriftService.API/*.csproj ./HajorPay.ThriftService.API/
COPY src/HajorPay.ThriftService.Application/*.csproj ./HajorPay.ThriftService.Application/
COPY src/HajorPay.ThriftService.Domain/*.csproj ./HajorPay.ThriftService.Domain/
COPY src/HajorPay.ThriftService.Infrastructure/*.csproj ./HajorPay.ThriftService.Infrastructure/

# Restore dependencies
RUN dotnet restore HajorPay.ThriftService.API/HajorPay.ThriftService.API.csproj

# Copy all source code
COPY . .

# Build and publish
WORKDIR /src/HajorPay.ThriftService.API
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false


# ==========================
# STAGE 2: RUNTIME
# ==========================
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copy from build stage
COPY --from=build /app/publish .

# Configure port for Cloud Run
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

# Start the API
ENTRYPOINT ["dotnet", "HajorPay.ThriftService.API.dll"]