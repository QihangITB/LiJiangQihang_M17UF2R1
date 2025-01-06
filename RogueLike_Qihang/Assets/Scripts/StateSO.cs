using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateSO : ScriptableObject
{
    public List<StateSO> StatesToGo;
    public abstract void OnStateEnter(BomberController bomber);
    public abstract void OnStateUpdate(BomberController bomber);
    public abstract void OnStateExit(BomberController bomber);
}

