import { useLocation, useNavigate } from "react-router-dom";
import Drawer from "../Drawer/Drawer";
import { useEffect, useState } from "react";

const ListSV = () => {
    const Navigate = useNavigate();
    const location = useLocation();
    const khoaHoc = location.state?.Course;
    const [sinhviens, setSinhViens] = useState([]);

    // lấy danh sách sinh viên
    useEffect(() => {
        const fetchsinhviens = async () => {
            try {
                const response = await fetch(`https://localhost:7111/api/SinhVienKhoaHoc/laydanhsachsinhvientheoKhoa/${khoaHoc.IdKhoaHoc}`);
                setSinhViens(await response.json());
            } catch (error) {
                console.error('Error fetching data:', error);
            }
        }
        fetchsinhviens();
    }, []);


    return (
        <div className="container-cont">
            <Drawer />
            <div className="cont">
                <h1 className='d-flex justify-content-center align-items-center'>Danh Sách sinh viên</h1>
                <div className='d-flex justify-content-center align-items-center'>
                    <hr style={{ width: "95%" }} />
                </div>
                <div class="container">
                    <h1>{khoaHoc.TenKhoaHoc}</h1>
                    <table class="table table-striped">
                        <thead>
                            <tr>
                                <th>ID</th>
                                <th>Username</th>
                                <th>First Name</th>
                                <th>Last Name</th>
                                <th>Email</th>
                                <th>Address</th>
                                <th>Role</th>
                            </tr>
                        </thead>
                        <tbody>
                            {sinhviens.map((sv) => (
                                <tr key={sv.IdUser}>
                                    <td>{sv.IdUser}</td>
                                    <td>{sv.Username}</td>
                                    <td>{sv.FirstName}</td>
                                    <td>{sv.LastName}</td>
                                    <td>{sv.Email}</td>
                                    <td>{sv.Address}</td>
                                    <td>{sv.Role}</td>
                                </tr>
                            ))}
                        </tbody>
                    </table>
                </div>

            </div>
        </div>
    )
};

export default ListSV