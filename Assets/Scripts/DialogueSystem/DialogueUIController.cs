using UnityEngine;
using UnityEngine.EventSystems;

public class DialogueUIController : UIBehaviour
{
    public DialogueData data;

    [SerializeField]
    RectTransform content;

    [SerializeField]
    private DialogueUIImage prefabUIImage;

    private void Start()
    {
        if (!data)
            return;
        for (int i = 0; i < data.pages.Length; ++i)
        {
            DialoguePage page = data.pages[i];
            DialogueUIImage uiImage = Instantiate<DialogueUIImage>(prefabUIImage, content);
            uiImage.SetSprite(page.sprite);
        }
    }
}
