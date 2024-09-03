// nơi đây chứa các đối tượng muốn custome
namespace StudyHub.modelRequest
{
    //public class userRequest
    //{
    //    public string Username { get; set; } = null!;
    //    public string Password { get; set; } = null!;
    //}

    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class CauHoiInfo
    {
        public int IdBaiTap { get; set; }
        public string TenBaiTap { get; set; }
        public string TenLoaiBaiTap { get; set; }
        public int ThoiGian { get; set; }
        public string NoiDung { get; set; }
    }


}
