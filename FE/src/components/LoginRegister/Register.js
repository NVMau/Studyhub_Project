import { useContext, useState } from "react";
import { Link } from "react-router-dom";
import { UserContext } from "../../Navigation";
const Register = () => {
    const [username, setUsername] = useState();
    const [firstName, setFirstName] = useState('');
    const [lastName, setLastName] = useState('');
    const [password, setPassword] = useState('');
    const [email, setEmail] = useState('');
    const [user, dispatch] = useContext(UserContext);

    // hàm đăng ký
    const Register = async () => {
        const datauser = {
            Username: username,
            FirstName: firstName,
            LastName: lastName,
            Password: password,
            Email: email,
        };

        try {
            const response = await fetch('https://localhost:7111/api/Authentication/Register', { // Thay thế bằng URL API của bạn
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(datauser)
            });

            if (!response.ok) {
                const errorMessage = await response.text();
                throw new Error(`HTTP error! Status: ${response.status}, Message: ${errorMessage}`);
            } else {
                const userInfo = await response.json(); // Trả về kết quả dưới dạng JSON
                dispatch({ "type": "login", "payload": { userInfo } });
            }

        } catch (error) {
            console.error('Error creating user:', error);
        }

    }

    const DangKy = () => {
        Register();
    }

    return (
        <div className='d-flex justify-content-center align-items-center vh-100 bg-img' >
            <div className='form-login'>
                <div style={{ textAlign: "center" }}>
                    <h1>Register</h1>
                    <p>Vui lòng nhập đầy đủ thông tin dưới đây!</p>
                </div>

                <div style={{ width: "100%" }}>
                    <label style={{ marginLeft: "10px" }}>Tên đăng nhập</label>
                    <input className='form-input' type='text' value={username} onChange={(e) => setUsername(e.target.value)} placeholder='Username' />
                </div>

                <div style={{ width: "100%", display: "flex" }}>
                    <div>
                        <label style={{ marginLeft: "10px" }}>Họ</label>
                        <input className='form-input' type='text' value={firstName} onChange={(e) => setFirstName(e.target.value)} placeholder='Firstname' />
                    </div>
                    <div>
                        <label style={{ marginLeft: "10px" }}>Tên</label>
                        <input className='form-input' type='text' value={lastName} onChange={(e) => setLastName(e.target.value)} placeholder='Lastname' />
                    </div>
                </div>

                <div style={{ width: "100%" }}>
                    <label style={{ marginLeft: "10px" }}>Email</label>
                    <input className='form-input' type='email' value={email} onChange={(e) => setEmail(e.target.value)} placeholder='Email' />
                </div>

                <div style={{ width: "100%" }}>
                    <label style={{ marginLeft: "10px" }}>Mật khẩu</label>
                    <input className='form-input' type='password' value={password} onChange={(e) => setPassword(e.target.value)} placeholder='Password' />
                </div>

                <button type="button" class="btn btn-primary btn-login" onClick={DangKy}>Register</button>
                <h5>Đã có tài khoản?<Link to={"/login"} style={{ color: "black" }}>Đăng nhập</Link></h5>
            </div>
        </div>
    )
};

export default Register

