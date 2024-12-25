using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{

    public void ChagneScene(int Sceneid)
    {
        SceneManager.LoadScene(Sceneid);
    }
    public void QUIT()
    {
        Application.Quit();
    }
    
}
