import axiosClient from "./axiosClient"

export const getProductsRequest = async () => {
  const response = await axiosClient.get("/product")
  return response.data
}

export const createProductRequest = async (product) => {
  const response = await axiosClient.post("/product", product)
  return response.data
}

export const updateProductRequest = async (product) => {
  const response = await axiosClient.put(`/product/${product.id}`, product)
  return response.data
}

export const deleteProductRequest = async (id) => {
  const response = await axiosClient.delete(`/product/${id}`)
  return response.data
}

export const filterProductsRequest = async (amount) => {
  const response = await axiosClient.get(`/product/filter/${amount}`)
  return response.data
}

export const GetByIdProductRequest = async (id) => {
  const response = await axiosClient.get(`/product/${id}`)
  return response.data
}

export const GetCategoriesRequest = async () => {
  const response = await axiosClient.get(`/Category`)
  return response.data
}