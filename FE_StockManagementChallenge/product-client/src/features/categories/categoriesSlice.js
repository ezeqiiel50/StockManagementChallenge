import { createSlice, createAsyncThunk } from "@reduxjs/toolkit"
import { GetCategoriesRequest }  from "../../api/productApi"

export const getCategories = createAsyncThunk("categories/GetCategories",
  async (_, { rejectWithValue }) => {
    try 
    {
        const data = await GetCategoriesRequest()
        return data.value
    } 
    catch (error) {
      return rejectWithValue(error.response?.data?.errors?.[0]?.message)
    }
  }
)

const categoriesSlice = createSlice({
  name: "categories",

  initialState: {
    items: [],
    loading: false,
    error: null
  },
  reducers: {},
  extraReducers: (builder) => {
     builder
//==GetCategories=============================================================
    .addCase(getCategories.pending, (state) => {
      state.loading = true
      state.error = null
    })

    .addCase(getCategories.fulfilled, (state, action) => {
      state.loading = false
      state.items = action.payload
    })

    .addCase(getCategories.rejected, (state, action) => {
      state.loading = false
      state.error = action.payload
    })
  }
})

export default categoriesSlice.reducer