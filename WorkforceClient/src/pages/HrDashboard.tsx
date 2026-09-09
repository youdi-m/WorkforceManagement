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
	status: number
	shiftStart: string
	shiftEnd: string
	dateOfBirth: string
}

interface EnumType {
	value: number;
	label: string;
}

function HrDashboard() {

	const navigate = useNavigate();
	const {token, Logout} = useAuth()
	const [employee, setEmployees] = useState<EmployeeType[]>([])
	const [selectedEmployee, setSelectedEmployee] = useState<EmployeeType | null>(null)

	const [roles, setRoles] = useState<EnumType[]>([])
	const [statuses, setStatuses] = useState<EnumType[]>([])

	const [firstName, setFirstName] = useState('')
	const [lastName, setLastName] = useState('')
	const [email, setEmail] = useState('')
	const [role, setRole] = useState('')
	const [title, setTitle] = useState('')
	const [managerId, setManagerId] = useState('')
	const [status, setStatus] = useState('')
	const [shiftStart, setshiftStart] = useState('')
	const [shiftEnd, setshiftEnd] = useState('')
	const [dateOfBrith, setdateOfBirth] = useState('')

	// populate dashboard with employees on first render
	useEffect(() => {
		showEmployees();
		fetchRoles();

	}, [])

	// GET request to show employees on the dashboard
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

	async function fetchRoles() {

		const response = await fetch('http://localhost:5016/api/employeerole', {
			method: 'GET',
			headers: {'Authorization': `Bearer ${token}`},
		})

		if(response.ok) {
			const data = await response.json()
			setRoles(data)
		}
		else {
			console.log('Failed to fetch roles')
		}
	}

	// PUT request to update employee
	async function updateEmployee() {

		if (selectedEmployee?.id != null)
		{
			const response = await fetch(`http://localhost:5016/api/employee/update/${selectedEmployee.id}`, {
				method: 'PUT',
				headers: { 'Content-Type': 'application/json', 'Authorization': `Bearer ${token}`},
				body: JSON.stringify({firstName: firstName, lastName: lastName, email: email, role: Number(role),
															title: title, managerId: Number(managerId), status: Number(status),
															shiftStart: shiftStart, shiftEnd: shiftEnd, dateOfBrith: dateOfBrith
				})
			})

			if(response.ok) {
				console.log('updating employee')
				showEmployees()
			}
			else
			{
				console.log('error updating employee')
			}
		}
	}

	return (
		<div className='mainContainer'>
			<div className='navContainer'>
				<div className='navContainerLeft'>
					<a>HR Dashboard</a>
					<a>Payroll</a>
					<a>Compliance</a>
					<a>Time off</a>
				</div>
				<div className='navContainerRight'>
					<a>About</a>
					<a onClick={() => {Logout(); navigate('/login')}}>Sign Out</a>
				</div>
			</div>
			<div className='displayContainer'>
				<div className="toolBarContainer">
					<button>Onboarding</button>
				</div>
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
							<div>{emp.shiftStart}</div>
							<div>{emp.shiftEnd}</div>
							<div>{emp.dateOfBirth}</div>

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
							<div>First Name<input onChange={e => setFirstName(e.target.value)}></input></div>

							<div>Last Name<input onChange={e => setLastName(e.target.value)}></input></div>

							<div>Email<input onChange={e => setEmail(e.target.value)}></input></div>

							<div>Role<select onChange={e => setRole(e.target.value)}>
												<option value={selectedEmployee.status}></option>	
											 </select></div>

							<div>Title<input onChange={e => setTitle(e.target.value)}></input></div>

							<div>Manager<input onChange={e => setManagerId(e.target.value)}></input></div>

							<div>Shift Start<input type="Time" onChange={e => setshiftStart(e.target.value)}></input></div>

							<div>Shift End<input type="Time" onChange={e => setshiftEnd(e.target.value)}></input></div>

							<div>Date of Birth<input type="Date" onChange={e => setdateOfBirth(e.target.value)}></input></div>

							<div>Status<input onChange={e => setStatus(e.target.value)}></input></div>

						</div>
						<button onClick={updateEmployee}>submit</button>
				</div>
			)}
		</div>
		)
}

export default HrDashboard