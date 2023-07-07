using FourthPharos.Host.Models;

namespace FourthPharos.Host.Services;

public interface ICircleService
{
    ICollection<CircleModel> GetCircles();

    CircleModel CreateCircle(string? name, Guid userId);

    void DeleteCircle(Guid id);
}
