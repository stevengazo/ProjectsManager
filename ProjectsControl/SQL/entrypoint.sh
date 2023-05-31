#!/bin/bash

#start the script t create the DB and data then start the sqlserver
echo "Entrypoint.sh running ..."
/opt/mssql/bin/sqlservr

bash run-script.sh
echo "finished Entrypoint.sh..."