using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CardManager : Singleton<CardManager>
{
    [SerializeField]
    public List<CardObj> showingPlayerCard;
    [SerializeField]
    public List<Card> bagPlayerCard;
    [SerializeField]
    public List<Card> graveCard;
    public GameObject cardAmountText;
    public GameObject graveAmountText;
    public GameObject cardPrefab;
    public Transform cardspawn;
    public Transform cardRight;
    public Transform cardLeft;
    public Transform cardParent;
    ItemDB itemdb;
    public int playerMaxCard = 10;
    [SerializeField]
    float height = 0.5f;
    public float scaleAmount = 0.7f;
    float timer = 0;
    private void Start()
    {
        itemdb = GameObject.Find("GameManager").GetComponent<ItemDB>();
        showingPlayerCard = new List<CardObj>(7);
        graveCard = new List<Card>(30);
        bagPlayerCard = new List<Card>(30);
        //임시 카드 추가
        for (int i = 0; i < playerMaxCard; i++)
        {
            AddCard();
        }
    }
    private void Update()
    {
        timer += Time.deltaTime;
        IngameManager.Instance.UpdateCardDeckCount(cardAmountText, graveAmountText, bagPlayerCard.Count, graveCard.Count);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnCard();
        }
        //임시 카드 뽑기
        if (timer > 2)
        {
            SpawnCard();
            timer = 0;
        }
        if (bagPlayerCard.Count == 0 && graveCard.Count > 0 && showingPlayerCard.Count == 0)
        {
            ResetCardGraveToBag();
        }
    }
    public void ResetCardGraveToBag()
    {
        for (int i = 0; i < graveCard.Count; i++)
        {
            bagPlayerCard.Add(graveCard[i]);
        }
        graveCard.Clear();
    }
    public void AddCard()
    {
        if (showingPlayerCard.Count + graveCard.Count + bagPlayerCard.Count >= playerMaxCard)
        {
            Debug.Log("총제한걸림");
            return;
        }
        else
        {
            bagPlayerCard.Add(itemdb.FindItembyID(Random.RandomRange(0, itemdb.dataCount())));
        }
    }
    public void SpawnCard()
    {
        if (showingPlayerCard.Count >= 5)
        {
            return;
        }
        var cardObj = ObjectPoolManager.Instance.getPool("Card", cardParent);
        cardObj.transform.position = cardspawn.position;
        cardObj.transform.localScale = Vector3.one * 0.7f;
        var card = cardObj.GetComponent<CardObj>();
        if (bagPlayerCard.Count == 0)
        {
            if (showingPlayerCard.Count + graveCard.Count + bagPlayerCard.Count >= playerMaxCard)
            {
                Debug.Log("총제한걸림");
                ObjectPoolManager.Instance.Despawn(cardObj);
                return;
            }
            card.SetUpCard(itemdb.FindItembyID(Random.RandomRange(0, itemdb.dataCount())), true);
        }
        else
        {
            card.SetUpCard(bagPlayerCard[0], true);
            bagPlayerCard.Remove(bagPlayerCard[0]);
        }
        showingPlayerCard.Add(card);
        for (int i = 0; i < showingPlayerCard.Count; i++)
        {
            showingPlayerCard[i].GetComponent<Order>().SetOriginOrder(i);
        }
        CardAllignment();
        timer = 0;
    }
    public void CardAllignment()
    {
        List<PRS> originCardPrss = new List<PRS>();
        originCardPrss = RoundAlignment(cardLeft, cardRight, showingPlayerCard.Count, height, Vector3.one * scaleAmount);
        for (int i = 0; i < showingPlayerCard.Count; i++)
        {
            showingPlayerCard[i].originPrs = originCardPrss[i];
            showingPlayerCard[i].MoveTransform(showingPlayerCard[i].originPrs, true, scaleAmount);
        }
    }
    public void DeleteCard(CardObj cardObj = null)
    {
        ObjectPoolManager.Instance.Despawn(cardObj.gameObject);
        graveCard.Add(cardObj.cardData);
        showingPlayerCard.Remove(cardObj);
        CardAllignment();
    }
    List<PRS> RoundAlignment(Transform left, Transform right, int count, float height, Vector3 scale)
    {
        float[] objLerp = new float[count];
        List<PRS> result = new List<PRS>(count);
        switch (count)
        {
            case 1: objLerp = new float[] { 0.5f }; break;
            case 2: objLerp = new float[] { 0.2f, 0.73f }; break;
            case 3: objLerp = new float[] { 0.1f, 0.5f, 0.9f }; break;
            default:
                float interval = 1f / (count - 1);
                for (int i = 0; i < count; i++)
                {
                    objLerp[i] = interval * i;
                }
                break;
        }
        for (int i = 0; i < count; i++)
        {
            var targetPos = Vector3.Lerp(left.position, right.position, objLerp[i]);
            var targetRot = Quaternion.identity;
            if (count >= 4)
            {
                float curve = Mathf.Sqrt(Mathf.Pow(-0.5f, 2) - Mathf.Pow(objLerp[i] - 0.5f, 2));
                targetPos.y += curve * height;
                targetRot = Quaternion.Slerp(left.rotation, right.rotation, objLerp[i]);
            }
            result.Add(new PRS(targetPos, targetRot, scale));
        }
        return result;
    }

}
