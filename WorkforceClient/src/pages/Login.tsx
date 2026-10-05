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

	// getters and setters to login
	const [email, setEmail] = useState('')
	const [password, setPassword] = useState('')

	// getters and setters to register a new account


	// const to show and hide the login container
	const [showLoginDiv, setShowLoginDiv] = useState(false);
	const [showLoginForm, setShowLoginForm] = useState(false);
	const [showSignUpForm, setShowSignUpForm] = useState(false);

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
					navigate('/employee/dashboard')
					break
				}
				case 1: {
					navigate('/lead/dashboard')
					break
				}
				case 2: {
					navigate('/hr/dashboard')
					break
				}
				case 3: {
					navigate('/owner/dashboard')
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
					<a onClick={() => {setShowLoginDiv(true); setShowLoginForm(true);}}>Sign In</a>
				</div>
			</div>

			<div className={showLoginDiv ? 'loginContainer visible' : 'loginContainer'}>
				<form className={showLoginForm ? 'loginForm visible' : 'loginForm'}>
					<h3>TeamForge Sign In</h3>
					<input type="text" placeholder="email"
						value={email} onChange={e => setEmail(e.target.value)} />
					<input type="password" placeholder="password"
						value={password} onChange={e => setPassword(e.target.value)} />
					<button type="button" onClick={handleLogin}>Login</button>
					<button className="signUpButton" type="button"onClick={() => {setShowLoginForm(false);
																																				setShowSignUpForm(true);}}>Sign up</button>
				</form>

				<form className={showSignUpForm ? 'signUpForm visible' : 'signUpForm'}>
					<h3>TeamForge Sign Up</h3>
					<input type="text" placeholder="First Name"
						value={email} onChange={e => setEmail(e.target.value)} />
					<input type="password" placeholder="password"
						value={password} onChange={e => setPassword(e.target.value)} />
					<button type="button" onClick={handleLogin}>Login</button>
					<button type="button" onClick={() => {setShowLoginForm(true); setShowSignUpForm(false);}}>Sign In</button>
				</form>
			</div>

		</div>
	)
}
export default Login