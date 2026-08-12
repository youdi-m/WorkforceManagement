import { useNavigate } from "react-router-dom"
import { useAuth } from "../context/AuthContext"

function Login() {
	const navigate = useNavigate()
	const {setRole} = useAuth()

	return (
		// temporary fake login buttons
		<div>
			<h1>Login</h1>
			<button onClick={() => {setRole('employee'); navigate('/employee')}}>Employee</button>
			<button onClick={() => {setRole('lead'); navigate('/lead')}}>Lead</button>
			<button onClick={() => {setRole('hr'); navigate('/hr')}}>Hr</button>
		</div>
	)
}
export default Login