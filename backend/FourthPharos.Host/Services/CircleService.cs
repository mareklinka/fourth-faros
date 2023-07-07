using FourthPharos.Domain.CandelaObscuraCircle;
using FourthPharos.Host.Models;

namespace FourthPharos.Host.Services;

public sealed class CircleService : ICircleService
{
    private readonly List<CircleModel> _circles = new();

    public ICollection<CircleModel> GetCircles() => _circles;

    public CircleModel CreateCircle(string? name, Guid userId)
    {
        var circle = new CircleModel(CircleFactory.CreateCirle(name), userId);

        _circles.Add(circle);

        return circle;
    }

    public void DeleteCircle(Guid id) => _circles.RemoveAll(_ => _.Id == id);

}
