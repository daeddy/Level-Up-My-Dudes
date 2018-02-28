using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    private readonly string[] ROMAN_NUMBERS = { "I", "II", "III", "IV", "V" };

    public Text money;
    public Text healthLevel;
    public Text speedLevel;
    public Text powerLevel;
    public Text spawnLevel;
    public Text healthCost;
    public Text speedCost;
    public Text powerCost;
    public Text spawnCost;

    private GameBase baseScript;
    private UpgradeControl upgradeControl;

	// Use this for initialization
	void Start ()
    {
        baseScript = GetComponent<GameBase>();
        upgradeControl = GetComponent<UpgradeControl>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        money.text = "$" + baseScript.money;
        healthLevel.text = ConvertToRoman(upgradeControl.health.level);
        speedLevel.text = ConvertToRoman(upgradeControl.speed.level);
        powerLevel.text = ConvertToRoman(upgradeControl.power.level);
        spawnLevel.text = ConvertToRoman(upgradeControl.spawncount.level);
        healthCost.text = ConvertIntToString(upgradeControl.health.GetTotalCost());
        speedCost.text = ConvertIntToString(upgradeControl.speed.GetTotalCost());
        powerCost.text = ConvertIntToString(upgradeControl.power.GetTotalCost());
        spawnCost.text = ConvertIntToString(upgradeControl.spawncount.GetTotalCost());
    }

    private string ConvertToRoman(int number) {
        return ROMAN_NUMBERS[number - 1];
    }

    private string ConvertIntToString(int value) {
        return "" + value;
    }
}
