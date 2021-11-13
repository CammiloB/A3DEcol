using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using UnityEngine.UI;

public class FilePathController : MonoBehaviour
{
    
    private string path;
    public Text text;

    string[] extensions = {"pyecolib files", "csv"};


    public string getPath(){
        Debug.Log("GetPath: "+this.path);
        return this.path;
    }

    public void searchPath(){
        path = EditorUtility.OpenFilePanelWithFilters("Seleccione el archivo", "", extensions);
        text.text = path;
    }

}
