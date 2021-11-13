using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class StartSimulationController : MonoBehaviour
{
    
    private GameObject menuObject;
    private FilePathController filePathController;
    public Initializer initializer;

    public Text errorText;

    public NotificationExample notification;

    void Start()
    {
        this.menuObject = GameObject.FindGameObjectWithTag("Menu");
        this.filePathController = GameObject.FindGameObjectWithTag("FilePathController").GetComponent<FilePathController>();
    }

    
    void Update()
    {
        
    }

    public void startSimulation(){
        errorText.enabled = false;
        string path = this.filePathController.getPath();
        
        if(path != null && path != ""){
            Destroy(this.menuObject);
            initializer.startSimulation(path);
        }else{
            errorText.enabled = true;
            errorText.text = "ERROR: Error al cargar el archivo con los datos de simulación";
        }
    }
}
