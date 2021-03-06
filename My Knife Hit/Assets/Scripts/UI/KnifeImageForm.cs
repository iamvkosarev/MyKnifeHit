﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KnifeHit.UI
{
    public class KnifeImageForm : MonoBehaviour
    {
        [SerializeField] private float _destroyDelay = 2f;
        private Animator _animator;
        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void StartDestroy()
        {
            transform.parent = transform.parent.transform.parent;
            _animator.SetTrigger("close");
            Destroy(gameObject, _destroyDelay);
        }

    }

    

}
