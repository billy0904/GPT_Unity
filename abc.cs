using System;
public class Items
{
    public enum Type
    {
        Recover,
        Mob,
        Weapon
    }
    public static Type? RandomItemType()
    {
        // enum의 모든 값 가져오기 : Recover, Mob, Weapon
        Array values = Enum.GetValues(typeof(Type));

        // 랜덤 인덱스 생성
        Random random = new Random();
        int randomIndex = random.Next(values.Length + 1); // null 값 고려하여 + 1

        // 랜덤값이 null인 경우 null 리턴
        if (randomIndex == values.Length)
            return null;

        // 선택된 아이템 타입 값
        return (Type)values.GetValue(randomIndex);
    }

    // 테스트 메인문
    public static void Main(string[] args)
    {
        // 랜덤으로 선택된 enum 값 또는 null 출력
        Console.WriteLine("아이템 타입 or (null): " + RandomItemType());
    }
}