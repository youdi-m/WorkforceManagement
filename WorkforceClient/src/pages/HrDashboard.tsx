import { useNavigate } from "react-router-dom"
import { useAuth } from "../context/AuthContext"
import { useEffect, useState } from "react"
import StatusDisplay from "../components/StatusIndicator"
import RoleDisplay from "../components/RoleIndicator"
import './HrDashboard.css'

interface EmployeeType {
	id: number
	firstName: string
	lastName: string
	email: string
	role: number
	title: string
	managerId: number
	status: string
}

function HrDashboard() {

	const navigate = useNavigate();
	const {token, Logout} = useAuth()
	const [employee, setEmployees] = useState<EmployeeType[]>([])
	const [selectedEmployee, setSelectedEmployee] = useState<EmployeeType | null>(null)
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
			console.log("Error Fetching Employees")
		}
	}

	return (
		<div className='mainContainer'>
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
						<div onClick={() => setSelectedEmployee(emp)} className='employeeRow' key={emp.id}>
							<div id='statusDiv'><StatusDisplay status={emp.status}/></div>
							<div id='idDiv'>{emp.id}</div>
							<div>{emp.firstName}</div>
							<div>{emp.lastName}</div>
							<div id='emailDiv'>{emp.email}</div>
							<div><RoleDisplay role={emp.role}/></div>
							<div>{emp.title}</div>
							<div>{emp.managerId}</div>
						</div>
					))}
				</div>
			</div>
			{selectedEmployee && (
				<div className='employeeInformationContainer'>
						<div className='informationHeader'>
							<h2>{selectedEmployee.firstName} {selectedEmployee.lastName} {selectedEmployee.id}</h2>
							<button id='empInformationCloseButton' onClick={() => setSelectedEmployee(null)}>X</button>
						</div>

						<div className='employeeInformation'>
							<div><h3>{selectedEmployee.firstName}</h3></div>
							<div><h3>{selectedEmployee.lastName}</h3></div>
							<div><h3>{selectedEmployee.email}</h3></div>
							<div><h3>{selectedEmployee.role}</h3></div>
							<div><h3>{selectedEmployee.title}</h3></div>
							<div><h3>{selectedEmployee.managerId}</h3></div>
							<div><h3>{selectedEmployee.status}</h3></div>
						</div>
				</div>
			)}
		</div>
		)
}

export default HrDashboard