using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using System;

public class NavMove : MonoBehaviour
{
    private const float vcon = 0.1f;
    public NavMeshAgent agent;
    public Transform[] destPos = new Transform[] { };
    private LineRenderer lineRenderer;
    int currentPoint = 0;
    public AudioClip[] shellExplosionAudioClip= new AudioClip[] { };
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
        if(currentPoint==1)
        {
            text1[currentPoint].name = "coach";
        }
        agent.SetDestination(destPos[currentPoint].position);
        text1[currentPoint].GetComponentInChildren<TextMesh>().text = "����ǰ��" + destPos[currentPoint].position + "\n"+ text1[currentPoint].name;

        yield return StartCoroutine(WaitForDestination());// �ȴ�һ����Э�̽���

        StartCoroutine(NextWaypoint());
    }

    [Obsolete]
    IEnumerator WaitForDestination()
    {
        yield return new WaitForEndOfFrame();
        
        while (agent.pathPending)
            yield return null;//The coroutine will continue after all Update functions have been called on the next frame.
                                //��Cotoutine ������һ֡���е�Update()�������ù�֮�����
                yield return new WaitForEndOfFrame();

        float remain = agent.remainingDistance;
        while (remain == Mathf.Infinity || remain - agent.stoppingDistance > float.Epsilon
        || agent.pathStatus != NavMeshPathStatus.PathComplete)//����Ŀ�ĵ��˳�ѭ��
        {
            
            //text1[currentPoint].transform.position = destPos[currentPoint].position;
            //text1[currentPoint].AddComponent<TextMesh>();
            //GetComponent<TextMesh>().text = "3D TEXT";
            //�����Զ�Ѱ·�ĵ������
            Vector3[] path = agent.path.corners;
            //�߶�����y���1����λ
            for (int i = 0; i < path.Length; i++)
            {
                path[i] = path[i] + new Vector3(0, vcon, 0);//yСһ����Ծ�����������
            }

            //���ö��������
            lineRenderer.SetVertexCount(path.Length);
            for (int i = 0; i < path.Length; i++)
            {
                //�����߶ε�·��
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
        
        AudioSource.PlayClipAtPoint(shellExplosionAudioClip[currentPoint], destPos[currentPoint].position);
        // Debug.LogFormat("--- PathComplete to pos:{0}", destPos[currentPoint].position);
        //text1[currentPoint].GetComponentInChildren<TextMesh>().text = "����ǰ��"+destPos[currentPoint].position;
        currentPoint++;//next dest
        currentPoint = currentPoint % destPos.Length;//traverse between dest
        Transform next = destPos[currentPoint];
        agent.SetDestination(next.position);
        text1[currentPoint].name = "coach";
        text1[currentPoint].GetComponentInChildren<TextMesh>().text = "����ǰ��" + destPos[currentPoint].position+"\n"+ text1[currentPoint].name;
        yield return StartCoroutine(WaitForDestination());//

        StartCoroutine(NextWaypoint());//����Э�̣���֮ͣ������
    }
}