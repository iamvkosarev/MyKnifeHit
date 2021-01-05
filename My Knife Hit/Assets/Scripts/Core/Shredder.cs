using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KnifeHit.Core
{
    public class Shredder : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            Destroy(collision.gameObject);
        }
    }

}
