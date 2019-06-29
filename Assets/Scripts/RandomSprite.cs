using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSprite : MonoBehaviour
{
    [SerializeField] Sprite[] sprites;
    [SerializeField] float minSizeFactor = 1f;
    [SerializeField] float maxSizeFactor = 1f;

    int spriteIndex;
    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        SetSprite(sprites[Random.Range(0, sprites.Length)]);
        SetScale();
    }

    private void SetScale()
    {
        float sizeFactor = Random.Range(minSizeFactor, maxSizeFactor);
        transform.localScale = Vector2.one * sizeFactor;
    }

    private void SetSprite(Sprite sprite)
    {
        if (spriteRenderer)
        {
            spriteRenderer.sprite = sprite;
        }
        else
        {
            Debug.LogWarning("Sprite renderer is not present in"
                + " " + gameObject.name + " but "
                + GetType().Name + " needs it.");
        }
    }
}
