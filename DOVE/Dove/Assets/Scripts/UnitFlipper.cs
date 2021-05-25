using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitFlipper : MonoBehaviour
{
    public Sprite Front;
    public Sprite Back;
    public Sprite EnemyPolice;

    public void FlipBack()
    {
        Sprite currentSprite = gameObject.GetComponent<Image>().sprite;

        if(currentSprite == Front)
        {
            gameObject.GetComponent<Image>().sprite = Back;
        }
    }

    public void FlipFront()
    {
        Sprite currentSprite = gameObject.GetComponent<Image>().sprite;

        if(currentSprite == Back || EnemyPolice)
        {
            gameObject.GetComponent<Image>().sprite = Front;
        }
    }

    public void FlipPolice()
    {
        Sprite currentSprite = gameObject.GetComponent<Image>().sprite;

        if(currentSprite == Back)
        {
            gameObject.GetComponent<Image>().sprite = EnemyPolice;
        }
    }
}
