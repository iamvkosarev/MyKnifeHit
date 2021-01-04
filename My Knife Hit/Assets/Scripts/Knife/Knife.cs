using UnityEngine;
using System;
using System.Collections;

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
    public class Knife : MonoBehaviour
    {

        public event EventHandler<OnKnifeCollisionEventArgs> OnKnifeCollision;


        
    }

}