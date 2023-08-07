using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SkinSelection : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private int skinId;
    [SerializeField] private bool[] skinPurchased;
    [SerializeField] private int[] priceForSkin;
    [SerializeField] private GameObject buyBtn;
    [SerializeField] private GameObject selectBtn;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        SetupSkinInfo();
    }

    private void OnDisable()
    {
        selectBtn.SetActive(false);
    }

    public void NextSkin()
    {
        skinId++;

        if(skinId > 2)
        {
            skinId = 0;
        }

        SetupSkinInfo();
    } 
    
    public void PreviousSkin()
    {
        skinId--;

        if(skinId < 0)
        {
            skinId = 2;
        }

        SetupSkinInfo();
    }

    public void Buy()
    {
        skinPurchased[skinId] = true;
        SetupSkinInfo();
    }

    public void Equip()
    {
        if(PlayerManager.instance != null)
        {
            PlayerManager.instance.choosenSkinId = skinId;
            Debug.Log("skin was equip");
        }
    }

    private void SetupSkinInfo()
    {
        skinPurchased[0] = true;
        buyBtn.SetActive(!skinPurchased[skinId]);
        selectBtn.SetActive(skinPurchased[skinId]);

        if(!skinPurchased[skinId])
        {
            buyBtn.GetComponentInChildren<TextMeshProUGUI>().text = "Price: " + priceForSkin[skinId].ToString();
        }
        SetSkinAnim();
    }

    private void SetSkinAnim()
    {
        anim.SetInteger("skinId", skinId);
    }

    public void SwitchSelectButton(GameObject newButton)
    {
        selectBtn = newButton;
    }
}
