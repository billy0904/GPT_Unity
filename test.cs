using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenAI;
using Items;

public class MapManager : MonoBehaviour
{
    public string[] map = new string[13];
    string newPlace;
    int i = 0;

    // GPT 함수 호출
    private string CallGPT()
    {
        // GPT 호출 코드~~
        newPlace = "GPT가 만든 장소명";
        return newPlace;
    }

    // 0번 인덱스 == PNPC 장소 : 인트로에서 호출
    public void CreateMap()
    {
        PNPCPlace = CallGPT();
        map[0] = PNPCPlace;
        i++;
    }

    void AddMap()
    {

        // map 배열에 장소 저장
        for (int ch = 1; ch < 5; ch++;)
    {
            for (int cnt = 1; cnt < 4; cnt++;)
        {
                map[i] = "Chapter" + ch + " : 장소" + i; //CallGPT(); //해당 칸에 gpt 호출 값 저장
                i++;
            }
        }
    }

    // 장소 생성
    public string GetMap(string background, string genre)
    {
    private string map_setting = "너는 플레이어가 탐색할 수 있는 장소를 한 단어로 출력해야 해.";
    private string query = background + " 배경의 " + genre + "분위기에 어울리는 장소명 1개를 출력해줘";
    string[] messages = { map_setting, query };
    CreateMap();

return map[i];
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
