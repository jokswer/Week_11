using System;
using System.Collections;
using PassiveItems;
using UnityEngine;

namespace ActiveItems
{
    public class Dynamit : ActiveItem
    {
        [Header("Dynamit")] 
        [SerializeField] private float _affectRadius = 1.5f;
        [SerializeField] private float _forceValue = 1000f;
        [SerializeField] private GameObject _affectArea;
        [SerializeField] private GameObject _effectPrefab;

        protected override void Start()
        {
            base.Start();
            _affectArea.SetActive(false);
        }

        private IEnumerator AffectProcess()
        {
            _affectArea.SetActive(true);
            _animator.enabled = true;
            yield return new WaitForSeconds(1f);
            Instantiate(_effectPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
            UseForce();
        }

        private void UseForce()
        {
            var colliders = Physics.OverlapSphere(transform.position, _affectRadius);

            for (var i = 0; i < colliders.Length; i++)
            {
                var rb = colliders[i].attachedRigidbody;

                if (rb)
                {
                    var formTo = (rb.transform.position - transform.position).normalized;
                    rb.AddForce(formTo * _forceValue + Vector3.up * (_forceValue * 0.5f));

                    if (rb.TryGetComponent<PassiveItem>(out var item))
                    {
                        item.OnAffect();
                    }
                }
            }
        }

        private void OnValidate()
        {
            _affectArea.transform.localScale = Vector3.one * (_affectRadius * 2f);
        }

        public override void DoEffect()
        {
            base.DoEffect();
            StartCoroutine(AffectProcess());
        }
    }
}