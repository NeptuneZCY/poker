using poker;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CardContainer : MonoBehaviour
{
    private List<Card> cards;
    private Dictionary<Card, GameObject> cardToObjectMap = new Dictionary<Card, GameObject>();
    public GameObject pokerPort;
    public GameObject pokerEngine;

    public void setCards(List<Card> newCards)
    {
        cards = newCards;
        foreach (var card in cards)
        {
            GameObject portObj = card switch
            {
                PokerCard poker => Instantiate(pokerPort),
                _ => new GameObject()
            };
            cardToObjectMap.Add(card, portObj);
            portObj.transform.SetParent(transform);
            var allPorts = portObj.GetComponentsInChildren<CardPort<Card>>();
            foreach(var port in allPorts)
            {
                port.SetCard(card);
            }
            GameObject cardObj = card switch
            {
                PokerCard poker => pokerEngine.GetComponent<PokerEngine>().getPokerCardObj(poker),
                _ => new GameObject()
            };
            cardObj.transform.SetParent(portObj.transform);
        }
    }

    public void showContainer()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void arrangeCards()
    {

    }
}
