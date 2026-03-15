import ProductList from "../features/products/ProductList"
import { useNavigate } from "react-router-dom"

export default function ProductsPage() {
  const navigate = useNavigate()

  return (
    <div>
      <ProductList />
    </div>
  )
}