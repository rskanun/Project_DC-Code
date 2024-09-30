using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour
{
    [SerializeField]
    private NpcData npc;

    public bool isInteractive => npc.Lines != null;

    public int getID()
    {
        return npc.Id;
    }

    public List<Line> getLines()
    {
        return npc.Lines;
    }
}