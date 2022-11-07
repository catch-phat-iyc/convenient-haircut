using Admin.Haircut.Business.Core;

namespace Admin.Haircut.Business.Models.Base
{
    public class BaseModel
    {
        public long Id { get; set; }

        public AppEnums.Status Status { get; set; }
    }
}
