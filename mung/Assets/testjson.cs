using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.IO;
using UnityEngine;

public class testjson : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LinkData(string gid)
    {
        string url = "SheetURL" + gid;

        ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

        WebRequest request = WebRequest.Create(url);
        request.Credentials = CredentialCache.DefaultCredentials;
        WebResponse response = request.GetResponse();
        Stream dataStream = response.GetResponseStream();
        StreamReader reader = new StreamReader(dataStream);
        string LoadData = reader.ReadToEnd();
        //string TableToJsonString = LoadData.Split(WCutString, 4, System.StringSplitOptions.None)[2];
        //LoadGoogleSheet DataCall = ()
        
    }
}
