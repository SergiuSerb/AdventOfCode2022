namespace Day4.Models
{
    public class ElfPair
    {
        private readonly Elf _firstElf;
        private readonly Elf _secondElf;

        public ElfPair( Elf firstElf, Elf secondElf )
        {
            _firstElf = firstElf;
            _secondElf = secondElf;
        }

        public bool IsFullyDuplicated()
        {
            return _firstElf.DoesCover( _secondElf.GetCoveredSections() ) ||
                   _secondElf.DoesCover( _firstElf.GetCoveredSections() );
        }

        public bool IsOverlapping()
        {
            return _firstElf.DoesOverlap( _secondElf.GetCoveredSections() ) ||
                   _secondElf.DoesOverlap( _firstElf.GetCoveredSections() );
        }
    }
}