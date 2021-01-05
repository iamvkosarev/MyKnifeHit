using UnityEngine;
using System.Collections;
using System;

namespace KnifeHit.Log
{
    public class LogObj : MonoBehaviour
    {
        public void ConnectObject(Transform connectingObject)
        {
            connectingObject.parent = transform;
        }
    }
}