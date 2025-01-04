## Objetivos

- Proporcionar una API RESTful que permita realizar operaciones CRUD (Crear, Leer, Actualizar, Eliminar) sobre los recursos.
- Utilizar EF Core para interactuar con la base de datos de manera eficiente.
- Implementar `user-secrets` para almacenar configuraciones sensibles, como cadenas de conexión y claves API, evitando así su exposición en el código fuente.

## Requisitos

- [.NET SDK](https://dotnet.microsoft.com/download) (versión 9.0)
- [Visual Studio](https://visualstudio.microsoft.com/) o cualquier otro IDE compatible con .NET
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) o cualquier otro proveedor de base de datos compatible con EF Core

## Configuración del Proyecto

1. **Clonar el Repositorio**
   
   ```bash
   git clone https://github.com/JoseA-Cueto/Api-Rest-Minimal.git  
   cd ApiRestMinimal
   ```
2. **Restaurar dependencias**
   
   ```bash
   dotnet restore
   ```
3. **Migrar la base de datos**
   
   ```bash
   dotnet ef database update
   ```
4. **Ejecutar la aplicación**
   
   ```bash
   dotnet run
   ```
