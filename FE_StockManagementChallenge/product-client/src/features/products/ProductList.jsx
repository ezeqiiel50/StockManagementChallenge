import { useEffect } from "react"
import { useDispatch, useSelector } from "react-redux"
import { getProducts, deleteProduct } from "./productSlice"
import { useNavigate } from 'react-router-dom';

export default function ProductList() {
  const navigate = useNavigate();
  const { items = [], loading, error } = useSelector(state => state.products)
  const dispatch = useDispatch()

  useEffect(() => {
    dispatch(getProducts())
  }, [dispatch])

  const handleDelete = (id) => {
    dispatch(deleteProduct(id))
  }

  if (loading) { return <h3>Loading...</h3>}
  if (error) return <p style={{color:"red"}}>{error}</p>

  return (
    <>
      <table border="1">
        <thead>
          <tr>
            <th>Descripcion</th>
            <th>Precio</th>
            <th>Categoria</th>
            <th>FechaCarga</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {items.map(p => (
            <tr key={p.id}>
              <td>{p.descripcion}</td>
              <td>{p.precio}</td>
              <td>{p.categoria}</td>
              <td>{p.fechaCarga}</td>
              <td>
                <button onClick={() => navigate(`/products/edit/${p.id}`)}> Edit </button>
                <button onClick={() => handleDelete(p.id)}> Delete </button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </>
  )
}