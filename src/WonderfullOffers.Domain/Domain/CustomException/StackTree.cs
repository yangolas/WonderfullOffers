using System.Diagnostics;

namespace WonderfullOffers.Domain.Domain.CustomException;

public static class StackTree
{
    public static string GetPathError(StackTrace stackTrace)
    {
        var frame = stackTrace.GetFrame(0);
        var className = frame?.GetMethod()?.DeclaringType?.FullName;
        var methodName = frame?.GetMethod()?.Name;

        var lineNumber = frame?.GetFileLineNumber();
        return $"======>Class:\n{className}\n\n======>Method:\n{methodName}\n\n======>Line:\n{lineNumber}\n\n";
    }
}
