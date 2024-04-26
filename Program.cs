using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;

namespace Sparta2ndWeek
{
    internal class Program
    {
        static void Main(string[] args)
        {
            StartScene startScene = new StartScene();
            ItemData itemData = new ItemData();
            startScene.Intro();
            int chosen = int.Parse(Console.ReadLine());
            while (true)
            {
                if (chosen == 1 || chosen == 2 || chosen == 3)
                {
                    switch (chosen)
                    {
                        case 1: //상태보기
                            itemData.StatsScene();
                            chosen = int.Parse(Console.ReadLine());
                            while (true)
                            {
                                if (chosen == 0) //나가기
                                {
                                    startScene.Intro();
                                    break;
                                }
                                else
                                    Console.WriteLine(" 잘못된 입력입니다.");
                                    chosen = int.Parse(Console.ReadLine());
                            }
                            break;
                        case 2: //인벤토리
                            itemData.InventoryScene();
                            chosen = int.Parse(Console.ReadLine());
                            while (true)
                            {
                                if (chosen == 0) //나가기
                                {
                                    startScene.Intro();
                                    break;
                                }
                                else if (chosen == 1) //장착 관리
                                {
                                    itemData.ItemOnOffScene();
                                    chosen = int.Parse(Console.ReadLine());
                                    while (true)
                                    {
                                        if (chosen == 0) //나가기
                                        {
                                            startScene.Intro();
                                            break;
                                        }
                                        else if (chosen <= itemData.NumberOfBought()) //선택한 숫자와 구매아이템 개수에 오류가 없으면,
                                        {
                                            while (true)
                                            {
                                                itemData.AddOnOffData(chosen);
                                                itemData.ItemOnOffScene();
                                                chosen = int.Parse(Console.ReadLine());
                                                if (chosen == 0)
                                                {
                                                    startScene.Intro();
                                                    break;
                                                }
                                            }
                                            break;
                                        }
                                        else
                                        {
                                            Console.WriteLine(" 잘못된 입력입니다.");
                                            chosen = int.Parse(Console.ReadLine());
                                        }
                                    }
                                    break;
                                }
                                else
                                    Console.WriteLine(" 잘못된 입력입니다.");
                                    chosen = int.Parse(Console.ReadLine());
                            }
                            break;
                        case 3: //상점
                            itemData.StoreScene();
                            chosen = int.Parse(Console.ReadLine());
                            while (true)
                            {
                                if (chosen == 0) //나가기
                                {
                                    startScene.Intro();
                                    break;
                                }
                                else if (chosen == 1) //아이템 구매화면
                                {
                                    itemData.Buy();
                                    chosen = int.Parse(Console.ReadLine()); //0을 선택하면 메인화면으로// 1구매를 선택하면
                                    while (true)
                                    {
                                        if (chosen == 0)
                                        {
                                            startScene.Intro();
                                            break;
                                        }
                                        else if (chosen == 1 || chosen == 2 || chosen == 3 || chosen == 4 || chosen == 5 || chosen == 6)
                                        {
                                            while (true)
                                            {
                                                itemData.BoughtList(chosen);
                                                chosen = int.Parse(Console.ReadLine());
                                                if (chosen == 0)
                                                {
                                                    startScene.Intro();
                                                    break;
                                                }
                                            }
                                            break;
                                        }
                                        else
                                        {
                                            Console.WriteLine(" 잘못된 입력입니다.");
                                            chosen = int.Parse(Console.ReadLine());
                                        }
                                    }
                                    break;
                                    
                                }
                                else
                                    Console.WriteLine(" 잘못된 입력입니다.");
                                    chosen = int.Parse(Console.ReadLine());
                            }
                            break;
                    }
                }
                else
                    Console.WriteLine(" 잘못된 입력입니다.");
                    chosen = int.Parse(Console.ReadLine());
            }
        }

