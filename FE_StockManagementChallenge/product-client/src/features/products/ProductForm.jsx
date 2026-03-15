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
  const [fechaCarga, setFechaCarga] = useState('')

  const [errors, setErrors] = useState({ descripcion: '', precio: '' })

  const validateForm = () => {
  const newErrors = { descripcion: '', precio: '' }
  let isValid = true

  // Validación descripción
  if (!/^[a-zA-Z0-9 ]+$/.test(descripcion)) {
    newErrors.descripcion = 'Solo se permiten letras y números'
    isValid = false
  } else if (descripcion.length > 50) {
    newErrors.descripcion = 'Máximo 50 caracteres'
    isValid = false
  }

  // Validación precio
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
      setFechaCarga(currentProduct.fechaCarga || '')
    }
  }, [currentProduct, isEdit])

  const handleSubmit = async (e) => {
    e.preventDefault()

    if (!validateForm()) return
    const payload = { descripcion, precio: parseInt(precio), categoria }
    let result;

    if (isEdit) {
      result = await dispatch(updateProduct({ id, ...payload }))
    } else {
      result =await dispatch(createProduct(payload))
    }

     if (!result.error) {
      navigate('/products')
    }
  }

  return (

    <div style={{ maxWidth: 480, margin: '40px auto', padding: 24 }}>
      <h2>{isEdit ? 'Editar producto' : 'Crear producto'}</h2>

      {(errorCats || errorProduct) &&  <p style={{ color: 'red' }}>{errorProduct ?? errorCats}</p>}

      <form onSubmit={handleSubmit} style={{ display: 'flex', flexDirection: 'column', gap: 16 }}>

        <div style={{ display: 'flex', flexDirection: 'column', gap: 4 }}>
          <label htmlFor="descripcion">Descripción *</label>
          <input
            id="descripcion"
            placeholder="Ingresá una descripción"
            value={descripcion}
            onChange={(e) => {
              const value = e.target.value
              if (/^[a-zA-Z0-9 ]*$/.test(value)) {
                setDescripcion(value)
              }
            }}
            required
          />
          {errors.descripcion && <p style={{ color: 'red' }}>{errors.descripcion}</p>}
        </div>

        <div style={{ display: 'flex', flexDirection: 'column', gap: 4 }}>
          <label htmlFor="precio">Precio *</label>
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
          />
          {errors.precio && <p style={{ color: 'red' }}>{errors.precio}</p>}
        </div>

        <div style={{ display: 'flex', flexDirection: 'column', gap: 4 }}>
          <label htmlFor="categoria">Categoría *</label>
          <select
            id="categoria"
            value={categoria}
            onChange={(e) => setCategoria(e.target.value)}
            required
          >
            <option key= "default" value="">
              {loadingCats ? 'Cargando categorías...' : 'Seleccioná una categoría'}
            </option>
            {categories.map((cat) => (
              <option key={cat.categoriaCodigo} value={cat.categoriaCodigo}>
                {cat.categoriaDescripcion}
              </option>
            ))}
          </select>
        </div>

        <div style={{ display: 'flex', gap: 8, marginTop: 8 }}>
          <button type="submit" disabled={loadingProduct}>
            {loadingProduct ? 'Guardando...' : 'Guardar'}
          </button>
          <button type="button" onClick={() => navigate('/products')}>
            Cancelar
          </button>
        </div>

      </form>
    </div>
  )
}