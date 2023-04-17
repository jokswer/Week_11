using UnityEngine;

public enum ItemType
{
    Empty,
    Barrel,
    Stone,
    Box,
    Dynamit,
    Star,
    Ball,
}

public class Item : MonoBehaviour
{
    [SerializeField] private ItemType _type;
    public ItemType Type => _type;
}