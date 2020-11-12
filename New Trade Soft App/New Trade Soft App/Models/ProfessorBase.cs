namespace New_Trade_Soft_App.Models
{
    using System;

    public interface IProcessorInput
    {
        double Bid { get; set; }
        double Ask { get; set; }
        DateTime Time { get; set; }
    }

    class ProfessorBase
    {
    }
}
