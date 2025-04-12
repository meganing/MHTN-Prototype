using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DialogueUIImage : UIBehaviour
{
    [SerializeField]
    private Image image;

    public void SetSprite(Sprite sprite)
    {
        image.sprite = sprite;
        // TODO: calculate the aspect ratio;
        float aspectRatio = CalculateAspectRatioFromSprite(sprite);
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, rectTransform.sizeDelta.x / aspectRatio);
    }

    private float CalculateAspectRatioFromSprite(Sprite sprite)
    {
        return sprite.rect.width / sprite.rect.height;
    }

    #if UNITY_EDITOR
    [ContextMenu("Test Aspect Ratio Sprite")]
    private void Editor_TestAspectRatioSprite()
    {
        Sprite sprite = image.sprite;
        if (!sprite)
            return;
        SetSprite(sprite);
    }
    #endif
}
