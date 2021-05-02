using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitFlipper : MonoBehaviour
{
    public Sprite Front;
    public Sprite Back;

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

        if(currentSprite == Back)
        {
            gameObject.GetComponent<Image>().sprite = Front;
        }
    }
}
