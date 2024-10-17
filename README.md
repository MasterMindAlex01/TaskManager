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
  1) En la raiz del proyecto se encuentra uan carpeta llamada scripts, donde se encuenta un script para generar la base de datos en sqlserver
     se recomienda que crear la base de datos y solo ejecutar sobre esa base de datos la ejecucion de las tablas.
     cuando se tenga la base de datos puede naver a la carpeta src\Applications\TaskManager.Api y modificar el appsetting.development.json
     se recomienda ejecuatr la aplicacion desde visual studio para poder ejectar la opcion https en las opciones de arranque  pueda enlazarse localmente.

  2) Al ejecutarlo "https://localhost:7003/swagger/index.html" puede navegarr a la url anterior y ejecutar los endpoint en swagger o despues postman.
  3) Para realizar uso del servicio se recomienda que se logue con el usuario "alex1234" password "123456" desde 'https://localhost:7003/v1/Token/login'
     post del api rest para obtener token de autenticacion JWT (Json Web Token)


# Docker:
   Para ejecutar la aplicacion desde docker usar el comando docker-compose up -d esto inicia un grupo de contenedores con una imagen del proyecto y obtener imagen de mongo conectado a la app

       docker-compose up -d

   para cerrar la instancia usar el commando:

       docker-compose down
   
