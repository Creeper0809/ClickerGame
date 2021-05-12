using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class CardObj : MonoBehaviour, IBeginDragHandler, IDragHandler,IEndDragHandler
{
    [SerializeField] Image card;
    [SerializeField] Image character;
    [SerializeField] TMP_Text TMPName;
    [SerializeField] TMP_Text TMPMana;
    [SerializeField] TMP_Text TMPExplain;
    [SerializeField] Sprite CardFront;
    [SerializeField] Sprite CardBack;
    public Card cardData;
    public Skill skill;
    bool isFront;
    public PRS originPrs;
    bool isDraging;
    public void SetUpCard(Card cardData, bool isFront)
    {
        this.cardData = cardData;
        this.isFront = isFront;
        skill = new Skill();
        if (isFront)
        {
            character.sprite = cardData.sprite;
            TMPName.text = cardData.name;
            TMPMana.text = cardData.mana.ToString();
            TMPExplain.text = cardData.explain;
        }
        else
        {
            card.sprite = CardBack;
            TMPName.text = "";
            TMPMana.text = "";
            TMPExplain.text = "";
        }
    }
    public void MoveTransform(PRS prs, bool useDotween, float dotweenTime = 0)
    {
        if (useDotween)
        {
            transform.DOMove(prs.pos, dotweenTime);
            transform.DOLocalRotateQuaternion(prs.rotation, dotweenTime);
            transform.DOScale(prs.scale, dotweenTime);
        }
        else
        {
            transform.position = prs.pos;
            transform.rotation = prs.rotation;
            transform.localScale = prs.scale;
        }
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        isDraging = true;
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (DetectSpawnArea())
        {
            Debug.Log("에리어 존재");
        }
        this.transform.position = Input.mousePosition;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        if (DetectSpawnArea() && cardData.mana <= Player.Instance.getNowMp())
        {
            skill.UseSkill();
            Player.Instance.UseMana(cardData.mana);
            CardManager.Instance.DeleteCard(this);
        }
        else
        {
            this.transform.position = originPrs.pos;
        }
        isDraging = false;
        
    }
    public bool DetectSpawnArea()
    {
        RaycastHit2D[] ray = Physics2D.RaycastAll(Input.mousePosition, Vector3.forward, Mathf.Infinity);
        if (Array.Exists(ray, x => x.collider.gameObject.layer == LayerMask.NameToLayer("CardSpawnArea")))
            return true;
        return false;
    }
}
