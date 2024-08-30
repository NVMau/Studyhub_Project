import { Link, Navigate, useLocation, useNavigate } from 'react-router-dom';
import Drawer from '../Drawer/Drawer';
import '../Style.css'
import './Cource.css'
import { useEffect, useState } from 'react';
const Diem = () => {
    const Navigate = useNavigate();
    const location = useLocation();
    const BaiTap = location.state?.baitap;
    const [baiLam, setBaiLam] = useState([]);
    const [cauhoi, setCauhoi] = useState([]);
    const [diem, setDiem] = useState();

    // hàm lấy các câu hỏi trong bài tập 
    const fetchQuestion = async () => {
        try {
            const response = await fetch(`https://localhost:7111/api/CauHoi/byBaiTap/${BaiTap.IdBaiTap}`);
            setCauhoi(await response.json());
        } catch (error) {
            alert(error);
            console.error('Error fetching data:', error);
        }
    }
    // lấy danh sách các bài làm 
    useEffect(() => {
        const fetchBaiLam = async () => {
            try {
                const response = await fetch(`https://localhost:7111/api/XyLyLamBai/layBaiLamByBaiTap/${BaiTap.IdBaiTap}`);
                setBaiLam(await response.json());
            } catch (error) {
                console.error('Error fetching data:', error);
            }
        }
        fetchBaiLam();
        fetchQuestion();
    }, [])

    // nhapaj điểm
    const nhapDiem = async (bailamsv) => {
        // console.log(bailamsv.IdBaiLam)
        const data = {
            IdSinhVien: bailamsv.IdSinhVien,
            IdBaiLam: bailamsv.IdBaiLam,
            Diem: diem
        };

        try {
            const response = await fetch('https://localhost:7111/api/XyLyLamBai/nhap-diem-tu-luan', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(data)
            });

            if (!response.ok) {
                alert(response.status);
                throw new Error('Network response was not ok');
            } else {
                alert("Nhập điểm thành công");
            }
        } catch (error) {
            console.error('Error:', error);
            throw error;
        }
    };

    // thay đổi giá trị điểm
    const handleDiemChange = (value, index) => {
        if (value >= 0 && value <= 10) {
            const updatedBaiLam = [...baiLam];
            updatedBaiLam[index].CotDiem.Diem = value;
            setBaiLam(updatedBaiLam);
            // Cập nhật giá trị diem
            setDiem(value);
        } else {
            // Nếu giá trị nhập vào không hợp lệ, bạn có thể xử lý theo mong muốn của mình
            console.log("Giá trị không hợp lệ. Vui lòng nhập giá trị từ 0 đến 10.");
        }
    };

    const test = () => {
        console.log(diem);
    };

    return (
        <div className="container-cont">
            <Drawer />
            <div className="cont">
                <h1 className='d-flex justify-content-center align-items-center'>{BaiTap.TenBaiTap}</h1>
                <div className='d-flex justify-content-center align-items-center'>
                    <hr style={{ width: "95%" }} />
                </div>
                <div className='d-flex justify-content-center align-items-center cont-monhoc'>
                    <div className='view-Diem'>
                        {cauhoi ? (
                            <>
                                {cauhoi.map((quiz, index) => (
                                    <h5 key={index}>Câu {index + 1}: {quiz.noiDungCauHoi}</h5>
                                ))}
                            </>
                        ) : (
                            <>
                                <h6>Khong có câu hỏi</h6>
                            </>
                        )}
                    </div>
                </div>
                <div className='d-flex justify-content-center align-items-center cont-monhoc'>

                    {baiLam ? (
                        <>
                            {baiLam.map((bl, index) => (
                                < div key={index} className='view-Diem' >
                                    <h6>MSSV:{bl.IdSinhVien} - {bl.HoTenSinhVien} {bl.CotDiem.Diem}</h6>
                                    <div dangerouslySetInnerHTML={{ __html: bl.NoiDungBaiLam }} />
                                    <div style={{ display: 'flex', width: "290px", justifyContent: 'space-between', height: "50px", alignItems: "center" }}>
                                        {BaiTap.IdLoaiBaiTap === 2 ? (
                                            <>
                                                <h6>Điểm:</h6>
                                                <input type='number'
                                                    value={bl.CotDiem.Diem}
                                                    onChange={(e) => handleDiemChange(e.target.value, index)}
                                                    min={0}
                                                    max={10}
                                                    style={{ width: "80px", height: "40px" }} />
                                                <button className="btn btn-primary" onClick={() => nhapDiem(bl)}>Cập nhật Điểm</button>
                                            </>
                                        ) : (
                                            <>
                                                <h6>Điểm: {bl.CotDiem.Diem}</h6>
                                            </>
                                        )}
                                    </div>
                                </div>
                            ))}
                        </>
                    ) : (
                        <>
                        </>
                    )}


                </div>
            </div>
        </div >
    )
};

export default Diem