# Stock Management Challenge

REST API desarrollada en **.NET 8** siguiendo principios de **Clean Architecture** para la gestion de productos y autenticacion de usuarios, aplicando el **Patron Mediator**, **Inyeccion de Dependencias** y **Patron Repository** con **Stored Procedures** bajo los principios **ACID**.

---

## Tecnologias y Patrones

- **Framework:** .NET 8
- **Base de datos:** SQL Server
- **ORM:** Entity Framework Core (solo para ejecucion de Stored Procedures)
- **Autenticacion:** JWT Bearer
- **Encriptacion:** BCrypt
- **Logs:** Serilog
- **Patrones:** Clean Architecture, Repository, Mediator, Result Pattern (ROP)
- **Principios:** SOLID, ACID
- **Librerias:** MediatR, FluentValidation, Netmentor.ROP, Moq, FluentAssertions

---

## Estructura del Proyecto
```
StockManagementChallenge/
+-- 01-Api/
¦   +-- Configuration/
¦   +-- Controllers/
¦   +-- Middleware/
¦   +-- Services/
+-- 02-Application/
¦   +-- Behaviors/
¦   +-- DTOs/
¦   +-- Interfaces/
¦   +-- UsesCases/
+-- 03-Domain/
+-- 04-Data/
¦   +-- Repositories/
¦   +-- Context/
¦   +-- StoredProcedures/
+-- 05-Test/
    +-- Category/
    +-- Login/
    +-- Product/
```

---

## Configuracion

1. Clonar el repositorio

2. Ejecutar los scripts SQL de la carpeta `Database/Scripts` en el orden enumerado

4. Correr la API


---

## Autenticacion

La API usa **JWT Bearer**. Para acceder a los endpoints protegidos:

1. Realizar login en `POST /api/auth/login`
2. Copiar el token de la respuesta
3. Enviarlo en el header de cada request:
```
Authorization: Bearer {token}
```

---

## Endpoints

### Auth
| Metodo | Ruta | Descripcion | Auth |
|--------|------|-------------|------|
| POST | `/api/auth/login` | Iniciar sesion | No |

### Category
| Metodo | Ruta | Descripcion | Auth |
|--------|------|-------------|------|
| GET | `/api/Category` | Obtener lista de categorias | Si |

### Product
| Metodo | Ruta | Descripcion | Auth |
|--------|------|-------------|------|
| GET | `/api/Product` | Obtener todos los productos | Si |
| GET | `/api/Product/{id}` | Obtener producto por Id | Si |
| POST | `/api/Product` | Crear un producto | Si |
| PUT | `/api/Product/{id}` | Modificar un producto por Id | Si |
| DELETE | `/api/Product/{id}` | Eliminar un producto por Id | Si |
| GET | `/api/Product/filter/{monto}` | Obtener productos de distintas categorias que se ajusten al monto | Si |

---

## Principios aplicados

### SOLID
- **S** - Cada clase tiene una unica responsabilidad
- **O** - Abierto para extension, cerrado para modificacion
- **L** - Las implementaciones respetan los contratos de sus interfaces
- **I** - Interfaces especificas por dominio
- **D** - Las capas dependen de abstracciones, no de implementaciones concretas

### ACID (Stored Procedures)
- **Atomicidad** - Cada SP usa transacciones con `BEGIN/COMMIT/ROLLBACK`
- **Consistencia** - Validaciones previas a cada escritura
- **Aislamiento** - Uso de `UPDLOCK` y `HOLDLOCK` para evitar race conditions
- **Durabilidad** - Los cambios confirmados persisten ante fallos

---

## Flujo de una request
```
Controller
  +-- Handler (MediatR)
        +-- Repository
              +-- Stored Procedure (SQL Server)
                    +-- Resultado mapeado a DTO
                          +-- Result<T> (ROP)
                                +-- ToActionResult() -> HTTP Response
```

---

## Scripts SQL

Los scripts de base de datos estan en `Database/Scripts` y se deben correr en el orden enumerado:
```
Database/Scripts/
+-- 01_CreateDatabase.sql
+-- 02_CreateTables.sql
+-- 03_StoredProcedures.sql
+-- 04_SeedData.sql
```