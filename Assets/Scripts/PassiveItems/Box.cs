using System;
using UnityEngine;

namespace PassiveItems
{
    public class Box : PassiveItem
    {
        [Range(0, 2)] 
        [SerializeField] private int _health = 1;
        [SerializeField] private GameObject[] _levels;
        [SerializeField] private GameObject _breackEffect;
        [SerializeField] private Animator _animator;

        private void Start()
        {
            SetHealth(_health);
        }

        public override void OnAffect()
        {
            base.OnAffect();
            _health -= 1;
            Instantiate(_breackEffect, transform.position, Quaternion.Euler(-90f, 0, 0));
            _animator.SetTrigger("Shake");

            if (_health < 0)
            {
                Die();
            }
            else
            {
                SetHealth(_health);
            }
        }

        private void Die()
        {
            Destroy(gameObject);
        }

        private void SetHealth(int health)
        {
            for (int i = 0; i < _levels.Length; i++)
            {
                _levels[i].SetActive(i <= health);
            }
        }
    }
}