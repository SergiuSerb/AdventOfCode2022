namespace Day4.Models
{
    public class Elf
    {
        private readonly int _id;
        private readonly IList<Section> _sections;

        public Elf( int id )
        {
            _id = id;
            _sections = new List<Section>();
        }

        public void AddSection( Section section )
        {
            _sections.Add(section);
        }

        public IList<Section> GetCoveredSections()
        {
            return _sections;
        }

        public bool DoesCover( IList<Section> sections )
        {
            return _sections.Min( x => x.Id ) <= sections.Min( x => x.Id ) &&
                   _sections.Max( x => x.Id ) >= sections.Max( x => x.Id );
        }

        public bool DoesOverlap( IList<Section> sections )
        {
            foreach ( Section section in sections )
            {
                bool doesContain = _sections.Contains( section );

                if ( doesContain )
                {
                    return true;
                }
            }

            return false;
        }
    }
}