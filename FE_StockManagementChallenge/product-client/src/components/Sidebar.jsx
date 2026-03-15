import { NavLink } from "react-router-dom"

export default function Sidebar() {
  return (
    <aside className="w-56 bg-white shadow-md flex flex-col p-3 gap-2">
      <h1 className="text-xl font-bold text-blue-600 mb-6">Stock App</h1>
      <NavLink
        to="/products"
        end
        className={({ isActive }) =>
          `px-4 py-2 rounded-lg text-lm font-medium transition-colors ${
            isActive ? "bg-blue-600 text-white" : "text-gray-600 hover:bg-gray-100"
          }`
        }
      >
        📦 Productos
      </NavLink>
      <NavLink
        to="/products/filter"
        className={({ isActive }) =>
          `px-4 py-2 rounded-lg text-lm font-medium transition-colors ${
            isActive ? "bg-blue-600 text-white" : "text-gray-600 hover:bg-gray-100"
          }`
        }
      >
        🔍 Filtrar productos
      </NavLink>
      <NavLink
        to="/products/new"
        className={({ isActive }) =>
          `px-4 py-2 rounded-lg text-lm font-medium transition-colors ${
            isActive ? "bg-blue-600 text-white" : "text-gray-600 hover:bg-gray-100"
          }`
        }
      >
        ➕ Nuevo producto
      </NavLink>
    </aside>
  )
}