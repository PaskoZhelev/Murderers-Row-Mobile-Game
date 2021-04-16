using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameHandler : MonoBehaviour
{
    [HideInInspector]
    public Card clickedCard;
    [HideInInspector]
    public Card randomCard;
    public Image clickedCardImage;
    public Image randomCardImage;
    public Sprite emptyClickedCardSprite;
    public GameObject board;

    public Card[] cardPrefabs;
    [HideInInspector]
    //all cards in play
    public List<Card> allCards;

    [HideInInspector]
    //all 18 cards in random order
    public List<Card> randomizedCards;
    [HideInInspector]
    public int currentRandomCardIndex = Constants.STARTING_RANDOM_CARD_INDEX;


    // Start is called before the first frame update
    void Start()
    {
        GenerateCards();
    }

    public void GenerateCards()
    {
        List<Card> cards = randomizeCards();
        for (int i = 0; i < Constants.STARTING_NUM_CARDS; i++)
        {
            Card card = Instantiate(cards[i], gameObject.transform.position, Quaternion.identity, board.transform);
        }
    }

    private List<Card> randomizeCards()
    {
        randomizedCards = new List<Card>(cardPrefabs);
        randomizedCards = randomizedCards.OrderBy(a => Guid.NewGuid()).ToList();
        return randomizedCards;
    }

    public void setClickedCardImage(Card card)
    {
        clickedCard = card;
        setClickedCardImage();
    }

    public void setClickedCardImage()
    {
        clickedCardImage.sprite = clickedCard.currentImage.sprite;
    }

    public void ClickActivateButton()
    {
        if(null != clickedCard)
        {
            clickedCard.StartFlip();
        }
    }
    public void ClickDrawRandomButton()
    {
        if(null != randomCard)
        {
            randomCard.Discard();
        }

        randomCard = Instantiate(randomizedCards[currentRandomCardIndex], randomCardImage.transform.position, Quaternion.identity, randomCardImage.gameObject.transform);
        randomCardImage.sprite = randomCard.currentImage.sprite;
        if (currentRandomCardIndex < Constants.MAX_RANDOM_CARD_INDEX)
        {
            currentRandomCardIndex++;
        } else
        {
            currentRandomCardIndex = Constants.STARTING_RANDOM_CARD_INDEX;
        }
    }

    public void ClickDiscardButton()
    {
        if (null != clickedCard)
        {
            clickedCard.Discard();
            clickedCard = null;
            clickedCardImage.sprite = emptyClickedCardSprite;
        }
    }

    /* SINGLETON */
    private static GameHandler instance;
    public static GameHandler Instance { get => instance; set => instance = value; }

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

        //DontDestroyOnLoad(gameObject);

    }
}
