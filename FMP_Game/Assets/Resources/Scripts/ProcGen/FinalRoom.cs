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
    internal GameObject DemoEnd;
    void Start()
    {
        wallface = (GameObject)Resources.Load("Prefabs/Guardian");
        dialogueTrigger = (GameObject)Resources.Load("Prefabs/DialogueTrigger");

        rType = GetComponent<RoomType>();

        // 0 ---> (spawnerNum - 1)
        int randomSpawn = Random.Range(0, rType.endSpawners.Length);

        GameObject wall = Instantiate(wallface, rType.endSpawners[randomSpawn]);
        WallFace w = wall.GetComponent<WallFace>();
        w.endOfLevel = true;
        w.startRoomSpawn = GameObject.Find("PlayerSpawn").GetComponent<Transform>();
        w.DemoEnd = DemoEnd;

        dialogue = Instantiate(dialogueTrigger, rType.endSpawners[randomSpawn]);
     
        dialogueTrig = dialogue.GetComponent<DialogueTrigger>();
        dialogueTrig.SelfDelete = false;

        dialogueTrig.dialogue.name = "Guardian";
        dialogueTrig.dialogue.sentences.Add("All finished?");
        dialogueTrig.dialogue.sentences.Add("You managed to kill a few enemies I guess.");
        dialogueTrig.dialogue.sentences.Add("You can stay here and keep killing if you want.");
        dialogueTrig.dialogue.sentences.Add("Or if you're happy to leave now just press E again.");    
    }
}
