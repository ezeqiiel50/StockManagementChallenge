import { BrowserRouter, Routes, Route } from "react-router-dom"
import LoginPage from "../features/auth/LoginPage"
import ProductsPage from "../pages/ProductsPage"
import ProductForm from "../features/products/ProductForm"
import FilterProducts from "../features/products/FilterProducts"
import ProtectedRoute from "./ProtectedRoute"
import Layout from "../components/Layout"

function AppRoutes() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<LoginPage />} />
        <Route element={<ProtectedRoute />}>
          <Route element={<Layout />}>
            <Route path="/products" element={<ProductsPage />} />
            <Route path="/products/new" element={<ProductForm />} />
            <Route path="/products/edit/:id" element={<ProductForm />} />
            <Route path="/products/filter" element={<FilterProducts />} />
          </Route>
        </Route>
      </Routes>
    </BrowserRouter>
  )
}

export default AppRoutes