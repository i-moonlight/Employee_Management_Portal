import { createContext, useContext, useMemo, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { APP_BASE_URL } from '../App.constants';


const AuthContext = createContext();

export const AuthProvider = ({children}) => {
    const [user, setUser] = useState(JSON.parse(localStorage.getItem("user")));
    const navigate = useNavigate();

    const signin = async (username, password) => {
        try {
            const response = await fetch(`${APP_BASE_URL}/auth/signin`, {
                method: "POST",
                body: JSON.stringify({
                    username,
                    password
                }),
                headers: {
                    "Content-Type": "application/json"
                }
            });

            const data = await response.json();
            localStorage.setItem("user", JSON.stringify(data));
            setUser(data);
            if (user.role === "admin") {
                navigate("/admin/dashboard", {
                    replace: true
                });
            } else if (user.role === "user") {
                navigate("/user/dashboard", {
                    replace: true
                });
            }
        } catch (error) {
            console.error(error);
        }
    }

    const signout = () => {
        setUser(null);
        localStorage.removeItem("user");
        navigate(
            "/signin", {replace: true}
        );
    }

    const value = useMemo(() => (
        {user, signin, signout}
        ), [user]
    );

    return <AuthContext.Provider value={value}>{children}</AuthContext.Provider>
}

export const useAuth = () => {
    return useContext(AuthContext);
}
