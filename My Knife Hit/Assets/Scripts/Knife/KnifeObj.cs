using UnityEngine;
using System;
using KnifeHit.Log;
using KnifeHit.Core;
using KnifeHit.Movement;

namespace KnifeHit.Knife
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
        public event EventHandler<OnKnifeCollisionEventArgs> OnKnifeCollision;
        private Mover _mover;
        private Rotator _rotator;
        private bool _canReact = true;

        private void Awake()
        {
            _mover = GetComponent<Mover>();
            _rotator = GetComponent<Rotator>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!_canReact) { return; }
            _canReact = false;


            LogObj log = collision.gameObject.GetComponent<LogObj>();
            if (collision.gameObject.GetComponent<KnifeObj>())
            {
                GameController.instance.RestartGame();
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
                _mover.SetVelocity(new Vector2(0, 0));
            }
        }
        public void SetDestroyTime(float time)
        {
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