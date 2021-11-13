using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Globalization;

using System;
using System.IO;

public class Initializer : MonoBehaviour
{

    public GameObject microtubuloPrefab;
    public GameObject bacteriaPrefab;

    public Camera camera;
    public int idCamera = 0;
    private int idCameraTemp;

    public Text timeText;
    public Dropdown cameraOptions;
    List<string> dropOptions = new List<string>();

    private string path;

    private string[] data;
    private int time = 1;


    public List<GameObject> bacterias = new List<GameObject>();


    private float spaceDif = 10;
    private float space = 0;


    public void startSimulation(string path){
        Debug.Log("Path: "+path);
        this.path = path;

        DebugGUI.SetGraphProperties("prueba", "Size Graph \n(Micrometros)", 0, 7, 3, new Color(0, 1, 1), false);

        cameraOptions.ClearOptions();

        

        dataInitializer();

        objectsInitialization();
  
        StartCoroutine(wait());

    }

    IEnumerator wait(){
        this.idCameraTemp = cameraOptions.value;
        for(;;){
            yield return new WaitForSeconds(0.5f);

            this.idCamera = cameraOptions.value;
        
            if(time < data.Length ){

                this.timeText.text = time.ToString() + " / "+data.Length;

                String tmp = "";
            
                for(int i=0; i<data[time].Length; i++){
                    tmp += data[time][i];
                    
                }

                

                String[] sizes = tmp.Split(',');
                DebugGUI.Graph("prueba", float.Parse(sizes[this.idCamera+1], CultureInfo.InvariantCulture.NumberFormat));

                for(int i=0; i<this.bacterias.Count; i++){
                    float bacteriaSize = float.Parse(sizes[i+1], CultureInfo.InvariantCulture.NumberFormat);
                    this.bacterias[i].transform.localScale = new Vector3(0.5f, bacteriaSize/10, 0.5f);
                }
                time += 1;
                
                this.changeCameraPosition();

                
            }
        }
    }

    void changeCameraPosition(){
        float x = this.bacterias[this.idCamera].transform.position.x;
        float y = this.camera.transform.position.y;
        float z = this.camera.transform.position.z;

        this.camera.transform.position = new Vector3(x, y, z);

        if(this.idCamera != this.idCameraTemp){
            DebugGUI.SetGraphProperties("prueba", "Size Graph\n(Micrometro)", 0, 7, 3, new Color( 
                UnityEngine.Random.Range(0f, 1f), 
                UnityEngine.Random.Range(0f, 1f), 
                UnityEngine.Random.Range(0f, 1f)), false);

            this.idCameraTemp = this.idCamera;
        }
        
    }

    void dataInitializer(){
        Debug.Log("Cargando Datos");
        try{
            //data = File.ReadAllLines("./Assets/Data/dataCRMnoisy.csv");
            data = File.ReadAllLines(this.path);
            for(int i=0; i<10; i++){
                var temp = data[i].Split(',');
            }
        } catch (Exception ex){
            Debug.Log("Error al cargar el archivo de tamaños. Error: "+ex);
        }
    }

    void objectsInitialization(){
        if(data.Length > 10){
            for(int i=0; i< 10; i++){
                Instantiate(microtubuloPrefab, new Vector3(space, 0f, 0f), Quaternion.identity);
                GameObject bacteriaTemp = Instantiate(bacteriaPrefab, new Vector3(space, 1f, 0f), Quaternion.identity);
                bacteriaTemp.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                this.bacterias.Add(bacteriaTemp);
                space += spaceDif;

                dropOptions.Add("Microcanal: "+i.ToString());
            }
        }else{
            for(int i=0; i<data.Length; i++){
                Instantiate(microtubuloPrefab, new Vector3(space, 0f, 0f), Quaternion.identity);
                GameObject bacteriaTemp = Instantiate(bacteriaPrefab, new Vector3(space, 1f, 0f), Quaternion.identity);
                bacteriaTemp.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                this.bacterias.Add(bacteriaTemp);
                space += spaceDif;

                dropOptions.Add("Microcanal: "+i.ToString());
            }
        }

        cameraOptions.AddOptions(dropOptions);
        
    }
}
