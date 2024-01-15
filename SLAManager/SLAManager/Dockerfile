FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
COPY . ./
RUN dotnet publish SLAManager/SLAManager.csproj -c Release -o out/SLAManager
RUN dotnet publish EntityFramework/EntityFramework.csproj -c Release -o out/ef
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out/SLAManager ./SLAManager
COPY --from=build /app/out/ef ./ef
COPY --from=build /app/SLAManager/appsettings.json /app/appsettings.json
COPY --from=build /app/SLAManager/init-sqlServer.sql /app/init-sqlServer.sql
EXPOSE 8081
ENTRYPOINT ["dotnet", "SLAManager/SLAManager.dll"]
