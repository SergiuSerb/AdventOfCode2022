﻿namespace Day2.Models
{
    public class Player
    {
        public string Name { get; }

        public int Score { get; set; }

        public Player(string name)
        {
            Name = name;
        }

        public void AddScore(int score)
        {
            Score += score;
        }
    }
}