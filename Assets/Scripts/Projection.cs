using TMPro;
using UnityEngine;

public class Projection : MonoBehaviour
{
    [SerializeField] private Renderer _renderer;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Transform _visualTransform;

    public void Setup(Material material, string level, float radius)
    {
        _renderer.material = material;
        _text.text = level;
        _visualTransform.localScale = Vector3.one * (radius * 2f);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }
}