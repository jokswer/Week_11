using System.Collections;
using ActiveItems;
using PassiveItems;
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

        ExplodeBall(itemB.transform.position, itemB.Radius + 0.15f);
    }

    private void ExplodeBall(Vector3 position, float radius)
    {
        var colliders = Physics.OverlapSphere(position, radius);

        for (var i = 0; i < colliders.Length; i++)
        {
            var item = colliders[i].GetComponent<PassiveItem>();

            var rb = colliders[i].attachedRigidbody;

            if (rb)
            {
                item = rb.GetComponent<PassiveItem>();
            }

            if (item)
            {
                item.OnAffect();
            }
        }
    }
}