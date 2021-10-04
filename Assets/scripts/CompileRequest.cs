using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CompileRequest : MonoBehaviour
{
    public EntityBase entityBase;
    void Start()
    {
        
    }

    void Update()
    {
    
    }
    private UnityWebRequest url;
    public void compile()
    {
        if (0 == entityBase.entities.Count)
            return;
        EntityBase.current.select(null);
        String strtoSend = createString();
        Debug.Log(strtoSend);
        

        
        StartCoroutine(SendRequest(strtoSend));
        //Application.OpenURL(DownloadHandlerBuffer.GetContent(url));
    }
    public IEnumerator SendRequest(string str)
    {
        url = new UnityWebRequest("https://localhost:8083/create", "GET");
        url.downloadHandler = new DownloadHandlerBuffer();
        url.certificateHandler = new BypassCertificate();
        url.SetRequestHeader("parseLanguage", str);
        yield return url.SendWebRequest();
        if (url.isHttpError || url.isNetworkError)
        {
            Debug.Log("Server Error: " + url.error);
            yield break;
        }
        Debug.Log(url.downloadHandler.text);

    }
    public String createString()
    {

        EntityList el = new EntityList();
        el.entities = new Entity[entityBase.entities.Count];
        el.global_gesture = entityBase.global_gesture;
        for(int i = 0; i < entityBase.entities.Count; ++i)
        {
            el.entities[i] = new Entity(entityBase.entities[i].GetComponent<EntityComponents>().type, entityBase.entities[i].transform);
            el.entities[i].color = ColorUtility.ToHtmlStringRGB(entityBase.entities[i].GetComponent<Renderer>().material.GetColor("_Color"));
            el.entities[i].animation = new EntityAnimation(entityBase.entities[i].GetComponent<EntityComponents>());
            el.entities[i].swipe = entityBase.entities[i].GetComponent<EntityComponents>().swipe;
            el.entities[i].marker = entityBase.entities[i].GetComponent<EntityComponents>().marker;
        }

        return JsonConvert.SerializeObject(el);
    }
}

internal class EntityList
{
    public Entity[] entities;
    public bool global_gesture;
}
internal class Entity{
    public string type;
    public Vector3 position;
    public Vector3 rotation;
    public Vector3 scale;
    public string color;
    public EntityAnimation animation;
    public Swipe swipe;
    public string marker = "d";
    public Entity(string primitiveType, Transform t)
    {
        this.type = primitiveType;
        this.position = t.position;
        this.rotation = t.localEulerAngles;
        this.scale = t.localScale;
    }
}
internal class EntityAnimation
{
    public bool rotation;
    public Vector3 rotationAxis;
    public EntityAnimation(EntityComponents entityComponents)
    {
        rotation = entityComponents.rotation;
        rotationAxis = entityComponents.rotationAxis;
    }
}

public class BypassCertificate : CertificateHandler
{
    protected override bool ValidateCertificate(byte[] certificateData)
    {
        //Simply return true no matter what
        return true;
    }
}