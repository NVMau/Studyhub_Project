import { useContext, useEffect, useState } from 'react';
import Drawer from '../Drawer/Drawer';
import '../Style.css'
import './Scores.css'
import html2canvas from 'html2canvas';
import jsPDF from 'jspdf';
import { UserContext } from '../../Navigation';
const Scores = () => {
    const hocKy = JSON.parse(sessionStorage.getItem('hocky'));
    const [semester, setSemester] = useState(hocKy[hocKy.length - 1]);
    const [user, dispatch] = useContext(UserContext);
    const [cotDiem, setCotDiem] = useState([]);

    // hàm thay đổi học ky
    const handleSemesterChange = (event) => {
        const selectedSemesterId = parseInt(event.target.value); // Lấy giá trị Id của học kỳ được chọn từ dropdown
        const selectedSemester = JSON.parse(sessionStorage.getItem('hocky')).find(hk => hk.IdHocKy === selectedSemesterId); // Tìm học kỳ tương ứng với Id đã chọn
        setSemester(selectedSemester); // Cập nhật state semester với học kỳ được chọn
    };

    const generatePDF = () => {
        const input = document.getElementById('bangpdf');
        html2canvas(input)
            .then((canvas) => {
                const imgData = canvas.toDataURL('image/png');
                const pdf = new jsPDF('landscape', 'mm', 'a4');

                // Lấy kích thước của trang PDF
                const pdfWidth = pdf.internal.pageSize.getWidth();
                const pdfHeight = pdf.internal.pageSize.getHeight();

                // Lấy kích thước của hình ảnh
                const imgWidth = canvas.width;
                const imgHeight = canvas.height;

                // Tính toán tỷ lệ để đảm bảo rằng hình ảnh không bị méo
                const ratio = Math.min(pdfWidth / imgWidth, pdfHeight / imgHeight);

                // Tính toán kích thước mới của hình ảnh dựa trên tỷ lệ
                const newImgWidth = imgWidth * ratio;
                const newImgHeight = imgHeight * ratio;

                // Tính toán vị trí để đặt hình ảnh vào trung tâm của trang PDF
                const x = (pdfWidth - newImgWidth) / 2;
                const y = (pdfHeight - newImgHeight) / 2;

                // Thêm hình ảnh vào PDF với kích thước và vị trí mới
                pdf.addImage(imgData, 'PNG', x, y, newImgWidth, newImgHeight);

                // Lưu tệp PDF
                pdf.save('bangdiem.pdf');
            });
    };

    // lấy danh sách điểm
    const getlistscore = async () => {

        try {
            const response = await fetch(`https://localhost:7111/api/XyLyLamBai/ketQuaHocTapTheoKy/${user.userInfo.IdUser}/${semester.IdHocKy}`);
            setCotDiem(await response.json());
            console.log(cotDiem);
        } catch (error) {
            alert(error);
            console.error('Error fetching data:', error);
        }
    }

    useEffect(() => {
        getlistscore();
    }, [semester]);

    const test = () => {
        console.log(cotDiem);
    }

    return (
        <div className="container-cont">
            <Drawer />
            <div className="cont" >
                <div className='head-score'>
                    <h2>Bảng điểm</h2>
                    <div>
                        {!semester ? (
                            <div class="spinner-grow spinner-grow-sm" role="status">
                                <span class="visually-hidden">Loading...</span>
                            </div>

                        ) : (
                            < select class="form-select" aria-label="Default select example" value={semester.IdHocKy} onChange={handleSemesterChange}>
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
                </div>

                {/* danh sách bảng điểm môn học */}
                <div className="list-mon-diem d-flex justify-content-center align-items-center" id='bangpdf'>
                    <hr className='hr-border' style={{ width: "80%" }} />
                    <div>
                        <h3>Sinh Viên: {user.userInfo.FirstName} {user.userInfo.LastName} - {user.userInfo.IdUser}</h3>
                    </div>
                    <div className='form-table-diems'>
                        {cotDiem.length == 0 ? (
                            <>
                                loadding
                            </>
                        ) : (
                            <>
                                {cotDiem.KetQuaKhoaHocs.map((cotdiem) => (
                                    <>
                                        <h2>{cotdiem.TenKhoaHoc}</h2>
                                        <table className='table table-bordered' style={{ textAlign: 'center' }}>
                                            <thead>
                                                <tr >
                                                    {cotdiem.Diems.map((ten) => (
                                                        <th>{ten.Tencotdiem}</th>
                                                    ))}
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    {cotdiem.Diems.map((d) => (
                                                        <td>{d.Diem}</td>
                                                    ))}
                                                </tr>
                                            </tbody>
                                        </table>
                                    </>
                                ))}
                            </>
                        )}

                    </div>
                </div>
                <div className='d-flex justify-content-center align-items-center'>
                    <button className='btn btn-danger' onClick={generatePDF}>Xuất bảng điểm</button>
                    <button className='btn btn-outline-success' onClick={test}>test</button>
                </div>
            </div>
        </div >
    )
};

export default Scores