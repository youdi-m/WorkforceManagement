import {Route, Routes, Navigate} from 'react-router-dom'

import EmployeeDashboard from './pages/EmployeeDashboard'
import HrDashboard from './pages/HrDashboard'
import LeadDashboard from './pages/LeadDashboard'
import Login from './pages/Login'

function App() {
  return (
    // defining routes
    <Routes>
      <Route path="/" element={<Navigate to="/login" replace />} />
      <Route path="/login" element={<Login />} />
      <Route path="/employee/dashboard" element={<EmployeeDashboard />} />
      <Route path="/lead/dashboard" element={<LeadDashboard />} />
      <Route path="/hr/dashboard" element={<HrDashboard />} />
      <Route path="/owner/dashboard" element={<HrDashboard />} />
    </Routes>
  )
}
export default App