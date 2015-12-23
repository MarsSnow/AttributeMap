using UnityEngine;
using System.Collections;


public class DrawStateDiagramPanel : MonoBehaviour
{
	public MeshFilter m_polygonMeshFilter = null;
	public MeshFilter m_backgroundMeshFilter = null;
	[HideInInspector]
	public int m_stateDiagramCornerNum = 5;                                                
   
	//Polygond                                           
	private Vector3[] m_polygondVertices;                                           
	private Vector2[] m_polygondUv;                                              
	private Vector3[] m_polygondCornerPositionArray;
	private int[] m_polygondTriangles; 
	private Vector3[] m_polygondCornerCirclePositonArray;                              
	private int m_polygondVerticesIndex1 = 0;
	private int m_polygondVerticesIndex2 = 0;
	private Mesh m_polygonMesh = null;

	//Bg
	private Vector3[] m_bgVertices;
	private Vector2[] m_bgUv;
	private Vector3[] m_bgCornerPositionArray;
	private int[] m_bgCornerValueArray; 
	private Vector3[] m_bgCornerCirclePositonArray; 
	private int m_bgVerticesIndex1 = 0; 
	private int m_bgVerticesIndex2 = 0;
	private Mesh m_bgMesh = null;

	public void DrawPanel(float[] diagramCornerValueArray)
	{
		DrawPolygon(diagramCornerValueArray);
		DrawBackground();
	}

	private void DrawPolygon(float[] diagramCornerValueArray)
	{
		InitPolygonCornerArray(diagramCornerValueArray);

		InitPolygonUv();           //uv
		InitPolygonVertices();     //vectices
		InitPolygonTriangles();    //triangles

		DrawPolygon();
	}

	private void DrawBackground()
	{
		InitBgCornerArray();

		InitBgUv();
		InitBgVertices();
		InitBgTriangles();

		DrawBg();
	}

	private void InitBgCornerArray()
	{
		m_bgCornerPositionArray = new Vector3[m_stateDiagramCornerNum];
		m_bgCornerCirclePositonArray = new Vector3[m_stateDiagramCornerNum];

		for (int i = 0; i < m_stateDiagramCornerNum; i++)
		{
			float angle = i * 2 * Mathf.PI / m_stateDiagramCornerNum;
			m_bgCornerPositionArray[i] = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0);
			m_bgCornerCirclePositonArray[i] = m_bgCornerPositionArray[i] * StateDiagramConst.kScaleValue;
		}
	}
	private void InitBgUv()
	{
		m_bgUv = new Vector2[m_stateDiagramCornerNum * StateDiagramConst.kTriangles];

		for (int i = 0; i < m_stateDiagramCornerNum * StateDiagramConst.kTriangles; i++)
		{ 
			switch (i % 3)
			{
				case 0:
					m_bgUv[i] = new Vector2(0, 0);
					break;
				case 1:
					m_bgUv[i] = new Vector2(0, 1);
					break;
				case 2:
					m_bgUv[i] = new Vector2(1, 1);
					break;
			}
		}
	}
	private void InitBgVertices()
	{
		m_bgVertices = new Vector3[m_stateDiagramCornerNum * StateDiagramConst.kTriangles];

		for (int i = 0; i < m_stateDiagramCornerNum * StateDiagramConst.kTriangles; i++)
		{
			switch (i % 3)
			{
				case 0:
					m_bgVertices[i] = Vector3.zero;
					break;
				case 1:
					m_bgVertices[i] = m_bgCornerCirclePositonArray[m_bgVerticesIndex1];
					m_bgVerticesIndex1 += 1;
					if (m_bgVerticesIndex1 == m_stateDiagramCornerNum)
						m_bgVerticesIndex1 = 0;
					break;
				case 2:
					m_bgVerticesIndex2 += 1;
					if (m_bgVerticesIndex2 == m_stateDiagramCornerNum)
					{
						m_bgVerticesIndex2 = 0;
					}
					m_bgVertices[i] = m_bgCornerCirclePositonArray[m_bgVerticesIndex2];
					break;
			}
		}
	}
	private void InitBgTriangles()
	{
		m_bgCornerValueArray = new int[m_stateDiagramCornerNum * StateDiagramConst.kTriangles];

		for (int i = 0; i < m_stateDiagramCornerNum * StateDiagramConst.kTriangles; i++)
		{ 
			m_bgCornerValueArray[i] = i;
		}
	}



	private void DrawBg()
	{
		m_bgMesh = m_backgroundMeshFilter.mesh;
		m_bgMesh.vertices = m_bgVertices;
		m_bgMesh.uv = m_bgUv;
		m_bgMesh.triangles = m_bgCornerValueArray;
	}

	private void DrawPolygon()
	{
		m_polygonMesh = m_polygonMeshFilter.mesh;
		m_polygonMesh.vertices = m_polygondVertices;       //顶点
		m_polygonMesh.uv = m_polygondUv;                   //uv坐标
		m_polygonMesh.triangles = m_polygondTriangles;     //三角形
	}

	private void InitPolygonCornerArray(float[] diagramCornerValueArray)
	{
		m_polygondCornerPositionArray = new Vector3[m_stateDiagramCornerNum];
		m_polygondCornerCirclePositonArray = new Vector3[m_stateDiagramCornerNum];

		for (int i = 0; i < m_stateDiagramCornerNum; i++)
		{ 
			float angle = i * 2 * Mathf.PI / m_stateDiagramCornerNum;
			m_polygondCornerPositionArray[i] = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0);
			m_polygondCornerCirclePositonArray[i] = m_polygondCornerPositionArray[i] * diagramCornerValueArray[i];
		}
	}

	private void InitPolygonUv()
	{
		m_polygondUv = new Vector2[m_stateDiagramCornerNum * StateDiagramConst.kTriangles];

		for (int i = 0; i < m_stateDiagramCornerNum * StateDiagramConst.kTriangles; i++)
		{ 
			switch (i % 3)
			{
				case 0:
					m_polygondUv[i] = new Vector2(0, 0);
					break;
				case 1:
					m_polygondUv[i] = new Vector2(0, 1);
					break;
				case 2:
					m_polygondUv[i] = new Vector2(1, 1);
					break;
			}
		}
	}

	private void InitPolygonTriangles()
	{
		m_polygondTriangles = new int[m_stateDiagramCornerNum * StateDiagramConst.kTriangles];

		for (int i = 0; i < m_stateDiagramCornerNum * StateDiagramConst.kTriangles; i++)
		{ 
			m_polygondTriangles[i] = i;
		}
	}

	private void InitPolygonVertices () 
	{
		m_polygondVertices = new Vector3[m_stateDiagramCornerNum * StateDiagramConst.kTriangles];

		for (int i = 0; i < m_stateDiagramCornerNum * StateDiagramConst.kTriangles; i++)
		{
			switch (i % 3) 
			{
				case 0:
					m_polygondVertices[i] = Vector3.zero;
					break;
				case 1:
					m_polygondVertices[i] = m_polygondCornerCirclePositonArray[m_polygondVerticesIndex1];
					m_polygondVerticesIndex1 += 1;
					if (m_polygondVerticesIndex1 == m_stateDiagramCornerNum)
					{
						m_polygondVerticesIndex1 = 0;
					}
					break;
				case 2:
					m_polygondVerticesIndex2 += 1;
					if (m_polygondVerticesIndex2 == m_stateDiagramCornerNum)
					{
						m_polygondVerticesIndex2 = 0;
					}
					m_polygondVertices[i] = m_polygondCornerCirclePositonArray[m_polygondVerticesIndex2];
					break;
			}
		}
	}
}
