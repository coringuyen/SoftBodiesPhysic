using UnityEngine;
using System.Collections;
using UnityEngine.UI;
 
public class Exit : MonoBehaviour {

	public void exitApplication()
    {
        Application.Quit();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            exitApplication();
        }
    }
}
