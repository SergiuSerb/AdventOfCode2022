﻿using System.Collections.Generic;
using Day17.Tools.Math;

namespace Day17.Models.Rocks
{
    //..#
    //..#
    //###
    public class CornerRock : Rock, IPlaceable
    {
        public int Id { get; }

        public int CoordinateRow { get; set;}

        public int CoordinateColumn { get; set;}

        private IList<RockComponent> _components = new List<RockComponent>()
        {
            new RockComponent(0, 0),
            new RockComponent(1, 0),
            new RockComponent(2, 0),
            new RockComponent(2, -1),
            new RockComponent(2, -2)
        };

        public CornerRock(int id)
        {
            Id = id;
        }
    }
}