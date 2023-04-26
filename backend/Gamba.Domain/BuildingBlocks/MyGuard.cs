using System.Runtime.CompilerServices;
using Dawn;

namespace Gamba.Domain.BuildingBlocks;

public static class MyGuard
{
    public static Guard.ArgumentInfo<T> Argument<T>(T argument,
        [CallerArgumentExpression("argument")] string? argumentName = default)
    {
        return Guard.Argument(argument, argumentName);
    }
}