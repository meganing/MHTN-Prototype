using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CharacterCard : MonoBehaviour
{
    /// <summary>
    /// The image of the character.
    /// </summary>
    public Image characterImage;

    /// <summary>
    /// The name of the character.
    /// </summary>
    public TextMeshProUGUI characterName;

    /// <summary>
    /// The description of the character.
    /// </summary>
    public int characterID;

    /// <summary>
    /// The button to select the character.
    /// Bind delegate to the button click event.
    /// </summary>
    public void SelectCharacter()
    {
        // Save the selected character ID for later use
        PlayerPrefs.SetInt("SelectedCharacterID", characterID);
        // Load the character profile scene
        SceneManager.LoadScene("CharacterProfile");
    }
}