using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUDPopulate : MonoBehaviour {
    private CharacterStats current;
    private CharacterStats target;
    private Text CT;
    public Text cName;
    public Text tName;
    public BarScript cHealth;
    public BarScript tHealth;
    public BarScript cMana;
    public BarScript tMana;

    // Use this for initialization
    void Start () {
        CT = GameObject.Find("CT").GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
        if (current != GameManager.GM.getCurrent() && GameManager.GM.HUD.activeInHierarchy)
        {
            current = GameManager.GM.getCurrent();

            cName.text = current.charName;

            // if current is different then give the bars to the new current
            // then set the current and max in the bar to the values in current's 2 stats


            current.health.Initialize(cHealth, current.health.Value, current.getMaxHealth());
            current.mana.Initialize(cMana, current.mana.Value, current.getMaxMana());
        }

        CT.text = GameManager.GM.getCT().ToString();

        if (target != GameManager.GM.getTarget() && GameManager.GM.targetHUD.activeInHierarchy)
        {
            target = GameManager.GM.getTarget();

            tName.text = target.charName;

            // if current is different then give the bars to the new current
            // then set the current and max in the bar to the values in current's 2 stats


            target.health.Initialize(tHealth, target.health.Value, target.health.MaxValue);
            target.health.Initialize(tMana, target.mana.Value, target.mana.MaxValue);
        }
    }
}