        //게임 시작 화면
        class StartScene
        {
            public void Intro()
            {
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine(" 스파르타 마을에 오신 여러분 환영합니다.");
                Console.WriteLine(" 이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.");
                Console.WriteLine("");
                Console.WriteLine(" 1. 상태 보기");
                Console.WriteLine(" 2. 인벤토리");
                Console.WriteLine(" 3. 상점");
                Console.WriteLine("");
                Console.Write(" 원하시는 행동을 입력해주세요. \n >> ");
            }
        }

        //아이템 관련 클래스
        class ItemData
        {
            //재화
            int gold = 1500;

            //기존 아이템 정보 리스트
            List<string> originalValueList = new List<string>();

            //아이템 가격 딕셔너리
            Dictionary<int, int> priceDic = new Dictionary<int, int>();

            //능력치 딕셔너리
            Dictionary<int, int> statsDic = new Dictionary<int, int>();

            //구매목록(구매번호,구매정보) 딕셔너리
            Dictionary<int, string> boughtDic = new Dictionary<int, string>();

            //구매정보 리스트
            List<string> boughtValueList = new List<string>();

            //스탯 반영 리스트(상태보기 전용)
            List<string> statsReflectList = new List<string>();

            //장착목록(장착번호,장착정보) 딕셔너리
            Dictionary<int, string> onDic = new Dictionary<int, string>();


            //장착 및 해제된 아이템정보 추가
            public void AddOnOffData(int a)
            {
                if (!onDic.ContainsKey(a))
                {
                    onDic.Add(a, boughtValueList[a - 1]); //장착(onDic에 추가)
                    statsReflectList.Add(boughtValueList[a - 1]);
                }
                else
                {
                    onDic.Remove(a); //미장착(onDic에서 제거)
                    statsReflectList.Remove(boughtValueList[a - 1]);
                }
            }

            //구매정보 개수 반환
            public int NumberOfBought()
            {
                int a = boughtValueList.Count;
                return a;
            }

            //구매시 재화감소, 구매정보(키: 구매번호, 값: 구매번호에 해당하는 아이템 정보) 추가
            public void TryBuying(int a) //a는 1부터 시작
            {
                for (int i = 0; i < 1; i++)
                {
                    i += a;
                    if (gold >= priceDic[i]) 
                    {
                        gold -= priceDic[i];
                        boughtDic.Add(i, originalValueList[i - 1]);
                        Console.WriteLine(" 구매를 완료했습니다.");
                    }
                    else
                        Console.WriteLine(" Gold 가 부족합니다.");
                }
            }

            //구매에 따른 소지 아이템 목록 작성
            public void BoughtList(int a)
            {
                int count = boughtDic.Count;
                if (count == 0) 
                {
                    if (a < 1 && a > 6) ////////////////////////////////// a > 기존 아이템 개수 
                        Console.WriteLine(" 잘못된 입력입니다.");
                    else 
                    {
                        TryBuying(a);
                    }
                }
                else //데이터 존재
                {
                    if (a < 1 && a > 6)
                        Console.WriteLine(" 잘못된 입력입니다.");
                    else
                    {
                        if (!boughtDic.ContainsKey(a))
                        {
                            TryBuying(a);
                        }
                        else
                        {
                            Console.WriteLine(" 이미 구매한 아이템입니다.");
                        }
                    }
                }
            }

            //상태보기 화면
            public void StatsScene()
            {
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine(" 상태 보기");
                Console.WriteLine(" 캐릭터의 정보가 표시됩니다.");
                Console.WriteLine("");
                ChangedStats();
                Console.WriteLine(" Gold : {0} G", gold);
                Console.WriteLine("");
                Console.WriteLine(" 0. 나가기");
                Console.WriteLine("");
                Console.Write(" 원하시는 행동을 입력해주세요. \n >> ");
            }

