using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackChargeUI : MonoBehaviour
{
    public Slider leftUI;
    public Slider midUI;
    public Slider rightUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetLeftUI(int chargeTime)
    {
        leftUI.value = chargeTime;
    }

    public void SetMidUI(int chargeTime)
    {
        midUI.value = chargeTime;
    }

    public void SetRightUI(int chargeTime)
    {
        rightUI.value = chargeTime;
    }

}
