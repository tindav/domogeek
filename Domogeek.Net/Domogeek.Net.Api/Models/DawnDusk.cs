using System;
using System.Linq;
using Domogeek.Net.Api.Models.External;

namespace Domogeek.Net.Api.Models
{
    public class DawnDusk
    {
        private readonly DawnDuskType _dawnDuskType;

        public DawnDusk(DateTimeOffset date, GeoCoordinates coordinates, DawnDuskType dawnDuskType, double utcOffset)
        {
            Date = date.Date;
            var mLon = (coordinates.Longitude * 4.0) / 60.0;
            var solarDeclination = GetDeclination(GetDay(date));
            var Ho = GetHo(solarDeclination, coordinates.Latitude, coordinates.Longitude);
            var equationOfTime = GetEoT(GetDay(date));
            Meridian = 12.0 + equationOfTime - mLon + utcOffset;
            Sunrise = Meridian - Ho;
            Sunset = Meridian + Ho;
            Duration = Ho * 2;
            _dawnDuskType = dawnDuskType;
        }

        private double Meridian { get; }
        private double Sunrise { get; }
        private double Sunset { get; }
        private double Duration { get; }

        public DateTimeOffset Date { get; }
        public string MeridianTime => DisplayedValue(() => GetHm(Meridian), DawnDuskType.Zenith);
        public string SunriseTime => DisplayedValue(() => GetHm(Sunrise), DawnDuskType.Sunrise);
        public string SunsetTime => DisplayedValue(() => GetHm(Sunset), DawnDuskType.Sunset);
        public string DurationTime => DisplayedValue(() => GetHm(Duration), DawnDuskType.DayDuration);

        private string DisplayedValue(Func<string> displayValue, params DawnDuskType[] dawnDuskTypes)
        {
            return dawnDuskTypes.Union(new[] { DawnDuskType.All }).Contains(_dawnDuskType) ? displayValue.Invoke() : null;
        }

        private string GetHm(double nH)
        {
            var h = (int)nH;
            var m = (int)(((nH * 60.0) % 60) + 0.5);
            if (m == 60)
            {
                m = 00;
                h = h + 1;
            }
            return new TimeSpan(h, m, 0).ToString(@"hh\:mm");
        }

        private int GetDay(DateTimeOffset date)
        {
            var n1 = (int)((date.Month * 275.0) / 9.0);
            var n2 = (int)((date.Month + 9.0) / 12.0);
            var k = 1.0 + (int)((date.Year - 4.0 * (int)(date.Year / 4.0) + 2.0) / 3.0);
            return (int)(n1 - n2 * k + date.Day - 30.0);
        }

        private double DegreeToRadian(double angle)
        {
            return Math.PI * angle / 180.0;
        }

        private double RadianToDegree(double angle)
        {
            return angle * (180.0 / Math.PI);
        }

        private double GetEoT(double j)
        {
            var m = 357.0 + (0.9856 * j);
            var c = (1.914 * Math.Sin(DegreeToRadian(m))) + (0.02 * Math.Sin(DegreeToRadian(2.0 * m)));
            var l = 280.0 + c + (0.9856 * j);
            var r = (-2.465 * Math.Sin(DegreeToRadian(2.0 * l))) + (0.053 * Math.Sin(DegreeToRadian(4.0 * l)));
            return ((c + r) * 4.0) / 60.0;
        }

        private double GetDeclination(double j)
        {
            var m = 357.0 + (0.9856 * j);
            var c = (1.914 * Math.Sin(DegreeToRadian(m))) + (0.02 * Math.Sin(DegreeToRadian(2.0 * m)));
            var l = 280.0 + c + (0.9856 * j);
            var sinDec = 0.3978 * Math.Sin(DegreeToRadian(l));
            return RadianToDegree(Math.Asin(sinDec));
        }

        private double GetHo(double Dec, double Lat, double Lon)
        {
            var cosHo = (-0.01454 - Math.Sin(DegreeToRadian(Dec)) * Math.Sin(DegreeToRadian(Lat))) / (Math.Cos(DegreeToRadian(Dec)) * Math.Cos(DegreeToRadian(Lat)));
            return (RadianToDegree(Math.Acos(cosHo)) / 15.0);
        }

    }
}
