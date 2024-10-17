# TaskManager

Una aplicación rest full API de administración de Tareas en clean architecture, CQRS, DDD (Domain-Driven Desig), 
Patrones de diseño y de arquitectura en C# y .NET 8.0.

# Empezando
Para comenzar con este projecto, aquí están las opciones disponibles:

# Development Environment - Prerequisitos

  - .NET SDK: 8.0
  - API Testing Tools: POSTMAN
  - docker
  - SQL Server - Azure SQL
  - Visual studio
  - Sqlserver Manager studio
    
# Guía de inicio rápido 
  - Cloné el repositorio TaskManager. Ahora que nuestra solución está generada, 
    naveguemos a la carpeta raíz de la solución y abramos una terminal de comandos para construir la solución.

Paso para correr los proyectos:

# Backend - WebApi .NET 8 - Local
1) En la raíz del proyecto se encuentra una carpeta llamada "scripts", donde se ubica un script para generar la base de datos en SQL Server. 
Se recomienda crear la base de datos y luego ejecutar únicamente las instrucciones para crear las tablas sobre ella. Una vez creada la base de datos, 
navegue a la carpeta src\Applications\TaskManager.Api y modifique el archivo appsettings.Development.json con la configuración adecuada. 
Se sugiere ejecutar la aplicación desde Visual Studio, para habilitar la opción de HTTPS en las configuraciones de inicio y conectarse localmente.

2) Al ejecutar la aplicación, puede acceder a "https://localhost:7003/swagger/index.html" para navegar y probar los endpoints a través de Swagger o Postman.

3) Para utilizar el servicio, se recomienda iniciar sesión con el usuario "alex1234" y la contraseña "123456" en "https://localhost:7003/v1/Token/login" 
para obtener el token de autenticación JWT (Json Web Token). Este usuario administrador le permitirá realizar las demás acciones dentro del servicio.


# Docker:
   Para ejecutar la aplicacion desde docker usar el comando docker-compose up -d esta conectado a la base de datos desplegada en azure sql

       docker-compose up -d

   para cerrar la instancia usar el commando:

       docker-compose down
# Produccion
Se realizó el despliegue de una base de datos Azure SQL y una API Rest en Azure App Services, 
utilizando una imagen de DockerHub: "https://hub.docker.com/r/alexdrdeveloper01/taskmanagerapi". 
Esta API está disponible en la nube a través del enlace: "https://taskmanagertest-hecccrdrb9ekaadu.canadacentral-01.azurewebsites.net/swagger/index.html". 
Para interactuar con la API, inicie sesión con el usuario "alex1234" y la contraseña "123456" en "https://localhost:7003/v1/Token/login" y obtenga el token correspondiente.
