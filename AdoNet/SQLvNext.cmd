REM Docker Image herunterladen und starten

docker run -e ACCEPT_EULA=Y -e SA_PASSWORD=P@ssw0rd99 -p 2433:1433 -v C:\Temp\Docker\Databases:/var/opt/mssql/data -d microsoft/mssql-server-linux
pause