using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(NodeJs))]
public sealed class NodeJsEditor : Editor {

    bool showNode = false;
    SerializedProperty useNodeEmbeddedProp;
    SerializedProperty useNodeDefaultProp;
    SerializedProperty nodePathProp;

    bool showScript = true;
    SerializedProperty useScriptPathDefaultProp;
    SerializedProperty scriptPathProp;
    SerializedProperty scriptNameProp;
    SerializedProperty scriptArgumentsProp;

    SerializedProperty useCommunicationProp;
    SerializedProperty communicationControllerProp;


//TODO: should use "InitializeOnLoad"?
    private void OnEnable() {
        useNodeEmbeddedProp = serializedObject.FindProperty("useNodeEmbedded");
        useNodeDefaultProp = serializedObject.FindProperty("useNodeDefault");
        nodePathProp = serializedObject.FindProperty("nodePath");

        useScriptPathDefaultProp = serializedObject.FindProperty("useScriptPathDefault");
        scriptPathProp = serializedObject.FindProperty("scriptPath");
        scriptNameProp = serializedObject.FindProperty("scriptName");
        scriptArgumentsProp = serializedObject.FindProperty("scriptArguments");

        useCommunicationProp = serializedObject.FindProperty("useCommunication");
        communicationControllerProp = serializedObject.FindProperty("communicationController");

//TODO: check that each property is not null!
//...
    }


    public override void OnInspectorGUI() {
        serializedObject.Update();

        showNode = EditorGUILayout.Foldout(showNode, "Node.js");
        if (showNode) {
            EditorGUI.indentLevel++;

            EditorGUILayout.PropertyField(useNodeEmbeddedProp, new GUIContent("Use embedded Node.js"));
            if (useNodeEmbeddedProp.boolValue) {
                EditorGUI.indentLevel++;

                EditorGUILayout.PropertyField(useNodeDefaultProp, new GUIContent("Use default path"));
                if (!useNodeDefaultProp.boolValue) {

                    EditorGUILayout.PropertyField(nodePathProp, new GUIContent("Node.js relative path"));
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


        showScript = EditorGUILayout.Foldout(showScript, "JS Script");
        if (showScript) {
            EditorGUI.indentLevel++;

            EditorGUILayout.PropertyField(useScriptPathDefaultProp, new GUIContent("Use default script path"));
            if (!useScriptPathDefaultProp.boolValue) {
                EditorGUILayout.PropertyField(scriptPathProp, new GUIContent("Relative path"));
                EditorGUILayout.HelpBox("The specified path must be located in the 'StreamingAssets' directory or a subdirectory", MessageType.Warning);
            }
            else {
//TODO: keep this?
                EditorGUILayout.HelpBox("The default path for scripts is \"" + NodeJs.SCRIPT_DEFAULT_PATH + "\"", MessageType.Info);
            }

            EditorGUILayout.PropertyField(scriptNameProp, new GUIContent("Name"));
            EditorGUILayout.PropertyField(scriptArgumentsProp, new GUIContent("Arguments"));

            EditorGUI.indentLevel--;
        }


        EditorGUILayout.PropertyField(useCommunicationProp, new GUIContent("Use Communication"));
        if (useCommunicationProp.boolValue) {
            EditorGUILayout.PropertyField(communicationControllerProp, new GUIContent("Communication Controller"));
        }

        serializedObject.ApplyModifiedProperties();
    }

}
