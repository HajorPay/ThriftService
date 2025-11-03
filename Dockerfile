# ==========================
# STAGE 1: BUILD
# ==========================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy only project files first (for efficient layer caching)
COPY HajorPay.ThriftService.sln ./
COPY src/HajorPay.ThriftService.API/*.csproj ./src/HajorPay.ThriftService.API/
COPY src/HajorPay.ThriftService.Application/*.csproj ./src/HajorPay.ThriftService.Application/
COPY src/HajorPay.ThriftService.Domain/*.csproj ./src/HajorPay.ThriftService.Domain/
COPY src/HajorPay.ThriftService.Infrastructure/*.csproj ./src/HajorPay.ThriftService.Infrastructure/

# Restore dependencies (this layer is cached unless csproj files change)
RUN dotnet restore src/HajorPay.ThriftService.API/HajorPay.ThriftService.API.csproj

# Copy the entire source code
COPY . .

# Build and publish the API
WORKDIR /src/src/HajorPay.ThriftService.API
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false


# ========================
# STAGE 2: RUNTIME
# ==========================
# ========================
# STAGE 2: RUNTIME
# ========================
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copy the published output from build stage
COPY --from=build /app/publish .

# Let Kestrel listen on Cloud Run's dynamic port
ENV ASPNETCORE_URLS=http://+:${PORT:-8080}
EXPOSE 8080

# Start the API
ENTRYPOINT ["dotnet", "HajorPay.ThriftService.API.dll"]

