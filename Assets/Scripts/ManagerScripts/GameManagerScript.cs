using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
//using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.Rendering;

using DG.Tweening;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript instance;
    public static AttackAim attackAim;

    public int handSize = 5;

    public List<Card> allCards = new List<Card>();

    public Queue<Card> deck = new Queue<Card>();
    public List<Card> discard = new List<Card>();

    public List<Transform> hand = new List<Transform>();

    public GameObject cardPrefab;

    public GameObject attackPrefab;
    public Transform heroTransform;
    public Transform transform;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;

        allCards.AddRange(Resources.LoadAll<Card>("Cards"));

        while (allCards.Count > 0)
        {
            int idx = Random.Range(0, allCards.Count);

            Debug.Log("adding " + allCards[idx] + " to deck");

            deck.Enqueue(allCards[idx]);
            allCards.RemoveAt(idx);
        }
    }

    private void Start()
    {
        StartCoroutine(DealHand());

        currentTurn = TurnState.Villain;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentTurn)
        {
            case TurnState.Villain:
                PlayVillainTurnStuff();
                break;
        }

       if(currentTurn != TurnState.Villain)
       {
            if (heldCard != null)
            {
                heldCard = null;
                UpdateHandPositions();
            }   
       }
    }

    public void PlayVillainTurnStuff()
    {
        CheckMousedCards();

        if (Input.GetMouseButtonDown(0))
        {
            PickUpCard();
            //StartCoroutine(DealHand());
        }

        if (Input.GetMouseButtonUp(0))
        {
            PutDownCard();
        }

        if (Input.GetMouseButtonDown(1))
        {
            heldCard = null;
            UpdateHandPositions();

        }

        if (heldCard != null)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            heldCard.position = Vector2.Lerp(heldCard.position, mousePos, Time.deltaTime * 20);
        }
    }

    public float timeBetweenDealtCards = .1f;

    public IEnumerator DealHand()
    {
        while(hand.Count<handSize)
        {
            DealCard();
            yield return new WaitForSeconds(timeBetweenDealtCards);
        }

        EnergyManager.instance.MaxEnergy();
        currentTurn = TurnState.Villain;
        yield return null;
    }

    public MyVoidDelegate cardDealDel;

    public void DealCard()
    {
        Debug.Log("Deal Card");
        //if (hand.Count >= handSize) return;
        if (deck.Count <= 0) ShuffleDiscard();

        cardDealDel?.Invoke();

        Transform dealtCard = GameObject.Instantiate(cardPrefab, deckT.position, Quaternion.Euler(0,0,90)).transform;

        Debug.Log("dealing " + deck.Peek());
        dealtCard.GetComponent<CardDisplay>().setCard = deck.Peek();
        dealtCard.GetComponent<CardDisplay>().CardUpdate(deck.Dequeue());

        hand.Add(dealtCard);
        UpdateHandPositions();
    }

    public void ShuffleDiscard()
    {
        Debug.Log("shuffling");
        while(discard.Count>0)
        {
            int randomIdx = Random.Range(0, discard.Count);
            deck.Enqueue(discard[randomIdx]);
            discard.RemoveAt(randomIdx);
        }
    }

    public Transform deckT, discardT;
    public Transform handOrigin;

    public int cardSortLayer = 10;

    public float cardAnimSpeed = .2f;
    public float cardAngleSpacing = 20;
    public float cardDistanceFromOrigin = 10;

    Vector3 cardScale = Vector3.one;
    Vector3 zoomedCardScale = Vector3.one*1.3f;

    int handCount;
    float arc;
    float arcStart;

    public void UpdateHandPositions()
    {
        handCount = hand.Count;

        arc = cardAngleSpacing * (handCount - 1);
        arcStart = -arc / 2;

        for (int i=0; i < handCount; i++)
        {
            hand[i].GetComponent<SortingGroup>().sortingOrder = cardSortLayer;
            //
            float rad = arcStart + (cardAngleSpacing * i) + 90;
            rad *= Mathf.Deg2Rad;
            Vector3 dir = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad)).normalized;

            DOTween.Kill(hand[i]);
            hand[i].DORotate(new Vector3(0, 0, arcStart + (cardAngleSpacing * i)), cardAnimSpeed).SetEase(Ease.OutQuad);
            hand[i].DOMove(handOrigin.position + dir * cardDistanceFromOrigin, cardAnimSpeed).SetEase(Ease.OutQuad);

            if (hand[i].localScale != cardScale)
                hand[i].DOScale(cardScale, cardAnimSpeed);
        }
    }

    public void ResetCardPositionInHand(int i)
    {
        handCount = hand.Count;

        arc = cardAngleSpacing * (handCount - 1);
        arcStart = -arc / 2;

        hand[i].GetComponent<SortingGroup>().sortingOrder = cardSortLayer;
        //
        float rad = arcStart + (cardAngleSpacing * i) + 90;
        rad *= Mathf.Deg2Rad;
        Vector3 dir = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad)).normalized;

        DOTween.Kill(hand[i]);
        hand[i].DORotate(new Vector3(0, 0, arcStart + (cardAngleSpacing * i)), cardAnimSpeed).SetEase(Ease.OutQuad);
        hand[i].DOMove(handOrigin.position + dir * cardDistanceFromOrigin, cardAnimSpeed).SetEase(Ease.OutQuad);

        if (hand[i].localScale != cardScale)
            hand[i].DOScale(cardScale, cardAnimSpeed);
    }

    public List<Collider2D> hoveredCards = new List<Collider2D>();
    public LayerMask cardMask;
    public Collider2D activeCard;

    public void CheckMousedCards()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        hoveredCards.Clear();
        hoveredCards.AddRange(Physics2D.OverlapPointAll(mousePos, cardMask.value));

        if (heldCard!=null) return;

        if (activeCard == null && hoveredCards.Count > 0)
        {
            activeCard = hoveredCards[0];
            StartHover(activeCard.transform);
        }
            
        if(activeCard==null) return;
        if(!hoveredCards.Contains(activeCard))
        {
            if (hand.Contains(activeCard.transform))
            {
                ResetCardPositionInHand(hand.IndexOf(activeCard.transform));
            }

            if (hoveredCards.Count > 0)
            {
                activeCard = hoveredCards[0];
                StartHover(activeCard.transform);
            }
            else
            {
                activeCard = null;
                UpdateHandPositions();
            }
        }
    }

    public void StartHover(Transform hoveredCard)
    {
        int idx = hand.IndexOf(hoveredCard);
        if (idx == -1) return;

        cardRifleDel.Invoke();
        hoveredCard.GetComponent<SortingGroup>().sortingOrder += 1;

        float arc = cardAngleSpacing * (hand.Count - 1);
        float arcStart = -arc / 2;
        float rad = arcStart + (cardAngleSpacing * idx) + 90;
        rad *= Mathf.Deg2Rad;
        Vector3 dir = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad)).normalized;

        DOTween.Kill(hand[idx]);
        hand[idx].DORotate(Vector3.zero, cardAnimSpeed/2).SetEase(Ease.OutQuad);
        hand[idx].DOMove(handOrigin.position + dir * cardDistanceFromOrigin*1.1f, cardAnimSpeed/2).SetEase(Ease.OutQuad);
    }


    ///PLAYING A CARD

    public Transform heldCard;
    public void PickUpCard()
    {
        if (activeCard == null)
        {
            Debug.Log("no card to pickup");
            return;
        }

        heldCard = activeCard.transform;
    }

    public void PutDownCard()
    {
        if(heldCard==null)
        {
            Debug.Log("no card to play");
            return;
        }

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Card card = heldCard.GetComponent<CardDisplay>().setCard;

        if (Input.mousePosition.y > Screen.height / 3
            && card.energyCost <= EnergyManager.instance.energy)
        {
            hand.Remove(heldCard);
            EnergyManager.instance.ChangeEnergy(-card.energyCost);
            PlayCard(card);

            var playedCard = heldCard;

            playedCard.DOShakeRotation(cardAnimSpeed);
            playedCard.DOShakePosition(cardAnimSpeed/2).SetLoops(2, LoopType.Yoyo);
            playedCard.DOShakeScale(cardAnimSpeed)
                .OnComplete(() => DiscardCard(playedCard)).SetDelay(cardAnimSpeed * .5f);
        }

        heldCard = null;
        UpdateHandPositions();
    }

    public void DiscardCard(Transform card2Discard)
    {
        card2Discard.DOKill();
        card2Discard.DOMove(discardT.position, cardAnimSpeed).SetEase(Ease.InSine);
        card2Discard.DOScale(Vector3.zero, cardAnimSpeed).SetEase(Ease.InSine);
        card2Discard.DORotate(new Vector3(0,0,-90),cardAnimSpeed).SetEase(Ease.InSine)
            .OnComplete(() => 
            {
                discard.Add(card2Discard.GetComponent<CardDisplay>().setCard);
                GameObject.Destroy(card2Discard.gameObject, .1f);
            });


    }

    public float villainHealMult =1f, villainAttackMult =1f;
    public float heroHealMult =1f, heroAttackMult=1f;
    public void PlayCard(Card card)
    {
        cardPlayDel.Invoke();
        Instantiate(attackPrefab, GameObject.Find("Hero").transform);
        attackAim = FindAnyObjectByType<AttackAim>();
        //do effect
        if (card.effects != null)
        {
            foreach (CardEffect effect in card.effects)
            {
                Debug.Log("doing an effect");
                //effect.DoEffect(card);
                attackAim.Attack(card);
            }
        }

        DialougeManagerScriptv2.instance.StartTextBubble(card);

        //HealthManagerScript.instance.HeroDamage((int)(card.damage*villainAttackMult));
        //HealthManagerScript.instance.VillainDamage(-(int)(card.healing*villainHealMult));

        //DialogueManagerScript.dialogueOption = (DialogueOption)card.cardType;
        //Debug.Log(DialogueManagerScript.dialogueOption + ", " + (DialogueOption)card.cardType);
        //DialogueManagerScript.instance.TriggerDialogue((DialogueOption)card.cardType);

        //if (CourageMetre.instance == null) return;
        //CourageMetre.instance.increaseCourage((int)card.braveryVal);
        //DramaMetre.instance.increaseDrama((int)card.dramaVal);
    }

    public void PlayHeroMove(Card card)
    {
        Debug.Log("hero uses " + card.name);
        HealthManagerScript.instance.VillainDamage((int)(card.damage*heroAttackMult));
        //HealthManagerScript.instance.HeroDamage(-(int)(card.healing*heroHealMult));
        DialogueManagerScript.dialogueOption = (DialogueOption)card.cardType;
        //DialogueManagerScript.instance.TriggerDialogue();

        if (CourageMetre.instance == null) return;
        CourageMetre.instance.increaseCourage((int)card.braveryVal);
        DramaMetre.instance.increaseDrama((int)card.dramaVal);
    }

    //TURNS
    public delegate void MyVoidDelegate();
    public MyVoidDelegate cardRifleDel;
    public MyVoidDelegate cardPickUpDel;
    public MyVoidDelegate cardPutDownDel;
    public MyVoidDelegate cardPlayDel;

    enum TurnState { Shuffle, Hero, Villain, Wait, Animating };
    TurnState currentTurn = TurnState.Shuffle;

    public Card[] HeroMoves;

    public void EmptyHand()
    {
        Transform[] tempHand = hand.ToArray();
        hand.Clear();

        foreach(Transform card in tempHand)
        {
            discard.Add(card.GetComponent<CardDisplay>().setCard);

            DOTween.Kill(card);
            card.DOMove(discardT.position, cardAnimSpeed);
            card.DORotate(new Vector3(0, 0, -90), cardAnimSpeed)
                .OnComplete(() => { GameObject.Destroy(card.gameObject, cardAnimSpeed); }); 
        }
    }

    public MyVoidDelegate endTurnDel;

    public void EndTurn()
    {
        if (currentTurn != TurnState.Villain) return;
        EnergyManager.instance.ClearEnergy();

        endTurnDel?.Invoke();

        Debug.Log("End Turn");
        EmptyHand();
        currentTurn = TurnState.Hero;
        HeroTurn();
    }

    public void HeroTurn()
    {
        if (HeroMoves.Length>0)
        {
            Card move = HeroMoves[Random.Range(0, HeroMoves.Length)];
            PlayHeroMove(move);
        }

        Debug.Log("Did Hero's Turn");
        currentTurn = TurnState.Shuffle;
        DealingTurn();
    }

    public void DealingTurn()
    {
        Debug.Log("Dealing turn");
        StartCoroutine(DealHand());
        //currentTurn = TurnState.Villain;
    }

    float endGameDelaySeconds = 2f;

    public async void EndGame()
    {
        currentTurn = TurnState.Wait;
        await Task.Delay((int)(endGameDelaySeconds * 1000));
        Debug.Log("End Game");

        var hMS = HealthManagerScript.instance;
        var cBar = Bars.courageInstance;
        var dBar = Bars.dramaInstance;

        GameStateSaver.instance
            .SaveValues(hMS.heroHealth,hMS.villainHealth, cBar.currentValue, dBar.currentValue);

        SceneManager.LoadScene("End Scene", LoadSceneMode.Single);
    }
}
