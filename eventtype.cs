using System;

public class Eventrandom
{
    public struct place
    {
        public int ANPC_exist;
        public int event_type; //목표 이벤트 == 0, 일반 이벤트 == 이외 1, 2;
    }
    public static place[] map = new place[14];
    public static void Main(string[] args)
    {
        int place_idx = 1;
        //int billy = 0;
        while (place_idx < 13)
        {
            //목표 or 일반 이벤트 여부 정하기
            Random random = new Random();
            //billy = random.Next(3);
            map[place_idx].event_type = random.Next(2);
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
        int i = 0;
        while (i < 14)
        {
            Console.WriteLine("place" + i + ":" + map[i].event_type);
            i++;
        }
    }
}