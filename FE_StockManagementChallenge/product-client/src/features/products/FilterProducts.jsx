import { useState, useEffect } from "react"
import { useDispatch, useSelector } from "react-redux"
import { filterProducts, clearFilterItems, clearErrors } from "./productSlice"

export default function FilterProducts() {
  const dispatch = useDispatch()
  const { filteredItems = [], loading, error } = useSelector(state => state.products)
  const [param, setParam] = useState('')
  const [errors, setErrors] = useState({ param: '' })

  const validateForm = () => {
    const newErrors = { param: '' }
    let isValid = true
    const precioNum = parseInt(param)
    if (isNaN(precioNum) || precioNum < 1 || precioNum > 1000000) {
      newErrors.param = 'El precio debe ser un número entre 1 y 1.000.000'
      isValid = false
    }
    setErrors(newErrors)
    return isValid
  }

  useEffect(() => {
        return () => {
            dispatch(clearFilterItems()) ,
            dispatch(clearErrors())
        }
    }, [dispatch])

  const handleSearch = () => {
    if (!param || isNaN(parseInt(param))) return
    if (!validateForm()) return
    dispatch(filterProducts(parseInt(param)))
  }

  return (
    <div className="flex min-h-screen bg-gray-100">
      <main className="flex-1 p-8">
        <h2 className="text-2xl font-bold text-gray-800 mb-6">Filtrar Productos</h2>
        <div className="bg-white p-6 rounded-xl shadow mb-6">
          <div className="flex gap-3 items-start">
            <div className="flex flex-col gap-1 flex-1">
              <input
                type="number"
                placeholder="Ingresá un monto máximo..."
                value={param}
                onChange={(e) => {
                  const value = e.target.value
                  if (value === '' || (parseInt(value) >= 1 && parseInt(value) <= 1000000)) {
                    setParam(value)
                  }
                }}
                className="w-full border border-gray-300 rounded-lg px-3 py-2 text-sm focus:outline-none focus:ring-2 focus:ring-blue-500"
              />
              {errors.param && (
                <p className="text-red-500 text-xs">{errors.param}</p>
              )}
            </div>
            <button
              onClick={handleSearch}
              className="bg-blue-600 hover:bg-blue-700 text-white text-sm font-semibold px-5 py-2 rounded-lg transition-colors"
            >
              Buscar
            </button>
          </div>
        </div>

        {/* Estados */}
        {error && (
          <p className="text-red-500 text-sm bg-red-50 border border-red-200 rounded-lg p-2 mb-4">
            {error}
          </p>
        )}
        {loading && (
            <div className="fixed inset-0 bg-black/40 flex items-center justify-center z-50">
            <div className="bg-white rounded-xl p-6 flex flex-col items-center gap-3 shadow-lg">
            <svg className="animate-spin h-10 w-10 text-blue-600" xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24">
              <circle className="opacity-25" cx="12" cy="12" r="10" stroke="currentColor" strokeWidth="4"/>
              <path className="opacity-75" fill="currentColor" d="M4 12a8 8 0 018-8v8z"/>
            </svg>
            <p className="text-gray-700 font-medium">Procesando...</p>
          </div>
        </div>
        )}

        {filteredItems.length > 0 ? (
          <div>
            <h3 className="text-lg font-semibold text-gray-700 mb-4">
              Mejor combinación encontrada
            </h3>
            <div className="grid grid-cols-2 gap-4 mb-6">
              {filteredItems.map((p) => (
                <div key={p.id} className="bg-white rounded-xl shadow p-6 border-l-4 border-blue-500">
                  <span className="text-xs font-semibold uppercase text-blue-500 bg-blue-50 px-2 py-1 rounded-full">
                    {p.categoria}
                  </span>
                  <h4 className="text-lg font-bold text-gray-800 mt-3 mb-1">{p.descripcion}</h4>
                  <p className="text-2xl font-bold text-blue-600">${p.precio}</p>
                  <p className="text-xs text-gray-400 mt-2">Cargado el {p.fechaCarga}</p>
                </div>
              ))}
            </div>
          
            {/* Total */}
            <div className="bg-white rounded-xl shadow p-4 flex justify-between items-center">
              <span className="text-gray-600 font-medium">Total de la combinación</span>
              <span className="text-xl font-bold text-blue-600">
                ${filteredItems.reduce((acc, p) => acc + p.precio, 0)}
              </span>
            </div>
          </div>
        ) : (
          !loading && (
            <div className="bg-white rounded-xl shadow p-8 text-center text-gray-400">
              Ingresá un monto y presioná Buscar para ver resultados.
            </div>
          )
        )}
      </main>
    </div>
  )
}