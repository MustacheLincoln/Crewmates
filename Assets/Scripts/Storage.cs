using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Storage : MonoBehaviour
{
    public int items;
    public int incomingItems;
    public int maxItems;

    public abstract void Ready();
}
