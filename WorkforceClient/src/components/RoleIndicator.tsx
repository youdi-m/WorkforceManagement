interface RoleIndicator {
	role: number
}

function RoleDisplay({role}: RoleIndicator) {

	switch (role) {
		case 0:
			return 'Employee'
		case 1:
			return 'Lead'
		case 2:
			return 'HR'
		default:
			return 'Unknown'
	}
}

export default RoleDisplay;