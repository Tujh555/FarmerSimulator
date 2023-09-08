using System;

namespace FarmerSimulator;

public static class Utils
{
    public static void Require(Func<bool> predicate)
    {
        if (predicate().Not())
        {
            throw new Exception();
        }
    }

    public static bool Not(this bool expression) => !expression;
}