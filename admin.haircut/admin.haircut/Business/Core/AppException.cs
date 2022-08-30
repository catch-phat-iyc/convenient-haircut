using static Admin.Haircut.Business.Core.AppEnums;

namespace Admin.Haircut.Business.Core
{
    public class AppException : Exception
    {
        public AppError Error { get; }

        public AppException(AppError error = AppError.INVALID_OPERATION, string message = "") : base(message)
        {
            Error = error;
        }

        public AppException(string message) : base(message)
        {
            Error = AppError.INVALID_OPERATION;
        }
    }
}
