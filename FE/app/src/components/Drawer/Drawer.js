<<<<<<< HEAD
// import { useContext } from "react";
// import { useNavigate } from "react-router-dom";

// const Drawer = () => {
//   const navigate = useNavigate(); // dùng để chuyển trang
//   const [user, dispatch] = useContext(UserContext);

//   const logout = () => {
//     navigate("/logout");
//     // Xóa token từ localStorage
//     localStorage.removeItem("token");
//   };
//   return (
//     <div className="Navbar">
//       <img src={user.userInfo.Avatar} className="avatar" alt="loi" />
//       <h2>
//         {user.userInfo.FirstName} {user.userInfo.LastName}
//       </h2>
//       <h5>{user.userInfo.Role}</h5>
//       <Link to={"/profile"}>
//         <button
//           className="btn custome-btn-profile"
//           style={{ width: "250px", margin: "5px" }}
//         >
//           My profile
//         </button>
//       </Link>

//       <Link to={"/"}>
//         <button type="button" class="btn btn-primary btn-nav">
//           <RiHome7Fill />
//           Trang chủ
//         </button>
//       </Link>
//       <button type="button" class="btn btn-primary btn-nav">
//         <AiFillMessage />
//         Nhắn tin
//       </button>

//       {user.userInfo.Role !== "Teacher" && (
//         <>
//           <Link to={"/scores"}>
//             <button type="button" className="btn btn-primary btn-nav">
//               <IoDocumentTextSharp />
//               Xem điểm
//             </button>
//           </Link>
//           <Link to={"/dkmon"}>
//             <button type="button" className="btn btn-primary btn-nav">
//               <MdFileOpen />
//               Đăng ký môn
//             </button>
//           </Link>
//         </>
//       )}

//       <hr className="hr-border" />
//       <button type="button" class="btn btn-danger btn-nav" onClick={logout}>
//         <TbLogout />
//         Logout
//       </button>
//     </div>
//   );
// };

// export default Drawer;
=======
// import { useContext } from "react";
// import { useNavigate } from "react-router-dom";

// const Drawer = () => {
//   const navigate = useNavigate(); // dùng để chuyển trang
//   const [user, dispatch] = useContext(UserContext);

//   const logout = () => {
//     navigate("/logout");
//     // Xóa token từ localStorage
//     localStorage.removeItem("token");
//   };
//   return (
//     <div className="Navbar">
//       <img src={user.userInfo.Avatar} className="avatar" alt="loi" />
//       <h2>
//         {user.userInfo.FirstName} {user.userInfo.LastName}
//       </h2>
//       <h5>{user.userInfo.Role}</h5>
//       <Link to={"/profile"}>
//         <button
//           className="btn custome-btn-profile"
//           style={{ width: "250px", margin: "5px" }}
//         >
//           My profile
//         </button>
//       </Link>

//       <Link to={"/"}>
//         <button type="button" class="btn btn-primary btn-nav">
//           <RiHome7Fill />
//           Trang chủ
//         </button>
//       </Link>
//       <button type="button" class="btn btn-primary btn-nav">
//         <AiFillMessage />
//         Nhắn tin
//       </button>

//       {user.userInfo.Role !== "Teacher" && (
//         <>
//           <Link to={"/scores"}>
//             <button type="button" className="btn btn-primary btn-nav">
//               <IoDocumentTextSharp />
//               Xem điểm
//             </button>
//           </Link>
//           <Link to={"/dkmon"}>
//             <button type="button" className="btn btn-primary btn-nav">
//               <MdFileOpen />
//               Đăng ký môn
//             </button>
//           </Link>
//         </>
//       )}

//       <hr className="hr-border" />
//       <button type="button" class="btn btn-danger btn-nav" onClick={logout}>
//         <TbLogout />
//         Logout
//       </button>
//     </div>
//   );
// };

// export default Drawer;
>>>>>>> 4b7ae76bcaddb780f7cbf1f931b9e71a240a5481
