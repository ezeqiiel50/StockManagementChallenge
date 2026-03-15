import { useState } from "react"
import { useDispatch, useSelector } from "react-redux"
import { filterProducts } from "./productSlice"

export default function FilterProducts() {
  const dispatch = useDispatch()
  const { items = [], loading, error } = useSelector(state => state.products)
  const [param, setParam] = useState('')

  const handleSearch = () => {
    if (!param || isNaN(parseInt(param))) return
    dispatch(filterProducts(parseInt(param)))
  }

  return (
    <>
      <div>
        <input
          type="number"
          placeholder="Ingresá un monto..."
          value={param}
          onChange={(e) => setParam(e.target.value)}
        />
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