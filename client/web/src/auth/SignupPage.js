import React, {useState} from 'react';
import {Link, useNavigate} from 'react-router-dom';
import {Button} from 'primereact/button';
import {InputText} from 'primereact/inputtext';
import {Password} from 'primereact/password';
import {Message} from 'primereact/message';
import {APP_BASE_URL} from '../App.constants';


const SignupPage = () => {

    const [username, setUsername] = useState("");
    const [password, setPassword] = useState("");
    const [name, setName] = useState("");
    const [email, setEmail] = useState("");

    const [error, setError] = useState(false);
    const [errorMessage, setErrorMessage] = useState("");

    const navigate = useNavigate();

    const isValidForm = () => {
        return username.length > 0 && password.length > 0 && name.length > 0 && email.length > 0;
    }

    const handleSubmit = async (e) => {
        e.preventDefault();

        try {
            const response = await fetch(`${APP_BASE_URL}/auth/signup`, {
                method: "POST",
                body: JSON.stringify({
                    username,
                    password,
                    email,
                    name
                }),
                headers: {
                    "Content-Type": "application/json"
                }
            });

            if (!response.ok) {
                setError(true);
                const data = await response.json();
                setErrorMessage(`${data.message}`);
                return;
            }

            const data = await response.json();
            console.log(data);
            navigate("/signin");

        } catch (error) {
            console.error(error);
            setError(true);
            setErrorMessage(error);
        }
    }

    return (
        <div className="login-panel shadow-4 p-fluid">
            <form onSubmit={handleSubmit}>
                <h1>Registration</h1>

                {error && <Message severity="error" text={errorMessage}/>}

                <div className="mb-2">
                    <InputText value={username}
                               onChange={(e) => setUsername(e.target.value)}
                               placeholder="Username"/>
                </div>

                <div className="mb-2">
                    <Password value={password}
                              onChange={(e) => setPassword(e.target.value)}
                              toggleMask
                              feedback={false}
                              placeholder="Password"
                    />
                </div>

                <div className="mb-2">
                    <InputText value={name}
                               onChange={(e) => setName(e.target.value)}
                               placeholder="Name"/>
                </div>

                <div className="mb-2">
                    <InputText value={email}
                               onChange={(e) => setEmail(e.target.value)}
                               placeholder="Email"/>
                </div>

                <div>
                    <Button type="submit" disabled={!isValidForm()}>
                        Sign Up
                    </Button>
                    <br/>
                    <br/>
                    <Link to="/signin">Sign In</Link>
                </div>
            </form>
        </div>
    );
}

export default SignupPage;