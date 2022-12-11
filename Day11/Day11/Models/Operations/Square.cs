namespace Day11.Models.Operations
{
    internal class Square : OperationBase
    {
        public Square( ulong rightOperand ) : base( rightOperand ) { }

        public override ulong Execute( ulong leftOperand )
        {
            return Execute( leftOperand, leftOperand );
        }

        protected override ulong Execute( ulong leftOperand, ulong rightOperand )
        {
            return leftOperand * leftOperand;
        }
    }
}