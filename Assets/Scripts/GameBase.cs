using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBase : GameEntity
{
    private const int UPGRADE_COST = 10;

    public GameBase enemyBase1;
    public GameBase enemyBase2;
    public GameBase enemyBase3;
    public List<GameUnit> units;
	public int money;
    public GameObject radioWaves;
    public AudioSource transmissionSound;
    private UpgradeControl upgradeControl;
    private SpawnUnit spawnUnit;
    private float soundTimer;

    protected override void Start()
    {
        base.Start();
        units = new List<GameUnit>();
        upgradeControl = GetComponent<UpgradeControl>();
        spawnUnit = GetComponent<SpawnUnit>();
    }

    protected override void Update()
    {
        base.Update();

        if (Input.GetButtonDown("Send1" + (player + 1)))
            SendUnits(enemyBase1, enemyBase2, enemyBase3);

        if (Input.GetButtonDown("Send2" + (player + 1)))
            SendUnits(enemyBase3, enemyBase2, enemyBase1);

        CheckUpgradeInput("UpgradeHealth", ref upgradeControl.health);
        CheckUpgradeInput("UpgradeSpeed", ref upgradeControl.speed);
        CheckUpgradeInput("UpgradeDamage", ref upgradeControl.power);
        CheckUpgradeInput("UpgradeSpawn", ref upgradeControl.spawncount);
    }

    private void SendUnits(GameBase base1, GameBase base2, GameBase base3)
    {
        if (units.Count > 0)
        {
            Instantiate(radioWaves, new Vector3(transform.position.x, transform.position.y, -5), Quaternion.identity);

            transmissionSound.Play();
        }

        foreach (GameUnit unit in units)
        {
            unit.enemyBase1 = base1;
            unit.enemyBase2 = base2;
            unit.enemyBase3 = base3;

            unit.sent = true;
            unit.state = GameUnit.UnitStates.Move;

            spawnUnit.ResetSpawnLocations();
        }

        units.Clear();
    }

    private void CheckUpgradeInput(string input, ref UpgradeControl.Upgrade stat)
    {
        if (Input.GetButtonDown(input + (player + 1)))
        {
            if (stat.level < UpgradeControl.MAX && money >= stat.GetTotalCost())
            {
                money -= stat.GetTotalCost();
                stat.level++;
            }
        }
    }
}

