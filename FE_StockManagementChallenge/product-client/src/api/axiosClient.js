import axios from "axios"

const axiosClient = axios.create({
  baseURL: "https://localhost:7101/api"
})

axiosClient.interceptors.request.use((config) => {

  const token = localStorage.getItem("token")

  if (token) {
    config.headers.Authorization = `Bearer ${token}`
  }
  return config
})

axiosClient.interceptors.response.use(
  (response) => response,
  (error) => {
    if (error.response?.status === 401) {
      localStorage.removeItem("token")
      window.location.href = "/"
    }
    return Promise.reject(error)
  }
)

export default axiosClient