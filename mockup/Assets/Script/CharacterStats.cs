using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine.UI;

public class CharacterStats: MonoBehaviour {
    public string charName = "name";

    //[SerializeField]
    //private Stat health;

    //[SerializeField]
    //private Stat mana;
    public Stat health;
    public Stat mana;

    public int baseHealth;
    public int baseMana;

    public bool spell;      // true = mana, false = ability points
    public float ct;
    public float speed;     // may not need
    public float MAXCT;
    public float move = 5f;
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
    
    public float moveSpeed = 5.0f;
    public float drag = 0.5f;
    public float terminalRotationSpeed = 25.0f;

    private Rigidbody rb;
    public int Index { set; get; }

    public Text txt;

    public void Awake()
    {
        health.Initialize(5f, 5f);
        mana.Initialize(5f, 5f);
    }

	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody>();
        rb.maxAngularVelocity = terminalRotationSpeed;
        rb.drag = drag;

        moveCircle = transform.GetChild(0).GetComponent<SpriteRenderer>();
        actionCircle = transform.GetChild(1).GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update() {
        //if (ct != MAXCT)
        //{
        //    if (ct + Time.deltaTime >= MAXCT)
        //    {
        //        ct = MAXCT;
        //        //moveCircle.transform.localScale = new Vector3(move, move, 1);
        //    }
        //    else {
        //        ct += Time.deltaTime;
        //    }
        //}

        if (ct + Time.deltaTime >= MAXCT)
        {
            if (ct < MAXCT)
            {
                moveCircle.transform.localScale = new Vector3(move, move, 1);
            }
            ct = MAXCT;
        } else if (ct < MAXCT)
        {
            ct += Time.deltaTime;
        }

        //if (ct < MAXCT)
        //{
        //    ct += Time.deltaTime;
        //}
        //else
        //{
        //    ct = MAXCT;
        //    moveCircle.transform.localScale = new Vector3(move, move, 1);
        //}

        if (GameManager.GM.movingChar && GameManager.GM.current == Index)
        {
            if (moveCircle.transform.localScale != Vector3.forward)
            {
                moveCharacter();
            }
        }
    }

    public void moveCharacter()
    {
        Vector3 dir = Vector3.zero;
        dir.x = Input.GetAxisRaw("Horizontal");
        dir.z = Input.GetAxisRaw("Vertical");

        if (GameManager.GM.gesture.InputDirectionM != Vector3.zero)
        {
            Vector3 inputVec = GameManager.GM.gesture.InputDirectionM;
            dir.x = inputVec.x;
            dir.z = inputVec.z;
        }

        if (dir.magnitude > 1)
        {
            dir.Normalize();
        }

        Vector3 rotatedDir = GameManager.GM.camera.transform.TransformDirection(dir);
        rotatedDir = new Vector3(rotatedDir.x, 0, rotatedDir.z);
        rotatedDir = rotatedDir.normalized * dir.magnitude;

        rb.velocity = rotatedDir * moveSpeed;

        GameManager.GM.cameraAxis.transform.position = transform.position;

        if (moveCircle.transform.localScale.x - dir.magnitude * Time.deltaTime < 0) {
            moveCircle.transform.localScale = Vector3.forward;
            rb.velocity = Vector3.zero;
        } else {
            moveCircle.transform.localScale = new Vector3(
                moveCircle.transform.localScale.x - dir.magnitude * Time.deltaTime,
                moveCircle.transform.localScale.y - dir.magnitude * Time.deltaTime,
                1);
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

    public float getMaxHealth()
    {
        return health.MaxValue;
    }

    public float getMaxMana()
    {
        return mana.MaxValue //baseMana + mana from armor and skills and stuff
            ;
    }
}
