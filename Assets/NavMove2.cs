using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using System;

public class NavMove2 : MonoBehaviour
{
    private const float vcon = 0.1f;
    public NavMeshAgent agent;
    public Transform[] destPos = new Transform[] { };
    private LineRenderer lineRenderer;
    int currentPoint = 0;
    public AudioClip[] shellExplosionAudioClip = new AudioClip[] { };
    public GameObject[] text1 = new GameObject[] { };
    //public AudioClip shellExplosionAudioClip ;
    [Obsolete]
    void Start()
    {
        agent.Stop();

        lineRenderer = gameObject.GetComponent<LineRenderer>();
        TextMesh myText = (TextMesh)GetComponent("TextMesh");
        StartCoroutine(Move());
    }

    [Obsolete]
    IEnumerator Move()
    {
        //enable agent updates
        agent.Resume();
        agent.updateRotation = true;
        if (currentPoint == 1)
        {
            text1[currentPoint].name = "coach";
        }
        AudioSource.PlayClipAtPoint(shellExplosionAudioClip[0], destPos[currentPoint].position);
        agent.SetDestination(destPos[currentPoint].position);
        text1[currentPoint].GetComponentInChildren<TextMesh>().text = "正在前往" + destPos[currentPoint].position + "\n" + text1[currentPoint].name;

        yield return StartCoroutine(WaitForDestination());// 等待一个新协程结束

        StartCoroutine(NextWaypoint());
    }
  
    [Obsolete]
    IEnumerator WaitForDestination()
    {
        yield return new WaitForEndOfFrame();

        while (agent.pathPending)
            yield return null;//The coroutine will continue after all Update functions have been called on the next frame.
                              //该Cotoutine 会在下一帧所有的Update()函数调用过之后继续
        yield return new WaitForEndOfFrame();

        float remain = agent.remainingDistance;
        while (remain == Mathf.Infinity || remain - agent.stoppingDistance > float.Epsilon
        || agent.pathStatus != NavMeshPathStatus.PathComplete)//到达目的地退出循环
        {

            //text1[currentPoint].transform.position = destPos[currentPoint].position;
            //text1[currentPoint].AddComponent<TextMesh>();
            //GetComponent<TextMesh>().text = "3D TEXT";
            //储存自动寻路的点的坐标
            Vector3[] path = agent.path.corners;
            //线段整体y轴加1个单位
            for (int i = 0; i < path.Length; i++)
            {
                path[i] = path[i] + new Vector3(0, vcon, 0);//y小一点可以尽量贴近地面
            }

            //设置定点的数量
            lineRenderer.SetVertexCount(path.Length);
            for (int i = 0; i < path.Length; i++)
            {
                //设置线段的路劲
                lineRenderer.SetPosition(i, path[i]);
            }

            remain = agent.remainingDistance;

            yield return null;
        }

        Debug.LogFormat("--- PathComplete to pos:{0}", currentPoint);
    }
    [Obsolete]
    IEnumerator NextWaypoint()
    {

        AudioSource.PlayClipAtPoint(shellExplosionAudioClip[0], destPos[currentPoint].position);
        // Debug.LogFormat("--- PathComplete to pos:{0}", destPos[currentPoint].position);
        //text1[currentPoint].GetComponentInChildren<TextMesh>().text = "正在前往"+destPos[currentPoint].position;
        currentPoint++;//next dest
        currentPoint = currentPoint % destPos.Length;//traverse between dest
        Transform next = destPos[currentPoint];
        agent.SetDestination(next.position);
        //text1[currentPoint].name = "coach";
       //text1[currentPoint].GetComponentInChildren<TextMesh>().text = "正在前往" + destPos[currentPoint].position + "\n" + text1[currentPoint].name;
        yield return StartCoroutine(WaitForDestination());//

        StartCoroutine(NextWaypoint());//开启协程，暂停之后运行
    }
}