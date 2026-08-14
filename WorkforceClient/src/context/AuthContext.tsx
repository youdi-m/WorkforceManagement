import { createContext, useContext, useState } from "react"
import type { ReactNode } from "react";

// declaring new type
interface AuthContextType {
	role: string | null
	setRole: (role: string | null) => void
}

// declaring undefined context
const AuthContext = createContext<AuthContextType | undefined>(undefined);

// function to wrap the app
export function AuthProvider({children} : {children: ReactNode}){
	const [role, setRole] = useState<string | null>(null)

	return (
		// holds the role and provides it to wrapped children
		<AuthContext.Provider value={{role, setRole}}>
			{children}
		</AuthContext.Provider>
	)
}

// function to read the contexts current value
export function useAuth() {
	const context = useContext(AuthContext)
	// handle using useAuth outsite the AuthProvider
	if(context == undefined) {
		throw new Error('useAuth must be used within an AuthProvider')
	}
	return context
}