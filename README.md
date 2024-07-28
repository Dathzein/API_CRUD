# API_CRUD

## Guia de inicio rapido de API

### Descripcion:

Este proyectop es una API RESTful construida con ASP NET CORE que permite realizar un CRUD basico para modulo de clientes 

### Requisitos previos:

- **.NET SDK**: Asegúrate de tener instalado el .NET SDK (https://dotnet.microsoft.com/download).
- **Un editor de código**: Visual Studio, Visual Studio Code o cualquier otro editor que soporte C# son buenas opciones. Esto por si desea ver el codigo
- **Base de datos**: Motor de base de datos SQL Server y Microsoft SQL Server Management Studio.
- **Script "Query Final"**: Si ya se tienen todos los requisitos anteriores ejecutar el Script que esta en el repositorio en SQL server para crear la base de datos y las tablas

### Clonar repositorio

```shell
git clone https://github.com/Dathzein/API_CRUD.git

```
### **IMPORTANTE**

Importante en el archivo **API/appsettings.json** reemplazar la informacion de la cadena de conexion:

- Servidor
- Usuario servidor
- Contraseña del usuario

```json

"ConnectionStrings": {
    "Api_Crud": "Server=(Servidor);Database=Api_Crud;User Id=(usuarioServidor);Password=(contraseñaDelUsuario);Trusted_Connection=True;Encrypt=True;TrustServerCertificate=True;"
  }

```

### Arrancar el proyecto

```shell

dotnet build
dotnet run

```
### Punto de Ingreso Api Localmente

La API estará disponible en el siguiente endpoint: http://localhost:5026 

***Tener en cuenta***: Para poder utilizar los endpoints del CRUD es importante:
  - Primero:  En el andpoint http://localhost:5026/api/User/register registrarse. 
  - Segundo:  En http://localhost:5026/api/User/login ingresar para obtener el token que es de tipo Bearer

  psd: Comparto collecion en postman con ejemplos

### Ejecutar Tests


```shell

dotnet test

```