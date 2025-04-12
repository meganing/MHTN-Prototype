using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterCard : MonoBehaviour {
    public Image characterImage;
    public TextMeshProUGUI characterName;
    public int characterID;
    
    public void SelectCharacter() {
        // Save the selected character ID for later use
        PlayerPrefs.SetInt("SelectedCharacterID", characterID);
        // Load the character profile scene
        FindObjectOfType<SceneLoader>().LoadScene("CharacterProfile");
    }
}