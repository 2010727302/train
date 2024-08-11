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
        // �������ʵ��
        material = lineRenderer.material;
        // ��������id����ֹ�����������Ե�ʱ���ظ��������ֵĹ�ϣ
        mainTexProperty = Shader.PropertyToID("_MainTex");
       

       tiling = new Vector2(1000, 0);
        offset = new Vector2(0, 0);
        // ����Tiling
        material.SetTextureScale(mainTexProperty, tiling);
        // ����Offset
        material.SetTextureOffset(mainTexProperty, offset);
    }
    // �߳�
    private float lineLen;
    // �ܶ�
    [SerializeField]
    private float density = 1000f;

    private void Update()
    {
        // ...

        // �����߳���

        
        lineLen = (lineRenderer.GetPosition(1) - lineRenderer.GetPosition(0)).magnitude;

       

        Debug.LogFormat("--- lineRenderer.GetPosition{0}", lineRenderer.GetPosition(1));
        // �����߶γ��ȼ���Tiling
        tiling = new Vector2(lineLen * density, 0);
        // ����Tiling
        material.SetTextureScale(mainTexProperty, tiling);
    }

}
