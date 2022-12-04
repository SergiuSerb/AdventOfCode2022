namespace Day4.Models
{
    public class ElfPair
    {
        public Elf FirstElf { get; }

        public Elf SecondElf { get; }

        public ElfPair( Elf firstElf, Elf secondElf )
        {
            FirstElf = firstElf;
            SecondElf = secondElf;
        }

        public bool IsFullyDuplicated()
        {
            return FirstElf.DoesCover( SecondElf.GetCoveredSections() ) ||
                   SecondElf.DoesCover( FirstElf.GetCoveredSections() );
        }

        public bool IsOverlapping()
        {
            return FirstElf.DoesOverlap( SecondElf.GetCoveredSections() ) ||
                   SecondElf.DoesOverlap( FirstElf.GetCoveredSections() );
        }
    }
}