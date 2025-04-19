using UnityEngine;
using UnityEngine.EventSystems;

public class DialogueUIController : UIBehaviour
{
    /// <summary>
    /// The data for the dialogue.
    /// </summary>
    public DialogueData data;

    /// <summary>
    /// The content of the dialogue.
    /// </summary>
    [SerializeField]
    RectTransform content;

    /// <summary>
    /// The prefab for the image.
    /// </summary>
    [SerializeField]
    private DialogueUIImage prefabUIImage;

    protected override void Start()
    {
        if (!data)
            return;
        for (int i = 0; i < data.pages.Length; ++i)
        {
            DialoguePage page = data.pages[i];
            DialogueUIImage uiImage = Instantiate(prefabUIImage, content);
            uiImage.SetSprite(page.sprite);
        }
    }
}
