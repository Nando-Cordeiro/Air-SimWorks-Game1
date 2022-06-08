using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class miniGameButtons : MonoBehaviour
{
    public int sceneIndex;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void loadIndex()
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
