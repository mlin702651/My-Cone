using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeHat : MonoBehaviour
{
     [SerializeField] private WhichHat whichHat = WhichHat.hatWood;
    [SerializeField]GameObject hatWood;
    [SerializeField]GameObject hatTie;
    [SerializeField]GameObject hatEgg;
    [SerializeField]GameObject hatCrimical;
    [SerializeField]GameObject hatCook;
    [SerializeField]GameObject hatMagic;




    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        switch (whichHat)
        {
            case WhichHat.idle:
                hatWood.SetActive(false);
                hatTie.SetActive(false);
                hatEgg.SetActive(false);
                hatCrimical.SetActive(false);
                hatCook.SetActive(false);
                hatMagic.SetActive(false);
                break;
            case WhichHat.hatWood:
                hatWood.SetActive(true);
                hatTie.SetActive(false);
                hatEgg.SetActive(false);
                hatCrimical.SetActive(false);
                hatCook.SetActive(false);
                hatMagic.SetActive(false);
                break;
            case WhichHat.hatTie:
                hatWood.SetActive(false);
                hatTie.SetActive(true);
                hatEgg.SetActive(false);
                hatCrimical.SetActive(false);
                hatCook.SetActive(false);
                hatMagic.SetActive(false);
                break;
            case WhichHat.hatEgg:
                hatWood.SetActive(false);
                hatTie.SetActive(false);
                hatEgg.SetActive(true);
                hatCrimical.SetActive(false);
                hatCook.SetActive(false);
                hatMagic.SetActive(false);
                break;
            case WhichHat.hatCrimical:
                hatWood.SetActive(false);
                hatTie.SetActive(false);
                hatEgg.SetActive(false);
                hatCrimical.SetActive(true);
                hatCook.SetActive(false);
                hatMagic.SetActive(false);
                break;
            case WhichHat.hatCook:
                hatWood.SetActive(false);
                hatTie.SetActive(false);
                hatEgg.SetActive(false);
                hatCrimical.SetActive(false);
                hatCook.SetActive(true);
                hatMagic.SetActive(false);
                break;
            case WhichHat.hatMagic:
                hatWood.SetActive(false);
                hatTie.SetActive(false);
                hatEgg.SetActive(false);
                hatCrimical.SetActive(false);
                hatCook.SetActive(false);
                hatMagic.SetActive(true);
                break;
            default:
                break;
        }
    }

    public enum WhichHat
    {
        hatWood,
        hatTie,
        hatEgg,
        hatCrimical,
        hatCook,
        hatMagic,
        idle
    }
}
