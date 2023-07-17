using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using DG.Tweening;

public class PopUpText : MonoBehaviour
{
    public static PopUpText instance;

    public GameObject popupPrefab;
    private Transform popupParent;

    [Range(.2f, 1f)]
    public float popUpTime = .5f;
    public float popUPScale = 5f;

    #region housekeeping
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        CreateParent();
    }

    private void CreateParent()
    {
        popupParent = new GameObject("PopUps").transform;
        popupParent.parent = transform;
    }
    #endregion

    public void PreviewPopUp(string text)
    {
        PopUp(Vector3.zero, text);
    }

    public void PopUp(Vector3 pos, string text)
    {
        PopUp(pos, Color.white, text);
    }

    public void PopUp(Vector3 pos, Color color, string text)
    {
        if (popupParent == null) CreateParent();

        var popup = GameObject.Instantiate(popupPrefab, pos, Quaternion.identity, popupParent).transform;
        popup.localScale = Vector3.one*.1f;

        var textComp = popup.GetComponent<TMP_Text>();
        textComp.color = color;
        textComp.text = text;

        var jumpTo = new Vector3(Random.Range(-2f,2f), Random.Range(-2f, -3f));

        //popup.DOLocalMoveX(1, popUpTime*1.5f).SetEase(Ease.OutSine);
        popup.DOScale(Vector3.one * popUPScale, popUpTime*.3f).SetEase(Ease.OutSine)
            .OnComplete(() => { popup.DOScale(Vector3.zero, popUpTime * .6f).SetEase(Ease.InSine); });
        //popup.DOLocalMoveY(2, popUpTime).SetEase(Ease.OutElastic);
        popup.DOLocalJump(jumpTo, 2, 1, popUpTime*1.5f).SetEase(Ease.InSine)        
            .OnComplete(() => { Destroy(popup.gameObject); });
    }
}
