# ==========================
# STAGE 1: BUILD
# ==========================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the solution and restore dependencies
COPY ../../HajorPay.ThriftService.sln ./
COPY ../../src/HajorPay.ThriftService.API/*.csproj ./HajorPay.ThriftService.API/
COPY ../../src/HajorPay.ThriftService.Application/*.csproj ./HajorPay.ThriftService.Application/
COPY ../../src/HajorPay.ThriftService.Domain/*.csproj ./HajorPay.ThriftService.Domain/
COPY ../../src/HajorPay.ThriftService.Infrastructure/*.csproj ./HajorPay.ThriftService.Infrastructure/

RUN dotnet restore HajorPay.ThriftService.API/HajorPay.ThriftService.API.csproj

# Copy the rest of the source code
COPY ../../src ./ 

# Build and publish the app
WORKDIR /src/HajorPay.ThriftService.API
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false


# ==========================
# STAGE 2: RUNTIME
# ==========================
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copy the published output
COPY --from=build /app/publish .

# Expose default ASP.NET port for Cloud Run
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

# Start the API
ENTRYPOINT ["dotnet", "HajorPay.ThriftService.API.dll"]
