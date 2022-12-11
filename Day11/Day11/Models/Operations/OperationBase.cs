using System.Numerics;

namespace Day11.Models.Operations
{
    internal abstract class OperationBase
    {
        protected ulong rightOperand;

        protected OperationBase( ulong rightOperand )
        {
            this.rightOperand = rightOperand;
        }

        public abstract ulong Execute( ulong leftOperand );
        protected abstract ulong Execute( ulong leftOperand, ulong rightOperand );
    }
}