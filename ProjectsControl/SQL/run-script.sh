#!/bin/bash
echo "Running Script..."
/opt/mssql-tools/bin/sqlcmd -S 127.0.0.1 -U sa -P Password123 -d master -i IdentityScript.sql 
#/opt/mssql-tools/bin/sqlcmd -S 127.0.0.1 -U sa -P Password123 -i ProjectsScript.sql 
echo "Running Sc."
