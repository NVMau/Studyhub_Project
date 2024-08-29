import { Link } from 'react-router-dom';
import './Auth.css';
import '../../App.css';
import { useCallback, useContext, useEffect, useState } from 'react';
import { UserContext } from '../../Navigation';

const Login = () => {
    const [, dispatch] = useContext(UserContext);
    const [username, setUsername] = useState("");
    const [password, setPassword] = useState("");
    const [show, setShow] = useState(false);
    const [token, setToken] = useState(localStorage.getItem('token') || '');

    // hàm fetch token
    const fetchToken = async () => {
        try {
            // Thực hiện yêu cầu API để đăng nhập và nhận token
            const response = await fetch('https://localhost:7111/api/Authentication/login', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({
                    Username: username,
                    Password: password,
                }),
            });

            if (response.ok) {
                const data = await response.json();
                // Lưu token vào localStorage
                localStorage.setItem('token', data.token);
                setToken(data.token);
            } else {
                // Xử lý lỗi nếu có
                console.error('Đăng nhập không thành công');
                setShow(true);
            }
        } catch (error) {
            console.error('Lỗi:', error);
        }
    };

    // hàm lấy thông tin user
    const fetchUserInfo = useCallback(async () => {
        try {
            const response = await fetch('https://localhost:7111/api/Authentication/userinfo', {
                method: 'GET',
                headers: {
                    'Authorization': `Bearer ${token}`, // Sử dụng token từ state
                    'Content-Type': 'application/json',
                },
            });

            if (response.ok) {
                const userInfo = await response.json();
                if (userInfo.Avatar == null) {
                    // Gán một đường dẫn ảnh bất kỳ vào userInfo.avatar
                    userInfo.Avatar = 'https://i.pinimg.com/564x/74/37/89/74378930719d0bd2663d30d695c57063.jpg';
                }
                // lưu user vào thiết bị
                dispatch({ "type": "login", "payload": { userInfo } });
            } else {
                console.error('Lấy thông tin người dùng không thành công');
                return null;
            }
        } catch (error) {
            console.error('Lỗi:', error);
            return null;
        }
    }, [dispatch, token]);


    useEffect(() => {
        if (token != null) {
            // gọi hàm lấy thông tin user
            fetchUserInfo();
        }

    }, [token, fetchUserInfo]);


    // ham đăng nhập
    const login = async () => {
        fetchToken();
    };

    // hàm nhập dữ liệu
    const changusername = (event) => {
        setUsername(event.target.value);
    };

    const changpassword = (event) => {
        setPassword(event.target.value);
    };

    const handleEnterPress = (event) => {
        if (event.key === 'Enter') {
            event.preventDefault();
            // Gọi hàm bạn muốn khi nhấn Enter ở đây
            login();
        }
    }

    const showalert = () => {
        setShow(false);

    };

    return (
        <div className='d-flex justify-content-center align-items-center vh-100 bg-img cot'>
            {show && (
                <div class="alert alert-danger" role="alert">
                    Mật khẩu hoặc tên đăng nhập không đúng!
                    <div className='btn btn-close' onClick={showalert}></div>
                </div>
            )}

            <div className='form-login'>
                <div style={{ textAlign: "center" }}>
                    <h1>Login</h1>
                    <p>Please enter your username and password!</p>
                </div>
                <form style={{ width: "100%" }}>
                    <label style={{ marginLeft: "10px" }}>Tên đăng nhập</label>
                    <input className='form-input' type='text' placeholder='Username' onChange={changusername} onKeyDown={handleEnterPress} />
                    <br />
                    <label style={{ marginLeft: "10px" }}>Mật khẩu</label>
                    <input className='form-input' type='password' placeholder='Password' onChange={changpassword} onKeyDown={handleEnterPress} />
                </form>

                <Link to={""} style={{ textDecoration: "none", color: "yellow" }}>Forgot password?</Link>
                <button type="button" class="btn btn-primary btn-login" onClick={login}>LOGIN</button>
                <h5>Chưa có tài khoản?<Link to={"/register"} style={{ color: "black" }}>Đăng ký</Link></h5>
            </div>
        </div>
    )
};

export default Login;