using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LocationTracking : NetworkBehaviour {
	[SyncVar]
	public float latitudeLocation;
	[SyncVar]
	public float longitudeLocation;
    private bool updateStart = true;

	public IEnumerator Start()
    {
		//StartCoroutine(loop());
        // First, check if user has location service enabled
        if (!Input.location.isEnabledByUser)
            yield break;

        // Start service before querying location
        Input.location.Start(0.5f,0.5f);

        // Wait until service initializes
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        // Service didn't initialize in 20 seconds
        if (maxWait < 1)
        {
            print("Timed out");
            yield break;
        }

        // Connection has failed
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            print("Unable to determine device location");
            yield break;
        }
        else
        {
            // Access granted and location value could be retrieved
            print("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);
            latitudeLocation 			= Input.location.lastData.latitude;
		    longitudeLocation 			= Input.location.lastData.longitude;
            CmdUpdateLocation(Input.location.lastData.latitude, Input.location.lastData.longitude);
        }

        // Stop service if there is no need to query location updates continuously
        Input.location.Stop();
    }

    void Update(){
        if(updateStart){
            StopAllCoroutines();
            StartCoroutine(loop());
            updateStart = false;
        }
    }

    /*
	void OnGUI(){
		GUIStyle style = new GUIStyle();
		style.fontSize = Screen.height/16;
		style.normal.textColor = Color.white;
		
		GUI.Label(new Rect(Screen.width/16, Screen.height/100, Screen.width, Screen.height/8), latitudeLocation.ToString(),style);
		GUI.Label(new Rect(Screen.width/16, Screen.height/16, Screen.width, Screen.height/8), longitudeLocation.ToString(),style); 
	}
    */

	public IEnumerator loop(){
		StopCoroutine(Start());
		yield return new WaitForSeconds(2);
		StartCoroutine(Start());
		StartCoroutine(loop());
	}

    [Command]
    public void CmdUpdateLocation(float latit, float longit){
        latitudeLocation 			= latit;
		longitudeLocation 			= longit;
    }
}
