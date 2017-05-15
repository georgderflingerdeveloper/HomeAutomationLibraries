using System;

namespace SystemServices
{
    public class TimeUtil : ITimeUtil
    {
        public const string FORMAT_DAY_MONTH_HOUR_MINUTE_SECOND = "{0:00}";
        public const string FORMAT_MS = "{0:000}";

        static string getTimeStamp()
        {
           return( String.Format( FORMAT_DAY_MONTH_HOUR_MINUTE_SECOND, DateTime.Now.Day ) +
                                 String.Format( FORMAT_DAY_MONTH_HOUR_MINUTE_SECOND, DateTime.Now.Month ) +
                                 DateTime.Now.Year.ToString( ) +
                                 ":" +
                                 String.Format( FORMAT_DAY_MONTH_HOUR_MINUTE_SECOND, DateTime.Now.Hour ) +
                                 "h" +
                                 String.Format( FORMAT_DAY_MONTH_HOUR_MINUTE_SECOND, DateTime.Now.Minute ) +
                                 "m" +
                                 String.Format( FORMAT_DAY_MONTH_HOUR_MINUTE_SECOND, DateTime.Now.Second ) +
                                 "s" +
                                 String.Format( FORMAT_MS, DateTime.Now.Millisecond ) +
                                 "ms" );
        }

        public static string GetTimestamp()
        {
            return ( getTimeStamp( ) );
        }

        public string IGetTimeStamp()
        {
            return ( getTimeStamp( ) );
        }

        public static string GetTimestamp_()
        {
            string timestamp = String.Format( FORMAT_DAY_MONTH_HOUR_MINUTE_SECOND, DateTime.Now.Day ) +
                                 String.Format( FORMAT_DAY_MONTH_HOUR_MINUTE_SECOND, DateTime.Now.Month ) +
                                 DateTime.Now.Year.ToString( ) +
                                 "_" +
                                 String.Format( FORMAT_DAY_MONTH_HOUR_MINUTE_SECOND, DateTime.Now.Hour ) +
                                 "h" +
                                 String.Format( FORMAT_DAY_MONTH_HOUR_MINUTE_SECOND, DateTime.Now.Minute ) +
                                 "m" +
                                 String.Format( FORMAT_DAY_MONTH_HOUR_MINUTE_SECOND, DateTime.Now.Second ) +
                                 "s" +
                                 String.Format( FORMAT_MS, DateTime.Now.Millisecond ) +
                                 "ms";
            return ( timestamp );
        }

        public static string GetDate()
        {
            string timestamp = String.Format( FORMAT_DAY_MONTH_HOUR_MINUTE_SECOND, DateTime.Now.Day ) +
                                 String.Format( FORMAT_DAY_MONTH_HOUR_MINUTE_SECOND, DateTime.Now.Month ) +
                                 DateTime.Now.Year.ToString( );
            return ( timestamp );
        }

        public static string GetDate_()
        {
            string timestamp = String.Format( FORMAT_DAY_MONTH_HOUR_MINUTE_SECOND, DateTime.Now.Day ) + "_" +
                                 String.Format( FORMAT_DAY_MONTH_HOUR_MINUTE_SECOND, DateTime.Now.Month ) + "_" +
                                 DateTime.Now.Year.ToString( );
            return ( timestamp );
        }

        public static string GetTimeHmSms()
        {
            string timestamp = String.Format( FORMAT_DAY_MONTH_HOUR_MINUTE_SECOND, DateTime.Now.Hour ) +
                                 "h" +
                                 String.Format( FORMAT_DAY_MONTH_HOUR_MINUTE_SECOND, DateTime.Now.Minute ) +
                                 "m" +
                                 String.Format( FORMAT_DAY_MONTH_HOUR_MINUTE_SECOND, DateTime.Now.Second ) +
                                 "s" +
                                 String.Format( FORMAT_MS, DateTime.Now.Millisecond ) +
                                 "ms";
            return ( timestamp );
        }

        public static string GetTimeHmS()
        {
            string timestamp = String.Format( FORMAT_DAY_MONTH_HOUR_MINUTE_SECOND, DateTime.Now.Hour ) +
                                 "h" +
                                 String.Format( FORMAT_DAY_MONTH_HOUR_MINUTE_SECOND, DateTime.Now.Minute ) +
                                 "m" +
                                 String.Format( FORMAT_DAY_MONTH_HOUR_MINUTE_SECOND, DateTime.Now.Second ) +
                                 "s";
            return ( timestamp );
        }

        public static string GetTimeHm()
        {
            string timestamp = String.Format( FORMAT_DAY_MONTH_HOUR_MINUTE_SECOND, DateTime.Now.Hour ) +
                                 "h" +
                                 String.Format( FORMAT_DAY_MONTH_HOUR_MINUTE_SECOND, DateTime.Now.Minute ) +
                                 "m";
            return ( timestamp );
        }

        public static string GetTimeH()
        {
            string timestamp = String.Format( FORMAT_DAY_MONTH_HOUR_MINUTE_SECOND, DateTime.Now.Hour ) +
                                 "h";
            return ( timestamp );
        }


    }

    public static class Month
    {
        /// <summary>
        /// Gets the first day of the month.
        /// </summary>
        /// <param name="givenDate">The given date.</param>
        /// <returns>the first day of the month</returns>
        public static DateTime GetFirstDayOfMonth( DateTime givenDate )
        {
            return new DateTime( givenDate.Year, givenDate.Month, 1 );
        }

        /// <summary>
        /// Gets the last day of month.
        /// </summary>
        /// <param name="givenDate">The given date.</param>
        /// <returns>the last day of the month</returns>
        public static DateTime GetTheLastDayOfMonth( DateTime givenDate )
        {
            return GetFirstDayOfMonth( givenDate ).AddMonths( 1 ).Subtract( new TimeSpan( 1, 0, 0, 0, 0 ) );
        }
    }

    static class TimeConverter
    {
        static public double ToMiliseconds( double seconds )
        {
            return ( seconds * 1000 );
        }

        static public double ToMiliseconds( double minutes, double seconds )
        {
            return ( ( minutes * 60 + seconds ) * 1000 );
        }

        static public double ToMiliseconds( double hours, double minutes, double seconds )
        {
            return ( ( hours * 3600 + minutes * 60 + seconds ) * 1000 );
        }
    }

}