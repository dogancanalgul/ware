using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddEntity : MonoBehaviour, Undoable
{
    public GameObject lastCreated;
    public GameObject narutoPrefab;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void add(GameObject entity, string type)
    {
        lastCreated = entity;
        entity.tag = "Manipulatable";
        entity.AddComponent<EntityComponents>();
        entity.GetComponent<EntityComponents>().type = type;
        EntityBase.current.entities.Add(lastCreated);
        UndoButton.current.Register(this);
    }
    public void addBox()
    {
        GameObject ob = GameObject.CreatePrimitive(PrimitiveType.Cube);
        add(ob, "box");
    }
    public void addSphere()
    {
        GameObject ob = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        add(ob, "sphere");
    }
    public void addNaruto()
    {
        GameObject ob = GameObject.Instantiate(narutoPrefab);
        ob.GetComponentInChildren<MeshRenderer>().gameObject.tag = "Manipulatable";
        add(ob, "naruto");
    }
    public void Undo()
    {
        EntityBase.current.entities.Remove(lastCreated);
        GameObject.Destroy(lastCreated);
        lastCreated = null;
    }
}
