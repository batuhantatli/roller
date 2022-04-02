using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIExperienceBar : MonoSingleton<UIExperienceBar>
{
    public TMP_Text experienceText;
    public TMP_Text playerPowerCanvasText;
    public Image experienceBar;
    public float currentExp;
    public float maxExp = 99f;
    public float lerpSpeed; 
    private void Start() 
    {   
        playerPowerCanvasText.text = PlayerBall.Instance.power + "";
    }
    private void Update() {
        experienceText.text = "Experience: " + currentExp + "%";

        lerpSpeed = 3f*Time.deltaTime;

        ExperienceBarFiller();
    }
    public void ExperienceBarFiller()
    {
        experienceBar.fillAmount = Mathf.Lerp(experienceBar.fillAmount , currentExp/maxExp ,lerpSpeed);
    }
    public void LoseExperience(float losePoint)
    {
        if(currentExp > 0 )
        {
            currentExp -= losePoint;
        }
    }
    public void GainExperience(float gainPoint )
    {
        
        if(currentExp < maxExp)
        {
            currentExp += gainPoint;
        }
        if (currentExp >= maxExp)
        {
            Debug.Log("Level up");
            LevelUp();
        }
        
    }

    public void LevelUp()
    {
        PlayerBall.Instance.PlayerLevelUp();
        currentExp = 0 ;
        playerPowerCanvasText.text = PlayerBall.Instance.power + "";
        
    }
}
