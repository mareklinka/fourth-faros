namespace FourthPharos.Host.Shared;

public static class Routes
{
    public const string Home = "/";
    public const string MyCircles = "/circles/my";
    public const string NewCircle = "/circles/create";
    public const string MyCharacters = "/my-characters";

    public static string EditCircle(Guid id) => $"/circles/edit/{id}";
}