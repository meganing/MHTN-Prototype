using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Collections;

public class DialogueManager : MonoBehaviour
{
    public TMP_Text dialogueText;
    public TMP_Text timerText;
    public GameObject choiceButtonPrefab;
    public GameObject highlightButtonPrefab;
    public Transform choiceContainer;
    public Sprite[] sceneBackgrounds; // Array for webtoon art
    public SpriteRenderer backgroundRenderer;
    public Image fadePanel;
    public float fadeDuration = 0.5f;

    private List<string> dialogueLines = new List<string>();
    private List<string[]> choices = new List<string[]>();
    private List<string> selectedFlags = new List<string>();
    private int currentLine = 0;
    private float score = 0f; // Changed from int to float to support 0.5
    private float timer = 30f; // 30-second timer for urgency
    private bool isTimerActive = false;
    private int correctFlagsNeeded = 0;

    void Start()
    {
        LoadInitialCall();
        DisplayNextLine();
    }

    void Update()
    {
        if (isTimerActive)
        {
            timer -= Time.deltaTime;
            timerText.text = $"Time Left: {Mathf.Ceil(timer)}s";
            if (timer <= 0) EndGame("Time's up! You hesitated too long.");
        }
    }

    void LoadInitialCall()
    {
        backgroundRenderer.sprite = sceneBackgrounds[0]; // Phone ringing scene
        dialogueLines.Add("Dinglingling, your phone rings **again**.\nDoes something **really happen this time?**");
        choices.Add(new string[] { "Answer the call" });
    }

    public void DisplayNextLine()
    {
        if (currentLine >= dialogueLines.Count) return;

        dialogueText.text = dialogueLines[currentLine];
        ClearChoices();

        if (currentLine < choices.Count && (selectedFlags.Count >= correctFlagsNeeded || choices[currentLine][0] != "Continue"))
        {
            foreach (string choice in choices[currentLine])
            {
                GameObject button = Instantiate(choiceButtonPrefab, choiceContainer);
                button.GetComponentInChildren<TMP_Text>().text = choice;
                button.GetComponent<Button>().onClick.AddListener(() => OnChoiceSelected(choice));
            }
        }
        currentLine++;
    }

    void ClearChoices()
    {
        foreach (Transform child in choiceContainer) Destroy(child.gameObject);
    }

    void CreateHighlightButtons(params string[] phrases)
    {
        foreach (string phrase in phrases)
        {
            GameObject button = Instantiate(highlightButtonPrefab, dialogueText.transform);
            button.GetComponentInChildren<TMP_Text>().text = phrase;
            button.GetComponent<Button>().onClick.AddListener(() => HighlightPhrase(phrase));
            // Position manually or use a Horizontal Layout Group on dialogueText
        }
    }

    void HighlightPhrase(string phrase)
    {
        if (!selectedFlags.Contains(phrase))
        {
            selectedFlags.Add(phrase);
            dialogueText.text = dialogueText.text.Replace(phrase, $"**{phrase}**"); // Bold the phrase
            if (selectedFlags.Count == correctFlagsNeeded)
            {
                DisplayNextLine(); // Show "Continue" button
            }
        }
    }

    public void OnChoiceSelected(string choice)
    {
        switch (choice)
        {
            case "Answer the call":
                StartCoroutine(FadeToScene(1, LoadScamCall));
                break;
            case "Continue":
                if (dialogueLines[currentLine - 1].Contains("serious crime"))
                    StartCoroutine(FadeToScene(2, LoadResponseChoices));
                else if (dialogueLines[currentLine - 1].Contains("warrant for your arrest"))
                    StartCoroutine(FadeToScene(4, LoadResponseChoices2));
                else if (dialogueLines[currentLine - 1].Contains("transfer money"))
                    StartCoroutine(FadeToScene(6, LoadFinalResponse));
                break;
            // Question 1 Responses
            case "\"Which police station are you calling from?\"":
                score += 1f; // Changed to float
                dialogueLines.Clear();
                choices.Clear();
                currentLine = 0;
                dialogueLines.Add("✅ **Smart choice!**\nA real officer would **never demand secrecy** or make phone arrests");
                choices.Add(new string[] { "Next Question" });
                break;
            case "\"Oh no! What should I do?\"":
            case "\"I will come to the station right now!\"":
                dialogueLines.Clear();
                choices.Clear();
                currentLine = 0;
                dialogueLines.Add("❌ **Wrong choice.**\nYou are falling for the scam!");
                choices.Add(new string[] { "Next Question" });
                break;
            case "Next Question":
                StartCoroutine(FadeToScene(3, LoadQuestion2));
                break;
            // Question 2 Responses
            case "\"Fines can only be paid at a police station.\"":
                score += 1f; // Changed to float
                dialogueLines.Clear();
                choices.Clear();
                currentLine = 0;
                dialogueLines.Add("✅ **Great job!**\nReal police **never demand money over the phone**.");
                choices.Add(new string[] { "Continue to Final Question" });
                break;
            case "\"I don’t have money. Can I pay later?\"":
            case "\"I’ll ask my parents to send money!\"":
                dialogueLines.Clear();
                choices.Clear();
                currentLine = 0;
                dialogueLines.Add("❌ **Wrong choice.**\nYou are falling for the scam!");
                choices.Add(new string[] { "Continue to Final Question" });
                break;
            case "Continue to Final Question":
                StartCoroutine(FadeToScene(5, LoadFinalQuestion));
                break;
            // Final Responses
            case "\"This is a scam. I am reporting you.\"":
                score += 1f; // Changed to float
                EndGame($"🎉 **You passed!**\nScore: {score}/3\nYou recognized every scam trick and shut it down.\nYou **block the number and report it**, helping prevent others from getting scammed.");
                break;
            case "\"Wait! I need more time!\"":
                score += 0.5f; // Float value, no error now
                EndGame($"🤔 **You hesitated.**\nScore: {score}/3\nYou are **still unsure** if it was real, but you **don’t send money**.\nYou later **find out it was a scam** and feel relieved.");
                break;
            case "\"Okay, I’ll transfer now...\"":
                EndGame($"💸 **You got scammed.**\nScore: {score}/3\nYou **transferred the money**. Your parents **lost their retirement savings**.\nYou feel guilty but determined to learn more.");
                break;
        }
        DisplayNextLine();
    }

