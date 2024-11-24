using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;

namespace poker
{
    public class DeckEngine
    {
        public List<int> pokerRecorder;

        public DeckEngine()
        {
            int[] ints = new int[PokerCard.NumberCount * PokerCard.SuitCount * PokerCard.TypeCount];
            for (int i = 0; i < ints.Length; i++)
            {
                ints[i] = 1; 
            }
            initPokerRecorder(ints);
        }

        public void initPokerRecorder(int[] ints)
        {
            pokerRecorder = new List<int>(ints);
        }

        // Start is called before the first frame update
        void Start()
        {
           
        }

        // Update is called once per frame
        void Update()
        {

        }

        public struct PokerCounter
        {
            PokerCard card;
            int count;
        }
    }
}