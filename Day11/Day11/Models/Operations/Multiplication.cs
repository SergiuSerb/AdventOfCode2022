namespace Day11.Models.Operations
{
    internal class Multiplication : OperationBase
    {
        public static string Representation => "*";

        public override ulong Execute( ulong leftOperand )
        {
            return Execute( leftOperand, rightOperand );
        }

        protected override ulong Execute( ulong leftOperand, ulong rightOperand )
        {
            return leftOperand * rightOperand;
        }

        public Multiplication( ulong rightOperand ) : base( rightOperand ) { }
    }
}