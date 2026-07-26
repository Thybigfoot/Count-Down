using TarodevController;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;
using UnityEngine.UI;


public class DialogueManager : MonoBehaviour
{
    private GameObject player;
    private GameObject diaryButton;
    private GameObject scrollbar;
    private GameObject exit;
    private GameObject mainCamera;
    [SerializeField] private GameObject victimEvidence;
    [SerializeField] private GameObject weaponEvidence;
    
    [SerializeField] private bool enable = false;

    // Text appearance
    [SerializeField] private float characterDelay = 0.03f;
    [SerializeField] private DialogueData[] dialogues;
    private TextMeshProUGUI dialogueText;
    private Coroutine typingCoroutine;
    private bool isTyping;
    private int currentDialogueNumber = 0;
    private DialogueData currentDialogue;
    private string currentLine;
    private int currentLineNumber = 0;

    private void Awake()
    {
        if (enable && dialogues != null){
            // Connect objects
            mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
            player = GameObject.FindGameObjectWithTag("Player");
            diaryButton = GameObject.FindGameObjectWithTag("DiaryButton");
            scrollbar = GameObject.FindGameObjectWithTag("Scrollbar");
            exit = GameObject.FindGameObjectWithTag("Exit");
            dialogueText = GetComponent<TextMeshProUGUI>();
            
            // Disable movement
            player.GetComponent<PlayerController>().SetMovementEnabled(false);

            // Hide other UI
            if (diaryButton != null)
            {
                diaryButton.SetActive(false);
            }
            if (scrollbar != null)
            {
                scrollbar.SetActive(false);
            }

            // Zoom in on the player
            Camera.main.orthographicSize = 5f;
        }
    }

    void Start()
    {
        if (enable){
            PlayDialogue();
        }
    }

    private void ApplyFlag(string flag)
    {
        switch (flag)
        {
            case "victim_zoom":
                mainCamera.GetComponent<CameraFollow>().target = victimEvidence.transform;
                break;
            case "weapon_zoom":
                mainCamera.GetComponent<CameraFollow>().target = weaponEvidence.transform;
                break;
            case "diary":
                diaryButton.SetActive(true);
                break;
            case "scrollbar":
                scrollbar.SetActive(true);
                break;
            case "exit":
                mainCamera.GetComponent<CameraFollow>().target = exit.transform;
                break;
            case "player_zoom":
                mainCamera.GetComponent<CameraFollow>().target = player.transform;
                break;
        }
    }

    // Text appearance
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
            

            // Flag checker
            if (currentDialogue.lines[currentLineNumber].flag != null)
            {
                ApplyFlag(currentDialogue.lines[currentLineNumber].flag);
            }

            currentLineNumber += 1;
        }
        else
        {
            // Dialogue ended 
            dialogueText.text = "";
            currentDialogue = null;
            currentDialogueNumber += 1;
            currentLineNumber = 0;

            player.GetComponent<PlayerController>().SetMovementEnabled(true);
            mainCamera.GetComponent<CameraFollow>().target = player.transform;
            Camera.main.orthographicSize = 8f;
        }
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
        // Typewrite effect
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
