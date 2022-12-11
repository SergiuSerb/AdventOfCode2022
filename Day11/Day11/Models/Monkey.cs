using System.Collections.Generic;
using Day11.Models.Operations;
using Day11.Models.Tests;

namespace Day11.Models
{
    internal class Monkey
    {
        public int Id { get; }

        private IList<Item> Items { get; }

        private OperationBase DuringInspectionOperation { get; }

        internal OperationBase BeforeTestOperation { get; set; }

        public Test Test { get; }

        public Monkey MonkeyIfTestSucceeds { get; set; }

        public Monkey MonkeyIfTestFails { get; set; }
        
        public int InspectionCount { get; private set; }

        public bool selfManageWorryLevels;

        public Monkey( int id, OperationBase duringInspectionOperation, OperationBase beforeTestOperation, Test test )
        {
            Id = id;
            DuringInspectionOperation = duringInspectionOperation;
            BeforeTestOperation = beforeTestOperation;
            Test = test;
            Items = new List<Item>();
            InspectionCount = 0;
            selfManageWorryLevels = false;
        }

        public void ReceiveItem( Item item )
        {
            Items.Add(item);
        }

        private void ThrowItem( Item item, Monkey monkeyToThrowTo )
        {
            Items.Remove( item );
            monkeyToThrowTo.ReceiveItem( item );
        }

        public void DoTheMonkeyThing( ulong lowestCommonMultiple )
        {
            IList<Item> items = new List<Item>( Items );
            
            foreach ( Item item in items )
            {
                InspectItem( item );
                
                if ( selfManageWorryLevels )
                {
                    item.Importance %= lowestCommonMultiple;
                }

                if ( BeforeTestOperation != null )
                {
                    item.Importance = BeforeTestOperation.Execute( item.Importance );
                }
                
                bool hasPassed = Test.Run( item.Importance );

                ThrowItem( item, hasPassed ? MonkeyIfTestSucceeds : MonkeyIfTestFails );
            }
        }

        private void InspectItem( Item item )
        {
            item.Importance = DuringInspectionOperation.Execute( item.Importance );
            InspectionCount++;
        }
    }
}