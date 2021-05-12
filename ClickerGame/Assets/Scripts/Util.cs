using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util
{
    public static Quaternion QI => Quaternion.identity;
}
public class PRS
{
    public Vector3 pos;
    public Quaternion rotation;
    public Vector3 scale;
    public PRS(Vector3 pos,Quaternion rot,Vector3 scale)
    {
        this.pos = pos;
        this.rotation = rot;
        this.scale = scale;
    }
}