FROM mcr.microsoft.com/mssql/server
ENV SA_PASSWORD=RootRoot.1 \
    ACCEPT_EULA=Y
	# Installazione di PowerShell

WORKDIR /docker-entrypoint-initdb.d/
COPY init-sqlServer.sql .

#CMD ["powershell", "-Command", "$ErrorActionPreference = 'Stop'; $ProgressPreference = 'SilentlyContinue';", ".\\start -sa_password $env:RootRoot.1 -ACCEPT_EULA $env:ACCEPT_EULA -attach_dbs \\\"$env:attach_dbs\\\" -Verbose" ]
RUN (sqlcmd -S localhost -U SA -P RootRoot.1 -i init-sqlServer.sql)