            //인벤토리 화면
            public void InventoryScene()
            {
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine(" 인벤토리");
                Console.WriteLine(" 보유 중인 아이템을 관리할 수 있습니다.");
                Console.WriteLine("");
                Console.WriteLine(" [아이템 목록]");
                InventoryItemList(0); //x=0:인벤토리 x=1:장착관리
                Console.WriteLine("");
                Console.WriteLine(" 1. 장착 관리");
                Console.WriteLine(" 0. 나가기");
                Console.WriteLine("");
                Console.Write(" 원하시는 행동을 입력해주세요. \n >> ");
            }

            //인벤토리-장착관리 화면
            public void ItemOnOffScene()
            {
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine(" 인벤토리 - 장착 관리");
                Console.WriteLine(" 보유 중인 아이템을 관리할 수 있습니다.");
                Console.WriteLine("");
                Console.WriteLine(" [아이템 목록]");
                InventoryItemList(1); //x=0:인벤토리 x=1:장착관리
                Console.WriteLine("");
                Console.WriteLine(" 0. 나가기");
                Console.WriteLine("");
                Console.Write(" 원하시는 행동을 입력해주세요. \n >> ");
            }

            //아이템 목록(인벤토리,장착관리)
            public void InventoryItemList(int x) //x=0:인벤토리 x=1:장착관리
            {
                if (boughtDic.Count > 0)
                {
                    foreach (KeyValuePair<int, string> item in boughtDic)
                    {
                        if (!boughtValueList.Contains(item.Value))
                            boughtValueList.Add(item.Value);
                    }
                    foreach (string value in boughtValueList)
                    {
                        for (int i = 1; i < boughtValueList.Count + 1; i++)
                        {
                            if (onDic.ContainsKey(i) && x == 1) //장착관리 목록리스트
                                Console.WriteLine(" - {0} [E]{1}", i, value);
                            else if (!onDic.ContainsKey(i) && x == 1)
                                Console.WriteLine(" - {0} {1}", i, value);
                            else if (onDic.ContainsKey(i) && x == 0) //인벤토리 리스트
                                Console.WriteLine(" - [E]{0}", value);
                            else if (!onDic.ContainsKey(i) && x == 0)
                                Console.WriteLine(" - {0}", value);
                        }
                    }
                }
            }

            //상점 화면
            public void StoreScene()
            {
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine(" 상점");
                Console.WriteLine(" 필요한 아이템을 얻을 수 있는 상점입니다.");
                Console.WriteLine("");
                Console.WriteLine(" [보유 골드]");
                Console.WriteLine(" {0} G", gold);
                Console.WriteLine("");
                Console.WriteLine(" [아이템 목록]");
                StoreItemList(0);
                Console.WriteLine("");
                Console.WriteLine(" 1. 아이템 구매");
                Console.WriteLine(" 0. 나가기");
                Console.WriteLine("");
                Console.Write(" 원하시는 행동을 입력해주세요. \n >> ");
            }

            //상점-아이템구매 화면
            public void Buy()
            {
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine(" 상점 - 아이템 구매");
                Console.WriteLine(" 필요한 아이템을 얻을 수 있는 상점입니다.");
                Console.WriteLine("");
                Console.WriteLine(" [보유 골드]");
                Console.WriteLine(" {0} G", gold);
                Console.WriteLine("");
                Console.WriteLine(" [아이템 목록]");
                StoreItemList(1);
                Console.WriteLine("");
                Console.WriteLine(" 0. 나가기");
                Console.WriteLine("");
                Console.Write(" 원하시는 행동을 입력해주세요. \n >> ");
            }

