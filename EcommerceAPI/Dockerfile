#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["EcommerceAPI/EcommerceAPI.csproj", "EcommerceAPI/"]
COPY ["EcommerceAPI.Application/EcommerceAPI.Application.csproj", "EcommerceAPI.Application/"]
COPY ["EcommerceAPI.Domain/EcommerceAPI.Domain.csproj", "EcommerceAPI.Domain/"]
COPY ["EcommerceAPI.Infra/EcommerceAPI.Infra.csproj", "EcommerceAPI.Infra/"]
RUN dotnet restore "EcommerceAPI/EcommerceAPI.csproj"
COPY . .
WORKDIR "/src/EcommerceAPI"
RUN dotnet build "EcommerceAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EcommerceAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EcommerceAPI.dll"]