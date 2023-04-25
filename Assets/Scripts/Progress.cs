using SaveSystem;
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
        Load();
    }

    public void SetLevel(int level)
    {
        _level = level;
        Save();
    }

    public void AddCoins(int value)
    {
        _coins += value;
        Save();
    }

    private void Save()
    {
        SaveSystemService.Save(this);
    }

    private void Load()
    {
        var data = SaveSystemService.Load();

        if (data != null)
        {
            _coins = data.Coins;
            _level = data.Level;
        }
        else
        {
            _coins = 0;
            _level = 1;
        }
    }
}