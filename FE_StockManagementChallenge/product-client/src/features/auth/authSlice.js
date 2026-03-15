import { createSlice, createAsyncThunk } from "@reduxjs/toolkit"
import { loginRequest } from "../../api/authApi"

export const login = createAsyncThunk("auth/login",
  async (credentials, { rejectWithValue }) => {
    try 
    {
      const data = await loginRequest(credentials)
      localStorage.setItem("token", data.value.token)
      return data.value.token
    }
    catch (error) {
      return rejectWithValue(error.response?.data?.errors?.[0]?.message)
    } 
  }
)

const authSlice = createSlice({
  name: "auth",

  initialState: {
    token: localStorage.getItem("token"),
    loading: false,
    error: null
  },

  reducers: {

    logout: (state) => {
      state.token = null
      localStorage.removeItem("token")
    }

  },

  extraReducers: (builder) => {

    builder.addCase(login.pending, (state) => {
      state.loading = true
      state.error = null
    })

    builder.addCase(login.fulfilled, (state, action) => {
      state.loading = false
      state.token = action.payload
    })

    builder.addCase(login.rejected, (state, action) => {
      state.loading = false
      state.error = action.payload
    })
  }
})

export const { logout } = authSlice.actions
export default authSlice.reducer