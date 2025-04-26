using UnityEngine;
using UnityEngine.UI; // Added for Button and Image
using TMPro;
using UnityEngine.SceneManagement;

public class CharacterCard : MonoBehaviour
{
    [Header("UI References")] // Added header for clarity
    public Image characterImage;
    public TextMeshProUGUI characterName;
    // No TextMeshPro for description needed on the card itself based on your prefab

    [Header("Data")] // Added header for clarity
    public int characterID; // The ID of the character this card represents

    [Header("Button Reference")] // Added header
    public Button selectButton; // Assign the Button component of this GameObject in the prefab

    /// <summary>
    /// Called when the button on this card is clicked.
    /// </summary>
    public void SelectCharacter()
    {
        Debug.Log("Selected Character ID: " + characterID);
        // Save the selected character ID for later use
        PlayerPrefs.SetInt("SelectedCharacterID", characterID);
        PlayerPrefs.Save(); // Good practice to save immediately

        // Load the character profile scene (ensure your scene is named "CharacterProfileScene")
        SceneManager.LoadScene("CharacterProfileScene");
    }

    // Optional: Setup method if want to pass data directly to the card
    // public void SetupCard(CharacterData data, int id)
    // {
    //     characterImage.sprite = data.portrait;
    //     characterName.text = data.characterName;
    //     characterID = id; // Or data.characterID depending on your system
    //     // The manager will hook up the button click
    // }
}