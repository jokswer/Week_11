using System;
using UnityEngine;

public class Progress : MonoBehaviour
{
    private static Progress _instance;
    [SerializeField] private int _coins;
    [SerializeField] private int _level;

    public static Progress Instance => _instance;
    public int Level => _level;
    public int Coins => _coins;

    private void Awake()
    {
        if (_instance)
            Destroy(gameObject);
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void SetLevel(int level)
    {
        _level = level;
    }

    public void AddCoins(int value)
    {
        _coins += value;
    }
}