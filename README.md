# asp.net-angular-mssql
 
1. architecture  
![alt text](./architecture.png)

2. setup  
 a. run sql-server in docker:  
 docker run -d --name sun_sql_server -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=sun.123456' -p 1433:1433 mcr.microsoft.com/mssql/server:2019-latest  
 install sql-cli  
 b. set up sql-server connection in .net backend  
 c. init database: dotnet ef migrations add DataModels and dotnet ef database update  
 d. run Zookeeper and Kafaka: download Kafaka, ./bin/zookeeper-server-start.sh ./config/zookeeper.properties and ./bin/kafka-server-start.sh ./config/server.properties  
 e. create topics: ./bin/kafka-topics.sh --create --zookeeper localhost:2181 --replication-factor 1 --partitions 5 --topic new-user  
 ./bin/kafka-topics.sh --create --zookeeper localhost:2181 --replication-factor 1 --partitions 5 --topic new-voting  
 f. run web-socket server: cd sunWsServer, npm start dev  
 g. run backend (sunny-dn-01) and frontend (sun-fe-user-app)  
 
3. notice  
to implement the CQRS, the MediatR is used in the backend.  
to implement the async communication between the frontend and kafka, socket.io is in used  
if a new user/voting is created, the backend will publish this event with kafka, the web-socket-server will consume this event and send the event to all sockets, at the end all with web-socket connected frontends will receive this event and update the pages.  


 
 
