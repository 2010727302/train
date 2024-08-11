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
        //设置自动寻路的目标点
        agent.SetDestination(target.position);
        //储存自动寻路的点的坐标
        Vector3[] path = agent.path.corners;
        //线段整体y轴加1个单位
        for (int i = 0; i < path.Length; i++)
        {
            path[i] = path[i] + new Vector3(0, 1, 0);
        }
        //设置定点的数量
        lineRenderer.SetVertexCount(path.Length);
        for (int i = 0; i < path.Length; i++)
        {
            //设置线段的路劲
            lineRenderer.SetPosition(i, path[i]);
        }
    }
}

