using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KnifeHit.UI
{
    public class KnifeImageForm : MonoBehaviour
    {
        private Animator _animator;
        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void StartDestroy()
        {
            transform.parent = transform.parent.transform.parent;
            _animator.SetTrigger("close");
        }

        public void Destroy()
        {
            Destroy(gameObject);
    }
    }

    

}
