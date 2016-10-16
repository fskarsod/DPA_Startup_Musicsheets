namespace DPA_Musicsheets.Core.Interface
{
    public interface IMusicComponent
    {
        void Accept(IMusicComponentVisitor visitor);
        string ToLilypond();
    }
}