interface StatusIndicator {
	status: number
}

function StatusDisplay({status}: StatusIndicator) {

	return (
		<span className={status == 0 ? 'status-active' : 'status-inactive'}></span>
	)
}

export default StatusDisplay