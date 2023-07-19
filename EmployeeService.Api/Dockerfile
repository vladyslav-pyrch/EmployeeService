FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["EmployeeService.Api/EmployeeService.Api.csproj", "EmployeeService.Api/"]
RUN dotnet restore "EmployeeService.Api/EmployeeService.Api.csproj"
COPY . .
WORKDIR "/src/EmployeeService.Api"
RUN dotnet build "EmployeeService.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EmployeeService.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EmployeeService.Api.dll"]
