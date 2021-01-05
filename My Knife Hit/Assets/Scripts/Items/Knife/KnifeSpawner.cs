using UnityEngine;
using System;
using KnifeHit.Core;
using KnifeHit.Movement;
using System.Collections;

namespace KnifeHit.Items.Knife
{
    public class KnifeSpawner : MonoBehaviour
    {

        [SerializeField] private GameProperies _gameProperies;
        [SerializeField] private GameObject _knifePrefab;

        private int _numOfKnifeToSpawn = 0;
        private KnifeObj _currentKnife;
        private Mover _currentKnifeMover;
        private bool _wasCurrentKnifeThrown = false;

        public void ReactOnKnifeTouch(object obj, OnKnifeCollisionEventArgs args)
        {
            if (args.knifeCollisionType == KnifeCollisionType.Knife)
            {
                ProhibitSpawning();
            }
        }

        public void ProhibitSpawning()
        {
            this._numOfKnifeToSpawn = 0;
            if (!_wasCurrentKnifeThrown)
            {
                DestroyCurrentKnife();
            }
        }

        public void SetNumToSpawn(int num)
        {
            this._numOfKnifeToSpawn = num;
        }

        public void SpawnNewKnife()
        {
            if (_numOfKnifeToSpawn <= 0) { return; }
            _numOfKnifeToSpawn--;
            GameObject newKnife = Instantiate(_knifePrefab);
            newKnife.transform.position = transform.position;
            _currentKnife = newKnife.GetComponent<KnifeObj>();
            _currentKnifeMover = newKnife.GetComponent<Mover>();
            _currentKnife.OnKnifeCollision += ReactOnKnifeTouch;
            _wasCurrentKnifeThrown = false;
        }

        /*private void Update()
        {
            CheckClicking();
        }

        private void CheckClicking()
        {
            if (Input.GetMouseButtonDown(0) && _currentKnifeMover != null)
            {
                ThrowKnife();
            }
        }
*/
        public void ThrowKnife()
        {
            if (_wasCurrentKnifeThrown) { return; }
            _wasCurrentKnifeThrown = true;
               Vector2 velocity = new Vector2(0, _gameProperies.knifeSpeed);
            if (_currentKnifeMover != null)
            {
                _currentKnifeMover.SetVelocity(velocity);
            _currentKnifeMover = null;
            }

            StartCoroutine(WaitForRespawn());
        }

        IEnumerator WaitForRespawn()
        {
            yield return new WaitForSeconds(_gameProperies.respawnKnifeDelay);
            SpawnNewKnife();
        }

        public void DestroyCurrentKnife()
        {
            if (_currentKnife)
            {
                Destroy(_currentKnife.gameObject);
            }
        }
    }
}