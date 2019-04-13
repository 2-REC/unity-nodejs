using UnityEngine;

public abstract class CommunicationController : MonoBehaviour {

//TODO: input
//    public abstract void InputData(string message);

    public abstract void OutputData(string message);

    public abstract void ErrorData(string message);

}
