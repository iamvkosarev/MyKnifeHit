using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KnifeHit.UI
{
    public class LoseCanvas : MonoBehaviour
    {
        private Animator _animator;
        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            _animator.SetTrigger("open");

        }
    }
}

