import axiosClient from "./axiosClient"

export const loginRequest = async (credentials) => {

  const response = await axiosClient.post("/login/login", credentials)

  return response.data
}