using UnityEngine;
using System.Collections;
using System;
using KnifeHit.Knife;
using KnifeHit.Movement;

namespace KnifeHit.Log
{
    public class LogObj : MonoBehaviour
    {
        [SerializeField] private GameObject LogChipsVFXPrefab;
        [SerializeField] private GameObject LogsVFXPrefab;
        [SerializeField] private GameObject SmallLogChipsVFXPrefab;
        [SerializeField] private float destroyAndChildVFXDelay = 3f;

        private int _currentNumOfChild;

        private void Update()
        {
            if (transform.childCount != _currentNumOfChild)
            {
                _currentNumOfChild = transform.childCount;
                SpawnVFX(SmallLogChipsVFXPrefab);
            }
        }
        public void DestroyVFX()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Transform knife = transform.GetChild(i);
                SetMovementProps(knife);
                SetKnifeProps(knife);
            }
            SpawnVFX(LogsVFXPrefab);
            SpawnVFX(LogChipsVFXPrefab);
            SpawnVFX(SmallLogChipsVFXPrefab);
            Destroy(gameObject);
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
                Vector2 velocity = new Vector2(UnityEngine.Random.Range(-2f, 2f), UnityEngine.Random.Range(0f, 5f));
                mover.SetVelocity(velocity);
                mover.SwitchRigidbodyType(RigidbodyType2D.Dynamic);
            }
            if (rotator)
            {
                float rotationSpeed = UnityEngine.Random.Range(140f, 300f);
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