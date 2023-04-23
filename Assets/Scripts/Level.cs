using System;
using UnityEngine;

[Serializable]
public struct Task
{
    public ItemType Type;
    public int Number;
    public int Level;
}

public class Level : MonoBehaviour
{
    public int NumberOfballs = 50;
    public int MaxCreatedBallLevel = 1;
    public Task[] Tasks;
}