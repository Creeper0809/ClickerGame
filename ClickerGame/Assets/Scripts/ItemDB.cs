using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ItemDB : MonoBehaviour
{
    [SerializeField]
    private List<Card> database = new List<Card>();
    List<Dictionary<string, object>> data;
    private void Awake()
    {
        data = CSVReader.Read("CardData");
        ConstructItemDataBase();
    }
    public int dataCount()
    {
        return database.Count;
    }
    public Card FindItembyID(int id)
    {
        for (int i = 0; i < database.Count; i++)
        {
            if (database[i].id == id)
            {
                return database[i];
            }
        }
        return null;
    }
    void ConstructItemDataBase()
    {
        for (int i = 0; i < data.Count; i++)
        {
            int id = (int)data[i]["아이디"];
            int attack = (int)data[i]["사용마나"];
            string name = (string)data[i]["이름"];
            string explain = (string)data[i]["설명"];
            float per = (int)data[i]["확률"];
            Debug.Log(id + name + attack + explain + per);
            database.Add(new Card(id, name, attack, explain, per));
        }

    }
}
public class Item
{
    public int ID { get; set; }
    public string name { get; set; }
    public int damage { get; set; }
    public int HP { get; set; }
    public bool stackable { get; set; }
    public Sprite sprite { get; set; }
    public ItemType itemType { get; set; }
    public Item(int ID, string name, int damage, int HP, bool stackable, ItemType itemtype)
    {
        this.ID = ID;
        this.name = name;
        this.damage = damage;
        this.HP = HP;
        this.stackable = stackable;
        this.sprite = Resources.Load<Sprite>("Sprites/items/" + name);
        this.itemType = itemtype;
    }
    public Item()
    {
        this.ID = -1;
    }
}
public enum ItemType
{
    EQUIPMENT,
    POTION,
    ETC
}
public class Card
{
    public int id;
    public string name;
    public int mana;
    public string explain;
    public Sprite sprite;
    public float percentage;
    public Card(int id, string name,int mana, string explain, float percentage)
    {
        this.id = id;
        this.name = name;
        this.mana = mana;
        this.explain = explain;
        this.sprite = Resources.Load<Sprite>("Sprite/Card/" + name);
        this.percentage = percentage;
    }
}

