using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum GraphicType
{
    @Mesh,
    @Sprite
}

public class GraphicChanger : MonoBehaviour {
    Dictionary<GraphicType, Transform> GraphicObjects = new Dictionary<GraphicType, Transform>();
    public static GraphicType GraphicType = GraphicType.Mesh;
    Texture2D texture;
    Sprite sprite;
	// Use this for initialization
	void Start () {
        GameManager.ChangeGraphic += ChangeGraphicType;
        Transform[] children = GetComponentsInChildren<Transform>();
        for (int i = 0; i < children.Length; i++)
        {
            if (children[i].name.ToLower().Contains(GraphicType.Mesh.ToString().ToLower()))
            {
                GraphicObjects.Add(GraphicType.Mesh, children[i]);                
                texture = (Texture2D)children[i].GetComponent<Renderer>().material.mainTexture;

                sprite = Sprite.Create(
                    (Texture2D)children[i].GetComponent<Renderer>().material.GetTexture("_MainTex"),
                    new Rect(0, 0, children[i].GetComponent<Renderer>().material.mainTexture.width, children[i].GetComponent<Renderer>().material.mainTexture.height),
                    new Vector2(0.5f, 0.5f)
                );
            }            
            if (children[i].name.ToLower().Contains(GraphicType.Sprite.ToString().ToLower()))
            {
                GraphicObjects.Add(GraphicType.Sprite, children[i]);
            }
        }
        GraphicObjects[GraphicType.Sprite].GetComponent<SpriteRenderer>().sprite = sprite;
        ChangeGraphicType(GameManager.GraphicType);
	}

    void ChangeGraphicType(GraphicType type)
    {        
        foreach (var item in GraphicObjects)
        {
            item.Value.transform.gameObject.SetActive(item.Key.ToString()==type.ToString());
        }
    }



}
