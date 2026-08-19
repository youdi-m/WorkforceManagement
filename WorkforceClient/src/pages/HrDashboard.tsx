import {useAuth} from "../context/AuthContext"
import { useNavigate } from "react-router-dom"

import './HrDashboard.css'

function HrDashboard() {

	const navigate = useNavigate();
	const {Logout} = useAuth();

	return (
		<div className="mainContainer">
			<div className='navContainer'>
				<div className='navContainerLeft'>
					<a>HR Dashboard</a>
				</div>
				<div className='navContainerRight'>
					<a>About</a>
					<a onClick={() => {Logout(); navigate('/login')}}>Sign Out</a>
				</div>
			</div>
			<div className='displayContainer'>
				<div className='employeeContainer'>
					
				</div>
			</div>
		</div>
		)
}

export default HrDashboard