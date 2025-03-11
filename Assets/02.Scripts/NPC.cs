using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class NPC : MonoBehaviour, IInteractable
{
    public NPCData NPCData;
    public DialogueUI dialogueUI;
    public TextMeshProUGUI linetxt;
    public TextMeshProUGUI nametxt;

    private void Start()
    {
        this.dialogueUI = UIManager.Instance.dialogueUI;
        linetxt = dialogueUI.lineTxt;
        nametxt = dialogueUI.nameTxt;
    }
    public string GetInteractInfo()
    {
        string str = $"{NPCData.npcName}\n{NPCData.npcDescription}";
        return str;
    }

    public void OnInteract()
    {
        if (NPCData.lines.Length > 0)
        {
            StartCoroutine(SpeakCo());
        }
    }

    public IEnumerator SpeakCo()
    {
        Time.timeScale = 0;
        Interaction interaction = CharacterManager.Instance.Player.GetComponent< Interaction>();
        CharacterManager.Instance.Player.controller.canLook = false;
        interaction.canInteract = false;
        nametxt.text = NPCData.npcName;
        linetxt.text = NPCData.lines[0];
        dialogueUI.gameObject.SetActive(true);
        yield return new WaitUntil(() => Input.GetKeyUp(KeyCode.E));
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.E));
        if(NPCData.lines.Length > 1)
        {
            for(int i = 1; i < NPCData.lines.Length; i++)
            {
                linetxt.text = NPCData.lines[i];
                yield return new WaitUntil(() => Input.GetKeyUp(KeyCode.E));
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.E));
            }
        }
        dialogueUI.gameObject.SetActive(false);
        Time.timeScale = 1;
        interaction.canInteract = true;
        CharacterManager.Instance.Player.controller.canLook = true;
    }
}
