import { useNavigate } from "react-router-dom"
import { useAuth } from "../context/AuthContext"
import { useEffect, useState } from "react"
import './HrDashboard.css'

interface EmployeeType {
	id: string
	firstName: string
	lastName: string
	email: string
	role: string
	manager: string
	status: string
}

function HrDashboard() {

	const navigate = useNavigate();
	const {token, Logout} = useAuth()
	const [employee, setEmployees] = useState<EmployeeType[]>([])

	useEffect(() => {
		showEmployees()
	}, [])

	async function showEmployees() {

		const response = await fetch('http://localhost:5016/api/employee', {
			method: 'GET',
			headers: {'Authorization': `Bearer ${token}`},
		})

		if(response.ok) {
			const data = await response.json()
			setEmployees(data)
		}
		else
		{
			alert("Error Fetching Employees")
		}
	}


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
					{employee.map(emp => (
						<div key={emp.id}>
							<div>{emp.id}</div>
							<div><input value={emp.firstName}></input></div>
							<div><input value={emp.lastName}></input></div>
							<div><input value={emp.email}></input></div>
							<div>{emp.role}</div>
							<div>{emp.manager}</div>
							<div>{emp.status}</div>
						</div>
					))}
				</div>
			</div>
		</div>
		)
}

export default HrDashboard