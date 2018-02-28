using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeControl : MonoBehaviour {

    public const int HEALTH_MODIFIER = 2;
    public const int DMG_MODIFIER = 1;
    public const int SPEED_MODIFIER = 1;

    public const float SIZE_MODIFIER = 0.05f;

    public const int MIN = 1; // Minimum base upgrade level
    public const int MAX = 5; // Maximum base upgrade level

    [Tooltip("Unit HP upgrade level")]
    public Upgrade health = new Upgrade();

    [Tooltip("Unit speed upgrade level")]
    public Upgrade speed = new Upgrade();

    [Tooltip("Unit power upgrade level")]
    public Upgrade power = new Upgrade();

    [Tooltip("Units spawned upgrade level")]
    public Upgrade spawncount = new Upgrade();

    [System.Serializable]
    public class Upgrade
    {
        [Range(MIN, MAX)]
        public int level = 1;

        public int cost = 10;

        public int GetTotalCost() {
            return cost * level;
        }
    }
}
