FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
COPY . ./
RUN dotnet publish ExposeAPIService/ExposeAPI.csproj -c Release -o out/api
RUN dotnet publish EntityFramework/EntityFramework.csproj -c Release -o out/ef
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out/api ./api
COPY --from=build /app/out/ef ./ef
COPY --from=build /app/ExposeAPIService/appsettings.json /app/appsettings.json
COPY --from=build /app/ExposeAPIService/init-sqlServer.sql /app/init-sqlServer.sql
EXPOSE 8080
ENTRYPOINT ["dotnet", "api/ExposeAPI.dll"]
