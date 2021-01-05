using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace KnifeHit.Core 
{

    [CreateAssetMenu(menuName = "GameProperies")]
    public class GameProperies : ScriptableObject
    {
        
        [Tooltip("Degrees per second")]
        [Range(0, 360)][SerializeField] public float rotationSpeedOfLog = 30;
        [Range(0f,1f)][SerializeField] public float chanceOfAppleAppearing= 0.25f;
        [Range(5f, 25f)][SerializeField] public float knifeSpeed = 10f;
        [SerializeField] public float respawnKnifeDelay = 0.1f;
        [SerializeField] public float restartGameDelay = 1f;
        [Header("Knifes to throw")]
        [SerializeField] public int minNumOfKnivesThrow = 4;
        [SerializeField] public int maxNumOfKnivesThrow = 6;
        [Header("Start knifes in log")]
        [SerializeField] public int minNumOfStartKnives = 0;
        [SerializeField] public int maxNumOfStartKnives = 3;
    }
}


