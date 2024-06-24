using EM.Domain.Constants;

namespace EM.CrossCuttingConcerns.DateTimes
{
    public interface IDateTimeProvider
    {
        private static TimeZoneInfo AzerbaijanTimeZoneInfo
            => TimeZoneInfo.FindSystemTimeZoneById(DateTimeConstants.AzerbaijanTimeZoneId);

        public static DateTime Now => TimeZoneInfo.ConvertTime(DateTime.UtcNow, AzerbaijanTimeZoneInfo);

        public static DateTime DateNow => TimeZoneInfo.ConvertTime(DateTime.UtcNow, AzerbaijanTimeZoneInfo).Date;
    }
}