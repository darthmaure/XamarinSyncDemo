using System;

namespace FileSync.Shared.Services
{
    public interface IDateToHeaderFormatService
    {
        string Format(DateTime date);
    }
}
