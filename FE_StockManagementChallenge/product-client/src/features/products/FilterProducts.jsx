import { useState } from "react"
import { useDispatch, useSelector } from "react-redux"
import { filterProducts } from "./productSlice"

export default function FilterProducts() {
  const dispatch = useDispatch()
  const { items = [], loading, error } = useSelector(state => state.products)
  const [param, setParam] = useState('')
   const [errors, setErrors] = useState({ precio: '' })

  const handleSearch = () => {
    if (!param || isNaN(parseInt(param))) return
    if (!validateForm()) return
    dispatch(filterProducts(parseInt(param)))
  }

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

  return (
    <>
      <div>
        <input
          type="number"
          placeholder="Ingresá un monto..."
          value={param}
          required
          onChange={(e) => {
              const value = e.target.value
              if (value === '' || (parseInt(value) >= 1 && parseInt(value) <= 1000000)) {
                setParam(value)
              }
            }}
          />
          {errors.param && <p style={{ color: 'red' }}>{errors.param}</p>}
        <button onClick={handleSearch}>Buscar</button>
      </div>

      {error && <p style={{ color: 'red' }}>{error}</p>}
      {loading && <h3>Loading...</h3>}

      <table border="1">
        <thead>
          <tr>
            <th>Descripcion</th>
            <th>Precio</th>
            <th>Categoria</th>
            <th>FechaCarga</th>
          </tr>
        </thead>
        <tbody>
          {items.map(p => (
            <tr key={p.id}>
              <td>{p.descripcion}</td>
              <td>{p.precio}</td>
              <td>{p.categoria}</td>
              <td>{p.fechaCarga}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </>
  )
}