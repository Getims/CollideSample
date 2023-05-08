using System;
using System.Collections;

public class UnixTime {
	public static int Now { get => CurrentUnixTimeSeconds(); }
	public static UnixTime Today { get => new UnixTime( CurrentUnixTimeSeconds() ); }
	public int second { get => dateTime.Second; }
	public int minute { get => dateTime.Minute; }
	public int hour { get => dateTime.Hour; }
	public int day { get => dateTime.Day; }
	public int month { get => dateTime.Month; }
	public int year { get => dateTime.Year; }

	int unixSeconds;
	DateTime dateTime;
	DateTimeOffset dateTimeOffset;

	/* --- constructors ----------------------------------------------------------------------------- */

	public UnixTime( int unixSeconds = 0 ) {
		this.unixSeconds = unixSeconds;
		dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds( this.unixSeconds ).ToLocalTime();
		dateTime = dateTimeOffset.DateTime;
	}

	/* --- public methods ----------------------------------------------------------------------------- */

	public static int DaysToSeconds( int d ) { return d * 24 * 60 * 60; }
	public static int HoursToSeconds( int h ) { return h * 60 * 60; }
	public static int MinutesToSeconds( int m ) { return m * 60; }

	public int ToInt() {
		return unixSeconds;
	}

	public new string ToString() {
		return dateTime.ToString();
	}

	public int AddDays( int days ) {
		unixSeconds += DaysToSeconds( days );
		return unixSeconds;
	}

	/* --- private methods ----------------------------------------------------------------------------- */

	static int CurrentUnixTimeSeconds() {
		return ( int )DateTimeOffset.UtcNow.ToUnixTimeSeconds();
	}
}