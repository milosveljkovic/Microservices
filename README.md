# Microserices Application
Microservice app is developed using .Net Core, RabbitMQ, MongoDB for backend while client is Angular application. Basic idea of application is to see how microservices can communicate between each other using RabbitMQ broker or Post/Put. In Device microservice you'll se file named sensor.txt ([sensor](http://archive.ics.uci.edu/ml/datasets/Beijing+Multi-Site+Air-Quality+Data)). Purpose of this application is to 'read' sensor data about Air Quality and do some preventive action to improve air quality in area (house/office/builing). As is mentioned, there is used two MongoDB for storing sensor's data (that two MongoDBs are totally independent). Data-microservice and Analytics-microservice has MongoDB. Data-ms store all data while Analytics-ms store only critical data ([what_is_critical_data](https://w.ndtvimg.com/sites/3/2019/12/18122322/air_quality_index_standards_CPCB_650.jpg)). If there is critical data in Analytics, we should send notification to WebDashboar and also we print critial situation on Aktuator(which is like a monitor). WebDeshboard consist of couple different pages, you will se there Dashboard, Critical, Settings and Home page.

Application consist of next microservices :
* Device
* Data
* Command
* Aktuator
* Analytics
* ApiGateway (Ocelot)
* WebDashboard (Angular app)

Basic schema :
![Runner Form](https://i.postimg.cc/g0Jywctx/Service-Oriented-Architecture.png)
### Getting Started

1. Git clone
2. Open cmd or shell in Microservices/src (there should be located docker-compose files)
3. Type in console : docker-compose build 
4. In same console type : docker-compose up 
5. After this point, you have started all backend microservices
6. Open WebDashboard in some Code Editor (VS Code for example)
7. Install all dependencies : npm install 
8. Start application: npm run start
9. Open localhost:4200 there should be application
