using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[CustomEditor(typeof(NodeJs))]
public sealed class NodeJsEditor : Editor {

    SerializedProperty useNodeEmbeddedProp;
    SerializedProperty useNodeDefaultProp;
    SerializedProperty nodePathProp;

    SerializedProperty useScriptPathDefaultProp;
    SerializedProperty scriptPathProp;
    SerializedProperty scriptNameProp;
    SerializedProperty scriptArgumentsProp;

    bool showNode = false;
    bool showScript = true;


    //TODO: should use "InitializeOnLoad"?
    private void OnEnable() {
        useNodeEmbeddedProp = serializedObject.FindProperty("useNodeEmbedded");
        useNodeDefaultProp = serializedObject.FindProperty("useNodeDefault");
        nodePathProp = serializedObject.FindProperty("nodePath");
//TODO: check that not null!

        EditorPrefs.SetBool("useNodeEmbedded", useNodeEmbeddedProp.boolValue);
        EditorPrefs.SetBool("useNodeDefault", useNodeDefaultProp.boolValue);
        EditorPrefs.SetString("nodePath", nodePathProp.stringValue);


        useScriptPathDefaultProp = serializedObject.FindProperty("useScriptPathDefault");
        scriptPathProp = serializedObject.FindProperty("scriptPath");
        scriptNameProp = serializedObject.FindProperty("scriptName");
        scriptArgumentsProp = serializedObject.FindProperty("scriptArguments");
//TODO: check that not null!

        EditorPrefs.SetBool("useScriptPathDefault", useScriptPathDefaultProp.boolValue);
        EditorPrefs.SetString("scriptPath", scriptPathProp.stringValue);
        EditorPrefs.SetString("scriptName", scriptNameProp.stringValue);
        EditorPrefs.SetString("scriptArguments", scriptArgumentsProp.stringValue);
    }


    public override void OnInspectorGUI() {

        serializedObject.Update();


        showNode = EditorGUILayout.Foldout(showNode, "Node.js");
        if (showNode) {
            EditorGUI.indentLevel++;

/*
        EditorGUI.BeginChangeCheck();
        EditorGUILayout.PropertyField(useNodeProvidedProp, new GUIContent("Use provided Node.js"));
        if (EditorGUI.EndChangeCheck()) {
            EditorPrefs.SetBool("useNodeProvided", useNodeProvidedProp.boolValue);
        }

        if (!useNodeProvidedProp.boolValue) {
            EditorGUI.indentLevel++;

            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(useNodeSystemProp, new GUIContent("Use system Node.js"));
            if (EditorGUI.EndChangeCheck()) {
                EditorPrefs.SetBool("useNodeSystem", useNodeSystemProp.boolValue);
            }

            if (!useNodeSystemProp.boolValue) {

                EditorGUI.BeginChangeCheck();
                EditorGUILayout.PropertyField(nodePathProp, new GUIContent("Node.js path"));
                if (EditorGUI.EndChangeCheck()) {
                    EditorPrefs.SetString("nodePath", nodePathProp.stringValue);
                }
//TODO: change this if can use a script to copy external files (Python script?)
                EditorGUILayout.HelpBox("The specified path must be located in the 'StreamingAssets' directory or a subdirectory, and contain the Node.js executable.\n"
                        + "It will be included in the generated build", MessageType.Warning);
            }
            else {
                EditorGUILayout.HelpBox("The device running the application will need to have a Node.js installed and included in the system's path variable!", MessageType.Warning);
            }
            EditorGUI.indentLevel--;
        }
*/
        EditorGUI.BeginChangeCheck();
        EditorGUILayout.PropertyField(useNodeEmbeddedProp, new GUIContent("Use embedded Node.js"));
        if (EditorGUI.EndChangeCheck()) {
            EditorPrefs.SetBool("useNodeEmbedded", useNodeEmbeddedProp.boolValue);
        }

        if (useNodeEmbeddedProp.boolValue) {
            EditorGUI.indentLevel++;

            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(useNodeDefaultProp, new GUIContent("Use default path"));
            if (EditorGUI.EndChangeCheck()) {
                EditorPrefs.SetBool("useNodeDefault", useNodeDefaultProp.boolValue);
            }

            if (!useNodeDefaultProp.boolValue) {

                EditorGUI.BeginChangeCheck();
                EditorGUILayout.PropertyField(nodePathProp, new GUIContent("Node.js relative path"));
                if (EditorGUI.EndChangeCheck()) {
                    EditorPrefs.SetString("nodePath", nodePathProp.stringValue);
                }
//TODO: change this if can use a script to copy external files (Python script?)
                EditorGUILayout.HelpBox("The specified path must be located in the 'StreamingAssets' directory or a subdirectory, and contain the Node.js executable.\n"
                        + "It will be included in the generated build", MessageType.Warning);
            }
            else {
//TODO: keep this?
                EditorGUILayout.HelpBox("The default path for Node.js is \"" + NodeJs.NODE_DEFAULT_PATH + "\"", MessageType.Info);
            }
            EditorGUI.indentLevel--;
        }
        else {
            EditorGUILayout.HelpBox("The device running the application will need to have a Node.js installed and included in the system's path variable!", MessageType.Warning);
        }

            EditorGUI.indentLevel--;
        }


        var nodejs = target as NodeJs;

        showScript = EditorGUILayout.Foldout(showScript, "JS Script");
        if (showScript) {
            EditorGUI.indentLevel++;

            EditorGUI.BeginChangeCheck();
        EditorGUILayout.PropertyField(useScriptPathDefaultProp, new GUIContent("Use default script path"));
        if (EditorGUI.EndChangeCheck()) {
            EditorPrefs.SetBool("useScriptPathDefault", useScriptPathDefaultProp.boolValue);
        }

            if (!useScriptPathDefaultProp.boolValue) {
                EditorGUI.BeginChangeCheck();
                EditorGUILayout.PropertyField(scriptPathProp, new GUIContent("Relative path"));
                if (EditorGUI.EndChangeCheck()) {
                    EditorPrefs.SetString("scriptPath", scriptPathProp.stringValue);
                }
                EditorGUILayout.HelpBox("The specified path must be located in the 'StreamingAssets' directory or a subdirectory", MessageType.Warning);
            }
            else {
//TODO: keep this?
                EditorGUILayout.HelpBox("The default path for scripts is \"" + NodeJs.SCRIPT_DEFAULT_PATH + "\"", MessageType.Info);
            }

            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(scriptNameProp, new GUIContent("Name"));
            if (EditorGUI.EndChangeCheck()) {
                EditorPrefs.SetString("scriptName", scriptNameProp.stringValue);
            }

            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(scriptArgumentsProp, new GUIContent("Arguments"));
            if (EditorGUI.EndChangeCheck()) {
                EditorPrefs.SetString("scriptArguments", scriptArgumentsProp.stringValue);
            }

            EditorGUI.indentLevel--;
        }


//TODO: DO AS REST => PROPERTYFIELDS! (else problems of persistency)
//TODO: rewrite?
        var logLength = EditorGUILayout.IntField("Log Length", nodejs.logLength);
		if (logLength != nodejs.logLength) {
			nodejs.logLength = logLength;
		}

//TODO: should be optional
		EditorGUILayout.LabelField("Output Log");
		EditorGUI.indentLevel++;
		EditorGUILayout.TextArea(nodejs.log);
		EditorGUI.indentLevel--;

//TODO: REMOVE!
        var text = EditorGUILayout.ObjectField("text", nodejs.text, typeof(Text));
        if (text != nodejs.text)
            nodejs.text = (Text)text;


        serializedObject.ApplyModifiedProperties();

    }

}

