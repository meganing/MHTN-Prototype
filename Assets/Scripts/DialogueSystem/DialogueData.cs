using UnityEngine;

[CreateAssetMenu(menuName = "Data/Dialogue Data")]
public class DialogueData : ScriptableObject
{
    public string dialogueID;
    public DialoguePage[] pages;
}
