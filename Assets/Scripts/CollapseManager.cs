using System.Collections;
using UnityEngine;

public class CollapseManager : MonoBehaviour
{
    private static CollapseManager _instance;
    public static CollapseManager Instance => _instance;

    private void Awake()
    {
        if (!_instance)
        {
            _instance = this;
        }
    }

    public void Collapse(ActiveItem itemA, ActiveItem itemB)
    {
        StartCoroutine(CollapseProcess(itemA, itemB));
    }

    private IEnumerator CollapseProcess(ActiveItem itemA, ActiveItem itemB)
    {
        itemA.Disable();
        
        var startPosition = itemA.transform.position;

        for (float t = 0; t < 1f; t += Time.deltaTime / 0.08f)
        {
            itemA.transform.position = Vector3.Lerp(startPosition, itemB.transform.position, t);
            yield return null;
        }

        itemA.transform.position = itemB.transform.position;
        itemA.Die();
        itemB.IncreaseLevel();
    }
}