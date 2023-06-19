using FourthPharos.Domain.CandelaObscuraCircle.Features;
using FourthPharos.Domain.CandelaObscuraCircle.Models;
using FourthPharos.Domain.Features;

namespace FourthPharos.Host.Models;

public sealed record CircleModel(Circle Circle, Guid OwnerId)
{
    public Guid Id => Circle.Id;

    public string? Name => Circle.GetFeature<Circle, CircleNameFeature>().Name;

    public string? Location => Circle.GetFeature<Circle, CircleLocationFeature>().Location;

    public int Rank => Circle.GetFeature<Circle, CircleIlluminationFeature>().Rank;

    public int Illumination => Circle.GetFeature<Circle, CircleIlluminationFeature>().Illumination;

    public int MemberCount => Circle.Characters.Length;

    public IReadOnlyCollection<CircleGear> Gear => Circle.GetFeature<Circle, CircleGearFeature>().Gear;
}