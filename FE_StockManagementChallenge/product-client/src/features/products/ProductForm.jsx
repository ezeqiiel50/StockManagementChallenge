import { useEffect, useState } from 'react'
import { useNavigate, useParams } from 'react-router-dom'
import { useDispatch, useSelector } from 'react-redux'
import { createProduct, updateProduct, getByProductId } from './productSlice'
import { getCategories } from '../categories/categoriesSlice'

export default function ProductForm() {
  const navigate = useNavigate()
  const dispatch = useDispatch()
  const { id } = useParams()
  const isEdit = Boolean(id)

  const { currentProduct, loading: loadingProduct, error: errorProduct } = useSelector(state => state.products)
  const { items: categories = [], loading: loadingCats, error: errorCats } = useSelector(state => state.categories)

  const [descripcion, setDescripcion] = useState('')
  const [precio, setPrecio] = useState('')
  const [categoria, setCategoria] = useState('')
  const [errors, setErrors] = useState({ descripcion: '', precio: '' })

  const validateForm = () => {
    const newErrors = { descripcion: '', precio: '' }
    let isValid = true

    if (!/^[a-zA-Z0-9 ]+$/.test(descripcion)) {
      newErrors.descripcion = 'Solo se permiten letras y números'
      isValid = false
    } else if (descripcion.length > 50) {
      newErrors.descripcion = 'Máximo 50 caracteres'
      isValid = false
    }

    const precioNum = parseInt(precio)
    if (isNaN(precioNum) || precioNum < 1 || precioNum > 1000000) {
      newErrors.precio = 'El precio debe ser un número entre 1 y 1.000.000'
      isValid = false
    }

    setErrors(newErrors)
    return isValid
  }

  useEffect(() => {
    dispatch(getCategories())
    if (isEdit) dispatch(getByProductId(id))
  }, [id, isEdit, dispatch])

  useEffect(() => {
    if (isEdit && currentProduct) {
      setDescripcion(currentProduct.descripcion || '')
      setPrecio(currentProduct.precio || '')
      setCategoria(currentProduct.categoria || '')
    }
  }, [currentProduct, isEdit])

  const handleSubmit = async (e) => {
    e.preventDefault()
    if (!validateForm()) return
    const payload = { descripcion, precio: parseInt(precio), categoria }
    let result

    if (isEdit) {
      result = await dispatch(updateProduct({ id, ...payload }))
    } else {
      result = await dispatch(createProduct(payload))
    }

    if (!result.error) navigate('/products')
  }

  return (
    <div className="max-w-lg mx-auto">
      <h2 className="text-2xl font-bold text-gray-800 mb-6">
        {isEdit ? 'Editar producto' : 'Crear producto'}
      </h2>

      {(errorProduct || errorCats) && (
        <p className="text-red-500 text-sm bg-red-50 border border-red-200 rounded-lg p-2 mb-4">
          {errorProduct ?? errorCats}
        </p>
      )}

      <div className="bg-white rounded-xl shadow p-6">
        <form onSubmit={handleSubmit} className="flex flex-col gap-4">

          {/* Descripción */}
          <div className="flex flex-col gap-1">
            <label htmlFor="descripcion" className="text-sm font-medium text-gray-700">
              Descripción *
            </label>
            <input
              id="descripcion"
              placeholder="Ingresá una descripción"
              value={descripcion}
              onChange={(e) => {
                const value = e.target.value
                if (/^[a-zA-Z0-9 ]*$/.test(value)) setDescripcion(value)
              }}
              required
              className="w-full border border-gray-300 rounded-lg px-3 py-2 text-sm focus:outline-none focus:ring-2 focus:ring-blue-500"
            />
            {errors.descripcion && (
              <p className="text-red-500 text-xs">{errors.descripcion}</p>
            )}
          </div>

          {/* Precio */}
          <div className="flex flex-col gap-1">
            <label htmlFor="precio" className="text-sm font-medium text-gray-700">
              Precio *
            </label>
            <input
              id="precio"
              type="number"
              placeholder="Ingresá el precio"
              value={precio}
              required
              onChange={(e) => {
                const value = e.target.value
                if (value === '' || (parseInt(value) >= 1 && parseInt(value) <= 1000000)) {
                  setPrecio(value)
                }
              }}
              className="w-full border border-gray-300 rounded-lg px-3 py-2 text-sm focus:outline-none focus:ring-2 focus:ring-blue-500"
            />
            {errors.precio && (
              <p className="text-red-500 text-xs">{errors.precio}</p>
            )}
          </div>

          {/* Categoría */}
          <div className="flex flex-col gap-1">
            <label htmlFor="categoria" className="text-sm font-medium text-gray-700">
              Categoría *
            </label>
            <select
              id="categoria"
              value={categoria}
              onChange={(e) => setCategoria(e.target.value)}
              required
              className="w-full border border-gray-300 rounded-lg px-3 py-2 text-sm focus:outline-none focus:ring-2 focus:ring-blue-500 bg-white"
            >
              <option value="">
                {loadingCats ? 'Cargando categorías...' : 'Seleccioná una categoría'}
              </option>
              {categories.map((cat) => (
                <option key={cat.categoriaCodigo} value={cat.categoriaCodigo}>
                  {cat.categoriaDescripcion}
                </option>
              ))}
            </select>
          </div>

          {/* Botones */}
          <div className="flex gap-3 mt-2">
            <button
              type="submit"
              disabled={loadingProduct}
              className="flex-1 bg-blue-600 hover:bg-blue-700 text-white font-semibold py-2 rounded-lg transition-colors disabled:opacity-50 disabled:cursor-not-allowed"
            >
              {loadingProduct ? 'Guardando...' : 'Guardar'}
            </button>
            <button
              type="button"
              onClick={() => navigate('/products')}
              className="flex-1 bg-gray-100 hover:bg-gray-200 text-gray-700 font-semibold py-2 rounded-lg transition-colors"
            >
              Cancelar
            </button>
          </div>

        </form>
      </div>
    </div>
  )
}