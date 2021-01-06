using UnityEngine;
using System;
using KnifeHit.Items.Log;
using KnifeHit.Items.Apple;
using KnifeHit.Core;
using KnifeHit.Movement;

namespace KnifeHit.Items.Knife
{
    public enum KnifeCollisionType
    {
        Log,
        Knife
    }
    public class OnKnifeCollisionEventArgs : EventArgs
    {
        public KnifeCollisionType knifeCollisionType;

        public OnKnifeCollisionEventArgs(KnifeCollisionType knifeCollisionType)
        {
            this.knifeCollisionType = knifeCollisionType;
        }
    }
    public class KnifeObj : MonoBehaviour
    {
        [SerializeField] private bool _canReact = true;
        public event EventHandler<OnKnifeCollisionEventArgs> OnKnifeCollision;
        private Mover _mover;
        private Rotator _rotator;
        private Animator _animator;

        private void Awake()
        {
            _mover = GetComponent<Mover>();
            _rotator = GetComponent<Rotator>();
            _animator = GetComponent<Animator>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!_canReact) { return; }
            _canReact = false;


            LogObj log = collision.gameObject.GetComponent<LogObj>();
            AppleObj apple = collision.gameObject.GetComponent<AppleObj>();
            if (collision.gameObject.GetComponent<Mover>())
            {

                GameController.instance.LoadLoseCanvas();
                SetPostPunchMovement();
                if (OnKnifeCollision != null)
                {
                    OnKnifeCollision.Invoke(this, new OnKnifeCollisionEventArgs(KnifeCollisionType.Knife));
                }
            }
            else if (log != null)
            {
                GameController.instance.AddHitPoint();
                log.ConnectObject(transform);
                log.Hit();
                _mover.SetVelocity(new Vector2(0, 0));
            }
            else if(apple != null)
            {
                GameController.instance.AddApplePoint();
                apple.DestroyVFX();
                _canReact = true;
            }
        }
        public void SetDestroyTime(float time)
        {
            _animator.SetTrigger("close");
            Destroy(gameObject, time);
        }
        public void MakeFree()
        {
            transform.parent = null;
            GetComponent<PolygonCollider2D>().enabled = false;
        }
        private void SetPostPunchMovement()
        {
            _mover.SetVelocity(new Vector2(UnityEngine.Random.Range(-2f,2f), 
                UnityEngine.Random.Range(-1f, -5f)));
            _mover.SwitchRigidbodyType(RigidbodyType2D.Dynamic);
            _rotator.SetRotationSpeed(UnityEngine.Random.Range(300f,700f));
            _rotator.SetRotationSide(UnityEngine.Random.Range(0f, 1f) < 0.5f);
        }
    }

}