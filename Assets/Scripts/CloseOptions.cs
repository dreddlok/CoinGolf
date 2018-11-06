using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseOptions : MonoBehaviour {

    public void CloseAndSave()
    {
        FindObjectOfType<PlayerSave>().Save();
    }

}
