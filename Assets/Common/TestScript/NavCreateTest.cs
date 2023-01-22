using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using NavMeshBuilder =UnityEngine.AI.NavMeshBuilder;

public class NavCreateTest : MonoBehaviour
{
    public NavMeshData m_NavMeshData;
    private NavMeshDataInstance m_NavMeshInstance;

	private void Start()
	{
        m_NavMeshInstance = NavMesh.AddNavMeshData(m_NavMeshData);
    }

	void OnEnable()
    {
        //m_NavMeshInstance = NavMesh.AddNavMeshData(m_NavMeshData);
    }

    void OnDisable()
    {
        //NavMesh.RemoveNavMeshData(m_NavMeshInstance);
    }
}
