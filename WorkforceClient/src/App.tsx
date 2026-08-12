import './App.css'
import { Route, Routes } from 'react-router-dom'

import EmployeeDashboard from './pages/EmployeeDashboard'
import HrDashboard from './pages/HrDashboard'
import LeadDashboard from './pages/LeadDashboard'
import Login from './pages/Login'

function App() {
  return (
    // defining routes
    <Routes>
      <Route path="/login" element={<Login />} />
      <Route path="/employee" element={<EmployeeDashboard />} />
      <Route path="/lead" element={<LeadDashboard />} />
      <Route path="/hr" element={<HrDashboard />} />
    </Routes>
  )
}

export default App