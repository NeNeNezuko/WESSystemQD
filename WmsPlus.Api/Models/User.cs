namespace WmsPlus.Api.Models
{
    public class User
    {
        public string COMPNO { get; set; } = "";
        public string USR { get; set; } = "";
        public string NAME { get; set; } = "";
        public string? PWD { get; set; }
        public string? MNG { get; set; }
        public DateTime? E_DAT { get; set; }
        public string? DEP { get; set; }
        public DateTime? B_DAT { get; set; }
        public string? COMP_BOSS { get; set; }
        public string? DEP_UP { get; set; }
        public string? E_MAIL { get; set; }
        public string? ISGROUP { get; set; }
        public string? ISCUST { get; set; }
        public string? DEPRO_NO { get; set; }
        public string? PWD_CHG { get; set; }
        public string? ID_CODE { get; set; }
        public string? TP_ID { get; set; }
        public string? LANG_ID { get; set; }
        public DateTime? SYS_DATE { get; set; }
        public string? CREATOR { get; set; }
        public string? IGNORECASE { get; set; }
    }

    public class LoginRequest
    {
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";
    }

    public class LoginResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = "";
        public string? Token { get; set; }
        public UserInfo? User { get; set; }
    }

    public class UserInfo
    {
        public string Name { get; set; } = "";
        public string Dept { get; set; } = "";
        public string CompNo { get; set; } = "";
        public string Usr { get; set; } = "";
    }

    public class ChangePasswordRequest
    {
        public string Username { get; set; } = "";
        public string OldPassword { get; set; } = "";
        public string NewPassword { get; set; } = "";
    }
}
