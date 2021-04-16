using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Card : MonoBehaviour, IPointerDownHandler
{
    public Sprite frontSprite;
    public Sprite backSprite;
    public bool active;
    

    // Start is called before the first frame update
    void Start()
    {
        active = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        GameHandler.Instance.setClickedCardImage(this);
    }

    public void StartFlip()
    {
        StartCoroutine(CalculateFlip());
    }

    private void Flip()
    {
        if (active)
        {
            active = false;
            currentImage.sprite = backSprite;
        }
        else
        {
            active = true;
            currentImage.sprite = frontSprite;
        }

        GameHandler.Instance.setClickedCardImage();
    }

    IEnumerator CalculateFlip()
    {
        float duration = 0.0000000001f;
        float size = 1.0f;
        while (size > 0.1)
        {
            size -= 0.18f;
            transform.localScale = new Vector3(size, 1, 1);
            yield return new WaitForSeconds(duration);
        }
        Flip();
        while (size < 0.99)
        {
            size += 0.18f;
            transform.localScale = new Vector3(size, 1, size);
            yield return new WaitForSeconds(duration);
        }
    }

    public void Discard()
    {
        Destroy(gameObject);
    }


    private Image _currentImage;
    [HideInInspector]
    public Image currentImage
    {
        get
        {
            if (null == _currentImage)
            {
                _currentImage = GetComponent<Image>();
                return _currentImage;
            }
            return _currentImage;
        }

        set
        {
            _currentImage = value;
        }
    }

}
