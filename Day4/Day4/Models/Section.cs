namespace Day4.Models
{
    public class Section
    {
        public int Id { get; }

        public Section( int id )
        {
            Id = id;
        }

        private bool Equals( Section other )
        {
            return Id == other.Id;
        }

        public override bool Equals( object? obj )
        {
            if ( ReferenceEquals( null, obj ) )
            {
                return false;
            }

            if ( ReferenceEquals( this, obj ) )
            {
                return true;
            }

            if ( obj.GetType() != this.GetType() )
            {
                return false;
            }

            return Equals( (Section)obj );
        }

        public override int GetHashCode()
        {
            return Id;
        }
    }
}