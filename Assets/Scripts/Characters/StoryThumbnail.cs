using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public sealed class StoryThumbnail : UIBehaviour
{
    public class OnButtonThumbtailClickedEvent : UnityEvent<string>
    {

    }


    [SerializeField]
    private Button m_button = default;

    [SerializeField]
    private Image m_imageThumbnail = default;

    private bool m_isUnlocked;

    public OnButtonThumbtailClickedEvent onClick = new();

    public string storyId;

    public Sprite sprite
    {
        get => m_imageThumbnail.sprite;
        set => m_imageThumbnail.sprite = value;
    }

    public bool isUnlocked
    {
        get => m_isUnlocked;
        set
        {
            m_isUnlocked = value;
            // m_imageThumbnail.color = value ? Color.white : Color.gray;
            m_button.interactable = value;
        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        m_button.onClick.AddListener(OnButtonClicked);
    }

    protected override void OnDisable()
    {
        m_button.onClick.RemoveListener(OnButtonClicked);
        base.OnDisable();
    }

    private void OnButtonClicked()
    {
        onClick?.Invoke(storyId);
    }
}
