using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class arenaEnd : MonoBehaviour
{
    public score score;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(score.points >= 20)
        {
            SceneManager.LoadScene(0);
        }
        if(Input.GetButtonDown("Cancel"))
        {
            //SceneManager.LoadScene(0);
        }
    }
}
