namespace FourthPharos.Host.Shared;

public static class Routes
{
    public const string Home = "/";
    public const string MyCircles = "/circles/my";
    public const string NewCircle = "/circles/create";
    public const string MyCharacters = "/my-characters";

    public static string CircleEditor(Guid id) => $"/circles/edit/{id}";

    public static string CircleSheet(Guid id) => $"/circles/sheet/{id}";
}