using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum GraphicType
{
    @Mesh,
    @Sprite
}

class GraphicStateInfo
{
    public Sprite sprite;
    public Material material;
    public Mesh mesh;
}

public class GraphicChanger : MonoBehaviour {
    Dictionary<Transform, GraphicStateInfo> info = new Dictionary<Transform, GraphicStateInfo>();
    public static GraphicType GraphicType = GraphicType.Mesh;
	// Use this for initialization
	void Start () {
        Renderer[] rs = GetComponentsInChildren<Renderer>();
        Sprite tmp = new Sprite();
        for (int i = 0; i < rs.Length; i++)
        {
               tmp = Sprite.Create(
                    (Texture2D)rs[i].material.GetTexture("_MainTex"),
                    new Rect(0, 0, rs[i].material.mainTexture.width, rs[i].material.mainTexture.height),
                    new Vector2(0.5f, 0.5f)
                );
           
                
            info.Add(rs[i].transform, new GraphicStateInfo {
                material = rs[i].material,
                mesh = rs[i].transform.GetComponent<MeshFilter>().mesh,
                sprite = tmp
            });
        }
	}

    void ChangeGraphicType(GraphicType type)
    {
        switch (type)
        {
            case GraphicType.Mesh:
                foreach (var item in info)
                {
                    StartCoroutine(WaitForEndOfFrame(
                        () =>
                        {
                            Destroy(item.Key.GetComponent<SpriteRenderer>());
                        }
                        ,
                        () =>
                        {
                            item.Key.gameObject.AddComponent<MeshFilter>().mesh = item.Value.mesh;
                            item.Key.gameObject.AddComponent<MeshRenderer>().material = item.Value.material;
                        }
                        ));                   
                    item.Key.localScale = new Vector3(.76f, 1, 1);
                    GraphicType = global::GraphicType.Mesh;
                }
               
                break;

            case GraphicType.Sprite:
                foreach (var item in info)
                {
                    StartCoroutine(WaitForEndOfFrame(
                        () =>
                        {
                            Destroy(item.Key.GetComponent<MeshFilter>());
                            Destroy(item.Key.GetComponent<MeshRenderer>());
                        }
                        ,
                        () =>
                        {
                            item.Key.gameObject.AddComponent<SpriteRenderer>();
                            ((SpriteRenderer)item.Key.GetComponent<Renderer>()).sprite = item.Value.sprite;
                        }
                        ));
                    GraphicType = global::GraphicType.Sprite;
                    item.Key.localScale = Vector3.one * .1f;
                }

                break;


            default:
                break;
        }
    }

    IEnumerator WaitForEndOfFrame(System.Action b, System.Action a)
    {
        b += () => { };
        a += () => { };
        b();
        yield return new WaitForEndOfFrame();
        a();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            ChangeGraphicType(GraphicType== global::GraphicType.Mesh? GraphicType.Sprite:GraphicType.Mesh);
        }
    }
}
