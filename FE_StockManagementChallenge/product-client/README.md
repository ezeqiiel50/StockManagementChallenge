# 📦 Stock Manager Frontend

Frontend del challenge **Stock Manager**, desarrollado con React, Vite y Redux Toolkit.  
La aplicación consume una API REST para autenticación y gestión de productos.

---

# 🚀 Tecnologías utilizadas

- React
- Redux Toolkit
- React Router
- Axios
- Vite

Arquitectura basada en **feature folders** para facilitar la escalabilidad del proyecto.

---

# 📁 Estructura del proyecto
src
│
├── app
├── api
├── features
│ ├── auth
│ └── products
├── pages
├── routes
├── App.jsx
└── main.jsx


---

# 🔐 Autenticación

La autenticación se realiza mediante **JWT**.

Flujo:

```
Login
  ↓
API devuelve token
  ↓
token guardado en localStorage
  ↓
Redux guarda token en estado global
```

Las rutas están protegidas mediante **ProtectedRoute**.

Si el usuario no está autenticado:

```
/products → redirige a /
```

---

# 🧭 Navegación

Rutas principales:

| Ruta | Descripción |
|-----|-------------|
| `/` | Login |
| `/products` | Listado de productos |
| `/products/new` | Crear producto |
| `/products/edit/:id` | Editar producto |

---

# 📦 Gestión de productos (ABM)

La aplicación permite:

### ✔ Listar productos
Obtiene la lista desde la API.

### ✔ Crear producto
Formulario reutilizable para crear productos.

### ✔ Editar producto
El mismo formulario detecta el `id` en la URL y carga el producto desde la API.

### ✔ Eliminar producto
Elimina el producto desde el listado y actualiza el estado global.

---

# 🔎 Filtrado de productos

Pantalla para obtener productos filtrados por monto.

El usuario ingresa un monto y la aplicación consulta el endpoint correspondiente.

---

# 🌐 Axios Client

Se utiliza un cliente centralizado de **Axios**.

### Funcionalidades

- Agrega automáticamente el **token JWT** en cada request.
- Maneja errores globales de autenticación.

### 🔒 Manejo de expiración de token

Cuando la API responde con:
401 Unauthorized
Se ejecuta un interceptor que:
remove token
redirect login

### 🧠 Manejo de estado

El estado global se maneja con Redux Toolkit.
Slices implementados:
Auth Slice
	Maneja:
		login
		logout
		token
		loading
		error
Product Slice
	Maneja:
		listado de productos
		producto seleccionado
		loading
		errores
		acciones CRUD
		
### 🧪 Manejo de errores

Los errores provenientes de la API se muestran en la UI.
Ejemplo de respuesta de API:

{
  "value": null,
  "errors": [
    {
      "message": "Ha ocurrido un error al obtener los productos."
    }
  ],
  "success": false
}
El mensaje se muestra al usuario.

### ▶️ Instalación

1° Clonar el repositorio:
2° Instalar dependencias:
	npm install
3° Ejecutar proyecto:
	npm run dev

La aplicación estará disponible en:
http://localhost:5173
La URL base de la API se encuentra en:
src/api/axiosClient.js