namespace Day17.Tools.Math
{
    public interface IIdentifiablePlaceable : IPlaceable
    {
        public ulong Id { get; set; }
    }
}