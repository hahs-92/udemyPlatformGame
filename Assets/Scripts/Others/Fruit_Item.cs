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
    [SerializeField] private Animator anim;
    [SerializeField] private Sprite[] fruitImage;
    public FruitType fruitType;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void SetLayerWeight(int fruitIndex)
    {
        Debug.Log("fruitIndex: " +  fruitIndex);
        for (int i = 0; i < anim.layerCount; i++)
        {
            anim.SetLayerWeight(i, 0);
        }

        anim.SetLayerWeight(fruitIndex, 1);
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
