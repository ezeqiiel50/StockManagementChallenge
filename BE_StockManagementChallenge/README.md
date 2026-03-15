# 📦 Stock Management Challenge

REST API desarrollada en **.NET 8** siguiendo principios de **Clean Architecture** para la gestión de productos y autenticación de usuarios, aplicando el **Patrón Mediator**, **Inyección de Dependencias** y **Patrón Repository** con **Stored Procedures** bajo los principios **ACID**.

---

## 🛠️ Tecnologías y Patrones

- **Framework:** .NET 8
- **Base de datos:** SQL Server
- **ORM:** Entity Framework Core (solo para ejecución de Stored Procedures)
- **Autenticación:** JWT Bearer
- **Encriptación:** BCrypt
- **Logs:** Serilog
- **Patrones:** Clean Architecture, Repository, Mediator, Result Pattern (ROP)
- **Principios:** SOLID, ACID
- **Librerías:** MediatR, FluentValidation, Netmentor.ROP, Moq, FluentAssertions

---

## 🏗️ Estructura del Proyecto
```
StockManagementChallenge/
├── 01-Api/
│   ├── Controllers/
│   ├── Middleware/
│   └── Services/
├── 02-Application/
│   ├── Handlers/
│   ├── Interfaces/
│   ├── DTOs/
│   └── Validators/
├── 03-Domain/
├── 04-Data/
│   ├── Repositories/
│   ├── Context/
│   └── StoredProcedures/
└── 05-Test/
    ├── Category/
    ├── Login/
    └── Product/
```

---

## ⚙️ Configuración

1. Clonar el repositorio
```bash
git clone https://github.com/tu-usuario/StockManagementChallenge.git
cd StockManagementChallenge
```

2. Ejecutar los scripts SQL de la carpeta `Database/Scripts` en el orden enumerado
3. Crear el archivo `appsettings.Development.json` con la siguiente estructura:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=StockManagementChallenge;User Id=tu_usuario;Password=tu_password;TrustServerCertificate=True;"
  },
  "Jwt": {
    "Key": "tu-clave-secreta-minimo-32-caracteres",
    "Issuer": "StockManagementChallenge",
    "Audience": "StockManagementChallenge"
  }
}
```

4. Correr la API
```bash
dotnet run --project 01-Api
```

---

## 🔐 Autenticación

La API usa **JWT Bearer**. Para acceder a los endpoints protegidos:

1. Realizá login en `POST /api/auth/login`
2. Copiá el token de la respuesta
3. Envialo en el header de cada request:
```
Authorization: Bearer {token}
```

---

## 📌 Endpoints

### Auth
| Método | Ruta | Descripción | Auth |
|--------|------|-------------|------|
| POST | `/api/auth/login` | Iniciar sesión | ❌ |

### Category
| Método | Ruta | Descripción | Auth |
|--------|------|-------------|------|
| GET | `/api/Category` | Obtener lista de categorías | ✅ |

### Product
| Método | Ruta | Descripción | Auth |
|--------|------|-------------|------|
| GET | `/api/Product` | Obtener todos los productos | ✅ |
| GET | `/api/Product/{id}` | Obtener producto por Id | ✅ |
| POST | `/api/Product` | Crear un producto | ✅ |
| PUT | `/api/Product/{id}` | Modificar un producto por Id | ✅ |
| DELETE | `/api/Product/{id}` | Eliminar un producto por Id | ✅ |
| GET | `/api/Product/filter/{monto}` | Obtener productos de distintas categorías que se ajusten al monto | ✅ |

---

## 📐 Principios aplicados

### SOLID
- **S** — Cada clase tiene una única responsabilidad
- **O** — Abierto para extensión, cerrado para modificación
- **L** — Las implementaciones respetan los contratos de sus interfaces
- **I** — Interfaces específicas por dominio
- **D** — Las capas dependen de abstracciones, no de implementaciones concretas

### ACID (Stored Procedures)
- **Atomicidad** — Cada SP usa transacciones con `BEGIN/COMMIT/ROLLBACK`
- **Consistencia** — Validaciones previas a cada escritura
- **Aislamiento** — Uso de `UPDLOCK` y `HOLDLOCK` para evitar race conditions
- **Durabilidad** — Los cambios confirmados persisten ante fallos

---

## 🔄 Flujo de una request
```
Controller
  └── Handler (MediatR)
        └── Repository
              └── Stored Procedure (SQL Server)
                    └── Resultado mapeado a DTO
                          └── Result<T> (ROP)
                                └── ToActionResult() → HTTP Response
```

---

## 📂 Scripts SQL

Los scripts de base de datos están en `Database/Scripts` y se deben correr en el orden enumerado:
```
Database/Scripts/
├── 01_CreateDatabase.sql
├── 02_CreateTables.sql
├── 03_StoredProcedures.sql
└── 04_SeedData.sql
```