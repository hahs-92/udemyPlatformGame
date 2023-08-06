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
    [SerializeField] private GameObject equipBtn;

    private void Awake()
    {
        anim = GetComponent<Animator>();
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
        Debug.Log("skin was equip");
    }

    private void SetupSkinInfo()
    {
        buyBtn.SetActive(!skinPurchased[skinId]);
        equipBtn.SetActive(skinPurchased[skinId]);

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
}
