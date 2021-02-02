# asp.net-angular-mssql
 
1. architecture  
![alt text](./architecture.png)

2. setup  
 a. run sql-server in docker:  
 docker run -d --name sun_sql_server -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=sun.123456' -p 1433:1433 mcr.microsoft.com/mssql/server:2019-latest  
 install sql-cli  
 b. set up sql-server connection in .net backend  
 c. run Zookeeper and Kafaka  
 
