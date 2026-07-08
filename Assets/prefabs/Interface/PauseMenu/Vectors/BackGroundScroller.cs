using UnityEngine;
using UnityEngine.UI;

public class BackGroundScroller : MonoBehaviour
{
    [SerializeField] private RawImage rawImage;
    [SerializeField] private float speedX = 0.1f;
    [SerializeField] private float speedY;
    void Update()
    {
        if (rawImage != null)
        {
            //idk why, but it works fine
            Rect currentRect = rawImage.uvRect;
            currentRect.y -= speedX * Time.unscaledDeltaTime;
            currentRect.x += speedY * Time.unscaledDeltaTime;
            rawImage.uvRect = currentRect;
        }
    }
}
