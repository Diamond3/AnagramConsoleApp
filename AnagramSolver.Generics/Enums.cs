using System;

namespace AnagramSolver.Generics;

public class Enums
{
    public enum Gender
    {
        Male = 1,
        Female = 2,
        Other = 3
    }

    public enum Weekday
    {
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday,
        Sunday
    }

    public static T1 MapValueToEnum<T1, T2>(T2 value)where T1 : Enum
    { 
        var stringValue = value.ToString();
        if (string.IsNullOrEmpty(stringValue))
        {
            throw new Exception($"Value '{value}' is not convertible to string!");
        }
        
        var result = (T1)Enum.Parse(typeof(T1), stringValue);
        if (result == null)
            throw new Exception($"Value '{value}' is not part of {typeof(T1)}");

        return result;
    }
    
    public static Gender MapIntToGender(int value)
    {
        Gender result;
        if (!Enum.TryParse(value.ToString(), out result))
            throw new Exception($"Value '{value}' is not part of Gender enum");

        return result;
    }

    public static Gender MapStringToGender(string value)
    {
        Gender result;
        if (!Enum.TryParse(value, out result)) throw new Exception($"Value '{value}' is not part of Gender enum");

        return result;
    }

    public static Weekday MapStringToWeekday(string value)
    {
        Weekday result;
        if (!Enum.TryParse(value, out result)) throw new Exception($"Value '{value}' is not part of Weekday enum");
        return result;
    }
}