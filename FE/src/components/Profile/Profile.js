import { useContext, useState } from 'react';
import Drawer from '../Drawer/Drawer';
import '../Style.css'
import './Profile.css'
import { UserContext } from '../../Navigation';
const Profile = () => {
    const [user, dispatch] = useContext(UserContext);
    const [firstname, setFirstName] = useState(user.userInfo.FirstName);
    const [lastname, setLasttName] = useState(user.userInfo.LastName);
    const [email, setEmail] = useState(user.userInfo.Email);
    const [username, setUserName] = useState(user.userInfo.Username);
    const [avatar, setAvatar] = useState(user.userInfo.Avatar);
    const [address, setAddress] = useState(user.userInfo.Address);
    const [role, setRole] = useState(user.userInfo.Role);
    const [readOnly, setReadOnly] = useState(false);
    const [showpass, setShowPass] = useState(false);
    const [password, setPassword] = useState('');


    // chinh sửa thông tin
    const toggleReadOnly = () => {
        setReadOnly(!readOnly);
    };

    const cancel = () => {
        setReadOnly(false);
        setuser();
    };

    const setuser = () => {
        setFirstName(user.userInfo.FirstName);
        setLasttName(user.userInfo.LastName);
        setEmail(user.userInfo.Email);
        setUserName(user.userInfo.Username);
        setAvatar(user.userInfo.Avatar);
        setAddress(user.userInfo.Address);
    };

    const update = () => {
        setReadOnly(false);
    };

    const ChangeFN = (e) => {
        if (readOnly) {
            setFirstName(e.target.value)

        }
    };

    const ChangeLN = (e) => {
        if (readOnly) {
            setLasttName(e.target.value)
        }
    };

    const ChangeUN = (e) => {
        if (readOnly) {
            setUserName(e.target.value)
        }
    };

    const ChangeE = (e) => {
        if (readOnly) {
            setEmail(e.target.value)
        }
    };

    const ChangeA = (e) => {
        if (readOnly) {
            setAddress(e.target.value)
        }
    };

    const changePass = () => {
        if (showpass)
            setShowPass(false);
        else
            setShowPass(true);
    }
    // cap nhật pass
    const fetchUserpass = async () => {
        try {
            const response = await fetch(`https://localhost:7111/api/User/changPassword?idUser=${user.userInfo.IdUser}`, {
                method: 'PUT',
                headers: {
                    'Authorization': `Bearer ${localStorage.getItem('token')}`,
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(password)
            });

            if (response.ok) {
                alert("cập nhật thành công!");
                setShowPass(false);
                update();
            } else {
                console.error('Cập nhật mật khẩu không thành công');
                return null;
            }
        } catch (error) {
            console.error('Lỗi:', error);
            return null;
        }
    };
    // cập nhật user
    const updateUser = async () => {

        try {
            const response = await fetch(`https://localhost:7111/api/User/${user.userInfo.IdUser}`, {
                method: 'PUT',
                headers: {
                    'Authorization': `Bearer ${localStorage.getItem('token')}`,
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({
                    IdUser: user.userInfo.IdUser,
                    Username: "",
                    Password: "",
                    FirstName: firstname,
                    LastName: lastname,
                    Avatar: "",
                    Email: email,
                    Address: address,
                    Role: ""
                })
            });

            if (response.ok) {
                setReadOnly(false);
                alert("cập nhật thành công!");
                const userInfo = await response.json();
                dispatch({ "type": "login", "payload": { userInfo } });
                setuser();
            } else {
                console.error('Cập nhật thông tin không thành công');
                alert('Cập nhật thông tin không thành công');
                return null;
            }
        } catch (error) {
            console.error('Lỗi:', error);
            return null;
        }
    };



    return (
        <div className="container-cont">
            <Drawer />
            <div className="cont">
                <div className='d-flex justify-content-center align-items-center vh-100'>
                    <div className='view-profile d-flex justify-content-center align-items-center'>
                        <div className='img-profile' style={{ display: 'flex', flexDirection: "column" }}>
                            <img src={avatar} />
                            <br />
                            <button className='btn btn-info' onClick={toggleReadOnly}>Edit your profile</button>
                            <br></br>
                            <button className='btn btn-dark' onClick={changePass}>Đổi mật khẩu</button>
                        </div>
                        <div className='info_form'>
                            <h1>{username}</h1>
                            <p>"{role}"</p>
                            {!showpass ? (
                                <>
                                    <form>
                                        <div style={{ display: "flex" }}>
                                            <div class="mb-3">
                                                <label for="exampleFormControlInput1" class="form-label">First name</label>
                                                <input type="text" class="form-control" value={firstname} onChange={ChangeFN} />
                                            </div>
                                            <div class="mb-3">
                                                <label for="exampleFormControlInput1" class="form-label" >Last name</label>
                                                <input type="text" class="form-control" value={lastname} onChange={ChangeLN} />
                                            </div>
                                        </div>
                                        <div class="mb-3">
                                            <label for="exampleFormControlInput1" class="form-label">Email</label>
                                            <input type="email" class="form-control" value={email} onChange={ChangeE} />
                                        </div>

                                        <div class="mb-3">
                                            <label for="exampleFormControlInput1" class="form-label">Address</label>
                                            <input type="text" class="form-control" value={address} onChange={ChangeA} />
                                        </div>
                                    </form>
                                    <div className={readOnly ? "edit" : "unedit"}>
                                        <button className='btn btn-danger' onClick={cancel} style={{ width: "200px" }}>Hủy</button>
                                        <button className='btn btn-xanh' onClick={updateUser} style={{ width: "200px" }}>cập nhật</button>
                                    </div>
                                </>
                            ) : (
                                <>
                                    <div className='box-change-pass'>
                                        <div class="mb-3">
                                            <label for="exampleInputPassword1" class="form-label">Password</label>
                                            <input type="password" class="form-control" value={password} onChange={(p) => setPassword(p.target.value)} />
                                        </div>
                                        <div style={{ display: 'flex', justifyContent: 'space-between' }}>
                                            <button class="btn btn-secondary" style={{ width: "180px" }} onClick={fetchUserpass}>Cập nhật mật khẩu</button>
                                            <button class="btn btn-danger" style={{ width: "180px" }} onClick={changePass}>Hủy</button>
                                        </div>
                                    </div>
                                </>
                            )}
                        </div>
                    </div>
                </div>
            </div>
        </div >

    )
};

export default Profile