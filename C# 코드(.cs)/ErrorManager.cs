using System.IO;
using DefineDatas;

// ������ �����ϴ� class
public class ErrorManager
{
    // ���� �߻� Ȯ�� ����(bool)
    public static bool isErrorOccur = false;
    // ���� text ���� ���(string)
    private static string filePath = "Assets/GameDatas/Error.txt";
    private static StreamWriter sw;

     /*
     * ���� �ڵ� text���Ͽ� ����
     * �Ű�����: �����ڵ�(ErrorCode)
     * ���� ��: void
     */
    public static void SaveErrorData(ErrorCode errorCode)
    {
        isErrorOccur = true;
        if (!File.Exists(filePath))
        {
            sw = new StreamWriter(filePath);
            sw.WriteLine(MakeErrorSentence(errorCode));
            sw.Flush();
            sw.Close();
        }

        else
        {
            File.AppendAllText(filePath, MakeErrorSentence(errorCode) + "\n");
        }
    }

    /*
    * ���� ���ڿ� text���Ͽ� ����
    * �Ű�����: ���� ���ڿ�(string)
    * ���� ��: void
    */
    public static void SaveErrorData(string data)
    {
        isErrorOccur = true;
        if (!File.Exists(filePath))
        {
            sw = new StreamWriter(filePath);
            sw.WriteLine(data);
            sw.Flush();
            sw.Close();
        }

        else
        {
            File.AppendAllText(filePath, data + "\n");
        }
    }

    /*
    * ���� �ڵ� �� ���ڿ� text���Ͽ� ����
    * �Ű�����: �����ڵ�(ErrorCode), ���� ���ڿ�(string)
    * ���� ��: void
    */
    public static void SaveErrorData(ErrorCode errorCode, string addData)
    {
        isErrorOccur = true;
        if (!File.Exists(filePath))
        {
            sw = new StreamWriter(filePath);
            sw.WriteLine(MakeErrorSentence(errorCode));
            sw.Flush();
            sw.Close();
        }

        else
        {
            File.AppendAllText(filePath, MakeErrorSentence(errorCode) + addData + "\n");
        }
    }

    /*
    * ���� ���ڿ� ����
    * �Ű�����: ���� �ڵ�(ErrorCode)
    * ���� ��: string
    */
    private static string MakeErrorSentence(ErrorCode errorCode)
    {
        return "ERROR CODE " + (int)errorCode + " : " + errorCode.ToString();
    }

    /*
    * Debug.Log() ȣ��
    * PlayerSettings�� ScriptCompilation���� ENABLE_DEBUG�� �߰� �Ǿ�� ȣ�� ����
    * �Ű�����: ����� �޼���(object)
    * ���� ��: void
    */
    [System.Diagnostics.Conditional("ENABLE_DEBUG")]
    public static void Log(object message)
    {
        Debug.Log(message);
    }
    
}
