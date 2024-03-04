using System.IO;
using DefineDatas;

// 에러를 관리하는 class
public class ErrorManager
{
    // 에러 발생 확인 변수(bool)
    public static bool isErrorOccur = false;
    // 에러 text 저장 경로(string)
    private static string filePath = "Assets/GameDatas/Error.txt";
    private static StreamWriter sw;

     /*
     * 에러 코드 text파일에 저장
     * 매개변수: 에러코드(ErrorCode)
     * 리턴 값: void
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
    * 에러 문자열 text파일에 저장
    * 매개변수: 에러 문자열(string)
    * 리턴 값: void
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
    * 에러 코드 및 문자열 text파일에 저장
    * 매개변수: 에러코드(ErrorCode), 에러 문자열(string)
    * 리턴 값: void
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
    * 에러 문자열 생성
    * 매개변수: 에러 코드(ErrorCode)
    * 리턴 값: string
    */
    private static string MakeErrorSentence(ErrorCode errorCode)
    {
        return "ERROR CODE " + (int)errorCode + " : " + errorCode.ToString();
    }

    /*
    * Debug.Log() 호출
    * PlayerSettings의 ScriptCompilation에서 ENABLE_DEBUG가 추가 되어야 호출 가능
    * 매개변수: 출력할 메세지(object)
    * 리턴 값: void
    */
    [System.Diagnostics.Conditional("ENABLE_DEBUG")]
    public static void Log(object message)
    {
        Debug.Log(message);
    }
    
}
