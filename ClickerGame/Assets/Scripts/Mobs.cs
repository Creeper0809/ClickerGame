using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Mobs : Character
{
    protected int deadMoney;
    public GameObject Player;
    protected float gracePeriod = 0.1f;
    protected float gracePeriodTimer;
}
