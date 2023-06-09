using FourthPharos.Host.Models;

namespace FourthPharos.Host.Services;

public interface ICircleService
{
    ICollection<CircleModel> GetCircles();

    CircleModel CreateCircle(string name);
}
