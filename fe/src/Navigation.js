import { BrowserRouter, Navigate, Route, Routes } from "react-router-dom";
import React, { createContext, useEffect, useReducer } from "react";
import Login from "./components/Auth/Login";
import Register from "./components/Auth/Register";
import UserReducer from "./components/Configs/UserReducer";
import Logout from "./components/Auth/Logout";

// đường ống đẫn đến các components
export const UserContext = createContext();

const Navigation = () => {
    // biến dùng để chứa dữ liệu ng dùng
    const [user, dispatch] = useReducer(UserReducer);

    useEffect(() => {
        dispatch({ "type": "upstore" });
    }, [])

    return (
        <BrowserRouter>
            <UserContext.Provider value={[user, dispatch]}>
                <Routes>
                    {(user !== null && user !== undefined) ? (
                        <>
                            <Route path="/login" element={<Navigate to={"/"} />} />
                            <Route path="/register" element={<Navigate to={"/"} />} />
                            <Route path="/logout" element={<Logout />} />
                        </>) : (<>
                            <Route path="/login" element={<Login />} />
                            <Route path="/register" element={<Register />} />
                        </>
                    )}
                </Routes>
            </UserContext.Provider>

        </BrowserRouter>
    );
};

export default Navigation;