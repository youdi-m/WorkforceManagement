interface StatusIndicator {
	status: number
}

function StatusDisplay({status}: StatusIndicator) {
	function setColor(s: number) {
		if(s ==0) return '#00ff00'
		return '#ff0000'
	}

	return (
		<span style={{backgroundColor: setColor(status)}}></span>
	)
}

export default StatusDisplay