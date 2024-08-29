import { useContext, useEffect } from "react";
import { UserContext } from "../../Navigation";
import { useNavigate } from "react-router-dom";

const Logout = () => {
    const [, dispatch] = useContext(UserContext);
    const navigate = useNavigate();// dùng để chuyển trang

    useEffect(() => {
        dispatch({ "type": "logout" });
        // Xóa tất cả các biến trong sessionStorage
        sessionStorage.clear();
        navigate("/login");
    })

    return (
        <>
        </>
    )
};

export default Logout;