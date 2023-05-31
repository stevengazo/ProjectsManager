from mcr.microsoft.com/mssql/server:2019-latest as base

ENV ACCEPT_EULA y
ENV SA_PASSWORD Password123
ENV MSSQL_PID Express
COPY --chmod=0755 entrypoint.sh .
COPY --chmod=0755  run-script.sh .
COPY --chmod=777  IdentityScript.sql .
COPY --chmod=777  CreateDatabases.sql .
COPY --chmod=777  ProjectsScript.sql .

EXPOSE 1433
#run  /bin/bash /entrypoint.sh


