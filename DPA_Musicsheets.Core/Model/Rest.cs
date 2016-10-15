using DPA_Musicsheets.Core.Model.Enum;

namespace DPA_Musicsheets.Core.Model
{
    public class Rest : BaseNote
    {
        public override string AsString()
        {
            return "r" + Duration + (HasDot ? "." : "");
        }
    }
}