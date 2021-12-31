

#Exam DDD project
## Application urls 
- Dau 6 la UI , dau 5 la  api
- Identity provides :  https://localhost:5001
- Exam Api :           https://localhost:5002
- Exam Admin Manage :  https://localhost:6001
- Exam Portal :        https://localhost:6002
- 
##Docker command Example 
- docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=Admin@123$' -p 1433:1433 -d mcr.microsoft.com/mssql/server:2017-latest
- docker ps or docker container ls
- docker run -d --name mongodb -e MONGO_INITDB_ROOT_USERNAME=mongoadmin -e MONGO_INITDB_ROOT_PASSWORD=Admin@123$ -p 27017:27017 mongo
