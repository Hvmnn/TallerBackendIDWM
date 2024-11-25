# Backend del Taller de Introducción al Desarrollo Web/Móvil

### Por Felipe Hamen e Ignacio Morales
****

## INSTALACIÓN:
Se debe tener instalar [Visual Studio Code] y el [SDK .NET8] para el proyecto.

Luego para comenzar con la instalación, se debe abrir Visual Studio Code, ir a File -> Open Folder y con ello seleccionar la carpeta donde se quiera clonar el proyecto.

Ir a la terminar de Visual Studio Code y ejecutar los siguientes comandos en orden:

```bash
git clone https://github.com/Hvmnn/TallerBackendIDWM
```

```bash
dotnet restore
```
****
## CLOUDINARY:
El proyecto usa los servicios de [Cloudinary] por ende se debe de crear una cuenta en ella para poder obtener las credenciales necesarias para el proyecto como: Cloud name, API Key y API Secret.
Una ves obtenidas las credenciales se deben, se deben escribir en el archivo "appsettings.json" como aparece a continuación:

```json
{
  "AppSettings":{
    "Token": ""
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "CloudinarySettings":{
    "CloudName":"*INGRESAR CLOUDNAME",
    "ApiKey":"*INGRESAR APIKEY*",
    "ApiSecret":"*INGRESAR APISECRET*"
  }
}
```
En el apartado de "Token" se debe de tener una clave mayor o igual a 32 caracteres.

****
## INCIAR SISTEMA:
En Visual Studio Code, ir a File -> Open Folder, y seleccionar la carpeta TallerBackendIDWM.

En la terminal de Visual Studio Code ejecutar el siguiente comando:

```bash
dotnet ef migrations add Initial
```

```bash
dotnet ef database update
```

```bash
dotnet run
```

****
## Postman

Para probar el backend usando "postman", se necesita instalar el programa [Postman]
Al abrir Postman y elegir un espacio de trabajo, debes hacer click en "Import" y seleccionar "TallerBackendIDWM.postman_collection.json".
Asegúrate que el puerto de las solicitudes coincida con el puerto de la ejecución.
