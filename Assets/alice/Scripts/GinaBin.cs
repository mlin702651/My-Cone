using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GinaBin : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]private GameObject Rod;
    private Vector3 RodOriginalPosition;
    [SerializeField]private float RodFloatingDistance;
    [SerializeField]private float RodFloatingDuration;
    [SerializeField] private Ease RodFloatingEase = Ease.Linear;

    Sequence RodSequence;

    void Start()
    {
        RodOriginalPosition = Rod.transform.position;
        RodSequence = DOTween.Sequence();
        RodSequence.Append(Rod.transform.DOMoveY(RodOriginalPosition.y+RodFloatingDistance, RodFloatingDuration)
            .SetEase(RodFloatingEase)
            .SetLoops(-1))
            .Append(Rod.transform.DOMoveY(RodOriginalPosition.y, RodFloatingDuration)
            .SetEase(RodFloatingEase)
            .SetLoops(-1));
        RodSequence.SetLoops(-1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
