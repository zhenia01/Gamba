using System.Runtime.CompilerServices;

namespace Gamba.Domain.BuildingBlocks;

public static class MyGuard
{
    public static Dawn.Guard.ArgumentInfo<T> Argument<T>(T argument, [CallerArgumentExpression("argument")] string? argumentName = default)
    {
        return Dawn.Guard.Argument(argument, argumentName);
    }
}