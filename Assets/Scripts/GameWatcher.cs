using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameWatcher : MonoBehaviour
{
    public Rigidbody[] balls;
    public Transform[] spawns;
    public int index;
    public CueControls cue;
    public CueBall cb;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator CheckMotion()
    {
        Debug.Log("Checking...");

        bool sleeping = false;

        while (!sleeping)
        {
            sleeping = true;

            foreach (Rigidbody rb in balls)
            {
                if (!rb.IsSleeping())
                {
                    sleeping = false;
                    yield return null;
                    break;
                }
            }
        }
        Debug.Log("All set!");

        cue.transform.localPosition = cue.initialPos;
        cue.IsInUse = true;
        cue.IsShooting = false;
        cue.FollowCueBall = true;
        cue.IsRotating = true;

    }

    public void Rerack()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
    
}
