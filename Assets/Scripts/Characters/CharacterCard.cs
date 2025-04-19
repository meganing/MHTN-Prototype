using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

/// <summary>
/// This class represents a character card in the game.
/// </summary>
public sealed class CharacterCard : UserInterfaceBehaviour
{
    /// <summary>
    /// The image of the character.
    /// </summary>
    [SerializeField]
    private Image characterImage;

    /// <summary>
    /// The name of the character.
    /// </summary>
    [SerializeField]
    private TextMeshProUGUI characterName;

    /// <summary>
    /// The ID of the character.
    /// </summary>
    private int characterID = -1; // Invalid ID for checking if the character is set

    /// <summary>
    /// Set the character data to this card.
    /// </summary>
    /// <param name="data">The character data to set.</param>
    public void SetCharacterData(CharacterData data)
    {
        // Set the character image and name using the data provided
        characterImage.sprite = data.portrait;
        characterName.text = data.characterName;
        // Set the character ID used when selecting the character
        characterID = data.characterID;
    }

    /// <summary>
    /// The button to select the character.
    /// Bind delegate to the button click event.
    /// </summary>
    public void SelectCharacter()
    {
        // Save the selected character ID for later use
        PlayerPrefs.SetInt(ProjectValueConstant.PLAYER_PREFS_SELECTED_CHARACTER_ID, characterID);
        // Load the character profile scene
        SceneManager.LoadScene(ProjectValueConstant.SCENE_NAME_CHARACTER_PROFILE);
    }
}