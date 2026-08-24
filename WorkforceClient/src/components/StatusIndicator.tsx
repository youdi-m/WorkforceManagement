interface StatusIndicator {
	status: string
}

function StatusDisplay({status}: StatusIndicator) {
	function setColor(s: string) {
		if(s =='0') return '#90EE90'
		return '#EE9090'
	}

	return (
		<span style={{backgroundColor: setColor(status)}}></span>
	)
}

export default StatusDisplay