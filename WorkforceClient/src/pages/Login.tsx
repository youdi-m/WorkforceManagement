import { useNavigate } from "react-router-dom"
import { useAuth } from "../context/AuthContext"
import { useState } from "react"

// function to authenticate the user and navigate them to the proper page based on their role.
function Login() {
	// used to navigate to the needed page
	const navigate = useNavigate()
	// deconstructing userAuth to grab the setRole funtion
	const {setRole} = useAuth()

	// email and password getter/setters
	const [email, setEmail] = useState('')
	const [password, setPassword] = useState('')

	// sending POST request and routing appropriately
	const handleLogin = async () => {
		const response = await fetch('http://localhost:5016/api/auth/login', {
			method: 'POST',
			headers: { 'Content-Type': 'application/json' },
			body: JSON.stringify({ email: email, password: password })
		})

		// grabbing role from request and setting it
		const data = await response.json()
		setRole(data.role)
		
		// switch case to navigate depending on role
		switch(data.role){
			case 0: {
				navigate('/employee')
				break
			}
			case 1: {
				navigate('/lead')
				break
			}
			case 2: {
				navigate('/hr')
				break
			}
			// default for invalid credentials
			default: {
				alert('Invalid Creds')
				break
			}
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