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
    dispatch(
      login({ user, password})
    )
    .unwrap()
    .then(() => {navigate("/products")})
  }

  if (loading) { return <h3>Loading...</h3>}
  if (error) { return <p style={{color:"red"}}>{error}</p> }

  return (
    <div>
      <h2>Login</h2>
      <form onSubmit={handleSubmit}>
        <input
          placeholder="Usuario"
          value={user}
          onChange={(e) => setUser(e.target.value)}
        />
        <input
          type="password"
          placeholder="Password"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
        />
        <button type="submit">
          Login
        </button>
      </form>
    </div>
  )
}