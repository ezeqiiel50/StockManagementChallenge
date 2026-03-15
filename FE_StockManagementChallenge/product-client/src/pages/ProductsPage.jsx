import ProductList from "../features/products/ProductList"
import ProductForm from "../features/products/ProductForm"
import { useNavigate } from "react-router-dom"

export default function ProductsPage() {
  const navigate = useNavigate()

  return (
    <div>
      <h1>Products</h1>
      <button
        onClick={() => navigate("/products/new")}
      >
        Create Product
      </button>

      <ProductList />
    </div>
  )
}