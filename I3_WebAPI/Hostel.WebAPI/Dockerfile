# --- Build stage ---
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Kopiraj projekt i restore NuGet paketa
COPY ["Hostel.WebAPI.csproj", "."]
RUN dotnet restore "./Hostel.WebAPI.csproj"

# Kopiraj sve ostale datoteke i build
COPY . .
RUN dotnet build "Hostel.WebAPI.csproj" -c Release -o /app/build

# --- Publish stage ---
FROM build AS publish
RUN dotnet publish "Hostel.WebAPI.csproj" -c Release -o /app/publish \
    --no-restore \
    /p:UseAppHost=false

# --- Runtime stage ---
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

# Kopiraj publishane datoteke
COPY --from=publish /app/publish .

# Expose port 8080 (ASP.NET Core default u kontejneru)
EXPOSE 8080

# Environment varijabla - port
ENV ASPNETCORE_URLS=http://+:8080

# Pokretanje aplikacije
ENTRYPOINT ["dotnet", "Hostel.WebAPI.dll"]