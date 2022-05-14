using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoManager : MonoBehaviour
{
    [SerializeField]private Image playerHealthBar;
    [SerializeField]private float SetHealthDuration = 0.1f;
    [SerializeField]private Text CrystalAmountText;
    [SerializeField]private Text CrystalAmountTextShadow;
    [SerializeField]private float setCrystalDuration = 0.1f;
    [SerializeField]private Text SpiritAmountText;
    [SerializeField]private Text SpiritAmountTextShadow;
    [SerializeField]private float setSpiritDuration = 0.1f;

    private bool startSetHealth = false;
    private bool startSetCrystal = false;
    private bool startSetSpirit = false;
    private float setHealthTimer = 0;
    private float setCrystalTimer = 0;
    private float setSpiritTimer = 0;
    private float healthDisplayValue = 0f; // value during animation

    private int health = 100;
    private int newHealth = 100;

    private int crystalAmount = 0;
    private int newCrystalAmount = 0;
    private int spiritAmount = 0;
    private int newSpiritAmount = 0;
    
    
    public static PlayerInfoManager instance;
    private void Awake(){
        if(instance != null){
            //Debug.LogWarning("fix this: " + gameObject.name);
            Destroy(gameObject);
        }
        else instance = this;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(startSetHealth){
            SetPlayerHealth(playerHealthBar,ref setHealthTimer, SetHealthDuration, ref healthDisplayValue);
        }
        if(startSetCrystal){
            SetCrystalAmount();
        }
        if(startSetSpirit){
            SetSpiritAmount();
        }
    }

    public void StartSetPlayerHealth(int originHealth,int newHealth){
        startSetHealth = true;
        health = originHealth;
        this.newHealth = newHealth;
    }

    public void StartSetCrystalAmount(int originCrystal,int newCrystal){
        startSetCrystal = true;
        crystalAmount = originCrystal;
        newCrystalAmount = newCrystal;
    }
    public void StartSetSpiritAmount(int originSpirit,int newSpirit){
        startSetSpirit = true;
        spiritAmount = originSpirit;
        newSpiritAmount = newSpirit;
    }
    

    void SetPlayerHealth(Image healthBar,ref float timer,float duration,ref float displayValue){
        timer += (Time.deltaTime/duration);
        displayValue = Mathf.Lerp( health*0.01f, newHealth*0.01f, timer);
        healthBar.fillAmount = displayValue;

        if(newHealth<health&&displayValue<=newHealth*0.01f){ //minus
                startSetHealth = false;
                timer = 0;
        }
        else if(newHealth>=health&&displayValue>=newHealth*0.01f){
                startSetHealth = false;
                timer = 0;
        }
    }
    
    public void SetCrystalAmount(){
        setCrystalTimer += Time.deltaTime / setCrystalDuration;
        crystalAmount = (int)Mathf.Lerp (crystalAmount, newCrystalAmount, setCrystalTimer);
        CrystalAmountText.text = crystalAmount.ToString ();
        CrystalAmountTextShadow.text = crystalAmount.ToString ();

        if(crystalAmount == newCrystalAmount){
            startSetCrystal = false;
            setCrystalTimer = 0;
        }
    }
    public void SetSpiritAmount(){
        setSpiritTimer += Time.deltaTime / setSpiritDuration;
        spiritAmount = (int)Mathf.Lerp (spiritAmount, newSpiritAmount, setSpiritTimer);
        SpiritAmountText.text = spiritAmount.ToString ();
        SpiritAmountTextShadow.text = spiritAmount.ToString ();

        if(spiritAmount == newSpiritAmount){
            startSetSpirit = false;
            setSpiritTimer = 0;
        }
    }
}
