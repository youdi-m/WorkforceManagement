interface StatusIndicator {
	status: string
}

function StatusDisplay({status}: StatusIndicator) {
	function setColor(s: string) {
		if(s =='0') return '#00ff00'
		return '#ff0000'
	}

	return (
		<span style={{backgroundColor: setColor(status)}}></span>
	)
}

export default StatusDisplay