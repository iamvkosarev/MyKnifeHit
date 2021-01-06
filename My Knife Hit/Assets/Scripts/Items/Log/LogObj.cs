using UnityEngine;
using System.Collections;
using System;
using KnifeHit.Items.Knife;
using KnifeHit.Movement;

namespace KnifeHit.Items.Log
{
    public class LogObj : MonoBehaviour
    {
        [SerializeField] private GameObject LogChipsVFXPrefab;
        [SerializeField] private GameObject LogsVFXPrefab;
        [SerializeField] private GameObject SmallLogChipsVFXPrefab;
        [SerializeField] private float destroyAndChildVFXDelay = 3f;

        private int _currentNumOfChildren;
        private Animator _animator;
        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            _animator.SetTrigger("open");
        }

        private void Update()
        {
            if (transform.childCount != _currentNumOfChildren)
            {
                _currentNumOfChildren = transform.childCount;
                SpawnVFX(SmallLogChipsVFXPrefab);
            }
        }

        public void Hit()
        {
            _animator.SetTrigger("hit");
        }
        public void DestroyVFX()
        {
            Debug.Log(transform.childCount);
            for (int i = transform.childCount - 1; i >= 0; i--)
            {
                Debug.Log(i);
                Transform knife = transform.GetChild(i);
                SetMovementProps(knife);
                SetKnifeProps(knife);
            }
            SpawnVFX(LogsVFXPrefab);
            SpawnVFX(LogChipsVFXPrefab);
            SpawnVFX(SmallLogChipsVFXPrefab);
            Destroy(gameObject);
        }

        public void SetStartNumOfChildren(int num)
        {
            this._currentNumOfChildren = num;
        }

        private void SpawnVFX(GameObject VFXPrefab)
        {
            GameObject vfxGameObject = Instantiate(VFXPrefab);
            vfxGameObject.transform.position = transform.position + VFXPrefab.transform.localPosition / 2f;
            Destroy(vfxGameObject, destroyAndChildVFXDelay);
        }

        private void SetKnifeProps(Transform knifeTransform)
        {
            KnifeObj knife = knifeTransform.GetComponent<KnifeObj>();
            if(knife == null) { return; }
            knife.MakeFree();
            knife.SetDestroyTime(destroyAndChildVFXDelay);
        }

        private void SetMovementProps(Transform knife)
        {
            Mover mover = knife.GetComponent<Mover>();
            Rotator rotator = knife.GetComponent<Rotator>();
            if (mover)
            {
                Vector2 velocity = new Vector2(UnityEngine.Random.Range(-3f, 3f), UnityEngine.Random.Range(1f, 6f));
                mover.SetVelocity(velocity);
                mover.SwitchRigidbodyType(RigidbodyType2D.Dynamic);
            }
            if (rotator)
            {
                float rotationSpeed = UnityEngine.Random.Range(200f, 350f);
                rotator.SetRotationSpeed(rotationSpeed);
                rotator.SetRotationSide(UnityEngine.Random.Range(0, 2) == 1);
            }
        }

        public void ConnectObject(Transform connectingObject)
        {
            connectingObject.parent = transform;
        }
    }
}