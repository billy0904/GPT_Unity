using System;
public class ANPCrandom
{
    public static void Main(string[] args)
    {
        // 랜덤으로 선택된 enum 값 또는 null 출력
        Random random = new Random();

        //0 == ANPC 없음, 1 == ANPC 등장 
        int ANPCbool = random.Next(2);
        Console.WriteLine("ANPC 등장 여부: " + ANPCbool);
    }
}