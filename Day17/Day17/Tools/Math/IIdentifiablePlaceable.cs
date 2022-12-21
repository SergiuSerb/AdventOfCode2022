namespace Day17.Tools.Math
{
    public interface IIdentifiablePlaceable : IPlaceable
    {
        public int Id { get; set; }
    }
}