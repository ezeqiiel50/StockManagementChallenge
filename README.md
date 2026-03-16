# 📦 Stock Manager — Guía de Instalación y Configuración

Sistema completo de gestión de stock compuesto por una **API REST en .NET 8** y un **Frontend en React + Vite**.

---

## 📋 Requisitos previos

- .NET 8 SDK
- SQL Server
- Node.js + npm
- Git

---

## 🗄️ 1. Base de datos (SQL Server)

Los scripts se encuentran en `Database/Scripts/` y **deben ejecutarse en el orden numerado**:

| Orden | Archivo | Descripción |
|-------|---------|-------------|
| 1 | `01_CreateDatabase.sql` | Crea la base de datos |
| 2 | `02_CreateTables.sql` | Crea las tablas |
| 3 | `03_StoredProcedures.sql` | Crea los stored procedures |
| 4 | `04_SeedData.sql` | Carga los datos iniciales |

---

## ⚙️ 2. Backend — API .NET 8

### Clonar el repositorio

```bash
git clone <url-del-repositorio-backend>
cd StockManagementChallenge
```

### Configurar la conexión a la base de datos

Abrir el archivo `appsettings.Development.json` y configurar el connection string:

el user y password de la base de datos que utilisa la Api estan en el script `01_CreateDatabase.sql`

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=<servidor>;Database=StockManager;User Id=stock_user;Password=Stock@2024!;TrustServerCertificate=True;"
  }
}
```

> Reemplazar `<servidor>` con el valor del entorno.

### Ejecutar la API

```bash
dotnet run --project 01-Api
```

La API quedará disponible en:
```
https://localhost:<puerto>
http://localhost:<puerto>
```

> El puerto exacto se puede ver en los logs de consola al iniciar, o en `01-Api/Properties/launchSettings.json`.

### Autenticación

La API usa **JWT Bearer**. Tiene una duracion de 30min de vigencia, luego la app les solicitara ingresar las credenciales nuevamente.
Para acceder a los endpoints protegidos:

1. Hacer POST a `/api/auth/login` con las credenciales
	
	credenciales para pruebas:
	`user: admin, password: admin123
	 user: tester, password: tester123`
	
2. Copiar el token de la respuesta
3. Incluirlo en los headers de cada request:
```
Authorization: Bearer {token}
```

### Endpoints disponibles

**Auth**
| Método | Ruta | Auth |
|--------|------|------|
| POST | `/api/auth/login` | No |

**Categorías**
| Método | Ruta | Auth |
|--------|------|------|
| GET | `/api/Category` | Sí |

**Productos**
| Método | Ruta | Auth |
|--------|------|------|
| GET | `/api/Product` | Sí |
| GET | `/api/Product/{id}` | Sí |
| POST | `/api/Product` | Sí |
| PUT | `/api/Product/{id}` | Sí |
| DELETE | `/api/Product/{id}` | Sí |
| GET | `/api/Product/filter/{monto}` | Sí |

---

## 🖥️ 3. Frontend — React + Vite

### Clonar el repositorio

```bash
git clone <url-del-repositorio-frontend>
cd stock-manager-frontend
```

### Configurar la URL de la API

Antes de instalar dependencias, configurar la URL base de la API en:

```
src/api/axiosClient.js
```

Editar la `baseURL` para que apunte al backend:

```javascript
const axiosClient = axios.create({
  baseURL: "http://localhost:<puerto>", // 👈 Reemplazar con la URL de la API
});
```

> Usar la misma URL que se obtuvo al correr la API en el paso anterior.

### Instalar dependencias

```bash
npm install
```

### Ejecutar el frontend

```bash
npm run dev
```

La aplicación estará disponible en:
```
http://localhost:5173
```

---

## 🔄 Flujo completo del sistema

```
Usuario
  ↓
Frontend (React) → Login → Guarda JWT en localStorage + Redux
  ↓
Requests con Authorization: Bearer {token}
  ↓
API .NET 8 → Controller → Handler (MediatR) → Repository → Stored Procedure (SQL Server)
  ↓
Respuesta mapeada → Result<T> → HTTP Response → UI
```

---

## 🛠️ Stack tecnológico

**Backend**
- .NET 8 · SQL Server · Entity Framework Core · JWT · BCrypt · Serilog
- Patrones: Clean Architecture · Repository · Mediator · Result Pattern (ROP)
- Librerías: MediatR · FluentValidation · Moq · FluentAssertions

**Frontend**
- React · Vite · Redux Toolkit · React Router · Axios · Tailwind CSS

---

## ⚠️ Troubleshooting

- **Error 401 en la API:** el token expiró. Volver a hacer login; el interceptor de Axios redirige automáticamente al login.
- **Error de conexión a la DB:** verificar el connection string en `appsettings.json` y que SQL Server esté corriendo.
- **Frontend no conecta con la API:** verificar que `baseURL` en `src/api/axiosClient.js` coincida con el puerto donde corre la API.
- **CORS bloqueado:** verificar la configuración de CORS en `01-Api/Configuration/` del backend.
