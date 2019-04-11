using UnityEngine;

public class NodeJsController : MonoBehaviour {

    public NodeJs nodejs;

    void Awake() {
        Debug.Log("Initialising Node.js script");
        nodejs.Init();
    }

    void Start() {
        Debug.Log("Starting Node.js script");
        if (!nodejs.Run()) {
            Debug.LogError("Error starting Node.js script");
            Application.Quit(-1);
        }

    }

/*
    void Update() {
    }
*/

    void OnDestroy() {
        Debug.Log("Stopping Node.js script");
        nodejs.Stop();
    }

}
