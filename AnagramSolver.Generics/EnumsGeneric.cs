using System;
using System.ComponentModel;

namespace AnagramSolver.Generics;

public class EnumsGeneric
{
    public static T1 MapValueToEnum<T1, T2>(T2 value)where T1 : Enum
    {
        try
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
        catch (Exception)
        {
            throw;
        }
    }
}