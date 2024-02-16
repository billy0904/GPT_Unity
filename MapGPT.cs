using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class GPTAPIExample : MonoBehaviour
{
    private const string GPT_API_URL = "https://example.com/api/gpt"; // GPT API 엔드포인트 URL
    private const string API_KEY = "YOUR_API_KEY";

    void Start()
    {
        StartCoroutine(GetGPTData());
    }

    IEnumerator GetGPTData()
    {
        string url = GPT_API_URL + "?api_key=" + API_KEY; // API 요청 URL 구성

        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest(); // 요청 보내기

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error: " + request.error); // 오류 처리
            }
            else
            {
                string jsonResult = request.downloadHandler.text; // 응답 받은 JSON 데이터

                // JSON 데이터 처리 예시
                // 여기서는 단순히 콘솔에 출력하겠습니다.
                Debug.Log("Received JSON data: " + jsonResult);
            }
        }
    }
}

using UnityEngine;
using UnityEngine.Networking;

public class MapManager : MonoBehaviour
{
    public string[] map = new string[13];
    int i = 0;

    // GPT 함수 호출
    private string CallGPT()
    {
        string gptData = ""; // GPT API에서 가져온 데이터를 저장할 변수

        // GPT API 요청 URL 및 API 키 설정
        string apiUrl = "https://api.gpt.com"; // API 엔드포인트 URL
        string apiKey = "API_KEY";

        // GPT API 요청 보내기
        StartCoroutine(SendGPTRequest(apiUrl, apiKey, (result) =>
        {
            // 요청 결과를 콜백 함수로 받아 처리
            gptData = result;
        }));

        // API 요청 결과 반환
        return gptData;
    }

    // GPT API 요청 보내기
    private IEnumerator SendGPTRequest(string url, string apiKey, System.Action<string> callback)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(url + "?api_key=" + apiKey))
        {
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error: " + request.error);
            }
            else
            {
                string responseData = request.downloadHandler.text;
                callback?.Invoke(responseData);
            }
        }
    }

    // 0번 인덱스 == PNPC 장소 : 인트로에서 호출
    public void CreateMap()
    {
        string PNPCPlace = CallGPT(); // PNPCPlace 변수에 GPT API에서 가져온 데이터 저장
        map[0] = PNPCPlace;
        i++;
    }
}
