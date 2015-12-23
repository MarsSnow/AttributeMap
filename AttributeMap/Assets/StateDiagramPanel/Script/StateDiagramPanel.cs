using UnityEngine;
using System.Collections;
using System.Deployment.Internal;

public class StateDiagramPanel : MonoBehaviour
{
    public DrawStateDiagramPanel m_drawStateDiagramPanel = null;
    public UILabel[] m_attrDescribeArray = null;
    public UILabel[] m_attrValueArray = null;

    private void OnEnable()
    {
        float[] valueArray = GetAttrValueArray();
        m_drawStateDiagramPanel.DrawPanel(valueArray);
        SetAttrDescribeInfo(valueArray);
    }

    private float[] GetAttrValueArray()
    {
        int count = m_drawStateDiagramPanel.m_stateDiagramCornerNum;
        float[] valueArray = new float[count];
        for (int i = 0; i < count; i++)
        {
            valueArray[i] = 0.5f * StateDiagramConst.kScaleValue;
        }
        return valueArray;
    }

    private void SetAttrDescribeInfo(float[] valueArray)
    {
        m_attrDescribeArray[0].text = "力";
        m_attrDescribeArray[1].text = "精";
        m_attrDescribeArray[2].text = "耐";
        m_attrDescribeArray[3].text = "智";
        m_attrDescribeArray[4].text = "敏";

        for (int i = 0; i < valueArray.Length; ++i)
        {
            m_attrValueArray[i].text = valueArray[i] + "";
        }

    }
}
