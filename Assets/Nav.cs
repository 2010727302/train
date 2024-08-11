using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class Nav : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform target;
    private LineRenderer lineRenderer;
    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        lineRenderer = gameObject.GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    [System.Obsolete]
    void Update()
    {
        //�����Զ�Ѱ·��Ŀ���
        agent.SetDestination(target.position);
        //�����Զ�Ѱ·�ĵ������
        Vector3[] path = agent.path.corners;
        //�߶�����y���1����λ
        for (int i = 0; i < path.Length; i++)
        {
            path[i] = path[i] + new Vector3(0, 1, 0);
        }
        //���ö��������
        lineRenderer.SetVertexCount(path.Length);
        for (int i = 0; i < path.Length; i++)
        {
            //�����߶ε�·��
            lineRenderer.SetPosition(i, path[i]);
        }
    }
}

