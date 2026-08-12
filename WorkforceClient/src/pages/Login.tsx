
import { useNavigate } from "react-router-dom"

function Login() {
	const navigate = useNavigate()

	return (
		// temporary fake login buttons
		<div>
			<h1>Login</h1>
			<button onClick={() => navigate('/employee')}>Employee</button>
			<button onClick={() => navigate('/lead')}>Lead</button>
			<button onClick={() => navigate('/hr')}>Hr</button>
		</div>
	)
}
export default Login