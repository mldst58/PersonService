#!/bin/bash

docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=3f6Z@zO?Z4zw' --name persons-server -h persons-server -p 1433:1433 \
    -d mcr.microsoft.com/mssql/server:2019-latest

docker exec persons-server /opt/mssql-tools/bin/sqlcmd \
   -S localhost -U SA -P '3f6Z@zO?Z4zw' \
   -Q 'CREATE DATABASE PersonDB'

exec persons-server /opt/mssql-tools/bin/sqlcmd \
   -S localhost -U SA -P '3f6Z@zO?Z4zw' \
   -Q 'CREATE LOGIN PersonService WITH PASSWORD = "MyReallyBadPassword1"'

docker exec persons-server /opt/mssql-tools/bin/sqlcmd \
   -S localhost -U SA -P '3f6Z@zO?Z4zw' \
   -d PersonDB \
   -Q 'CREATE USER [PersonService] FOR LOGIN [PersonService]'

docker exec persons-server /opt/mssql-tools/bin/sqlcmd \
   -S localhost -U SA -P '3f6Z@zO?Z4zw' \
   -d PersonDB \
   -Q "EXEC sp_addrolemember N'db_owner', N'PersonService'"