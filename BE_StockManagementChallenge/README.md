# Stock Management Challenge

REST API desarrollada en .NET 8 con Clean Architecture.

## Tecnologías
- .NET 8
- Entity Framework Core
- SQL Server
- MediatR
- FluentValidation
- JWT Authentication
- BCrypt
- Serilog
- Netmentor.ROP

## Estructura
├── 01-Api          → Controllers, Middleware, Services
├── 02-Application  → Handlers, Interfaces, DTOs, Validators
├── 03-Domain       → Entities
├── 04-Data         → Repositories, DbContext, StoredProcedures
└── 05-Test         → Unit Tests

## Configuración
1. Clonar el repositorio
2. Configurar el `appsettings.Development.json` con la conexion del motor de base datos
3. Completar la connection string y JWT key
4. Ejecutar los scripts SQL de la carpeta `Database/Scripts`
5. Correr la API

## Scripts SQL
Los scripts de base de datos están en `Database/Scripts`: se los debe correr en el orden que estan enumerados
- `01_CreateDatabase.sql`
- `02_CreateTables.sql`  
- `03_StoredProcedures.sql`
- `04_SeedData.sql`

## Endpoints
| Método | Ruta | Descripción | Auth |
|--------|------|-------------|------|
| POST | /api/auth/login | Login | ❌ |
| GET | /api/productos/{id} | Obtener producto por un Id determinado | ✅ |
```

---

## 5. Carpeta Scripts con los SQL
```
📁 Scripts
 ├── 01_CreateDatabase.sql
 ├── 02_CreateTables.sql
 ├── 03_StoredProcedures.sql
 └── 04_SeedData.sql