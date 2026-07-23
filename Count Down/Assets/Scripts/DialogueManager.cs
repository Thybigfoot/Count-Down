using TarodevController;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;


public class DialogueManager : MonoBehaviour
{
    [SerializeField] private float characterDelay = 0.03f;
    [SerializeField] private PlayerController player;
    [SerializeField] private DialogueData[] dialogues;
    [SerializeField] private bool enable = false;
    private TextMeshProUGUI dialogueText;
    private Coroutine typingCoroutine;
    private bool isTyping;
    private int currentDialogueNumber = 0;
    private DialogueData currentDialogue;
    private string currentLine;
    private int currentLineNumber = 0;

    private void Awake()
    {
        if (enable){
            player.GetComponent<PlayerController>().SetMovementEnabled(false);
        }
        dialogueText = GetComponent<TextMeshProUGUI>();

    }

    void Start()
    {
        if (enable){
            PlayDialogue();
        }
    }

    public void PlayDialogue()
    {
        currentDialogue = dialogues[currentDialogueNumber];
        NextLine();
    }

    private void NextLine()
    {
        if (currentLineNumber < currentDialogue.lines.Length-1)
        {
            ShowLine(currentDialogue.lines[currentLineNumber].text);
            currentLineNumber += 1;

            // Flag checker
            if (currentDialogue.lines[currentLineNumber].flag != null)
            {
                ApplyFlag(currentDialogue.lines[currentLineNumber].flag);
            }
        }
        else
        {
            // Dialogue ended 
            dialogueText.text = "";
            currentDialogue = null;
            currentDialogueNumber += 1;
            currentLineNumber = 0;
            player.GetComponent<PlayerController>().SetMovementEnabled(true);
        }
    }

    private void ApplyFlag(string flag)
    {
        return;
    }

    public void ShowLine(string line)
    {
        currentLine = line;

        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        typingCoroutine = StartCoroutine(TypeLine(line));
    }

    private IEnumerator TypeLine(string line)
    {
        isTyping = true;
        dialogueText.text = "";

        foreach (char c in line)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(characterDelay);
        }

        isTyping = false;
    }

    private void Update()
    {
        if (currentDialogue != null){
            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                if (isTyping)
                {
                    StopCoroutine(typingCoroutine);
                    dialogueText.text = currentLine;
                    isTyping = false;
                }
                else
                {
                    NextLine();
                }
            }
        }
    }
}
