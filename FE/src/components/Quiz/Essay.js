import Drawer from "../Drawer/Drawer";
import React, { useContext, useEffect, useState } from "react";
import 'quill/dist/quill.snow.css'
import ReactQuill from 'react-quill'
import { Link, useLocation, useNavigate } from "react-router-dom";
import { UserContext } from "../../Navigation";
const Essay = () => {
    const [user, dispatch] = useContext(UserContext);
    const Navigate = useNavigate();
    const [content, setContent] = useState('');
    const location = useLocation();
    const baitaps = location.state?.baitap;
    const [cauhoi, setCauhoi] = useState([]);
    const [cauTl, setCauTL] = useState([]);
    const [result, setResult] = useState(location.state?.diemso || null);

    var modules = {
        toolbar: [
            [{ size: ["small", false, "large", "huge"] }],
            ["bold", "italic", "underline", "strike", "blockquote"],
            [{ list: "ordered" }, { list: "bullet" }],
            ["link", "image"],
            [
                { list: "ordered" },
                { list: "bullet" },
                { indent: "-1" },
                { indent: "+1" },
                { align: [] }
            ],
            [{ "color": ["#000000", "#e60000", "#ff9900", "#ffff00", "#008a00", "#0066cc", "#9933ff", "#ffffff", "#facccc", "#ffebcc", "#ffffcc", "#cce8cc", "#cce0f5", "#ebd6ff", "#bbbbbb", "#f06666", "#ffc266", "#ffff66", "#66b966", "#66a3e0", "#c285ff", "#888888", "#a10000", "#b26b00", "#b2b200", "#006100", "#0047b2", "#6b24b2", "#444444", "#5c0000", "#663d00", "#666600", "#003700", "#002966", "#3d1466", 'custom-color'] }],
        ]
    };

    var formats = [
        "header", "height", "bold", "italic",
        "underline", "strike", "blockquote",
        "list", "color", "bullet", "indent",
        "link", "image", "align", "size",
    ];

    const handleContentChange = (newContent) => {
        setContent(newContent);
    };

    // hàm lấy các câu hỏi trong bài tập 
    const fetchQuestion = async () => {
        try {
            const response = await fetch(`https://localhost:7111/api/CauHoi/byBaiTap/${baitaps.IdBaiTap}`);
            setCauhoi(await response.json());
        } catch (error) {
            alert(error);
            console.error('Error fetching data:', error);
        }
    }
    // lấy bài làm nếu có 
    const fetchAnswer = async () => {
        try {
            const response = await fetch(`https://localhost:7111/api/XyLyLamBai/getBaiTapByUserandBaiTap/${baitaps.IdBaiTap}/${user.userInfo.IdUser}`);
            setCauTL(await response.json());
        } catch (error) {
            // console.error('Error fetching data:', error);
        }
    };


    useEffect(() => {
        fetchQuestion();
    }, []);

    useEffect(() => {
        fetchAnswer();
    }, [cauhoi]);

    useEffect(() => {
        setContent(cauTl.NoiDungBaiLam);
    }, [cauTl]);
    const test = () => {
        // console.log("ok")
        console.log(cauTl.IdBaiLam)
        // console.log(cauTl.NoiDungBaiLam)
    }

    // hàm lưu bài
    const saveContent = async () => {
        if (cauTl.length === 0) {
            const data = {
                "IdSinhVien": user.userInfo.IdUser,
                "IdBaiTap": baitaps.IdBaiTap,
                "NoiDungBaiLam": content
            };

            try {
                const response = await fetch('https://localhost:7111/api/XyLyLamBai/lam-bai-tu-luan', {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json',
                    },
                    body: JSON.stringify(data),
                });

                if (!response.ok) {
                    alert("Thất bại");
                    throw new Error('Request failed');

                } else {
                    alert("Lưu bài thanh công quay về khóa học");
                    Navigate('/course');
                }
            } catch (error) {
                console.error('Error:', error);
            }
        } else {
            const formData = new FormData();
            formData.append('noidungbailam', content);
            try {
                const response = await fetch(`https://localhost:7111/api/XyLyLamBai/updateByIdBaiLam/${cauTl.IdBaiLam}`, {
                    method: 'PUT',
                    headers: {
                        'Content-Type': 'application/json',
                    },
                    body: JSON.stringify(content),
                });

                if (!response.ok) {
                    console.log(response);
                    alert("Thất bại 123");
                    throw new Error('Request failed');

                } else {
                    alert("Cập nhật bài thanh công quay về khóa học");
                    Navigate('/course');
                }
            } catch (error) {
                console.error('Error:', error);
            }
        }

    };



    return (
        <div className="container-cont">
            <Drawer />
            <div className="cont">
                <h1 className="d-flex justify-content-center align-items-center">Bài tập tự luận</h1>
                <hr />
                <div className="" style={{ padding: "10px" }}>
                    {cauhoi ? (
                        <>
                            {cauhoi.map((quiz, index) => (
                                <h2 key={index}>Câu {index + 1}: {quiz.noiDungCauHoi}</h2>
                            ))}
                        </>
                    ) : (
                        <>
                            <h2>Khong có câu hỏi</h2>
                        </>
                    )}

                    nhập câu trả lời tại đây
                    <div style={{ width: "100%", display: "flex", justifyContent: "left" }}>
                        <div style={{ width: "100%" }}>
                            <div style={{ display: "grid" }}>
                                <ReactQuill
                                    theme="snow"
                                    modules={modules}
                                    formats={formats}
                                    value={content}
                                    placeholder="write your content ...."
                                    onChange={handleContentChange}
                                    style={{ height: "220px" }}
                                >
                                </ReactQuill>
                            </div>
                        </div>
                    </div>
                </div>

                <div style={{ marginTop: "50px", display: "flex", justifyContent: "space-between", paddingRight: "10px" }} >
                    <div></div>
                    <h2>Điểm: </h2>
                    <div>
                        <Link to={"/course"}>
                            <button class="btn btn-info" style={{ margin: "4px", width: "200px" }}>Quay về khóa học</button>
                        </Link>
                        <button onClick={saveContent} class="btn btn-secondary" style={{ margin: "4px", width: "100px" }}>lưu bài</button>
                    </div>

                </div>

            </div>
        </div >
    );
};

export default Essay