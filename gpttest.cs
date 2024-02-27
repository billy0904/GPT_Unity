using System;
using System.Net.Http;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        string apiKey = "MY_API_KEY";
        string prompt = "졸업프로젝트를 성공적으로 끝내는 법을 알려줘";

        // API 요청을 보낼 URL
        string url = "https://api.openai.com/v1/chat/completions";

        // 요청 본문 데이터 구성
        string requestBody = "{{\"model\": \"text-davinci-003\", \"messages\": [{{\"role\": \"user\", \"content\": \"{prompt}\"}}]}}";

        // HttpClient 인스턴스 생성
        using (HttpClient client = new HttpClient())
        {
            // API 헤더 설정
            client.DefaultRequestHeaders.Add("Authorization", "Bearer {apiKey}");

            // API에 POST 요청 보내고 응답 받기
            HttpResponseMessage response = await client.PostAsync(url, new StringContent(requestBody));

            // 응답 확인
            if (response.IsSuccessStatusCode)
            {
                // 응답 데이터 읽기
                string responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseContent);
            }
            else
            {
                Console.WriteLine("Failed to call the API. Status code: {response.StatusCode}");
            }
        }
    }
}
