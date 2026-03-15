import { createSlice, createAsyncThunk } from "@reduxjs/toolkit"
import {
  getProductsRequest,
  createProductRequest,
  updateProductRequest,
  deleteProductRequest,
  filterProductsRequest,
  GetByIdProductRequest } from "../../api/productApi"

export const getProducts = createAsyncThunk("products/getProducts",
  async (_, { rejectWithValue }) => {
    try 
    {
      const data = await getProductsRequest()
      return data.value
    } 
    catch (error) {
      return rejectWithValue(error.response?.data?.errors?.[0]?.message)
    }
  }
)

export const createProduct = createAsyncThunk("products/createProduct",
  async (product, { rejectWithValue }) => {
    try 
    {
        const data = await createProductRequest(product)
        return data.value
    } 
    catch (error) {
      return rejectWithValue(error.response?.data?.errors?.[0]?.message)
    }
  }
)

export const updateProduct = createAsyncThunk("products/updateProduct",
  async (product) => {
    try 
    {
        const data = await updateProductRequest(product)
        return data.value
    } 
    catch (error) {
      return rejectWithValue(error.response?.data?.errors?.[0]?.message)
    }
  }
)

export const deleteProduct = createAsyncThunk("products/deleteProduct",
  async (id, { rejectWithValue }) => {
    try 
    {
       await deleteProductRequest(id)
       return id
    } 
    catch (error) {
      return rejectWithValue(error.response?.data?.errors?.[0]?.message)
    }
  }
)

export const filterProducts = createAsyncThunk("products/filterProducts",
  async (amount, { rejectWithValue }) => {
    try 
    {
      const data = await filterProductsRequest(amount)
      return data.value
    } 
    catch (error) {
      return rejectWithValue(error.response?.data?.errors?.[0]?.message)
    }
  }
)

export const getByProductId = createAsyncThunk("products/GetByProductId",
  async (id, { rejectWithValue }) => {
    try 
    {
      const data = await GetByIdProductRequest(id)
      return data.value 
    } 
    catch (error) {
      return rejectWithValue(error.response?.data?.errors?.[0]?.message)
    }
  }
)

const productSlice = createSlice({
  name: "products",

  initialState: {
    items: [],
    loading: false,
    error: null
  },

  reducers: {},

  extraReducers: (builder) => {

     builder
//==GetAll=============================================================
    .addCase(getProducts.pending, (state) => {
      state.loading = true
      state.error = null
    })

    .addCase(getProducts.fulfilled, (state, action) => {
      state.loading = false
      state.items = action.payload
    })

    .addCase(getProducts.rejected, (state, action) => {
      state.loading = false
      state.error = action.payload
    })
//==Create=============================================================
    .addCase(createProduct.pending, (state) => {
      state.loading = true
      state.error = null
    })

    .addCase(createProduct.fulfilled, (state, action) => {
      state.loading = false
      state.items.push(action.payload)
    })

    .addCase(createProduct.rejected, (state, action) => {
      state.loading = false
      state.error = action.payload
    })
//==Update=============================================================
   .addCase(updateProduct.pending, (state) => {
      state.loading = true
      state.error = null
    })
    
    .addCase(updateProduct.fulfilled, (state, action) => {
      state.loading = false
      const index = state.items.findIndex(p => p.id === action.payload.id)
      if (index !== -1) {
        state.items[index] = action.payload
      }
    })

    .addCase(updateProduct.rejected, (state, action) => {
      state.loading = false
      state.error = action.payload
    })

//==Delete=============================================================
     .addCase(deleteProduct.pending, (state) => {
      state.loading = true
      state.error = null
    })

    .addCase(deleteProduct.fulfilled, (state, action) => {
      state.items = state.items.filter(
        p => p.id !== action.payload
      )
      state.loading = false
    })

    .addCase(deleteProduct.rejected, (state, action) => {
      state.loading = false
      state.error = action.payload
    })
//==GetById=============================================================
    .addCase(getByProductId.pending, (state) => {
      state.loading = true
      state.error = null
    })

    .addCase(getByProductId.fulfilled, (state, action) => {
      state.loading = false
      state.currentProduct = action.payload
    })

    .addCase(getByProductId.rejected, (state, action) => {
      state.loading = false
      state.error = action.payload
    })

//==GetByFilter=============================================================
    .addCase(filterProducts.pending, (state) => {
      state.loading = true
      state.error = null
    })

    .addCase(filterProducts.fulfilled, (state, action) => {
      state.loading = false
      state.items = action.payload
    })

    .addCase(filterProducts.rejected, (state, action) => {
      state.loading = false
      state.error = action.payload
      console.log(action.payload)
    })
  }
})

export default productSlice.reducer