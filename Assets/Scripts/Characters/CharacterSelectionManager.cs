using UnityEngine;

public class CharacterSelectionManager : MonoBehaviour
{
    /// <summary>
    /// The prefab for the character card UI element.
    /// </summary>
    [SerializeField]
    private CharacterCard characterCardPrefab;

    /// <summary>
    /// The parent transform of the content area where character cards will be instantiated.
    /// </summary>
    [SerializeField]
    private RectTransform contentParent;

    /// <summary>
    /// The array of available character data to populate the character cards.
    /// </summary>
    [SerializeField]
    private CharacterData[] availableCharacters = default;

    private void Start()
    {
        InstantiateCharacterCards();
    }

    /// <summary>
    /// Instantiates character cards for each character in the available characters array.
    /// </summary>
    private void InstantiateCharacterCards()
    {
        foreach (CharacterData character in availableCharacters)
        {
            CharacterCard card = Instantiate(characterCardPrefab, contentParent);
            card.SetCharacterData(character);
        }
    }
}