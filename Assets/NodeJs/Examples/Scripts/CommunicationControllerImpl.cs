using System.Collections.Generic;
using UnityEngine.UI;

//TODO: for some unknown reason, the text fields are not updated in display...?

public class CommunicationControllerImpl : CommunicationController {

    public Text textOutput;
    public int textOutputLength = 10;

    public Text textError;
    public int textErrorLength = 10;


    private List<string> outputLogs = new List<string>();
    private List<string> errorLogs = new List<string>();


//TODO: input
//    public override void InputData(string message) {}

    public override void OutputData(string message) {
//        textOutput.text += message + '\n';

		if (outputLogs.Count > textOutputLength) {
            outputLogs.RemoveAt(0);
        }
        outputLogs.Add(message);

        string log = "";
        outputLogs.ForEach(line => { log += line + "\n"; });
print("output: " + log);
        textOutput.text = log;
    }

    public override void ErrorData(string message) {
//        textError.text += "ERROR: " + message + '\n';

		if (errorLogs.Count > textErrorLength) {
            errorLogs.RemoveAt(0);
        }
        errorLogs.Add(message);

        string log = "";
        errorLogs.ForEach(line => { log += line + "\n"; });
print("error: " + log);
        textError.text = log;
    }

}
