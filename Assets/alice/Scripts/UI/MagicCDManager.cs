using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MagicCDManager : MonoBehaviour
{
    [Header("Magic Sprite")]
    [SerializeField]private Image currentMagicImage;
    [SerializeField]private Sprite magicAccumulate;
    [SerializeField]private Sprite magicBubble;
    [SerializeField]private Sprite magicBomb;
    
    [Header("Accumulate Attack")]
    [SerializeField]private Image accumulateAttackCD;
    [SerializeField]private bool isAccumulateAttack;
    [SerializeField]private bool recoverAccumulateAttack = false;
    public bool IsAccumulateAttack {get{return isAccumulateAttack;}}
    public bool RecoverAccumulateAttack {get{return recoverAccumulateAttack;}}
    [SerializeField]private float accumulateAttackCDCircleDecreaseTime = 2f;
    [SerializeField]private float accumulateAttackCDCircleRecoverTime = 3f;
    [Header("Bomb Attack")]

    [SerializeField]private Image bombAttackCD;
    [SerializeField]private bool isBombAttack;
    [SerializeField]private bool recoverBombAttack = false;
    public bool IsBombAttack {get{return isBombAttack;}}
    public bool RecoverBombAttack {get{return recoverBombAttack;}}
    [SerializeField]private float bombAttackCDCircleDecreaseTime = 2f;
    [SerializeField]private float bombAttackCDCircleRecoverTime = 3f;

    [Header("Bomb Attack")]
    [SerializeField]private Image bubbleAttackCD;
    float CDCircleFullValue = 1f; // the goal
    float CDCircleClearValue = 0f; // animation start value
    float accumulateAttackCDCircleDisplayValue = 0f; // value during animation
    float accumulateAttackCDTimer = 0f;
    float accumulateAttackCDFulltimer = 0f;
    float bombAttackCDCircleDisplayValue = 0f; // value during animation
    float bombAttackCDTimer = 0f;
    float bombAttackCDFulltimer = 0f;

    [SerializeField]private Text currentBombLeft;
    
    public static MagicCDManager instance;
    private void Awake(){
        if(instance != null){
            Debug.LogWarning("fix this: " + gameObject.name);
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
        if(isAccumulateAttack){
            ClearCDCircle(accumulateAttackCD,ref accumulateAttackCDTimer,accumulateAttackCDCircleDecreaseTime,ref accumulateAttackCDCircleDisplayValue);
            if(accumulateAttackCDCircleDisplayValue<=0) {
                isAccumulateAttack = false;
                accumulateAttackCDTimer = 0;
                recoverAccumulateAttack = true;
            }
        }
        if(recoverAccumulateAttack){
            FullCDCircle(accumulateAttackCD,ref accumulateAttackCDTimer,accumulateAttackCDCircleRecoverTime,ref accumulateAttackCDCircleDisplayValue);
            if(accumulateAttackCDCircleDisplayValue>=1){
                recoverAccumulateAttack = false;
                accumulateAttackCDTimer = 0;
            }
        }
        if(isBombAttack){
            ClearCDCircle(bombAttackCD,ref bombAttackCDTimer,bombAttackCDCircleDecreaseTime,ref bombAttackCDCircleDisplayValue);
            if(bombAttackCDCircleDisplayValue<=0) {
                isBombAttack = false;
                bombAttackCDTimer = 0;
                recoverBombAttack = true;
            }
        }
        if(recoverBombAttack){
            FullCDCircle(bombAttackCD,ref bombAttackCDTimer,bombAttackCDCircleRecoverTime,ref bombAttackCDCircleDisplayValue);
            if(bombAttackCDCircleDisplayValue>=1){
                recoverBombAttack = false;
                bombAttackCDTimer = 0;
                SetBombLeftAmount(5);
            }
        }
    }

    void ClearCDCircle(Image circle,ref float timer,float duration,ref float displayValue ){
        timer += (Time.deltaTime/duration);
        displayValue = Mathf.Lerp( CDCircleFullValue, CDCircleClearValue, timer);
        circle.fillAmount = displayValue;
    }
    void FullCDCircle(Image circle,ref float timer,float duration,ref float displayValue){
        timer += (Time.deltaTime/duration);
        displayValue = Mathf.Lerp( CDCircleClearValue, CDCircleFullValue, timer);
        circle.fillAmount = displayValue;
    }
    public void StartAccumulateCD(){
        isAccumulateAttack = true;
    }
    public void StartBombCD(){
        isBombAttack = true;
    }

    public void SetBombLeftAmount(int currentBombCount){
        currentBombLeft.text = currentBombCount.ToString();
    }

    public void SetCurrentMagic(int magicStatus){
        switch(magicStatus){
            case 1:
            currentMagicImage.sprite = magicAccumulate;
            accumulateAttackCD.enabled = true;
            bubbleAttackCD.enabled = false;
            bombAttackCD.enabled = false;
            currentBombLeft.enabled = false;
                break;
            case 2:
            currentMagicImage.sprite = magicBubble;
            accumulateAttackCD.enabled = false;
            bubbleAttackCD.enabled = true;
            bombAttackCD.enabled = false;
            currentBombLeft.enabled = false;
                break;
            case 3:
            currentMagicImage.sprite = magicBomb;
            accumulateAttackCD.enabled = false;
            bubbleAttackCD.enabled = false;
            bombAttackCD.enabled = true;
            currentBombLeft.enabled = true;
                break;
                
            default:
                break;
        }
    }

}
