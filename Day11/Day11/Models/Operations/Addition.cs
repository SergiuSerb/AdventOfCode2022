namespace Day11.Models.Operations
{
    internal class Addition : OperationBase
    {
        public static string Representation => "+";

        public Addition(ulong rightOperand) : base(rightOperand) { }

        public override ulong Execute( ulong leftOperand )
        {
            return Execute( leftOperand, rightOperand );
        }

        protected override ulong Execute( ulong leftOperand, ulong rightOperand )
        {
            return leftOperand + rightOperand;
        }
    }
}