import Drawer from "../Drawer/Drawer";
import '../Style.css'
import './Home.css'
import { IoSearch } from "react-icons/io5";
import { IoIosSettings } from "react-icons/io";
import { useNavigate } from 'react-router-dom';
import { useContext, useEffect, useState } from "react";
import { UserContext } from "../../Navigation";
const Home = () => {
    const [user, dispatch] = useContext(UserContext);
    const navigate = useNavigate();
    const [days, setDays] = useState([]);
    const [month, setMonth] = useState();
    const [year, setYear] = useState();
    const [day, setDay] = useState();
    const [hocKy, setHocKy] = useState(() => {
        const savedData = sessionStorage.getItem('hocky');
        return savedData ? JSON.parse(savedData) : null;
    });
    const [semester, setSemester] = useState();// dùng để chứa học kỳ hiện tại
    const [courses, setCourses] = useState(() => {
        const savedData = sessionStorage.getItem('khoahoc');
        return savedData ? JSON.parse(savedData) : null;
    });
    const test = () => {
        console.log(JSON.parse(sessionStorage.getItem('hocky')));
    }
    // gọi các list học kỳ
    const fetchHocKy = async () => {
        try {
            const response = await fetch('https://localhost:7111/api/HocKy');
            const HocKyData = await response.json();
            setHocKy(HocKyData);
            sessionStorage.setItem('hocky', JSON.stringify(HocKyData));
        } catch (error) {
            alert(error);
            console.error('Error fetching data:', error);
        }
    }
    useEffect(() => {
        if (hocKy != null)
            setSemester(hocKy[hocKy.length - 1]);
    }, [hocKy]);
    // fetch api lấy danh khóa học
    useEffect(() => {
        const fetchCourseST = async () => {
            try {
                const response = await fetch(`https://localhost:7111/api/KhoaHoc/${user.userInfo.IdUser}/${semester.IdHocKy}`);
                const dataKhoaHoc = await response.json();
                sessionStorage.setItem('khoahoc', JSON.stringify(dataKhoaHoc));
                setCourses(JSON.parse(sessionStorage.getItem('khoahoc')));
            } catch (error) {
                alert(error);
                console.error('Error fetching data:', error);
            }
        }

        const fetchCourseTC = async () => {
            try {
                const response = await fetch(`https://localhost:7111/api/KhoaHoc/khoaHocByGVHK/${user.userInfo.IdUser}/${semester.IdHocKy}`);
                const dataKhoaHoc = await response.json();
                sessionStorage.setItem('khoahoc', JSON.stringify(dataKhoaHoc));
                setCourses(JSON.parse(sessionStorage.getItem('khoahoc')));
            } catch (error) {
                alert(error);
                console.error('Error fetching data:', error);
            }
        }

        if (semester != null) {
            if (user.userInfo.Role === 'Student')
                fetchCourseST();
            else
                fetchCourseTC();
        }
    }, [semester]);
    // hàm thay đổi học ky
    const handleSemesterChange = (event) => {
        const selectedSemesterId = parseInt(event.target.value); // Lấy giá trị Id của học kỳ được chọn từ dropdown
        const selectedSemester = hocKy.find(hk => hk.IdHocKy === selectedSemesterId); // Tìm học kỳ tương ứng với Id đã chọn
        setSemester(selectedSemester); // Cập nhật state semester với học kỳ được chọn
    };
    // hàm tạo mảng ngày
    useEffect(() => {
        fetchHocKy();

        // hàm lấy ngày tháng hiện tại
        var d = new Date();
        setDay(d.getDate());
        setMonth(d.getMonth() + 1);
        // setMonth();
        setYear(d.getFullYear());
        // console.log(day, month, year);
        // hàm lấy thứ index ngày 1 của tháng 
        var dayofweek = new Date(year, month - 1, 1).getDay();
        var cacThu = [6, 0, 1, 2, 3, 4, 5];
        var index = cacThu[dayofweek];
        var ngays = [];
        // gán ngày rổng
        for (var i = 0; i < index; i++) {
            ngays.push("");
        }
        // gán các ngày
        for (var i = 1; i <= laySoLuongNgay(); i++) {
            ngays.push(i);
        }
        //gàn cách ngày còn thiếu
        for (var i = 1; ngays.length < 35; i++) {
            ngays.push("");
        }
        setDays(ngays);

    }, [month]);
    // hàm lấy số lượng ngày của tháng
    function laySoLuongNgay() {
        return new Date(year, month, 0).getDate();
    }
    const courseDetail = (course) => {// data là id của cource
        if (user.userInfo.Role === 'Student')
            navigate('/course', { state: { course } });
        else
            navigate('/courseGV', { state: { course } });
    }
    // tạo lịch
    function Days() {
        return (
            <>
                {days.reduce((rows, item, index) => {
                    if (index % 7 === 0) rows.push([]);
                    const tdStyle = item === day ? { backgroundColor: '#00daff' } : {}; // Kiểm tra nếu item bằng giá trị của day thì gán style
                    rows[rows.length - 1].push(<td key={index} style={tdStyle}>{item}</td>);
                    return rows;
                }, []).map((row, index) => <tr key={index}>{row}</tr>)}
            </>
        );
    }
    return (
        <div className="container-cont">
            <Drawer />
            <div className="cont">
                {/* // tìm kiếm môn học */}
                <div className="head d-flex  align-items-center">
                    {/* <h2>Xin chào {user.username}</h2> */}
                    <form class="card card-sm">
                        <div class="row">
                            <div class="col">
                                <input class="form-control form-control-lg form-control-borderless" style={{ width: "500px" }} type="search" placeholder="Tìm kiếm khóa học..." />
                            </div>
                            <div class="col-auto">
                                <button class="btn btn-lg btn-info" type="submit" style={{ fontWeight: "400" }}>Tìm kiếm</button>
                            </div>
                        </div>
                    </form>

                    {/* hiển thị droplist */}
                    <div>
                        {!semester ? (
                            <div class="spinner-grow spinner-grow-sm" role="status">
                                <span class="visually-hidden">Loading...</span>
                            </div>

                        ) : (
                            <select class="form-select" aria-label="Default select example" value={semester.IdHocKy} onChange={handleSemesterChange}>
                                <option value="" selected disabled>
                                    {semester.NamHocKy}
                                </option>
                                {hocKy.map((hk) => (
                                    <option key={hk.IdHocKy} value={hk.IdHocKy}>
                                        {hk.NamHocKy}
                                    </option>
                                ))}
                            </select>
                        )}
                    </div>

                    <div>
                        <button className="btn btn-secondary" onClick={test}>Cài đặt <IoIosSettings /></button>
                    </div>
                </div>
                {/* hiện thị môn học và lịch */}
                <div className="course-calender">
                    <div className="mid">

                        {/* chia giáo viên và sinh viên */}
                        {user.userInfo.Role !== "Student" ? (
                            <>
                                <h2 className="text-center">Các lớp đang dạy</h2>
                                <div class="couses">
                                    {!courses ? (
                                        <div class="text-center">
                                            <div class="spinner-border" role="status">
                                                <span class="visually-hidden">Loading...</span>
                                            </div>
                                        </div>
                                    ) : (
                                        <>
                                            {courses.length === 0 ? (
                                                <p>Không có lớp nào!</p>
                                            ) : (
                                                <>
                                                    {JSON.parse(sessionStorage.getItem('khoahoc')).map((kh) => (
                                                        <div class="teacher btn" onClick={() => courseDetail(kh)}>
                                                            <img src="https://i.pinimg.com/564x/c9/91/8c/c9918c931842e016253fe8dd268bf35a.jpg" className="img-course" />
                                                            <div className="info-couse">
                                                                <h7>Mã Khóa học {kh.IdKhoaHoc}</h7>
                                                                <h2>{kh.TenKhoaHoc}</h2>
                                                                <h4>Tên Môn: {kh.TenMonHoc}</h4>
                                                                <p>Lớp có {kh.SoLuongSinhVien} học sinh</p>
                                                            </div>
                                                        </div>
                                                    ))}
                                                </>
                                            )}
                                        </>
                                    )}
                                </div>
                                {/* hiển thị các khóa học đang dạy
                                trang khóa học
                                trang tạo bài tập
                                trang nhập điểm sinh viên */}
                            </>
                        ) : (
                            <>
                                <h2 className="text-center">Các khóa học</h2>
                                <div class="couses">
                                    {!courses ? (
                                        <div class="text-center">
                                            <div class="spinner-border" role="status">
                                                <span class="visually-hidden">Loading...</span>
                                            </div>
                                        </div>
                                    ) : (
                                        <>
                                            {courses.length === 0 ? (
                                                <p>Không có khóa học nào được đăng ký!</p>
                                            ) : (
                                                <>
                                                    {JSON.parse(sessionStorage.getItem('khoahoc')).map((kh) => (
                                                        <div class="couse btn" onClick={() => courseDetail(kh)}>
                                                            <img src="https://i.pinimg.com/564x/56/65/37/566537bb231263dd1f26f6267ed722b7.jpg" className="img-course" />
                                                            <div className="info-couse">
                                                                <h7>Mã Khóa học {kh.IdKhoaHoc}</h7>
                                                                <h2>{kh.TenKhoaHoc}</h2>
                                                                <h4>Tên Môn: {kh.TenMonHoc} </h4>
                                                                <h6>Tên Giáo viên: {kh.TenGiangVien}</h6>
                                                                <p>Lớp có {kh.SoLuongSinhVien} học sinh - Số tín chỉ: {kh.SoTinChi}</p>
                                                            </div>
                                                        </div>
                                                    ))}
                                                </>
                                            )}
                                        </>
                                    )}
                                </div>
                            </>
                        )};

                    </div>
                    <div className="right-side d-flex justify-content-center ">
                        <div style={{}}>
                            <h4 style={{ width: "300px", backgroundColor: "white", textAlign: "center", padding: "13px", borderRadius: "10px", marginTop: "5px" }}>Thông báo</h4>
                            <div style={{ width: "300px", height: "280px", backgroundColor: "white", textAlign: "center", borderRadius: "10px" }}>
                                Không có thông báo nào!
                            </div>
                            {/* lịch */}
                            <div style={{ width: "300px", backgroundColor: "white", borderRadius: "10px", marginTop: "5px", height: "216px" }}>
                                <table className="table text-center">
                                    <thead>
                                        <tr>
                                            <th>ha</th>
                                            <th>ba</th>
                                            <th>bn</th>
                                            <th>nm</th>
                                            <th>su</th>
                                            <th>by</th>
                                            <th>cn</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <Days />
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            
            <df-messenger
                    chat-icon="https://res.cloudinary.com/daimrgwuu/image/upload/v1719025996/icon_ai_vj99jf.png"
                    intent="WELCOME"
                    chat-title="AI_STUDY_HUB"
                    agent-id="c599d7c1-b4b2-4f55-8467-a8f1e36ad465"
                    language-code="vi"
                ></df-messenger>


        </div>
    )
};

export default Home