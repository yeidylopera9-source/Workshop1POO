using Microsoft.Win32.SafeHandles;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace WorkShop1POO.Backend;

public class Time
{
    private int _hour;
    private int _millisecond;
    private int _minute;
    private int _second;


    public Time()
    {
        Hour = 0;
        Millisecond = 0;
        Minute = 0;
        Second = 0;
    }

    public Time(int hour)
    {
        Hour = hour;
    }

    public Time(int hour, int minute)
    {
        Hour = hour;
        Minute = minute;
    }

    public Time(int hour, int minute, int second)
    {
        Hour = hour;
        Minute = minute;
        Second = second;
    }

    public Time(int hour, int minute, int second, int millisecond)
    {
        Hour = hour;
        Minute = minute;   
        Second = second;
        Millisecond = millisecond;
    }

    public int Hour 
    { 
        get => _hour;
        set => _hour = ValidHour(value);
    } 

    public int Millisecond 
    {
        get => _millisecond;
        set => _millisecond = ValidMillisecond(value);
    }

    public int Minute 
    { 
        get => _minute;
        set => _minute = ValidMinute(value);
    }

    public int Second 
    { 
        get => _second;
        set => _second = ValidSecond(value);
    }

    public override string ToString()
    {
        int hour12 = Hour % 12;

        if (hour12 == 0) hour12 = 12;
        string period = Hour < 12 ? "AM" : "PM";

        return $"{hour12:D2}:{_minute:D2}:{_second:D2}.{_millisecond:D3} {period}";
    }

    public long ToMilliseconds()
    {
        return (_hour * 3600000) + (_minute * 60000) + (_second * 1000) + Millisecond;
    }

    public long ToSeconds()
    {
       return ToMilliseconds() / 1000;
    }

    public long ToMinutes()
    {
        return ToMilliseconds() / 60;
    }

    private int ValidHour(int hour)
    {
        if (hour < 0 || hour > 23)
        {
            throw new ArgumentOutOfRangeException(nameof(hour), $"The hour: {hour}, is not valid");
        }
        return hour;
    }

    private int ValidMillisecond(int millisecond)
    {
        if (millisecond < 0 || millisecond > 999)
        {
            throw new ArgumentOutOfRangeException(nameof(millisecond), $"The millisecond: { millisecond}, is not valid");
        }
        return millisecond;
    }
    private int ValidMinute(int minute)
    {
        if (minute < 0 || minute > 59)
        {
            throw new ArgumentOutOfRangeException(nameof(minute), $"The minute: {minute}, is not valid");
        }
        return minute;
    }

    private int ValidSecond(int second)
    {
        if (second < 0 || second > 59)
        {
            throw new ArgumentOutOfRangeException(nameof(second), $"The second: {second}, is not valid");
        }
        return second;
    }

    public bool IsOtherDay(Time otherTime)
    {
        int totalMinute = (Hour + otherTime.Hour) * 60 + (Minute - otherTime.Minute);

        return totalMinute >= 1440;
    }
    public Time Add(Time otherTime)
    {
       
        int totalSeconds1= Hour * 3600 + Minute * 60 + Second;
        int totalSeconds2= otherTime.Hour * 3600 + otherTime.Minute * 60 + otherTime.Second;
        int sumSecond = totalSeconds1 + totalSeconds2;
        
        int secondInDay = 86400;
        int effectiveSecond = sumSecond % secondInDay;

        int newHour = effectiveSecond / 3600;
        int remainderSecond = effectiveSecond % 3600;
        int newMinute = remainderSecond /60;
        int newSecond = remainderSecond % 60;


        return new Time(newHour, newMinute, newSecond);
        
    }
}