            //아이템 목록(상점,아이템구매)
            public void StoreItemList(int x) //x=0: 상점 //x=-1: 아이템구매
            {
                int[,] itemArray = new int[6, 3]
                {
                    {0, 1, 2},
                    {0, 1, 2},
                    {0, 1, 2},
                    {0, 1, 2},
                    {0, 1, 2},
                    {0, 1, 2}
                };
                for (int i = 0; i < 6; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (itemArray[i, j] == 0 && x == 0)
                        {
                            Console.Write(" - ");
                        }
                        else if (itemArray[i, j] == 0 && x == 1)
                        {
                            int k = i + 1;
                            Console.Write(" - {0} ", k);
                        }
                        else if (itemArray[i, j] == 1)
                        {
                            switch (i)
                            {
                                case 0:
                                    string v0 = "수련자 갑옷     | 방어력 +5  | 수련에 도움을 주는 갑옷입니다.                   ";
                                    if (!originalValueList.Contains(v0) && !statsDic.ContainsKey(i + 1))
                                    {
                                        originalValueList.Add(v0);
                                        statsDic.Add(i + 1, 5);
                                    }
                                    Console.Write(v0);
                                    break;
                                case 1:
                                    string v1 = "무쇠갑옷        | 방어력 +9  | 무쇠로 만들어져 튼튼한 갑옷입니다.               ";
                                    if (!originalValueList.Contains(v1) && !statsDic.ContainsKey(i + 1))
                                    {
                                        originalValueList.Add(v1);
                                        statsDic.Add(i + 1, 9);
                                    }
                                    Console.Write(v1);
                                    break;
                                case 2:
                                    string v2 = "스파르타의 갑옷 | 방어력 +15 | 스파르타의 전사들이 사용했다는 전설의 갑옷입니다.";
                                    if (!originalValueList.Contains(v2) && !statsDic.ContainsKey(i + 1))
                                    {
                                        originalValueList.Add(v2);
                                        statsDic.Add(i + 1, 15);
                                    }
                                    Console.Write(v2);
                                    break;
                                case 3:
                                    string v3 = "낡은 검         | 공격력 +2  | 쉽게 볼 수 있는 낡은 검 입니다.                  ";
                                    if (!originalValueList.Contains(v3) && !statsDic.ContainsKey(i + 1))
                                    {
                                        originalValueList.Add(v3);
                                        statsDic.Add(i + 1, 2);
                                    }
                                    Console.Write(v3);
                                    break;
                                case 4:
                                    string v4 = "청동 도끼       | 공격력 +5  |  어디선가 사용됐던거 같은 도끼입니다.            ";
                                    if (!originalValueList.Contains(v4) && !statsDic.ContainsKey(i + 1))
                                    {
                                        originalValueList.Add(v4);
                                        statsDic.Add(i + 1, 5);
                                    }
                                    Console.Write(v4);
                                    break;
                                case 5:
                                    string v5 = "스파르타의 창   | 공격력 +7  | 스파르타의 전사들이 사용했다는 전설의 창입니다.  ";
                                    if (!originalValueList.Contains(v5) && !statsDic.ContainsKey(i + 1))
                                    {
                                        originalValueList.Add(v5);
                                        statsDic.Add(i + 1, 7);
                                    }
                                    Console.Write(v5);
                                    break;
                            }
                        }
                        else if (itemArray[i, j] == 2)
                        {
                            switch (i)
                            {
                                case 0:
                                    if (x == 0 && boughtDic.ContainsKey(1)) //구매된 아이템 딕셔너리에서 1번 키값이 존재하면, 상점창에 구매완료 표시
                                        Console.Write("| 구매완료");
                                    else
                                    {
                                        Console.Write("| 1000 G");
                                        if (!priceDic.ContainsKey(i + 1))
                                            priceDic.Add(i + 1, 1000);
                                    }
                                    break;
                                case 1:
                                    if (x == 0 && boughtDic.ContainsKey(2))
                                        Console.Write("| 구매완료");
                                    else
                                    {
                                        Console.Write("| 1800 G");
                                        if (!priceDic.ContainsKey(i + 1))
                                            priceDic.Add(i + 1, 1800);
                                    }
                                    break;
                                case 2:
                                    if (x == 0 && boughtDic.ContainsKey(3))
                                        Console.Write("| 구매완료");
                                    else
                                    {
                                        Console.Write("| 3500 G");
                                        if (!priceDic.ContainsKey(i + 1))
                                            priceDic.Add(i + 1, 3500);
                                    }
                                    break;
                                case 3:
                                    if (x == 0 && boughtDic.ContainsKey(4))
                                        Console.Write("| 구매완료");
                                    else
                                    {
                                        Console.Write("| 600 G");
                                        if (!priceDic.ContainsKey(i + 1))
                                            priceDic.Add(i + 1, 600);
                                    }
                                    break;
                                case 4:
                                    if (x == 0 && boughtDic.ContainsKey(5))
                                        Console.Write("| 구매완료");
                                    else
                                    {
                                        Console.Write("| 1500 G");
                                        if (!priceDic.ContainsKey(i + 1))
                                            priceDic.Add(i + 1, 1500);
                                    }
                                    break;
                                case 5:
                                    if (x == 0 && boughtDic.ContainsKey(6))
                                        Console.Write("| 구매완료");
                                    else
                                    {
                                        Console.Write("| 2700 G");
                                        if (!priceDic.ContainsKey(i + 1))
                                            priceDic.Add(i + 1, 2700);
                                    }
                                    break;
                            }
                        }
                    }
                    Console.WriteLine();
                }
            }
            //아이템 장착에 따른 스탯 변경(상태보기)
            public void ChangedStats()
            {
                int[] playerStats = { 1, 10, 5, 100 };
                int level = playerStats[0];
                int atk = playerStats[1];
                int def = playerStats[2];
                int hp = playerStats[3];
                int a = 0, a1 = 0, a2 = 0, d = 0, d1 = 0, d2 = 0;

                if (statsReflectList != null)
                {
                    for (int i = 0; i < statsReflectList.Count; i++)
                    {
                        if (statsReflectList[i] == originalValueList[i])
                        {
                            d = 5;
                            def += d;
                        }
                        else if (statsReflectList[i] == "무쇠갑옷        | 방어력 +9  | 무쇠로 만들어져 튼튼한 갑옷입니다.               ")
                        {
                            d1 = 9;
                            def += d1;
                        }
                        else if (statsReflectList[i] == "스파르타의 갑옷 | 방어력 +15 | 스파르타의 전사들이 사용했다는 전설의 갑옷입니다.")
                        {
                            d2 = 15;
                            def += d2;
                        }
                        else if (statsReflectList[i] == "낡은 검         | 공격력 +2  | 쉽게 볼 수 있는 낡은 검 입니다.                  ")
                        {
                            a = 2;
                            atk += a;
                        }
                        else if (statsReflectList[i] == "청동 도끼       | 공격력 +5  |  어디선가 사용됐던거 같은 도끼입니다.            ")
                        {
                            a1 = 5;
                            atk += a1;
                        }
                        else if (statsReflectList[i] == "스파르타의 창   | 공격력 +7  | 스파르타의 전사들이 사용했다는 전설의 창입니다.  ")
                        {
                            a2 = 7;
                            atk += a2;
                        }
                    }
                }
                int sumA = a + a1 + a2;
                int sumD = d + d1 + d2;
                string sLevel = level.ToString("D2");
                Console.WriteLine(" Lv. {0}", sLevel);
                Console.WriteLine(" Shin ( 전사 )");
                if (sumA > 0)
                    Console.WriteLine(" 공격력 : {0}(+{1})", atk, sumA);
                else
                    Console.WriteLine(" 공격력 : {0}", atk);
                if (sumD > 0)
                    Console.WriteLine(" 방어력 : {0}(+{1})", def, sumD);
                else
                    Console.WriteLine(" 방어력 : {0}", def);
                Console.WriteLine(" 체 력 : {0}", hp);
            }
        }
    }
}
