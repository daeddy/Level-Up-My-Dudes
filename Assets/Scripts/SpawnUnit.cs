using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class SpawnUnit : MonoBehaviour
{
    [Tooltip("Determines whether units can be spawned or not")]
    public bool active = false;

    [Tooltip("Amount of seconds between each unit spawn")]
    public float interval = 10.0f;

    [Tooltip("Type of unit to be spawned")]
    public GameObject unit;

    public Button AttackButton;
    //[Tooltip("Sprite of spawned units")]
    //public Sprite unitSprite;

    private Transform spawnPoint;
    private GameBase baseScript; // GameBase script attached to this object.
    private float spawnTimer = 0.0f; // Timer used to count spawn intervals
    private int n = 0;
    private Vector3[] spawnPoints;
    private Color baseColor;
    private UpgradeControl upgradeControlScript;
    private const int MAX_SPAWN = 50;
    // Use this for initialization
    void Start()
    {
        active = false;
        //team = GetComponent<GameBase>().team;
        spawnPoint = this.transform;
        spawnPoints = new Vector3[MAX_SPAWN];
        baseScript = GetComponent<GameBase>();
        spawnTimer = interval;
        baseColor = GetComponent<SpriteRenderer>().color;
        upgradeControlScript = GetComponent<UpgradeControl>();
        //AttackButton.onClick.AddListener((UnityEngine.Events.UnityAction)this.OnClick);

        // (di, dj) is a vector - direction in which we move right now
        float di;
        if (baseScript.player == 0 || baseScript.player == 2)
            di = 1;
        else
            di = -1;
        float dj = 0;
        // length of current segment
        int segment_length = 1;

        // current position (i, j) and how much of current segment we passed
        float i = spawnPoint.position.x;
        float j = spawnPoint.position.y + 0.01f;
        int segment_passed = 0;
        for (int k = 0; k < MAX_SPAWN; ++k) {
            // make a step, add 'direction' vector (di, dj) to current position (i, j)
            i += di * 0.3f;
            j += dj * 0.3f;
            ++segment_passed;
            if (k > 8)
            {
                spawnPoints[k - 9].x = i;
                spawnPoints[k - 9].y = j;
            }
            if (segment_passed == segment_length) {
                // done with current segment
                segment_passed = 0;

                // 'rotate' directions
                float buffer = di;
                di = -dj;
                dj = buffer;

                // increase segment length if necessary
                if (dj == 0) {
                    ++segment_length;
                }
            }
        }
        active = true;
    }

    public void OnClick()
    {
        Debug.Log("You have clicked the button!");
        n = 0;
    }


    // Update is called once per frame
    void Update ()
    {
        if (active == true)
        {
            //spawncount = baseScript.upgrades.spawncount;
            if(n < MAX_SPAWN / 2 - 1)
                Spawn();
        }
    }

    /// <summary>
    /// Spawns a new unit against the enemy base.
    /// </summary>
    private void Spawn()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= interval)
        {
            spawnTimer = 0.0f;

            int spawnCount = Mathf.Min(upgradeControlScript.spawncount.level, MAX_SPAWN - baseScript.units.Count - 1);

            for (int i = 0; i < spawnCount; i++)
            {
                GameObject instance = Instantiate(unit, new Vector3(spawnPoints[n].x, spawnPoints[n].y, 0), Quaternion.identity);

                GameUnit unitScript = instance.GetComponent<GameUnit>();
                unitScript.player = baseScript.player;
                unitScript.baseScript = baseScript;

                int healthBonus = UpgradeControl.HEALTH_MODIFIER * (upgradeControlScript.health.level - 1);
                float sizeBonus = UpgradeControl.SIZE_MODIFIER * (upgradeControlScript.health.level - 1);

                unitScript.health += healthBonus;
                unitScript.transform.localScale += new Vector3(sizeBonus, sizeBonus, 0);

                unitScript.speed += UpgradeControl.SPEED_MODIFIER * (upgradeControlScript.speed.level - 1);

                unitScript.damage += UpgradeControl.DMG_MODIFIER * (upgradeControlScript.power.level - 1);

                instance.GetComponent<SpriteRenderer>().color = baseColor;
                instance.tag = transform.tag;

                baseScript.units.Add(unitScript);

                n++;
            }
        }
    }

    public void ResetSpawnLocations() {
        n = 0;
    }
}
