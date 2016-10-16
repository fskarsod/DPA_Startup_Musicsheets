using DPA_Musicsheets.Core.Interface;
using System;

namespace DPA_Musicsheets.Core.Model
{
    public class BarBoundary : IMusicComponent
    {
        public void Accept(IMusicComponentVisitor visitor)
        {
            visitor.Visit(this);
        }

        public string ToLilypond()
        {
            return "|";
        }
    }
}