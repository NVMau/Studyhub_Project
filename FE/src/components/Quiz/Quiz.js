import { useContext, useEffect, useState } from 'react';
import { useLocation, useNavigate } from 'react-router-dom';
import './Quiz.css';
import { UserContext } from '../../Navigation';

const Quiz = () => {
    const [user, dispatch] = useContext(UserContext);
    const Navigate = useNavigate();
    const location = useLocation();
    const [listCauHoi, setListCauHoi] = useState();
    const baiTap = location.state?.baitap;
    const [indexCau, setIndexCau] = useState(0);
    const [chose, setChose] = useState(0);
    const [listDN, setListDN] = useState([]);
    const [minutes, setMinutes] = useState(baiTap.ThoiGian);
    const [seconds, setSeconds] = useState(0);
    const [result, setResult] = useState(location.state?.diemso || null);
    //ham tao mang chua cau tra loi
    useEffect(() => {
        if (listCauHoi != null) {
            const arr = Array.from({ length: listCauHoi.length }, () => 0);
            setListDN(arr);
        }
    }, [listCauHoi]);

    // lay danh sach cau hoi
    const danhSachCauHoi = async () => {
        try {
            const response = await fetch(`https://localhost:7111/api/CauHoi/byBaiTap/${baiTap.IdBaiTap}`);
            const cauHoiData = await response.json();
            setListCauHoi(shuffleArray([...cauHoiData]));
        } catch (error) {
            alert(error);
            console.error('Error fetching data:', error);
        }
    };

    useEffect(() => {
        if (result != null) {
            setMinutes(0);
            setSeconds(0);
        }
        danhSachCauHoi();
    }, []);

    // hàm xảo trộn câu hỏi
    const shuffleArray = (array) => {
        for (let i = array.length - 1; i > 0; i--) {
            const j = Math.floor(Math.random() * (i + 1));
            [array[i], array[j]] = [array[j], array[i]];
        }
        return array;
    };


    const setDapAn = (idDA) => {
        listDN[indexCau] = idDA;
        setChose(idDA);
    };

    // đồng hồ đếm ngược 
    useEffect(() => {
        const intervalId = setInterval(() => {
            if (seconds > 0) {
                setSeconds(seconds - 1);
            } else {
                if (minutes === 0) {
                    clearInterval(intervalId);
                    tinhDiem();
                    // Xử lý khi hết thời gian
                } else {
                    setMinutes(minutes - 1);
                    setSeconds(59);
                }
            }
        }, 1000);

        return () => clearInterval(intervalId);
    }, [minutes, seconds]);

    const Question = ({ ques }) => {
        return (
            <div className='item-quiz'>
                <h3>Câu: {indexCau + 1}</h3>
                <h4>{ques.noiDungCauHoi}</h4>
                {ques.listDapAn.map((dapAn) => (
                    <div className='quesions'>
                        <input type='radio' name={ques.idCauHoi}
                            checked={listDN[indexCau] === dapAn.IdDapAn}
                            onChange={() => setDapAn(dapAn.IdDapAn)}
                        />
                        <span>{dapAn.NoiDung}</span>
                    </div>
                ))}
            </div>
        );
    };

    const tang = () => {
        if (listCauHoi.length - 1 > indexCau) {
            setIndexCau(indexCau + 1)
            setChose(listDN[indexCau]);
        }
    };

    const giam = () => {
        if (0 < indexCau) {
            setChose(listDN[indexCau]);
            setIndexCau(indexCau - 1);
        }
    };

    const setSoCau = ({ index }) => {
        setChose(listDN[index]);
        setIndexCau(index);
    };

    const tinhDiem = async () => {
        const data = {
            IDSinhVien: user.userInfo.IdUser,
            IDBaiTap: baiTap.IdBaiTap,
            DapAn: listDN
        };

        try {
            const response = await fetch('https://localhost:7111/api/XyLyLamBai/tinhDiemTracNghiem', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(data),
            });

            if (!response.ok) {
                throw new Error('Request failed');

            } else {
                const responseData = await response.json(); // hứng dữ liệu từ server
                setResult(responseData);
            }
        } catch (error) {
            console.error('Error:', error);
        }
    }
    const backCourse = () => {
        Navigate('/course');
    }

    // lưu điểm và tính điểm
    // làm bài xong thì gọi hàm này
    const done = () => {
        // console.log();
        setMinutes(0);
        setSeconds(0);
        if (result === null)
            tinhDiem();
    };
    // lấy tất cả các cột điểm

    return (
        <div className='container-quiz'>

            <div className='nav-timer'>
                <div>
                    <h3>thời gian còn:</h3>
                    <h1 style={{ fontSize: '50px' }}>{`${minutes.toString().padStart(2, '0')}:${seconds.toString().padStart(2, '0')}`}</h1>
                    <button className='btn btn-green' style={{ width: '180px', height: '50px', fontSize: "22px" }} onClick={done}>làm xong</button>
                </div>
            </div>
        // khi chưa có kết quả thì hiện thị form làm bài
            {result ? (
                <>
                    <div className='quiz-item'>
                        <div style={{ display: "flex", flexDirection: "column", justifyContent: "center", alignContent: "center", alignItems: "center" }}>
                            <h3>Số điểm: {result.Diem}</h3>
                            <button className='btn btn-info' onClick={backCourse}>Quay về trang chủ</button>
                        </div>
                    </div>
                </>
            ) : (
                <>
                    {/* danh sach cau trac nghiem */}
                    <div className='quiz-item'>
                        {!listCauHoi ? (
                            <p>Loading...</p>
                        ) : (
                            <Question ques={listCauHoi[indexCau]} />
                        )}
                        <div className='d-flex justify-content-center align-items-center'>
                            <button type="button" className="btn btn-secondary next" onClick={giam}>Câu sau</button>
                            <button type="button" className="btn btn-secondary next" onClick={tang}>Câu tiếp</button>
                        </div>
                        <div className='content-list-cauhoi'>
                            {listDN.map((item, index) => (
                                <button key={index} type="button" className="btn btn-info itemCau" onClick={() => setSoCau({ index })}> câu : {index + 1}</button>
                            ))}
                        </div>
                    </div>
                </>
            )}

        </div >
    )
};

export default Quiz