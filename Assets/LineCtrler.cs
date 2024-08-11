using UnityEngine;
using UnityEngine.AI;
public class LineCtrler : MonoBehaviour
{
    [SerializeField]
    private LineRenderer lineRenderer;
    private Material material;
    private Vector2 tiling;
    private Vector2 offset;
    private int mainTexProperty;
   // public NavMeshAgent agent;

    void Start()
    {
        // 缓存材质实例
        material = lineRenderer.material;
        // 缓存属性id，防止下面设置属性的时候重复计算名字的哈希
        mainTexProperty = Shader.PropertyToID("_MainTex");
       

       tiling = new Vector2(1000, 0);
        offset = new Vector2(0, 0);
        // 设置Tiling
        material.SetTextureScale(mainTexProperty, tiling);
        // 设置Offset
        material.SetTextureOffset(mainTexProperty, offset);
    }
    // 线长
    private float lineLen;
    // 密度
    [SerializeField]
    private float density = 1000f;

    private void Update()
    {
        // ...

        // 计算线长度

        
        lineLen = (lineRenderer.GetPosition(1) - lineRenderer.GetPosition(0)).magnitude;

       

        Debug.LogFormat("--- lineRenderer.GetPosition{0}", lineRenderer.GetPosition(1));
        // 根据线段长度计算Tiling
        tiling = new Vector2(lineLen * density, 0);
        // 设置Tiling
        material.SetTextureScale(mainTexProperty, tiling);
    }

}
