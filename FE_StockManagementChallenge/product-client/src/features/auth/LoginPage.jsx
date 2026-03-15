import { useState } from "react"
import { useSelector, useDispatch } from "react-redux"
import { useNavigate } from "react-router-dom"
import { login } from "./authSlice"

export default function LoginPage() {
  const navigate = useNavigate()
  const { loading, error } = useSelector(state => state.auth)
  const dispatch = useDispatch()
  const [user, setUser] = useState("")
  const [password, setPassword] = useState("")

  const handleSubmit = (e) => {
    e.preventDefault()
    dispatch(login({ user, password }))
      .unwrap()
      .then(() => { navigate("/products") })
  }

  return (
    <div className="min-h-screen bg-gray-100 flex items-center justify-center">
      <div className="bg-white p-8 rounded-xl shadow-md w-full max-w-sm">

        <h2 className="text-3xl font-bold text-blue-600 mb-6 text-center">Login</h2>

        {error && (
          <p className="text-red-500 text-sm text-center mb-4 bg-red-50 border border-red-200 rounded-lg p-2">
            {error}
          </p>
        )}

        <form onSubmit={handleSubmit} className="flex flex-col gap-4">
          <input
            placeholder="Usuario"
            value={user}
            required
            onChange={(e) => setUser(e.target.value)}
            className="w-full border border-gray-300 rounded-lg px-3 py-2 text-sm focus:outline-none focus:ring-2 focus:ring-blue-500"
          />
          <input
            type="password"
            placeholder="Password"
            value={password}
            required
            onChange={(e) => setPassword(e.target.value)}
            className="w-full border border-gray-300 rounded-lg px-3 py-2 text-sm focus:outline-none focus:ring-2 focus:ring-blue-500"
          />
          <button
            type="submit"
            disabled={loading}
            className="bg-blue-600 text-white font-semibold py-2 rounded-lg hover:bg-blue-700 transition-colors disabled:opacity-50 disabled:cursor-not-allowed"
          >
            {loading ? "Cargando..." : "Login"}
          </button>
        </form>

      </div>
    </div>
  )
}