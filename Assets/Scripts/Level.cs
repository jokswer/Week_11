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
    public static Level Instance => _instance;
    public Task[] Tasks;
    public int NumberOfBalls => _numberOfBalls;
    public int MaxCreatedLevel => _maxCreatedBallLevel;
    

    private static Level _instance;
    [SerializeField] private int _maxCreatedBallLevel = 1;
    [SerializeField] private int _numberOfBalls = 50;

    private void Awake()
    {
        if (_instance)
            Destroy(gameObject);
        else
            _instance = this;
    }
}