using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace KnifeHit.Core 
{

    [CreateAssetMenu(menuName = "GameProperies")]
    public class GameProperies : ScriptableObject
    {
        [Header("Rotation")]
        [Tooltip("Degrees per second")]
        [Range(0, 360)][SerializeField] public float rotationSpeedOfLog = 30;
        [SerializeField] public float minTimeOnStartRotation = 0f;
        [SerializeField] public float maxTimeOnStartRotation = 3f;
        [Range(0f,1f)][SerializeField] public float chanceOfAppleAppearing= 0.25f;
        [Range(0f, 1f)] [SerializeField] public float chanceOfRotationWithPeriod = 0.25f;
        [SerializeField] public float rotationgPeriod = 5f;
        [Range(5f, 25f)][SerializeField] public float knifeSpeed = 10f;
        [Range(0f, 1.2f)][SerializeField] public float respawnKnifeDelay = 0.1f;
        [SerializeField] public float restartGameDelay = 1f;

        [Header("Knives to throw")]
        [SerializeField] public int minNumOfKnivesThrow = 4;
        [SerializeField] public int maxNumOfKnivesThrow = 6;

        [Header("Start knives in log")]
        [SerializeField] public int minNumOfStartKnives = 0;
        [SerializeField] public int maxNumOfStartKnives = 3;

        [Header("Notification")]
        [SerializeField] public string notificationTitle = "Где ты воен?";
        [SerializeField] public string notificationText = "Время рубить дрова!";
        [Tooltip("Minuts")]
        [SerializeField] public float notificationTimeDelay = 0.25f;
    }
}


