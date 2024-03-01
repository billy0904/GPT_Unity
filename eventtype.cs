using System;
using System.Collections;
using System.Collections.Generic;

public class Eventrandom
{
    //외부 파일 enum 연결
    public enum ItemType
    {
        Recover,
        Mob,
        Weapon
    }
    public enum GoalType
    {
        Item,
        Report,
        Monster
    }
    public static void ChooseItemType()
    {
        int i = 1;
        Random random = new Random();
        while (i < 13)
        {
            if (map[i].event_type == 0) //일반 이벤트일 경우
            {
                //enum의 모든 값 리스트로 가져오기 : Recover, Mob, Weapon
                List<string> values = new List<string>(Enum.GetNames(typeof(ItemType)));
                //null 항목 4개 추가
                for (int idx = 0; idx < 4; idx++)
                    values.Add("NULL");
                //돌려돌려돌림판
                int randomIdx = random.Next(values.Count);

                map[i].item_type = values[randomIdx];
            }
            else //목표 이벤트일 경우
            {
                //enum의 모든 값 리스트로 가져오기 : Item, Report, Monster
                List<string> values = new List<string>(Enum.GetNames(typeof(GoalType)));
                //돌려돌려돌림판
                int randomIdx = random.Next(values.Count);

                map[i].item_type = values[randomIdx];
            }
            i++;
        }
    }

    public static void ItemStat()
    {
        Random random = new Random();
        int i = 1;
        while (i < 13)
        {
            if (map[i].item_type != "NULL")
            {
                map[i].item_stat = random.Next(1, 6);
            }
            i++;
        }
    }

    public static void IsANPCexists()
    {
        Random random = new Random();
        int i = 1;
        while (i < 13)
        {
            if (map[i].event_type == 1)
                map[i].ANPC_exist = 0;
            else
            {
                map[i].ANPC_exist = random.Next(2);
            }
            i++;
        }
    }
    public struct place
    {
        public string item_type;
        public int ANPC_exist;
        public int event_type; //목표 이벤트 == 0, 일반 이벤트 == 이외 1, 2;
        public int item_stat;
    }
    public static place[] map = new place[14];
    public static void Main(string[] args)
    {
        int place_idx = 1;
        int billy = 0;
        Random random = new Random();
        while (place_idx < 13)
        {
            //목표 or 일반 이벤트 여부 정하기
            billy = random.Next(3);
            if (billy == 0)
                map[place_idx].event_type = 1;
            else
                map[place_idx].event_type = 0;
            if (map[place_idx].event_type == 1) //100
            {
                map[place_idx + 1].event_type = 0;
                map[place_idx + 2].event_type = 0;
                place_idx += 3;
                continue;
            }
            else
            {
                place_idx++;
                map[place_idx].event_type = random.Next(2);
                if (map[place_idx].event_type == 1) //010
                {
                    map[place_idx + 1].event_type = 0;
                }
                else
                {
                    map[place_idx + 1].event_type = 1; //001
                }
                place_idx += 2;
            }
        }
        if (place_idx == 13) //최종 에필로그 보스방
        {
            map[place_idx].event_type = 1;
            map[place_idx].ANPC_exist = 0;
        }
        IsANPCexists();
        ChooseItemType();
        ItemStat();
        int i = 0;
        while (i < 14)
        {
            Console.WriteLine("place" + i + "- " + "이벤트타입: " + map[i].event_type + " ANPC등장여부: " + map[i].ANPC_exist + " 아이템타입: " + map[i].item_type + "/" + map[i].item_stat);
            i++;
        }
    }
}