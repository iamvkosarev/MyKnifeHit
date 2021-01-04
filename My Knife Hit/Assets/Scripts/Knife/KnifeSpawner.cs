using UnityEngine;
using System;
using KnifeHit.Core;
using KnifeHit.Movement;
using System.Collections;

namespace KnifeHit.Knife
{
    public class KnifeSpawner : MonoBehaviour
    {

        [SerializeField] private GameProperies gameProperies;
        [SerializeField] private GameObject knifePrefab;

        private Knife _currentKnife;
        private Mover _currentKnifeMover;

        private void Start()
        {
            SpawnNewKnife(new object(), new OnKnifeCollisionEventArgs(KnifeCollisionType.Log));
        }

        private void SpawnNewKnife(object sender, OnKnifeCollisionEventArgs e)
        {
            if (_currentKnife != null)
            {
                _currentKnife.OnKnifeCollision -= SpawnNewKnife;
            }

            GameObject newKnife = Instantiate(knifePrefab);
            newKnife.transform.position = transform.position;
            _currentKnife = newKnife.GetComponent<Knife>();
            _currentKnifeMover = newKnife.GetComponent<Mover>();
            _currentKnife.OnKnifeCollision += SpawnNewKnife;
        }

        private void Update()
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

        private void ThrowKnife()
        {
            Vector2 velocity = new Vector2(0, gameProperies.knifeSpeed);
            _currentKnifeMover.SetVelocity(velocity);
            _currentKnifeMover = null;
        }
    }
}