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
        ActiveItem fromItem;
        ActiveItem toItem;

        if (Mathf.Abs(itemA.transform.position.y - itemB.transform.position.y) > 0.2f)
        {
            if (itemA.transform.position.y > itemB.transform.position.y)
            {
                fromItem = itemA;
                toItem = itemB;
            }
            else
            {
                fromItem = itemB;
                toItem = itemA;
            }
        }
        else
        {
            if (itemA.Rigidbody.velocity.magnitude > itemB.Rigidbody.velocity.magnitude)
            {
                fromItem = itemA;
                toItem = itemB;
            }
            else
            {
                fromItem = itemB;
                toItem = itemA;
            }
        }

        StartCoroutine(CollapseProcess(fromItem, toItem));
    }

    private IEnumerator CollapseProcess(ActiveItem fromItem, ActiveItem toItem)
    {
        fromItem.Disable();

        if (fromItem.Type == ItemType.Ball || toItem.Type == ItemType.Ball)
        {
            var startPosition = fromItem.transform.position;

            for (float t = 0; t < 1f; t += Time.deltaTime / 0.08f)
            {
                fromItem.transform.position = Vector3.Lerp(startPosition, toItem.transform.position, t);
                yield return null;
            }

            fromItem.transform.position = toItem.transform.position;
        }

        if (fromItem.Type == ItemType.Ball && toItem.Type == ItemType.Ball)
        {
            fromItem.Die();
            toItem.DoEffect();
            ExplodeBall(toItem.transform.position, toItem.Radius + 0.15f);
        }
        else
        {
            if (fromItem.Type == ItemType.Ball)
            {
                fromItem.Die();
            }
            else
            {
                fromItem.DoEffect();
            }

            if (toItem.Type == ItemType.Ball)
            {
                toItem.Die();
            }
            else
            {
                toItem.DoEffect();
            }
        }
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