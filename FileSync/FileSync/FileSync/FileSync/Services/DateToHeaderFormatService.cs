using System;
using FileSync.Shared.Services;
using Res = FileSync.Resources.AppResources;

namespace FileSync.Services
{
    public class DateToHeaderFormatService : IDateToHeaderFormatService
    {
        public string Format(DateTime date)
        {
            if (date.Date == DateTime.Today) return Res.DateToday;
            if (date.Date.AddDays(1) == DateTime.Today) return Res.DateYesterday;
            if (date > DateTime.Today.AddDays(-7)) return Res.DateLastWeek;
            if (date > DateTime.Today.AddDays(-14)) return Res.DateTwoWeeksAgo;
            if (date > DateTime.Today.AddDays(-21)) return Res.DateThreeWeeksAgo;

            return date.ToLongDateString();
        }
    }
}