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
                
                log.ConnectObject(transform);
                _mover.SetVelocity(new Vector2(0, 0));
            }
        }

        private void SetPostPunchMovement()
        {
            _mover.SetVelocity(new Vector2(UnityEngine.Random.Range(-2f,2f), 
                UnityEngine.Random.Range(0f, 1f)));
            _mover.SwitchRigidbodyType(RigidbodyType2D.Dynamic);
            _rotator.SetRotationSpeed(UnityEngine.Random.Range(70f,110f));
            _rotator.SetRotationSide(UnityEngine.Random.Range(0f, 1f) < 0.5f);
        }
    }

}