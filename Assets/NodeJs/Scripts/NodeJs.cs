using UnityEngine;


public class NodeJs : MonoBehaviour {

	public static readonly string NODE_DEFAULT_PATH = ".node";
//TODO: Linux? Android?
	#if UNITY_STANDALONE_WIN
	private static readonly string NODE_BIN = "node.exe";
	#endif
	#if UNITY_STANDALONE_OSX
//TODO: change
//=> ADD IOS BINARY (portable?)
	private static readonly string NODE_BIN = "node";
	#endif

	public static readonly string SCRIPT_DEFAULT_PATH = ".script";


    public bool useNodeEmbedded = true;
    public bool useNodeDefault = true;
    public string nodePath = "";

    public bool useScriptPathDefault = true;
    public string scriptPath = "";
    public string scriptName = "";
    public string scriptArguments = "";

    public bool useCommunication;
    public CommunicationController communicationController;

    private string nodeFullPath;
    private string scriptFullPath;
    private bool isInitialized = false;

//TODO: should be public?
    private bool isRunning = false;
    private System.Diagnostics.Process process_ = null;


    public void Init() {
        if (isInitialized) {
            print("Process already initialized");
        }

        if (useNodeEmbedded) {
            if (useNodeDefault) {
                nodePath = NODE_DEFAULT_PATH;
                nodeFullPath = System.IO.Path.Combine(Application.streamingAssetsPath, nodePath);
            }
            else {
                nodeFullPath = System.IO.Path.Combine(Application.streamingAssetsPath, nodePath);
            }
        }
        else {
            nodePath = "";
            nodeFullPath = "";
        }
//print("nodeFullPath: " + nodeFullPath);


//TODO: allow possibility to use external script...
        if (useScriptPathDefault) {
            scriptPath = SCRIPT_DEFAULT_PATH;
            scriptFullPath = System.IO.Path.Combine(Application.streamingAssetsPath, scriptPath);
        }
        else {
            scriptFullPath = System.IO.Path.Combine(Application.streamingAssetsPath, scriptPath);
        }
//print("scriptFullPath: " + scriptFullPath);


        if (useCommunication) {
            if (communicationController == null) {
                throw new System.Exception("No Communication Manager provided!");
            }
        }

        isInitialized = true;
	}

//TODO: OK?
    public void Reset() {
        Stop();
        isInitialized = false;
    }

    public bool Run() {
		if (isRunning) {
			Debug.LogError("Already Running: " + scriptName);
			return true;
		}

        if (!isInitialized) {
			Debug.LogError("Process not initilized, call 'Init' first!");
			return false;
        }

        try {
            StartProcess();
            isRunning = true;
        }
        catch (System.Exception e) {
            Debug.LogException(e, this);
//TODO: do that here?
            if (process_ != null) {
                process_.Dispose();
                process_ = null;
            }
        }

        return isRunning;
	}

    public void Stop() {
		if (!isRunning) {
			Debug.LogError("Already Stopped: " + scriptName);
			return;
		}

        StopProcess();
    }


    private void StartProcess() {
        if (scriptName == "") {
//TODO: if no script provided, should redirect the input, & listen to user/app input!
//            print("No script name provided!");
//TODO: use other exception type?
            throw new System.Exception("No script name provided!");
        }

        process_ = new System.Diagnostics.Process();

        process_.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
        process_.StartInfo.FileName = System.IO.Path.Combine(nodeFullPath, NODE_BIN);
        process_.StartInfo.Arguments = System.IO.Path.Combine(scriptFullPath, scriptName) + " " + scriptArguments;
        process_.StartInfo.CreateNoWindow = true;
        process_.StartInfo.RedirectStandardOutput = true;
        process_.StartInfo.RedirectStandardError = true;
        process_.StartInfo.UseShellExecute = false;
        process_.StartInfo.WorkingDirectory = scriptFullPath;

        if (useCommunication) {
            process_.OutputDataReceived += OnOutputData;
            process_.ErrorDataReceived += OnErrorData;
        }
        process_.EnableRaisingEvents = true;
        process_.Exited += OnExit;

//TODO: remove - debug purpose
print("Starting: " + process_.StartInfo.FileName + " " + process_.StartInfo.Arguments);
        process_.Start();

        process_.BeginOutputReadLine();
        process_.BeginErrorReadLine();
    }

    private void StopProcess() {
        if ((process_ != null) && !process_.HasExited) {
			process_.Kill();
//TODO: call WaitForExit?
            process_.Dispose();
            process_ = null;
		}
    }

	void OnDestroy() {
        StopProcess();
	}


//TODO: do the same with input
	private void OnOutputData(object sender, System.Diagnostics.DataReceivedEventArgs e) {
        communicationController.OutputData(e.Data);
	}

    private void OnErrorData(object sender, System.Diagnostics.DataReceivedEventArgs e) {
        communicationController.ErrorData(e.Data);
	}

	private void OnExit(object sender, System.EventArgs e) {
		isRunning = false;
		if (process_.ExitCode != 0) {
			Debug.LogError("Error! Exit Code: " + process_.ExitCode);
		}
	}

}
