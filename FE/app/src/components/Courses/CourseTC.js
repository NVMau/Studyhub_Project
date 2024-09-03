
import { Link, Navigate, json, useLocation, useNavigate } from 'react-router-dom';
import Drawer from '../Drawer/Drawer';
import '../Style.css'
import './Cource.css'
import { useEffect, useState } from 'react';
const CourseTC = () => {
    const Navigate = useNavigate();
    const location = useLocation();
    const courseFromLocationState = location.state?.course;
    // Kiểm tra xem location.state?.course có giá trị không
    if (courseFromLocationState) {
        const courseString = JSON.stringify(courseFromLocationState);
        sessionStorage.setItem('course', courseString);
    }
    // Lấy giá trị từ sessionStorage
    const storedCourseString = sessionStorage.getItem('course');
    // Chuyển đổi chuỗi JSON thành đối tượng
    const storedCourse = storedCourseString ? JSON.parse(storedCourseString) : null;
    // Sử dụng giá trị từ location.state?.course nếu có, nếu không sử dụng giá trị từ sessionStorage
    // const Course = courseFromLocationState || storedCourse;
    const [Course, setCourse] = useState(courseFromLocationState || storedCourse);
    const [dataBaiTap, setDataBaiTap] = useState([]);
    const [datacauhoi, setDatacauhoi] = useState([]);
    const [contentBT, setContentBT] = useState('');
    const [selectedId, setSelectedId] = useState(0);
    const [thoigian, setThoiGian] = useState(1);
    const [selectedIdCreate, setSelectedIdCreate] = useState('1');
    //
    const [quesSelected, setQuesSelected] = useState([]);
    const [idListquizs, setiIdListquizs] = useState([]);

    const [selectedIDBT, setSelectedIDBT] = useState(1);// chọn loại bài tập

    // Hàm xử lý khi option được chọn
    const handleOptionChange = (event) => {
        setSelectedId(event.target.value); // Cập nhật giá trị của state thành ID của câu hỏi được chọn
    };

    const fetchBaiTap = async () => {
        try {
            const response = await fetch(`https://localhost:7111/api/BaiTap/byKhoaHoc/${Course.IdKhoaHoc}`);
            setDataBaiTap(await response.json());
        } catch (error) {
            console.error('Error fetching data:', error);
        }
    };

    useEffect(() => {
        fetchBaiTap();
        fetchTypeBaiTap();
        fetchTypeCauHoi();
    }, []);

    // lấy danh sách các loại câu hỏi và loại bài tập
    const fetchTypeBaiTap = async () => {
        try {
            const response = await fetch('https://localhost:7111/api/LoaiBaiTap');
            const data = await response.json();
            sessionStorage.setItem('typeBT', JSON.stringify(data));
        } catch (error) {
            console.error('Error fetching data:', error);
        }
    };

    const fetchTypeCauHoi = async () => {
        try {
            const response = await fetch('https://localhost:7111/api/LoaiCauHoi');
            const data = await response.json();
            sessionStorage.setItem('typeCauHoi', JSON.stringify(data));
        } catch (error) {
            console.error('Error fetching data:', error);
        }
    };
    const fetchListCauHoi = async () => {
        try {
            const response = await fetch(`https://localhost:7111/api/CauHoi/GetListCauHoiByMonAndByType/${Course.IdMonHoc}/${selectedId}`);
            setDatacauhoi(await response.json());
        } catch (error) {
            console.error('Error fetching data:', error);
        }
    };

    useEffect(() => {

        fetchListCauHoi();
    }, [selectedId]);

    const typeDeadline = (idType) => {
        switch (idType) {
            case 1:
                return "Phút";
            case 2:
                return "Ngày";
            default:
                return "không tìm thấy kiểu bài tập";
        }
    };


    // xóa bài tập
    const deleteBaiTap = async (idBaiTap) => {
        try {
            const response = await fetch(`https://localhost:7111/api/BaiTap/${idBaiTap}`, {
                method: 'DELETE',
                headers: {
                    'Content-Type': 'application/json'
                }
            });

            if (!response.ok) {
                throw new Error('Xóa bài tập không thành công'); // Xử lý trường hợp không thành công
            } else {
                fetchBaiTap();
                alert("xóa thành công!")
            }

            console.log('Bài tập đã được xóa thành công');
        } catch (error) {
            console.error('Lỗi khi xóa bài tập:', error);
        }
    };

    const postData = async () => {
        if (idListquizs.length === 0 || contentBT === '') {
            alert("vui điền thông tin đầy đủ")
        } else {
            const data = {
                "IdKhoaHoc": Course.IdKhoaHoc,
                "TenBaiTap": contentBT,
                "IdLoaiBaiTap": selectedIDBT,
                "DanhSachCauHoi": idListquizs,
                "ThoiGian": thoigian
            };
            try {
                const response = await fetch('https://localhost:7111/api/BaiTap/TaoListTracNghiem', {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify(data)
                });

                if (!response.ok) {
                    throw new Error('Network response was not ok');
                } else
                    fetchBaiTap();

                return await response.json();
            } catch (error) {
                console.error('Error:', error);
                throw error; // Ném lỗi để xử lý ở phần gọi hàm này
            }
        }
    };

    const test = () => {
        postData();
    };

    const addQuesToList = (cauhoi) => {
        // Kiểm tra xem câu hỏi đã tồn tại trong mảng chưa
        const isExist = quesSelected.some(item => item.IdCauHoi === cauhoi.IdCauHoi); // Giả sử IdCauHoi là thuộc tính để xác định tính duy nhất của mỗi câu hỏi

        // Nếu câu hỏi chưa tồn tại trong mảng, thêm vào
        if (!isExist) {
            setQuesSelected(prevState => [...prevState, cauhoi]);
            setiIdListquizs(prevState => [...prevState, cauhoi.IdCauHoi])
        }
    };

    const removeToList = (cauhoi) => {
        // Sử dụng phương thức filter() để lọc ra các đối tượng khác với đối tượng cần loại bỏ
        const newList = quesSelected.filter(item => item.IdCauHoi !== cauhoi.IdCauHoi); // Giả sử IdCauHoi là thuộc tính để xác định tính duy nhất của mỗi câu hỏi

        // Cập nhật mảng quesSelected với danh sách mới đã loại bỏ đối tượng
        setQuesSelected(newList);

        // Loại bỏ phần tử khỏi mảng listQuiz
        const newListQuizs = idListquizs.filter(id => id !== cauhoi.IdCauHoi);

        // Cập nhật mảng listQuiz với danh sách mới đã loại bỏ phần tử
        setiIdListquizs(newListQuizs);
    };

    // buton tạo câu hỏi
    const [question, setQuestion] = useState('');
    const [slDapAn, setSlDapAn] = useState(0);
    const [answers, setAnswers] = useState(Array.from({ length: slDapAn }, () => ''));
    const [selectedAnswer, setSelectedAnswer] = useState(Array.from({ length: slDapAn }, () => false)); // Thêm dòng này

    const handleAnswerChange = (index, value) => {
        const newAnswers = [...answers];
        newAnswers[index] = value;
        setAnswers(newAnswers);
    };

    const handleRadioChange = (index) => {
        const newSelectedAnswer = selectedAnswer.map((item, i) => i === index);
        setSelectedAnswer(newSelectedAnswer);
    };

    const addDapAn = () => {
        setSlDapAn(prevSlDapAn => prevSlDapAn + 1);
        setAnswers(prevAnswers => [...prevAnswers, '']);
        setSelectedAnswer(prevSelectedAnswer => [...prevSelectedAnswer, false]);
    };
    // chám điểm
    const ChamDiemSv = (baitap) => {
        Navigate('/xemDiem', { state: { baitap } });
    }

    const createQuesion = async () => {
        if (question === '') {
            alert("vui điền thông tin đầy đủ")
        } else {
            try {
                if (selectedIdCreate === 1) {
                    const data = {
                        "NoiDung": question,
                        "IdMonHoc": Course.IdMonHoc,
                        "IdLoaiCauHoi": selectedIdCreate,
                    };
                    const response = await fetch('https://localhost:7111/api/CauHoi', {
                        method: 'POST',
                        headers: {
                            'Content-Type': 'application/json'
                        },
                        body: JSON.stringify(data)
                    });
                    if (!response.ok) {
                        throw new Error('Network response was not ok');
                    } else {
                        fetchListCauHoi();
                        alert("tao thanh cong");
                    }
                    return await response.json();
                } else {
                    const data = {
                        "NoiDungCauHoi": question,
                        "IdMonHoc": Course.IdMonHoc,
                        "IdLoaiCauHoi": selectedIdCreate,
                        "NoiDungDapAn": answers,
                        "KetQua": selectedAnswer
                    };

                    const response = await fetch('https://localhost:7111/api/CauHoi/createCauHoiAndDapAn', {
                        method: 'POST',
                        headers: {
                            'Content-Type': 'application/json'
                        },
                        body: JSON.stringify(data)
                    });
                    if (!response.ok) {
                        throw new Error('Network response was not ok');
                    } else {
                        fetchListCauHoi();
                        alert("tao thanh cong");
                    }
                    return await response.json();
                }

            } catch (error) {
                console.error('Error:', error);
                throw error; // Ném lỗi để xử lý ở phần gọi hàm này
            }
        }
    };

    const listSV = () => {
        Navigate('/danhSachSinhVien', { state: { Course } });
    }

    return (
        <div className="container-cont">
            <Drawer />
            <div className="cont">
                {/* hiển thị tên môn học */}
                <div style={{ padding: "10px", display: "flex", alignItems: 'center', justifyContent: "space-between" }}>
                    <div>
                        <h3>GV: {Course.TenGiangVien}</h3>
                        <h5>mã: {Course.IdKhoaHoc} - {Course.TenMonHoc}</h5>
                    </div>
                    <h1 className='limited-text'>{Course.TenKhoaHoc} </h1>
                    <button className='btn btn-secondary' onClick={listSV}>xem danh sách sinh viên</button>
                </div>
                <div className='d-flex justify-content-center align-items-center'>
                    <hr style={{ width: "95%" }} />
                </div>
                <div className='d-flex justify-content-center align-items-center cont-monhoc'>
                    <div className='view-baihoc'>

                        {dataBaiTap.length === 0 ? (
                            <p>Chưa có bài tập!</p>
                        ) : (
                            <>
                                {dataBaiTap.map((bt) => (
                                    <>
                                        <div className='btap-cont'>
                                            <div style={{ paddingTop: "10px" }}>
                                                <h4>{bt.TenBaiTap}</h4>
                                                {sessionStorage.getItem('typeBT') && JSON.parse(sessionStorage.getItem('typeBT'))[bt.IdLoaiBaiTap - 1] && (
                                                    <p>{JSON.parse(sessionStorage.getItem('typeBT'))[bt.IdLoaiBaiTap - 1].TenBaiTap}</p>
                                                )}
                                                <p>thời gian: {bt.ThoiGian} {typeDeadline(bt.IdLoaiBaiTap)}</p>
                                                <button style={{ marginRight: "20px " }} className="btn btn-danger" onClick={() => deleteBaiTap(bt.IdBaiTap)}>Xóa Bài tập</button>
                                                <button className="btn btn-warning">Chỉnh sửa bài tập</button>
                                            </div>
                                            <button style={{ width: "100px", height: "100%" }} class="btn btn-info" onClick={() => ChamDiemSv(bt)}>Xem và nhập điểm</button>
                                        </div>
                                        <hr></hr>
                                    </>
                                ))}
                            </>
                        )}
                    </div>
                </div>
                <div className='d-flex justify-content-center align-items-center'>
                    <hr style={{ width: "95%" }} />
                </div>
                <div className='d-flex justify-content-center align-items-center cont-monhoc'>
                    tạo các bài tập
                    <div className='view-dien-dan form-create-exam'>
                        Chọn loại bài tập
                        <div className='create-quest'>
                            {JSON.parse(sessionStorage.getItem('typeBT')) !== null && (
                                // Hiển thị dữ liệu
                                <>
                                    <select
                                        className="form-select"
                                        aria-label="Default select example"
                                        style={{ width: "200px" }}
                                        value={selectedIDBT}
                                        onChange={(e) => setSelectedIDBT(e.target.value)} // Gọi hàm xử lý sự kiện khi giá trị được chọn thay đổi
                                    >
                                        {JSON.parse(sessionStorage.getItem('typeBT')).map((loaiBT) => (
                                            <option key={loaiBT.IdLoaiBaiTap} value={loaiBT.IdLoaiBaiTap}>
                                                {loaiBT.TenBaiTap}
                                            </option>
                                        ))}
                                    </select>
                                </>
                            )}
                            {/* lấy loại câu hỏi */}
                            {JSON.parse(sessionStorage.getItem('typeCauHoi')) !== null && (
                                // Hiển thị dữ liệu khi 
                                <>
                                    <select class="form-select"
                                        aria-label="Default select example"
                                        style={{ width: "200px" }}
                                        value={selectedId} // Đặt giá trị của select là giá trị của state selectedId
                                        onChange={handleOptionChange} // Gọi hàm xử lý khi option được chọn
                                    >
                                        {JSON.parse(sessionStorage.getItem('typeCauHoi')).map((loaiCH) => (
                                            <>
                                                <option value={loaiCH.IdLoaiCauHoi} >{loaiCH.TenLoaiCauHoi}</option>
                                            </>
                                        ))};
                                    </select>
                                </>
                            )}
                            <input type="text"
                                class="form-control form-control-lg"
                                placeholder="Nhập tiêu đề cho bài tập"
                                onChange={(e) => setContentBT(e.target.value)}
                                value={contentBT} />
                            <button style={{ width: "100px" }} class="btn btn-success" onClick={test}>Create</button>
                        </div>

                        <div className="container" style={{ marginTop: "20px" }}>
                            <div className="row">
                                <div className="col-md-6">
                                    <h5>các câu hỏi đã chọn</h5>
                                    <div style={{ height: '200px' }} className='box-quest-list'>
                                        {quesSelected.map((qs) => (
                                            <div className='quest-selected'>
                                                <h6>{qs.IdCauHoi} - {qs.NoiDung}</h6>
                                                <button style={{ width: "70px" }} class="btn btn-danger" onClick={() => removeToList(qs)}>Bỏ</button>
                                            </div>
                                        ))}
                                    </div>
                                </div>
                                <div className="col-md-6">
                                    <h5>danh sách câu hỏi</h5>
                                    <div style={{ height: '200px' }} className='box-quest-list'>
                                        {datacauhoi !== null && (
                                            <>
                                                {datacauhoi.map((cauhoi) => (
                                                    <div key={cauhoi.IdCauHoi} className='quest'>
                                                        <h6>{cauhoi.IdCauHoi} - {cauhoi.NoiDung}</h6>
                                                        <button style={{ width: "70px" }} className="btn btn-dark" onClick={() => addQuesToList(cauhoi)}>Thêm</button>
                                                    </div>
                                                ))}
                                            </>
                                        )}
                                    </div>
                                </div>
                            </div>
                            {/* {/* chon time? /} */}
                            <p>nhập thời gian làm bài(trắc nghiệm sẽ là phút, tự luận sẽ theo ngày!)</p>
                            <input
                                type="number"
                                value={thoigian}
                                onChange={(e) => setThoiGian(e.target.value)}
                                min={1}
                            />
                        </div>
                    </div>
                    Tạo câu hỏi
                    <div className='view-dien-dan' style={{ marginBottom: "20px" }}>
                        <div className='create-quest'>
                            {JSON.parse(sessionStorage.getItem('typeCauHoi')) !== null && (
                                <>
                                    <select className="form-select"
                                        aria-label="Default select example"
                                        style={{ width: "200px", height: "50px" }}
                                        value={selectedIdCreate}
                                        onChange={(e) => setSelectedIdCreate(e.target.value)}
                                    >
                                        {JSON.parse(sessionStorage.getItem('typeCauHoi')).map((loaiCH) => (
                                            <option key={loaiCH.IdLoaiCauHoi} value={loaiCH.IdLoaiCauHoi}>{loaiCH.TenLoaiCauHoi}</option>
                                        ))}
                                    </select>
                                </>
                            )}
                            <div className="mb-3" style={{ width: "100%" }}>
                                <textarea
                                    style={{ width: "100%", minHeight: "50px" }}
                                    className="form-control"
                                    id="questionInput"
                                    value={question}
                                    placeholder='Nhập nội dung câu hỏi'
                                    onChange={(e) => setQuestion(e.target.value)}
                                />
                            </div>
                            <button style={{ width: "100px", height: "50px" }} className="btn btn-success" onClick={createQuesion}>Create</button>
                        </div>

                        {selectedIdCreate !== '1' && ( // Kiểm tra selectedIdCreate khác '1'
                            <div className="container">
                                <div className="row">
                                    <div className="col-md-6 offset-md-3">
                                        <form>
                                            <div className="mb-3">
                                                <label className="form-label">Đáp Án:</label>
                                                {answers.map((answer, index) => (
                                                    <div key={index} className="mb-3 soanDapAn">
                                                        <input
                                                            type="text"
                                                            className="form-control"
                                                            placeholder={`Đáp Án ${index + 1}`}
                                                            value={answer}
                                                            onChange={(e) => handleAnswerChange(index, e.target.value)}
                                                        />
                                                        <input
                                                            type='radio'
                                                            name='answers'
                                                            checked={selectedAnswer[index]}
                                                            onChange={() => handleRadioChange(index)}
                                                        />
                                                    </div>
                                                ))}
                                            </div>
                                        </form>
                                        <button className="btn btn-primary" onClick={addDapAn}>add</button>
                                    </div>
                                </div>
                            </div>
                        )}
                    </div>

                </div>
            </div>
        </div >
    )
};

export default CourseTC
