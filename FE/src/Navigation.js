import { BrowserRouter, Navigate, Route, Routes } from "react-router-dom";
import Home from "./components/Home/Home";
import Login from "./components/LoginRegister/Login";
import Register from "./components/LoginRegister/Register";
import Profile from "./components/Profile/Profile";
import Quiz from "./components/Quiz/Quiz";
import Scores from "./components/ViewScores/Scores";
import DKmon from "./components/DKmon/DKmon";
import Course from "./components/Course/Cource";
import Page404 from "./components/PageNotFoult/Page404";
import React, { createContext, useEffect, useReducer } from "react";
import UserReducer from "./components/UserInfor/UserReducer";
import Logout from "./components/LoginRegister/Logout";
import Essay from "./components/Quiz/Essay";
import CourseTC from "./components/Course/CourseTC";
import Diem from "./components/Course/Diem";
import ListSV from "./components/Course/ListSV";
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
                            <Route path="/" element={<Home />} />
                            <Route path="/login" element={<Navigate to={"/"} />} />
                            <Route path="/register" element={<Navigate to={"/"} />} />
                            <Route path="/profile" element={<Profile />} />
                            <Route path="/scores" element={<Scores />} />
                            <Route path="/dkmon" element={<DKmon />} />
                            <Route path="/course" element={<Course />} />
                            <Route path="/xemDiem" element={<Diem />} />
                            <Route path="/danhSachSinhVien" element={<ListSV />} />
                            <Route path="/courseGV" element={<CourseTC />} />
                            <Route path="/quiz" element={<Quiz />} />
                            <Route path="/essay" element={<Essay />} />
                            <Route path="/logout" element={<Logout />} />
                        </>) : (<>
                            <Route path="/login" element={<Login />} />
                            <Route path="/register" element={<Register />} />
                            <Route path="/" element={<Navigate to={"/login"} />} />
                        </>
                    )}
                    <Route path="*" element={<Page404 />} />
                </Routes>
            </UserContext.Provider>

        </BrowserRouter>
    );
};

export default Navigation