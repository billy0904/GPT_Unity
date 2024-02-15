using UnityEngine;

public class MapManager : MonoBehaviour
{
    // 장소 생성
    public string GetPlaceName(string background, string genre)
    {
        string map_setting = "너는 플레이어가 탐색할 수 있는 장소를 한 단어로 출력해야 해.";
        string query = background + " 배경의 " + genre + "분위기에 어울리는 장소명 1개를 출력해줘";

        string[] messages = { map_setting, query };
        string place = CallGPT(messages, false);

        return place; //배열로 바꿔야 되나
    }

    // 아이템 생성
    public string GetItemName(string town, string place) //recover, weapon, mob, null
    {
        string item_setting = place + "에 어울려는 아이템 네 가지 중 한 카테고리에 맞게 한 단어로 출력해야 해.";
        string query = place + "에 위치한 아이템 1개를 출력해줘.";

        string[] messages = { item_setting, query };
        string item = CallGPT(messages, false);

        return item;
    }

    // GPT 함수 호출
    private string CallGPT(string[] messages, bool stream)
    {
        // GPT 호출하는 코드 작성
        return ""; // 호출 후의 결과값 반환
    }
}
