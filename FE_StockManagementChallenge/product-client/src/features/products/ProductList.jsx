import { useEffect, useState } from "react"
import { useDispatch, useSelector } from "react-redux"
import { getProducts, deleteProduct } from "./productSlice"
import { useNavigate, NavLink } from 'react-router-dom'

export default function ProductList() {
  const navigate = useNavigate()
  const { items = [], loading, error } = useSelector(state => state.products)
  const dispatch = useDispatch()
  const [deletingId, setDeletingId] = useState(null)

  useEffect(() => {
    dispatch(getProducts())
  }, [dispatch])

  const handleDelete = async (id) => {
    setDeletingId(id)
    await dispatch(deleteProduct(id))
    setDeletingId(null)
  }

  return (
    <div className="flex min-h-screen bg-gray-100">

       {deletingId && (
        <div className="fixed inset-0 bg-black/40 flex items-center justify-center z-50">
          <div className="bg-white rounded-xl p-6 flex flex-col items-center gap-3 shadow-lg">
            <svg className="animate-spin h-10 w-10 text-blue-600" xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24">
              <circle className="opacity-25" cx="12" cy="12" r="10" stroke="currentColor" strokeWidth="4"/>
              <path className="opacity-75" fill="currentColor" d="M4 12a8 8 0 018-8v8z"/>
            </svg>
            <p className="text-gray-700 font-medium">Eliminando producto...</p>
          </div>
        </div>
      )}
      <main className="flex-1 p-8">
        <h2 className="text-2xl font-bold text-gray-800 mb-6">Lista de Productos</h2>

        {loading && <p className="text-gray-500">Cargando...</p>}
        {error && (
          <p className="text-red-500 text-sm bg-red-50 border border-red-200 rounded-lg p-2 mb-4">
            {error}
          </p>
        )}

        <div className="bg-white rounded-xl shadow overflow-hidden">
          <table className="w-full text-sm text-left">
            <thead className="bg-gray-50 text-gray-600 uppercase text-xs">
              <tr>
                <th className="px-6 py-3">Descripción</th>
                <th className="px-6 py-3">Precio</th>
                <th className="px-6 py-3">Categoría</th>
                <th className="px-6 py-3">Fecha Carga</th>
                <th className="px-6 py-3">Acciones</th>
              </tr>
            </thead>
            <tbody className="divide-y divide-gray-100">
              {items.map(p => (
                <tr key={p.id} className="hover:bg-gray-50 transition-colors">
                  <td className="px-6 py-4 text-gray-800">{p.descripcion}</td>
                  <td className="px-6 py-4 text-gray-800">${p.precio}</td>
                  <td className="px-6 py-4 text-gray-800">{p.categoria}</td>
                  <td className="px-6 py-4 text-gray-800">{p.fechaCarga}</td>
                  <td className="px-6 py-4 flex gap-2 items-center">
                    <button
                      onClick={() => navigate(`/products/edit/${p.id}`)}
                      className="bg-yellow-400 hover:bg-yellow-500 text-white text-xs font-semibold px-3 py-1 rounded-lg transition-colors"
                    >
                      Editar
                    </button>
                    <button
                      onClick={() => handleDelete(p.id)}
                      disabled={deletingId === p.id}
                      className="bg-red-500 hover:bg-red-600 text-white text-xs font-semibold px-3 py-1 rounded-lg transition-colors disabled:opacity-50 flex items-center gap-1 min-w-[70px] justify-center"
                    >
                      {deletingId === p.id ? (
                        <>
                          {/* Spinner */}
                          <svg className="animate-spin h-3 w-3 text-white" xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24">
                            <circle className="opacity-25" cx="12" cy="12" r="10" stroke="currentColor" strokeWidth="4"/>
                            <path className="opacity-75" fill="currentColor" d="M4 12a8 8 0 018-8v8z"/>
                          </svg>
                          Borrando
                        </>
                      ) : (
                        "Eliminar"
                      )}
                    </button>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      </main>
    </div>
  )
}