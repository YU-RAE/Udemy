using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MsgDisp : MonoBehaviour
{
    public static string msg;
    public static bool flagDiaplay = false;
    public GUIStyle guiDisplay;
    public static int msgLen;
    public static float waitDelay;

    private float nextTime = 0;
    private Rect rtDisplay = new Rect();

    // OnGUI�� GUI �̺�Ʈ�� ������ �� ó���ϱ� ���� ȣ��ȴ�.
    private void OnGUI()
    {
        // GUI ũ�� : 16:9�� 1280x720 �ػ� ����
        const float guiScreen = 1280; 
        const float guiWidth = 800; // 800x200 �ȼ��� �޼���â
        const float guiHeight = 200;
        const float guiLeft = (guiScreen - guiWidth) / 2; // ���
        const float guiTop  = 720 - guiHeight - 20; // �ϴ�

        // ���� ȭ����� ����
        float gui_scale = Screen.width / guiScreen;

        if (flagDiaplay)
        {
            // �۲� ��Ÿ��
            GUIStyle msgFont = new GUIStyle
            {
                fontSize = (int)(30 * gui_scale)
            };
            // �޼���â ��ġ ���
            rtDisplay.x = guiLeft * gui_scale;
            rtDisplay.y = guiTop * gui_scale;
            rtDisplay.width = guiWidth * gui_scale;
            rtDisplay.height = guiHeight * gui_scale;

            // �޼���â ���
            GUI.Box(rtDisplay, "â", guiDisplay);

            // �޼��� �׸��� ���
            msgFont.normal.textColor = Color.black;
            rtDisplay.x = (guiLeft + 22) * gui_scale;
            rtDisplay.y = (guiTop + 22) * gui_scale;
            GUI.Label(rtDisplay, msg.Substring(0, msgLen), msgFont);

            // �޽��� ���
            msgFont.normal.textColor = Color.white;
            rtDisplay.x = (guiLeft + 20) * gui_scale;
            rtDisplay.y = (guiTop + 20) * gui_scale;
            GUI.Label(rtDisplay, msg.Substring(0, msgLen), msgFont);
        }
    }

    // �ῡ�� �޽��� �ޱ�
    public static void ShowMessage(string msg)
    {
        MsgDisp.msg = msg;
        flagDiaplay = true;
        msgLen = 0;
        waitDelay = 0;
    }
    void Start()
    {
        
    }

    void Update()
    {
        /*
        if (flagDiaplay && Time.time > nextTime)
        {
            if (msgLen < msg.Length)
                msgLen++;
            nextTime = Time.time + 0.02f;
        }
        */
        if (flagDiaplay)
        {
            if(msgLen < msg.Length)
            {
                if(Time.time > nextTime)
                {
                    msgLen++;
                    nextTime = Time.time + 0.02f;
                }
            }
            else
            {
                waitDelay += Time.deltaTime;
                if (waitDelay > 1 + msg.Length / 4)
                    flagDiaplay = false;
            }
        }
    }
}
