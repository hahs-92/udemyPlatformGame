using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FruitType
{
    Apple,
    Banana, 
    Cherry,
    Kiwi,
    Melon,
    Orange,
    Pineapple,
    Strawberry
}

public class Fruit_Item : MonoBehaviour
{
    private Animator anim;
    private SpriteRenderer sr;
    [SerializeField] private Sprite[] fruitImage;
    public FruitType fruitType;

    private void Awake()
    {
        anim= GetComponent<Animator>();
        sr= GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        SetImage();
        SetLayerWeight();
    }

    private void SetLayerWeight()
    {
        for (int i = 0; i < anim.layerCount; i++)
        {
            anim.SetLayerWeight(i, 0);
        }

        anim.SetLayerWeight((int)fruitType, 1);
    }

    private void SetImage()
    {
        sr.sprite = fruitImage[(int)fruitType];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Player player = collision?.GetComponent<Player>();
            if(player != null )
            {
                player.fruits++;
                Destroy(gameObject);
            }
        }
    }

    private void OnValidate()
    {
        
    }
}
