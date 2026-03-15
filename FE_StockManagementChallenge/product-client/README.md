# 📦Stock Manager Frontend

Frontend del challenge **Stock Manager**, desarrollado con React, Vite y Redux Toolkit.  
La aplicacion consume una API REST para autenticacion y gestion de productos.

---

# 🚀Tecnologias utilizadas

- React
- Redux Toolkit
- React Router
- Axios
- Vite
- Tailwind CSS

Arquitectura basada en **feature folders** para facilitar la escalabilidad del proyecto.

---

# 📁 Estructura del proyecto

```
src/
+-- app/
+-- api/
+-- features/
|   +-- auth/
|   +-- products/
+-- pages/
+-- routes/
+-- components/
```

---

# Estilos

La aplicacion utiliza **Tailwind CSS** para los estilos, integrado mediante el plugin oficial `@tailwindcss/vite`.

### Configuracion

El plugin se configura en `vite.config.js`:

### Layout

Se utiliza un componente `Layout.jsx` que incluye el `Sidebar` y envuelve todas las rutas protegidas mediante `<Outlet />` de React Router.

```
Layout
  +-- Sidebar (navegacion lateral)
  +-- Outlet (contenido de cada pagina)
```

### Sidebar

El componente `Sidebar.jsx` centraliza la navegacion entre pantallas usando `NavLink` de React Router, resaltando automaticamente la ruta activa.

---

# 🔐Autenticacion

La autenticacion se realiza mediante **JWT**.

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

Las rutas estan protegidas mediante **ProtectedRoute**.
Si el usuario no esta autenticado:

```
/products → redirige a / (Pantalla login)
```

---

# 🧭Navegacipon

Rutas principales:

| Ruta | Descripcion |
|-----|-------------|
| `/` | Login |
| `/products` | Listado de productos |
| `/products/new` | Crear producto |
| `/products/edit/:id` | Editar producto |
| `/products/filter` | Filtrado de productos |

---

# Gestion de productos (ABM)

La aplicacion permite:

### ✔ Listar productos
Obtiene la lista desde la API.

### ✔ Crear producto
Formulario reutilizable para crear productos.

### ✔ Editar producto
El mismo formulario detecta el `id` en la URL y carga el producto desde la API.

### ✔ Eliminar producto
Elimina el producto desde el listado y actualiza el estado global. Mientras se procesa la eliminaci贸n se muestra un overlay con spinner que bloquea la pantalla.

---

# 🔎 Filtrado de productos

Pantalla para obtener productos filtrados por monto.

El usuario ingresa un monto y la aplicacion consulta el endpoint correspondiente. Los resultados se muestran como cards destacando la mejor combinacion de productos disponibles, junto con el total de la combinacion.

En caso de no existir una combinacion posible, se muestra el mensaje devuelto por la API.

---

# 🌐Axios Client

Se utiliza un cliente centralizado de **Axios**.

### Funcionalidades

- Agrega automaticamente el **token JWT** en cada request.
- Maneja errores globales de autenticacion.

### Manejo de expiracion de token

Cuando la API responde con:
```
401 Unauthorized
```
Se ejecuta un interceptor que:
- Elimina el token del almacenamiento
- Redirige al login

### Manejo de estado

El estado global se maneja con Redux Toolkit.

Slices implementados:

**Auth Slice** Maneja: login, logout, token, loading, error

**Product Slice** Maneja: listado de productos, producto seleccionado, items filtrados, loading, errores, acciones CRUD

### Manejo de errores

Los errores provenientes de la API se muestran en la UI.
Ejemplo de respuesta de API:

```json
{
  "value": null,
  "errors": [
    {
      "message": "Ha ocurrido un error al obtener los productos."
    }
  ],
  "success": false
}
```

El mensaje se muestra al usuario.

---

# ▶️ Instalacion

1° Clonar el repositorio

2° Instalar dependencias:
```bash
npm install
```
3° Ejecutar proyecto:
```bash
npm run dev
```
La aplicaci0n estaria disponible en:
```
http://localhost:5173
```

La URL base de la API se encuentra en:
```
src/api/axiosClient.js
```
