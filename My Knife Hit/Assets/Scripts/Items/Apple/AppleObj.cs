using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KnifeHit.Movement;

namespace KnifeHit.Items.Apple
{
    public class AppleObj : MonoBehaviour
    {
        [SerializeField] private GameObject _applesHalfPrefab;
        [SerializeField] private float _destroyVFXDelay;

        public void DestroyVFX()
        {
            int numOfHalf = 2;
            for (int i = 0; i < numOfHalf; i++)
            {
                GameObject appleHalf = Instantiate(_applesHalfPrefab);
                appleHalf.transform.position = transform.position;
                Destroy(appleHalf, _destroyVFXDelay);
                Destroy(gameObject);
            }
        }
    }

}
