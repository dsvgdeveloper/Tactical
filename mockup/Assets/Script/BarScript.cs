using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BarScript : MonoBehaviour {
    [SerializeField]
    private float lerpSpeed = 2f;

    [SerializeField]
    private float fillAmount;

    [SerializeField]
    private Image content;

    [SerializeField]
    private Text valText;

    [SerializeField]
    private Color fullColor;

    [SerializeField]
    private Color lowColor;

    [SerializeField]
    bool lerpColors = true;

    public float MaxValue { get; set; }

    public float Value
    {
        set
        {
            float val = value;
            valText.text = val.ToString() + "/" + MaxValue;
            fillAmount = map(value, 0, MaxValue, 0, 1);
        }
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if(content.fillAmount != fillAmount && content != null)
        {
            content.fillAmount = Mathf.Lerp(content.fillAmount, fillAmount, Time.deltaTime * lerpSpeed);

            if (lerpColors)
            {
                content.color = Color.Lerp(lowColor, fullColor, fillAmount);
            }
        }
	}

    // (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin
    public float map(float value, float inMin, float inMax, float outMin, float outMax)
    {
        return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }

    public void setBarAndText(Transform trans)
    {
        content = trans.GetChild(0).GetComponent<Image>();
        valText = trans.GetChild(1).GetComponent<Text>();
    }
}
