using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalRoom : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject wallface;
    private RoomType rType;
    private GameObject dialogueTrigger;
    private GameObject dialogue;
    private DialogueTrigger dialogueTrig;
    void Start()
    {
        wallface = (GameObject)Resources.Load("Prefabs/Guardian");
        dialogueTrigger = (GameObject)Resources.Load("Prefabs/DialogueTrigger");

        rType = GetComponent<RoomType>();

        // 0 ---> (spawnerNum - 1)
        int randomSpawn = Random.Range(0, rType.endSpawners.Length);

        Instantiate(wallface, rType.endSpawners[randomSpawn]);
        dialogue = Instantiate(dialogueTrigger, rType.endSpawners[randomSpawn]);

        dialogueTrig = dialogue.GetComponent<DialogueTrigger>();  
        
        dialogueTrig.name = "Dialogue";
        dialogueTrig.dialogue.sentences.Add("1");
        dialogueTrig.dialogue.sentences.Add("2");
        dialogueTrig.dialogue.sentences.Add("3");
        dialogueTrig.dialogue.sentences.Add("4");
        dialogueTrig.dialogue.sentences.Add("5");
        //print(dialogueTrig.dialogue.sentences.Length);
        //dialogueTrig.dialogue.sentences[0] = "1";
        //dialogueTrig.dialogue.sentences[1] = "2";
        //dialogueTrig.dialogue.sentences[2] = "3";
        //dialogueTrig.dialogue.sentences[3] = "4";
    }
}
