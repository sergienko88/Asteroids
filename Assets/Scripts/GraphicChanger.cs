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
        GameManager.ChangeGraphicEvent += ChangeGraphicType;
        Transform[] children = GetComponentsInChildren<Transform>(true);
        GraphicObjects = new Dictionary<GraphicType, Transform>();
        
            for (int i = 0; i < children.Length; i++)
            {
                if (children[i].name.ToLower().Contains(GraphicType.Mesh.ToString().ToLower()))
                {                    
                    if (!GraphicObjects.ContainsKey(GraphicType.Mesh))
                    {
                        GraphicObjects.Add(GraphicType.Mesh, children[i]);
                    }
                    else
                    {
                        GraphicObjects[GraphicType.Mesh] = children[i];
                    }
                    texture = (Texture2D)children[i].GetComponent<Renderer>().material.mainTexture;
                    sprite = Sprite.Create(
                        (texture ? texture : new Texture2D(1, 1)),
                        new Rect(0, 0, texture ? texture.width : 5, texture?texture.height:30),
                        new Vector2(0.5f, 0.5f)
                    );
                }
                if (children[i].name.ToLower().Contains(GraphicType.Sprite.ToString().ToLower()))
                {                    
                    if (!GraphicObjects.ContainsKey(GraphicType.Sprite))
                    {
                        GraphicObjects.Add(GraphicType.Sprite, children[i]);
                    }
                    else
                    {
                        GraphicObjects[GraphicType.Sprite] = children[i];
                    }
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
    
    void OnDestroy()
    {        
        GameManager.ChangeGraphicEvent -= ChangeGraphicType;
    }
}
