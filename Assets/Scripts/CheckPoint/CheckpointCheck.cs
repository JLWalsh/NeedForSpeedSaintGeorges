using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckpointCheck : MonoBehaviour {
    public static bool checkpointbool1 = false;
    public static bool checkpointbool2 = false;
    public static bool checkpointbool3 = false;
	
	void Update () {
        if (checkpointbool3 == true)
        {
            print("checkpoint 3 !");
        }
        else if (checkpointbool2 == true)
        {
            print("check point 2 !");
        }
        else if (checkpointbool1 == true)
        {
            print("check point 1 :)");
            
        }
	}

    void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Checkpoint1":
                checkpointbool1 = true;
                break;
            case "Checkpoint2":
                if (checkpointbool1 == true)
                {
                    checkpointbool2 = true;
                }
                break;
            case "Checkpoint3":
                if (checkpointbool2 == true)
                {
                    checkpointbool3 = true;
                }
                break;
        }
    }
}
