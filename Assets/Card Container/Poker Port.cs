using poker;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokerPort : CardPort<PokerCard>
{
    public override void SetCard(Card card)
    {
        this.card = card;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
