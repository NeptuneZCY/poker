using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace poker
{
    public class PokerCard : Card
    {
        public PokerNumber number;
        public Suit suit;
        public PokerType pokerType;
        public static readonly int NumberCount = Enum.GetValues(typeof(PokerNumber)).Length;
        public static readonly int SuitCount = Enum.GetValues(typeof(Suit)).Length;
        public static readonly int TypeCount = Enum.GetValues(typeof(PokerType)).Length;


        public PokerCard(PokerNumber num, Suit suit, PokerType type)
        {
            this.number = num;
            this.suit = suit;
            this.pokerType = type;
        }

        public static int poker2int(PokerCard poker)
        {
            return (int)poker.pokerType * NumberCount * SuitCount + (int)poker.suit * NumberCount + (int)poker.number;
        }

        public static PokerCard int2poker(int i)
        {
            int numAndSuit = NumberCount * SuitCount; 
            return new PokerCard((PokerNumber)(i % NumberCount), (Suit)(i % numAndSuit / NumberCount), (PokerType)(i / numAndSuit));
        }

        public String toString()
        {
            return $"number: {number}, suit: {suit}, type: {pokerType}";
        }
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }

    public enum PokerNumber
    {
        NUM_A = 0,
        NUM_2 = 1,
        NUM_3 = 2,
        NUM_4 = 3,
        NUM_5 = 4,
        NUM_6 = 5,
        NUM_7 = 6,
        NUM_8 = 7,
        NUM_9 = 8,
        NUM_10 = 9,
        NUM_J = 10,
        NUM_Q = 11,
        NUM_K = 12
    }

    public enum Suit
    {
        DIAMOND = 0,
        HEART = 1,
        CLUB = 2,
        SPADE = 3
    }

    public enum PokerType
    {
        NONE = 0,
        CHIP = 1, // ≥Ô¬Î≈∆
        MAGNIFICATION = 2, // ±∂¬ ≈∆
        IRON = 3, // ∏÷Ã˙≈∆
        STONE = 4, //  ØÕ∑≈∆
        SUITS = 5 // ÕÚƒ‹≈∆
    }
}