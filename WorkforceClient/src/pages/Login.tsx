import { useNavigate } from "react-router-dom"
import { useAuth } from "../context/AuthContext"
import { useState } from "react"

function Login() {
	const navigate = useNavigate()
	const {setRole} = useAuth()

	const [email, setEmail] = useState('')
	const [password, setPassword] = useState('')

	const handleLogin = async () => {
		const response = await fetch('http://localhost:5016/api/auth/login', {
			method: 'POST',
			headers: { 'Content-Type': 'application/json' },
			body: JSON.stringify({ email: email, password: password })
		})

		if(response.ok){
			const data = await response.json()
			setRole(data.role)

			if(data.role == 0) {
				navigate('/employee')
			}

			else if(data.role == 1) {
				navigate('/lead')
			}

			else if(data.role == 2) {
				navigate('/hr')
			}
		}
			else {
				alert('Invalid Creds')
			}
	}

	return (
		<div>
			<h1>Login</h1>
			<input type="text" placeholder="email"
				value={email} onChange={e => setEmail(e.target.value)} />
			<input type="password" placeholder="password"
				value={password} onChange={e => setPassword(e.target.value)} />
			<button onClick={handleLogin}>Login</button>
		</div>
	)
}
export default Login