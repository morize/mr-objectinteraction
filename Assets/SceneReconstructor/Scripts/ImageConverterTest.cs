using System;
using UnityEngine;
using UnityEngine.UI;

public class ImageConverterTest : MonoBehaviour
{
    [SerializeField]
    GameObject imageObject;

    void Start()
    {
        Texture2D texture = gameObject.GetComponent<SpriteRenderer>().sprite.texture;
        Image image = imageObject.GetComponent<Image>();

        string textureBase64String = Convert.ToBase64String(texture.EncodeToJPG());

        
        if (textureBase64String != "")
        {
            Texture2D newtex = new Texture2D(2, 2, TextureFormat.RGBA32, false);
            newtex.LoadImage(Convert.FromBase64String(textureBase64String));

            Rect rect = new Rect(0, 0, newtex.width, newtex.height);
            Vector2 pivot = new Vector2(0, 0);

            image.sprite = Sprite.Create(newtex, rect, pivot);
            image.preserveAspect = true;
        }
    }
}
