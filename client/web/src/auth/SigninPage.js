import React, { useState } from 'react';
import { Link } from 'react-router-dom';

import { InputText } from 'primereact/inputtext';
import { Password } from 'primereact/password';

import { useAuth } from './useAuth';

const SigninPage = () => {

    const [username, setUsername] = useState("");
    const [password, setPassword] = useState("");

    const isValidForm = () => {
        return username.length > 0 && password.length > 0;
    }

    const {signin} = useAuth();
    const handleSubmit = async (e) => {
        e.preventDefault();
        await signin(username, password);
    }

    const [checked, setChecked] = useState(true);
    function changeCheckbox() {
        setChecked(!checked);
    }

    return (
        <div className="login-panel shadow-4 p-fluid blur">
            <form onSubmit={handleSubmit}>
                <h1>Login</h1>

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
                              placeholder="Password"/>
                </div>

                <div>
                    <Link to="#">Forgot password?</Link>
                </div>

                <br/>
                <br/>

                <div className="forget">
                    <label className="container-checkbox">
                        <input type="checkbox" checked={checked} onChange={changeCheckbox}/>
                        Remember Me
                    </label>

                </div>

                <div className="register">
                    <button type="submit" className="btn" disabled={!isValidForm}>
                        Sign In
                    </button>
                </div>

                <br/>
                <div>
                    <p>Don't have a account
                        <br/>
                        <Link to="/signup">Registration</Link>
                    </p>
                </div>
            </form>
        </div>
    );
}

export default SigninPage;
