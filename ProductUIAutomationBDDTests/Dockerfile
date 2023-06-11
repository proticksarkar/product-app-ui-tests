#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ProductUIAutomationBDDTests/ProductUIAutomationBDDTests.csproj", "ProductUIAutomationBDDTests/"]
COPY ["WebUIAutomationTestFramework/WebUIAutomationTestFramework.csproj", "WebUIAutomationTestFramework/"]
COPY ["ProductAPI/ProductAPI.csproj", "ProductAPI/"]
RUN dotnet restore "ProductUIAutomationBDDTests/ProductUIAutomationBDDTests.csproj"
COPY . .
WORKDIR "/src/ProductUIAutomationBDDTests"
RUN dotnet build "ProductUIAutomationBDDTests.csproj" -c Release -o /app/build

FROM build AS ProductUIAutomationBDDTests
WORKDIR /src/ProductUIAutomationBDDTests
CMD ["dotnet", "test"]