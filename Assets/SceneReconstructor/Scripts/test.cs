using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class test : MonoBehaviour
{
    Image image;
    Texture2D texture;
    public GameObject spriteToReplace;
    
    void Start()
    {
        texture = gameObject.GetComponent<SpriteRenderer>().sprite.texture;
        string textureString = Convert.ToBase64String(texture.EncodeToJPG());

        image = spriteToReplace.GetComponent<Image>();

        if (textureString != "")
        {
            Texture2D newtex = new Texture2D(2, 2, TextureFormat.RGBA32, false);
            newtex.LoadImage(Convert.FromBase64String(textureString));

            Rect rect = new Rect(0, 0, newtex.width, newtex.height);
            Vector2 pivot = new Vector2(0, 0);

            image.sprite = Sprite.Create(newtex, rect, pivot);
            image.preserveAspect = true;
        }
    }
}
