using poker;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.UIElements;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine.U2D;
using static UnityEditor.Progress;

public class PokerEngine : MonoBehaviour
{
    private DeckEngine deck = new DeckEngine();
    private List<PokerCard> cards = new List<PokerCard>();
    public int playCount = 4;
    public int discardCount = 3;
    public List<PokerCard> handCards = new List<PokerCard>();
    public GameObject pokerContainer;
    public GameObject pokerPrefab;
    public SpriteAtlas pokerAtlas;
    public SpriteAtlas pokerBgAtlas;

    public List<PokerCard> GetCards()
    {
        return cards;
    }

    private void initCards()
    {
        for(int i=0; i<deck.pokerRecorder.Count; i++)
        {
            for(int count=0; count < deck.pokerRecorder[i]; count++)
                cards.Add(PokerCard.int2poker(i));
        }
    }

    // 发牌
    private List<PokerCard> dealCards(int count)
    {
        List<PokerCard> result = new List<PokerCard>();
        for(int i=0; i<count; i++)
        {
            result.Add(cards.Last());
            cards.RemoveAt(cards.Count - 1);
        }
        return result;
    }

    public List<PokerCard> playCards(int[] indexs)
    {
        List<PokerCard> result = new List<PokerCard>();
        foreach (int index in indexs)
        {
            result.Add(handCards[index]);
            handCards.RemoveAt(index);
        }
        return result;
    }

    // Fisher-Yates Shuffle 算法
    public static void Shuffle<T>(List<T> list)
    {
        System.Random rand = new System.Random();
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rand.Next(n + 1);  // 生成一个0到n的随机索引
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    public void showDeck()
    {
        float xStep = 1f;
        float yStep = -2f;
        List<List<PokerCard>> arrangeVector = Enumerable.Range(0, 4)
            .Select(_ => new List<PokerCard>())
            .ToList();
        foreach (var card in cards)
        {
            if (card.pokerType != PokerType.NONE) continue;
            Debug.Log($"{card.toString()}");
            arrangeVector[(int)card.suit].Add(card);
        }
        for (int suit = 0; suit < PokerCard.SuitCount; suit++)
        {
            var list = arrangeVector[suit];
            list.Sort((a, b) => PokerCard.poker2int(b).CompareTo(PokerCard.poker2int(a)));
            Debug.Log($"length: {list.Count}");
            for (int i=0; i<list.Count; i++)
            {
                var item = list[i];
                GameObject pokerObj = getPokerCardObj(item);
                pokerObj.transform.localPosition = new Vector3(i * xStep, suit * yStep, 0);
                pokerObj.transform.SetParent(pokerContainer.transform, false);

                SpriteRenderer[] sprites = pokerObj.GetComponentsInChildren<SpriteRenderer>();
                for(int spriteIndex = 0; spriteIndex < sprites.Length; spriteIndex++)
                {
                    var sprite = sprites[spriteIndex];
                    sprite.sortingOrder = i * list.Count + (sprites.Length - spriteIndex);
                }
            }
        }
    }

    public void ShowContainer()
    {

    }

    public GameObject getPokerCardObj(PokerCard pokerCard)
    {
        GameObject pokerObj = Instantiate(pokerPrefab);
        SpriteRenderer[] sprites = pokerObj.GetComponentsInChildren<SpriteRenderer>();
        String pokerSpriteName = "8BitDeck_" + ((int)pokerCard.suit * 13 + (int)pokerCard.number);
        String bgSpriteName = "Enhancers_" + "1";
        Sprite pokerSprite = pokerAtlas.GetSprite(pokerSpriteName);
        Sprite bgSprite = pokerBgAtlas.GetSprite(bgSpriteName);
        if (pokerSprite != null)
        {
            // 获取当前物体的 SpriteRenderer 并设置加载的 Sprite
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            if (sprites[0] != null)
            {
                sprites[0].sprite = pokerSprite;
            }
            else
            {
                Debug.LogError("未找到 SpriteRenderer 组件！");
            }
        }
        else
        {
            Debug.LogError($"无法加载 Sprite: {pokerSpriteName}");
        }
        sprites[0].sprite = pokerSprite;
        sprites[1].sprite = bgSprite;
        return pokerObj;
    }

    // Start is called before the first frame update
    void Start()
    {
        initCards();
        Shuffle(cards);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
