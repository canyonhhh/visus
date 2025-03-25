using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace visus.Data.Converters
{
    public static class DateTimeUtcConverter
    {
        public static ValueConverter<DateTime, DateTime> Create()
        {
            return new ValueConverter<DateTime, DateTime>(
                v => v.Kind == DateTimeKind.Unspecified ? DateTime.SpecifyKind(v, DateTimeKind.Utc) : v.ToUniversalTime(),
                v => DateTime.SpecifyKind(v, DateTimeKind.Local)
            );
        }
    }
}