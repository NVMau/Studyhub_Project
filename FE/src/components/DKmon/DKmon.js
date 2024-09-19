import { useContext, useEffect, useState } from "react";
import Drawer from "../Drawer/Drawer";
import '../Style.css'
import './DKmon.css'
import { UserContext } from "../../Navigation";
const DKmon = () => {
    const [user, dispatch] = useContext(UserContext);
    const [khoaHocs, setKhoaHocs] = useState([]);
    const [khoaHocDK, setKhoaHocDK] = useState([]);
    const [hocKy, setHocKy] = useState(JSON.parse(sessionStorage.getItem('hocky')));
    const [searchTerm, setSearchTerm] = useState('');

    const test = () => {
        console.log(user.userInfo.IdUser);
    };

    // fetch lấy danh khóa học trong hệ thống
    const fetchKhoaHoc = async () => {
        try {
            const response = await fetch('https://localhost:7111/api/KhoaHoc');
            setKhoaHocs(await response.json());
        } catch (error) {
            alert(error);
            console.error('Error fetching data:', error);
        }
    }
    // fetch lấy danh khoa hoc dã đăng ký
    const fetchKhoaHocDK = async () => {
        try {
            const response = await fetch(`https://localhost:7111/api/SinhVienKhoaHoc/${user.userInfo.IdUser}`);
            setKhoaHocDK(await response.json());
        } catch (error) {
            alert(error);
            console.error('Error fetching data:', error);
        }
    }

    useEffect(() => {
        fetchKhoaHoc();
        fetchKhoaHocDK();
    }, []);

    // hàm lọc khóa học
    const filteredCourses = khoaHocs.filter(course => {
        return course.TenKhoaHoc.toLowerCase().includes(searchTerm.toLowerCase());
    });

    // đăng ký môn
    const dangKy = async (idKhoaHoc) => {
        const data = {
            IdKhoaHoc: idKhoaHoc,
            IdSinhVien: user.userInfo.IdUser
        };

        try {
            const response = await fetch('https://localhost:7111/api/SinhVienKhoaHoc/dangkykhoahoc', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(data)
            });

            if (!response.ok) {
                if (response.status === 422) {
                    const errorMessage = await response.text();
                    alert(errorMessage);
                } else {
                    throw new Error('Network response was not ok');
                }
            } else {
                fetchKhoaHocDK();
                alert("Đăng ký thành công!");
                const responseData = await response.json();
            }
        } catch (error) {
            console.error('Error:', error);
            throw error;
        }
    }
    // hủy đănng ký
    const cancelDK = async (idSinhVienKhoaHoc) => {
        console.log(idSinhVienKhoaHoc);
        try {
            const response = await fetch(`https://localhost:7111/api/SinhVienKhoaHoc/huydangky/${idSinhVienKhoaHoc}`, {
                method: 'DELETE',
                headers: {
                    'Content-Type': 'application/json'
                }
            });

            if (response.status === 204) {
                console.log('Record deleted successfully');
                fetchKhoaHocDK();
                alert('Xóa thành công!');
            } else {
                throw new Error('Failed to delete record');
            }
        } catch (error) {
            console.error('Error:', error);
        }
    }

    return (
        <div className="container-cont">
            <Drawer />
            <div className="cont">
                <div className="d-flex justify-content-center align-items-center">
                    <h2>Đăng ký môn</h2>
                </div>
                <div className='d-flex justify-content-center align-items-center'>
                    <hr style={{ width: "95%" }} />
                </div>
                <div className="row" style={{ padding: '10px', height: "90px" }}>
                    <div className="col-md-7 list-mon-hoc col-dk-mon">
                        <div class="d-flex">
                            <input class="form-control me-2"
                                placeholder="Tìm môn học"
                                style={{ borderRadius: "0px" }}
                                value={searchTerm}
                                onChange={e => setSearchTerm(e.target.value)} />
                            <button class="btn btn-success" style={{ borderRadius: "0px", width: '90px' }} onClick={test}>Tìm</button>
                        </div>
                        <div style={{ paddingTop: "10px" }} className="view-list">
                            <table class="table table-striped" style={{ textAlign: "center" }}>
                                <thead>
                                    <tr>
                                        <th scope="col">Tên môn học</th>
                                        <th scope="col">Số tín chỉ</th>
                                        <th scope="col">Tên giáo viên</th>
                                        <th scope="col"></th>
                                    </tr>
                                </thead>
                                <tbody>
                                    {khoaHocs ? (
                                        <>
                                            {filteredCourses.map((kh) => (
                                                <tr>
                                                    <td>{kh.TenKhoaHoc}</td>
                                                    <td>{kh.SoTinChi}</td>
                                                    <td>{kh.TenGiangVien}</td>
                                                    <td><button type="button" class="btn btn-primary" onClick={() => dangKy(kh.IdKhoaHoc)}>Đăng ký</button></td>
                                                </tr>
                                            ))}
                                        </>
                                    ) : (

                                        <tr>
                                            <td>
                                                <div class="spinner-grow text-primary">
                                                    <span class="visually-hidden">Loading...</span>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="spinner-grow text-primary">
                                                    <span class="visually-hidden">Loading...</span>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="spinner-grow text-primary">
                                                    <span class="visually-hidden">Loading...</span>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="spinner-grow text-primary">
                                                    <span class="visually-hidden">Loading...</span>
                                                </div>
                                            </td>
                                        </tr>
                                    )}
                                </tbody>
                            </table>

                        </div>
                    </div>

                    <div className="col-md-3 list-da-dk col-dk-mon">
                        <h4 className="text-center">Danh sách đã đăng ký</h4>
                        <table class="table table-striped" style={{ textAlign: "center" }}>
                            <thead>
                                <tr>
                                    <th scope="col">Tên môn học</th>
                                    <th scope="col">Hủy</th>
                                </tr>
                            </thead>
                            <tbody>
                                {khoaHocDK ? (
                                    <>
                                        {khoaHocDK.map((kh) => (
                                            <tr>
                                                <td>{kh.TenKhoaHoc}</td>
                                                <td><button type="button" class="btn btn-danger" onClick={() => cancelDK(kh.IdSvKhoaHoc)}>hủy đăng ký</button></td>
                                            </tr>
                                        ))}
                                    </>
                                ) : (
                                    <>
                                        không có khóa học nào
                                    </>
                                )}
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div >
    )
};

export default DKmon