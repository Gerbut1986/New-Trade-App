namespace New_Trade_Soft_App.Models
{
    using System.Collections.Generic;

    class Date
    {
        public static Dictionary<int, string> GetMonths => new Dictionary<int, string>()
        {
            { 1, "January" }, 
            { 2, "February" }, 
            { 3, "March" }, 
            { 4, "April" }, 
            { 5, "May" },
            { 6, "June" }, 
            { 7, "July"}, 
            { 8, "August" }, 
            { 9, "September" }, 
            { 10, "October" },
            { 11, "November" }, 
            { 12, "December" }
        };


    }
}
