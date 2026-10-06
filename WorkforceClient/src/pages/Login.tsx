import { useNavigate } from "react-router-dom"
import { useAuth } from "../context/AuthContext"
import { useState } from "react"
import './Login.css'

// function to authenticate the user and navigate them to the proper page based on their role.
function Login() {
	// used to navigate to the needed page
	const navigate = useNavigate()

	// grab the setRole and setToken funtions
	const {setRole, setToken} = useAuth()

	// getters and setters to login
	const [email, setEmail] = useState('')
	const [password, setPassword] = useState('')

	// getters and setters to register a new account
	// employee info
	const [firstName, setFirstName] = useState('')
	const [lastName, setLastName] = useState('')
	const [newEmail, setNewEmail] = useState('')
	const [newPassword, setNewPassword] = useState('')

	// company info
	const [companyName, setCompanyName] = useState('')
	const [legalName, setLegalName] = useState('')
	const [taxId, setTaxId] = useState('')
	const [timeZone, setTimeZone] = useState('')
	const [currency, setCurrency] = useState('')

	// company address info
	const [streetLine1, setStreetLine1] = useState('')
	const [streetLine2, setStreetLine2] = useState('')
	const [country, setCountry] = useState('')
	const [province, setProvince] = useState('')
	const [city, setCity] = useState('')
	const [postalCode, setPostalCode] = useState('')

	// const to show and hide the login container
	const [showForm, setShowForm] = useState('none');
	const [currentview, setCurrentView] = useState('login');

	// POST to sign in and route user
	const handleLogin = async () => {
		const response = await fetch('http://localhost:5016/api/auth/login', {
			method: 'POST',
			headers: { 'Content-Type': 'application/json',},
			body: JSON.stringify({email: email, password: password})
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

	// POST to register new user and company
	const handleSignUp = async () => {
		const response = await fetch('http://localhost:5016/api/auth/register', {
			method: 'POST',
			headers: { 'Content-Type': 'application/json',},
			body: JSON.stringify({firstName: firstName, lastName: lastName,
														email: newEmail, password: newPassword,

														name: companyName, legalName: legalName,
														taxId: taxId, timeZone: timeZone,
														currency: currency,
														
														streetLine1: streetLine1,
														streetLine2: streetLine2, country: country,
														province: province, city: city,
														postalCode: postalCode})
		})

		if (response.ok){
			alert("Registration Successful")
			setShowForm('login')
		}
		else
		{
			alert("Registration failed")
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
					<a onClick={() => {setShowForm('login');
														 setCurrentView('login');}}>Sign In</a>
				</div>
			</div>

			<div className={showForm != 'none' ? 'userContainer visible' : 'userContainer'}>
				<form className={currentview == 'login' ? 'loginForm visible' : 'loginForm'}>
					<h3>TeamForge Sign In</h3>

					<input type="text" placeholder="email"
						value={email} onChange={e => setEmail(e.target.value)} />

					<input type="password" placeholder="password"
						value={password} onChange={e => setPassword(e.target.value)} />

					<button type="button" onClick={handleLogin}>Login</button>

					<button className="signUpButton" type="button"onClick={() => {setShowForm('signUp');
																																				setCurrentView('signUp')}}>Sign up</button>
				</form>

				<form className={showForm == 'signUp' ? 'signUpForm visible' : 'signUpForm'}>
					<h3>TeamForge Sign Up</h3>

					<div className={currentview == 'signUp' ? 'signUpDiv visible' : 'signUpDiv'}>
						<input type="text" placeholder="First Name"
							value={firstName} onChange={e => setFirstName(e.target.value)} />

						<input type="text" placeholder="Last Name"
							value={lastName} onChange={e => setLastName(e.target.value)} />

						<input type="text" placeholder="Email"
							value={newEmail} onChange={e => setNewEmail(e.target.value)} />

						<input type="password" placeholder="Password"
							value={newPassword} onChange={e => setNewPassword(e.target.value)} />

						<button type="button" onClick={() => {setCurrentView('companySignUpForm')}}>Continue</button>
					</div>

					<div className={currentview == 'companySignUpForm' ? 'companySignUpDiv visible' : 'companySignUpDiv'}>
						<input type="text" placeholder="Company Name"
							value={companyName} onChange={e => setCompanyName(e.target.value)} />

						<input type="text" placeholder="Company Legal Name"
							value={legalName} onChange={e => setLegalName(e.target.value)} />

						<input type="text" placeholder="Tax Id"
							value={taxId} onChange={e => setTaxId(e.target.value)} />

						<input type="text" placeholder="TimeZone"
							value={timeZone} onChange={e => setTimeZone(e.target.value)} />

						<input type="text" placeholder="Currency"
							value={currency} onChange={e => setCurrency(e.target.value)} />

						<button type="button" onClick={() => {setCurrentView('addressSignUpForm')}}>Continue</button>
					</div>

					<div className={currentview == 'addressSignUpForm' ? 'addressSignUpDiv visible' : 'addressSignUpDiv'}>
						<input type="text" placeholder="Street Line 1"
							value={streetLine1} onChange={e => setStreetLine1(e.target.value)} />

						<input type="text" placeholder="Street Line 2"
							value={streetLine2} onChange={e => setStreetLine2(e.target.value)} />

						<input type="text" placeholder="Country"
							value={country} onChange={e => setCountry(e.target.value)} />

						<input type="text" placeholder="Province"
							value={province} onChange={e => setProvince(e.target.value)} />

						<input type="text" placeholder="City"
							value={city} onChange={e => setCity(e.target.value)} />

						<input type="text" placeholder="Postal Code"
							value={postalCode} onChange={e => setPostalCode(e.target.value)} />

						<button type="button" onClick={handleSignUp}>Sign Up</button>
					</div>

					<button type="button" onClick={() => {setCurrentView('login');
																								setShowForm('login')}}>Sign In</button>
				</form>
			</div>
		</div>
	)
}

export default Login