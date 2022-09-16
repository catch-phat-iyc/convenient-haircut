using System.ComponentModel;

namespace Admin.Haircut.Business.Core
{
    public class AppEnums
    {
        public enum AppError
        {
            UNKNOWN,
            OPERATION_FAIL,
            RECORD_NOTFOUND,
            INVALID_PARAMETERS,
            INVALID_OPERATION,
            TOKEN_INVALID,
            TOKEN_WRONG,
            TOKEN_EXPIRED,
            OTP_INVALID,
            OTP_WRONG,
            OTP_EXPIRED,
            GACODE_REQUIRED,
            PASSWORD_WRONG,
            GACODE_WRONG,
            INVALID_SIGNATURE,
            ACCOUNT_NOTFOUND,
            WRONG_CREDENTIALS,
            UNAUTHORIZED,
            PERMISSION_DENIED,
            ACCOUNT_IS_USED,
            INSUFFICIENT_FUNDS,
            GOOGLE_LOGIN_FAIL,
            NFT_NOT_FOUND
        }

        public enum ErrorCode
        {
            [Description("Internal server error")] InternalServerError,
            [Description("Invalid operation")] InvalidOperation,
            [Description("Bad Request")] BadRequest,
            [Description("Info")] Info
        }

        public enum Error
        {
            [Description("Dữ liệu đã tồn tại.")] DataAlreadyExists,
            [Description("404 - Dữ liệu không tồn tại.")] DataNotFound,
            [Description("405 - Bạn không có quyền.")] MethodNotAllowed,
        }
       
        public enum RoomStatus
        {
            [Description("Empty")] Empty = 0,
            [Description("CheckOut")] Full = 1,
            [Description("Maintenance")] Maintenance = 2
        }

        public enum Gender
        {
            [Description("Nữ")] Female = 0,
            [Description("Nam")] Male = 1,
            [Description("Khác")] Other = 2
        }

        public enum Button
        {
            [Description("Cancel")] Cancel,
            [Description("Close")] Close,
            [Description("Save changes")] SaveChanges,
            [Description("Create")] Create,
            [Description("Update")] Update,
            [Description("Delete")] Delete,
            [Description("Search")] Search,
            [Description("Inactive")] Inactive,
            [Description("Active")] Active,
            [Description("Add")] Add,
        }

        public enum Roles
        {
            [Description("Admin Root")] AdminRoot = 0,
            [Description("Admin")] Admin = 1,
            [Description("Test")] Test = 2,
        }

        public enum Log
        {
            [Description("Information")] Information,
            [Description("Warning")] Warning,
            [Description("Error")] Error,
        }

        #region Session

        public enum SessionKey
        {
            [Description("AdminLogin")] AdminLogin,
        }

        #endregion

        public enum DiscountType
        {
            [Description("Không")] No,
            [Description("Tiền")] Money,
            [Description("Phần trăm")] Percent,
        }

        public enum DefaultImage
        {
            [Description("uploads/default-girl.png")] Girl,
            [Description("uploads/default-man.png")] Man,
            [Description("uploads/default-img.png")] Img,
            [Description("uploads/default-product.png")] Product,
        }
    }
}
