using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts;

public class CharacterStats: MonoBehaviour {
    public string name = "name";
    public float health;
    public float MAXHEALTH;
    public float mana;
    public float MAXMANA;
    public bool spell;      // true = mana, false = ability points
    public float ct;
    public float speed;     // may not need
    public float MAXCT;
    public float move;
    public float jump;
    public int atk;
    public int baseAtk;
    public int def;
    public int baseDef;
    public int magic;
    public int baseMagic;
    public int magDef;
    public int baseMagDef;
    public int XP;
    public int XPGoal;
    public SpriteRenderer moveCircle;
    public SpriteRenderer actionCircle;

    //public int constitution;
    //public int widsom;
    //public int charisma;

    //public container with equipment
    Arm armArmor;
    Body bodyArmor;
    Head headArmor;
    Feet footArmor;
    Weapon rightHand;
    Weapon LeftHand;
    Ring ring;
    Trinket trinket;

    //public container with available skills
    List<Skill> skillList;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (ct < MAXCT) {
            ct += Time.deltaTime;
        } else {
            ct = MAXCT;
        }
	}

    // getters (accessors)
    public Arm getArmArmor()
    {
        return this.armArmor;
    }

    public Head getHeadArmor()
    {
        return this.headArmor;
    }

    public Body getBodyArmor()
    {
        return this.bodyArmor;
    }

    public Feet getFeetArmor()
    {
        return this.footArmor;
    }

    public Ring getRing()
    {
        return this.ring;
    }

    public Trinket getTrinket()
    {
        return this.trinket;
    }

    public Skill getSkill(string skillName)
    {
        Skill ret = null;

        foreach (Skill skill in skillList) {
            if (skill.name.Equals(skillName))
            {
                ret = skill;
            }
        }

        return ret;
    }

    public Skill getSkill(int index)
    {
        return skillList[index];
    }

    public List<Skill> getSkillList()
    {
        return this.skillList;
    }

    // setters (mutators)
    public void setArmArmor(Arm armArmor)
    {
        this.armArmor = armArmor;
    }

    public void setHeadArmor(Head headArmor)
    {
        this.headArmor = headArmor;
    }

    public void setBodyArmor(Body bodyArmor)
    {
        this.bodyArmor = bodyArmor;
    }

    public void setFeetArmor(Feet footArmor)
    {
        this.footArmor = footArmor;
    }

    public void setRing(Ring ring)
    {
        this.ring = ring;
    }

    public void setTrinket(Trinket trinket)
    {
        this.trinket = trinket;
    }

    public void addSkill(Skill newSkill)
    {
        this.skillList.Add(newSkill);
    }

    public void setSkillList(List<Skill> skillList)
    {
        this.skillList = skillList;
    }
}
