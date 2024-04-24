namespace Sparta2ndWeek
{
    internal class Program
    {
        static void Main(string[] args)
        {
            StartScene startScene1 = new StartScene();
            startScene1.Intro();
            int chosen = int.Parse(Console.ReadLine());
            if (chosen == 0)
            {
                Console.WriteLine("잘못된 입력입니다.");
            }

            while (true)
            {
                switch (chosen)
                {
                    case 0: //나가기(시작화면으로)
                        StartScene startScene2 = new StartScene();
                        startScene2.Intro();
                        chosen = int.Parse(Console.ReadLine());
                        break;
                    case 1: //상태보기
                        Section section1 = new Section();
                        section1.Stats();
                        chosen = int.Parse(Console.ReadLine());
                        break;
                    case 2: //인벤토리
                        Section section2 = new Section();
                        section2.Inventory();
                        chosen = int.Parse(Console.ReadLine());
                        if (chosen == 1)
                        {
                            chosen = 4; break;
                        }
                        break;
                    case 3: //상점
                        Section section3 = new Section();
                        section3.Store();
                        chosen = int.Parse(Console.ReadLine());
                        if (chosen == 1)
                        {
                            chosen = 5; break;
                        }
                        break;
                    case 4: //장착관리
                        Section section4 = new Section();
                        section4.ItemOnOff();
                        chosen = int.Parse(Console.ReadLine());
                        break;
                    case 5: //아이템구매
                        Section section5 = new Section();
                        section5.Buy();
                        chosen = int.Parse(Console.ReadLine());
                        break;
                    default:
                        Console.WriteLine("잘못된 입력입니다.");
                        chosen = int.Parse(Console.ReadLine());
                        break;
                }
            }
        }

        //게임 시작 화면
        class StartScene
        {
            public void Intro()
            {
                Console.WriteLine("");
                Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
                Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.");
                Console.WriteLine("");
                Console.WriteLine("1. 상태 보기");
                Console.WriteLine("2. 인벤토리");
                Console.WriteLine("3. 상점");
                Console.WriteLine("");
                Console.Write("원하시는 행동을 입력해주세요. \n>> ");
            }
        }

        class Section
        {
            bool forStats = false;
            //상태보기
            public void Stats()
            {
                Console.WriteLine("");
                Console.WriteLine("상태 보기");
                Console.WriteLine("캐릭터의 정보가 표시됩니다.");
                Console.WriteLine("");
                int[] playerStats = { 1, 10, 5, 100 };
                int level = playerStats[0];
                int atk = playerStats[1];
                int def = playerStats[2];
                int hp = playerStats[3];
                string sLevel = level.ToString("D2");
                Console.WriteLine("Lv. {0}", sLevel);
                Console.WriteLine("Shin ( 전사 )");
                Console.WriteLine("공격력 : {0}", atk);
                Console.WriteLine("방어력 : {0}", def);
                Console.WriteLine("체 력 : {0}", hp);
                forStats = true;
                Leftgold();
                //장비 착용 여부에 따라서 if절을 이용하여 stat 올리기
                Console.WriteLine("");
                Console.WriteLine("0. 나가기");
                Console.WriteLine("");
                Console.Write("원하시는 행동을 입력해주세요. \n>> ");
            }

            //보유 골드
            public void Leftgold()
            {
                int gold = 1500;
                if (forStats == true)
                {
                    Console.WriteLine("Gold : {0} G", gold);
                }
                else
                {
                    Console.WriteLine("{0} G", gold);
                }


            }

            //인벤토리
            public void Inventory()
            {
                Console.WriteLine("");
                Console.WriteLine("인벤토리");
                Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
                Console.WriteLine("");
                Console.WriteLine("[아이템 목록]");
                //만약 아이템이 존재하면 여기에 추가
                Console.WriteLine("");
                Console.WriteLine("1. 장착 관리");
                Console.WriteLine("0. 나가기");
                Console.WriteLine("");
                Console.Write("원하시는 행동을 입력해주세요. \n>> ");
            }

            //장착 관리
            public void ItemOnOff()
            {
                Console.WriteLine("");
                Console.WriteLine("인벤토리 - 장착 관리");
                Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
                Console.WriteLine("");
                Console.WriteLine("[아이템 목록]");
                //아이템목록 함수
                Console.WriteLine("");
                Console.WriteLine("0. 나가기");
                Console.WriteLine("");
                Console.Write("원하시는 행동을 입력해주세요. \n>> ");
            }

            //상점
            public void Store()
            {
                Console.WriteLine("");
                Console.WriteLine("상점");
                Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
                Console.WriteLine("");
                Console.WriteLine("[보유 골드]");
                forStats = false;
                Leftgold();
                Console.WriteLine("");
                Console.WriteLine("[아이템 목록]");
                //아이템목록 함수
                Console.WriteLine("");
                Console.WriteLine("1. 아이템 구매");
                Console.WriteLine("0. 나가기");
                Console.WriteLine("");
                Console.Write("원하시는 행동을 입력해주세요. \n>> ");
            }

            //아이템 구매
            public void Buy()
            {
                Console.WriteLine("");
                Console.WriteLine("상점 - 아이템 구매");
                Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
                Console.WriteLine("");
                Console.WriteLine("[보유 골드]");
                forStats = false;
                Leftgold();
                Console.WriteLine("");
                Console.WriteLine("[아이템 목록]");
                //아이템목록 함수
                Console.WriteLine("");
                Console.WriteLine("0. 나가기");
                Console.WriteLine("");
                Console.Write("원하시는 행동을 입력해주세요. \n>> ");
            }

            //아이템 목록
        }
    }
}
