import { useNavigate } from "react-router-dom"
import { useAuth } from "../context/AuthContext"
import { useState } from "react"
import './Login.css'

// function to authenticate the user and navigate them to the proper page based on their role.
function Login() {
	// used to navigate to the needed page
	const navigate = useNavigate()
	// deconstructing userAuth to grab the setRole and setToken funtions
	const {setRole, setToken} = useAuth()

	// email and password getter/setters
	const [email, setEmail] = useState('')
	const [password, setPassword] = useState('')

	// const to show and hide the login container
	const [showLogin, setShowLogin] = useState(false);

	// sending POST request and routing appropriately
	const handleLogin = async () => {
		const response = await fetch('http://localhost:5016/api/auth/login', {
			method: 'POST',
			headers: { 'Content-Type': 'application/json',},
			body: JSON.stringify({ email: email, password: password })
		})

		if (response.ok)
		{
			// grabbing role from request and setting it
			const data = await response.json()
			setRole(data.role)
			
			// setting token and saving so it persists across refresh
			setToken(data.token)
			localStorage.setItem('token', data.token)

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
				// default for unknown role
				default: {
					alert('Unknown Role')
					break
				}
			}
		}
		else
		{
			alert('Invalid credentials') 
		}
		
	}

	return (
		<div className="mainContainer">
			<div className='navContainer'>
				<div className='navContainerLeft'>
					<a>TeamForge</a>
				</div>
				<div className='navContainerRight'>
					<a>About</a>
					<a onClick={() => setShowLogin(true)}>Sign In</a>
				</div>
			</div>

			<div className={showLogin ? 'loginContainer visible' : 'loginContainer'}>
				<form className='loginForm'>
					<h3>TeamForge Login</h3>
					<input type="text" placeholder="email"
						value={email} onChange={e => setEmail(e.target.value)} />
					<input type="password" placeholder="password"
						value={password} onChange={e => setPassword(e.target.value)} />
					<button type="button" onClick={handleLogin}>Login</button>
				</form>
			</div>
		</div>
	)
}
export default Login