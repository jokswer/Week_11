using TMPro;
using UnityEngine;

public class ActiveItem : MonoBehaviour
{
    [SerializeField] private int _level;
    [SerializeField] private float _radius;

    [SerializeField] private TMP_Text _levelText;
    [SerializeField] private Transform _visualTransform;
    [SerializeField] private SphereCollider _collider;
    [SerializeField] private SphereCollider _trigger;

    private float _minRadius = 0.4f;
    private float _maxRadius = 0.7f;
    private float _maxRadiusLevel = 10f;

    [ContextMenu("IncreaseLevel")]
    public void IncreaseLevel()
    {
        _level++;
        SetLevel(_level);
    }

    public virtual void SetLevel(int level)
    {
        _level = level;
        var number = (int)Mathf.Pow(2, _level + 1);
        SetLevelText(number);
        SetRadius(number);
    }

    private void SetLevelText(int value)
    {
        _levelText.text = value.ToString();
    }

    private void SetRadius(int level)
    {
        _radius = Mathf.Lerp(_minRadius, _maxRadius, level / _maxRadiusLevel);
        _visualTransform.localScale = Vector3.one * _radius * 2f;
        _collider.radius = _radius;
        _trigger.radius = _radius + 0.1f;
    }
}