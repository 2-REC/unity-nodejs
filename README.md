# Introduction

Unity project allowing to execute a JavaScript script via a Node.js instance.<br>
It is based on the [NodejsUnity project](//github.com/hecomi/NodejsUnity) from [hecomi](//github.com/hecomi).<br>


# Usage
To use this project in an Unity project, download this repository, and copy/import the "NodeJs" and "StreamingAssets" directories in the project's "Assets" directory.<br>
The entire directories or only required elements can be copied.<br>

The project is structured as follow:<br>
- [NodeJs]
  - [Editor]
    - NodeJsEditor.cs
  - [Examples]
    - [Prefabs]
      - NodeJs
    - [Scenes]
      - ExampleScene.unity
    - [Scripts]
      - CommunicationControllerImpl.cs
      - NodeJsController.cs
  - [Scripts]
    - CommunicationController.cs
    - NodeJs.cs
- [StreamingAssets]
  - [.node]
    - LICENSE.txt
    - node.exe
  - [.script]
    - example_script.js


The core of the project is the "NodeJs" script located in the "NodeJs/Scripts" directory.<br>

... need NodeJs executable, a script and its dependencies (+node_modules)...

An example using a CommunicationController implementation can be found in the provided "ExampleScene" scene.<br>
... using script "example_script.js"...


# Parameters/Configuration

## Node.js

- Use Embedded Node.js<br>
If the setting is checked, the application will use the Node.js executable provided with the project, that will be embedded in the generated built.<br>
If the setting is not checked, the application will look for the Node.js executable installed in the system (which will have to be present on the end user's machine and defined in the PATH environment variable).<br>

- Use Default Path<br>
This setting is only enabled if "Use Embedded Node.js" is checked.<br>
If checked, the application will look for the Node.js executable in the default directory ".node" (located in "StreamingAssets"). The Node.js executable provided in the directory will be embedded in the generated built (as well as the entire directory).<br>
If the setting is not checked, a different path can be specified.<br>

- Node.js relative path<br>
This setting is only enabled if "Use Default Path" is checked.<br>
It allows to specify a different path for the Node.js executable than the default path. The provided path must be a subdirectory of the "StreamingAssets" directory.<br>
The Node.js executable provided in the directory will be embedded in the generated built (as well as the entire directory).<br>


## JS Script

- Use Default Script Path<br>
If the setting is checked, the application will look for the JavaScript script to execute in the default directory ".script" (located in "StreamingAssets"). The specified directory will be embedded in the generated built.<br>
If the setting is not checked, a different path can be specified.<br>

- Relative Path<br>
This setting is only enabled if "Use Default Script Path" is checked.<br>
It allows to specify a different path for the JavaScript script than the default path. The provided path must be a subdirectory of the "StreamingAssets" directory.<br>
The specified directory will be embedded in the generated built.<br>

- Name<br>
The name of the JavaScript script to execute.<br>
It must be located in the specified directory (either the default directory, or the one specified as "Relative Path").<br>

- Arguments<br>
The arguments to provide to the JavaScript script.<br>


## Communication

- Use Communication<br>
This setting allows to provide an object in order to exchange messages with the application.<br>

- Communication Controller<br>
This setting is only enabled if "Use Communication" is checked.<br>
It allows to provide an implementation of the CommunicationController class in order to exchange messages with the application.<br>
(Currently only output messages are handled: the output and error streams from the Node.js application)<br>


# Resources/Links
- [NodejsUnity project](//github.com/hecomi/NodejsUnity) from [hecomi](//github.com/hecomi): The original project from which this is based on.
- [Node.js](https://nodejs.org/): JavaScript run-time environment to execute JavaScript code outside of a browser.
