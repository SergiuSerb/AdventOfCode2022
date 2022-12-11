using System.Numerics;

namespace Day11.Models.Tests
{
    public class Test
    {
        public readonly ulong rightOperand;

        public Test( ulong rightOperand )
        {
            this.rightOperand = rightOperand;
        }

        public bool Run( ulong leftOperand )
        {
            return Run( leftOperand, rightOperand );
        }

        private bool Run( ulong leftOperand, ulong rightOperand )
        {
            return leftOperand % rightOperand == 0;
        }
    }
}