    void LoadScamCall()
    {
        dialogueLines.Clear();
        choices.Clear();
        currentLine = 0;
        selectedFlags.Clear();
        dialogueText.text = "\"This is the police. Your name is linked to a serious crime. Do not tell anyone. We are tracking your phone right now.\"\nBefore you respond, **circle any words that seem suspicious.**";
        correctFlagsNeeded = 3;
        CreateHighlightButtons("serious crime", "Do NOT tell anyone", "tracking your phone");
        choices.Add(new string[] { "Continue" });
        isTimerActive = true;
    }

    void LoadResponseChoices()
    {
        dialogueLines.Clear();
        choices.Clear();
        currentLine = 0;
        dialogueLines.Add("Now, how do you respond?");
        choices.Add(new string[] { "\"Which police station are you calling from?\"", "\"Oh no! What should I do?\"", "\"I will come to the station right now!\"" });
    }

    void LoadQuestion2()
    {
        dialogueLines.Clear();
        choices.Clear();
        currentLine = 0;
        selectedFlags.Clear();
        dialogueText.text = "\"You must follow instructions now, or we will issue a warrant for your arrest.\"\nWhat’s suspicious?";
        correctFlagsNeeded = 2;
        CreateHighlightButtons("must follow instructions", "warrant for your arrest");
        choices.Add(new string[] { "Continue" });
    }

    void LoadResponseChoices2()
    {
        dialogueLines.Clear();
        choices.Clear();
        currentLine = 0;
        dialogueLines.Add("How do you respond?");
        choices.Add(new string[] { "\"Fines can only be paid at a police station.\"", "\"I don’t have money. Can I pay later?\"", "\"I’ll ask my parents to send money!\"" });
    }

    void LoadFinalQuestion()
    {
        dialogueLines.Clear();
        choices.Clear();
        currentLine = 0;
        selectedFlags.Clear();
        dialogueText.text = "\"You have 5 minutes to transfer money, or police will come to your house.\"\nWhat’s suspicious?";
        correctFlagsNeeded = 2;
        CreateHighlightButtons("5 minutes", "transfer money");
        choices.Add(new string[] { "Continue" });
    }

    void LoadFinalResponse()
    {
        dialogueLines.Clear();
        choices.Clear();
        currentLine = 0;
        dialogueLines.Add("What do you do?");
        choices.Add(new string[] { "\"This is a scam. I am reporting you.\"", "\"Wait! I need more time!\"", "\"Okay, I’ll transfer now...\"" });
    }

    void EndGame(string message)
    {
        dialogueLines.Clear();
        choices.Clear();
        currentLine = 0;
        dialogueLines.Add(message);
        isTimerActive = false;
        DisplayNextLine();
    }

    IEnumerator FadeToScene(int sceneIndex, System.Action loadContent)
    {
        fadePanel.gameObject.SetActive(true);
        float alpha = 0;
        while (alpha < 1)
        {
            alpha += Time.deltaTime / fadeDuration;
            fadePanel.color = new Color(0, 0, 0, alpha);
            yield return null;
        }
        backgroundRenderer.sprite = sceneBackgrounds[sceneIndex];
        loadContent();
        while (alpha > 0)
        {
            alpha -= Time.deltaTime / fadeDuration;
            fadePanel.color = new Color(0, 0, 0, alpha);
            yield return null;
        }
        fadePanel.gameObject.SetActive(false);
    }
}