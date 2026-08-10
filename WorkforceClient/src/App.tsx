import { useState } from 'react'
import './App.css'

function App() {
  const [count, setCount] = useState(0)

  return (
    <div>
      <h1>Workforce Management</h1>
      <button onClick={() => setCount(count + 1)}> Count is {count}</button>
    </div>
  )
}

export default App
