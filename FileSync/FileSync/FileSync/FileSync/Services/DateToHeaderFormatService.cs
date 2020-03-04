using System;
using FileSync.Shared.Services;

namespace FileSync.Services
{
    public class DateToHeaderFormatService : IDateToHeaderFormatService
    {
        public string Format(DateTime date)
        {
            if (date.Date == DateTime.Today) return "Today";
            if (date.Date.AddDays(1) == DateTime.Today) return "Yesterday";
            if (date > DateTime.Today.AddDays(-7)) return "Last week";
            if (date > DateTime.Today.AddDays(-14)) return "Two weeks ago";
            if (date > DateTime.Today.AddDays(-21)) return "Three weeks ago";

            return date.ToString("ddd, MMM dd");
        }
    }
}