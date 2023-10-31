#include <stdio.h>
#include <time.h>   //rand를 위한 time 함수
#include <stdlib.h> //srand, rand함수
#include <termios.h>
#include <unistd.h>
int getch();

void PaintObj(int maps[10][40], int score);
void ReadMe(void);
int ChoonMove(int *x, int *y, int *type);
void ChoonMaps(int x, int y, int type);
void Start();
void InitObj(int maps[10][40]);
int CheckObj(int maps[10][40], int x, int y, int dir);
void KillEnemy(int maps[10][40], int x, int y, int score, int *now_item);
int Shoot(int maps[10][40], int chr_x, int chr_y, int score, int *now_item, char dir);
int ChrMove(int maps[10][40], int *chr_x, int *chr_y, int *item, int hp, int *score, int *now_item);
void RegenEnemy(int maps[10][40]);
int CheckEnemy(int maps[10][40]);
void MoveEnemy(int maps[10][40], int *hp, int type);
int Game1(int victory, int *ad); // 슈팅게임
int Game2(int victory, int *ad); // 팩맨
int Game3(int victory, int *ad); // 애니팡
int Game4(int victory, int *ad); // 지뢰 찾기

int main(void)
{
    int x = 22, y = 5;        // 춘식이 초기위치
    int type = 0, on_off = 0; // 맵 종류, 미니게임 종류
    int adventure = 0;
    int victory = 0;
    srand((unsigned)time(NULL));
    system("clear");
    ReadMe();
    while (1)
    {
        ChoonMaps(x, y, type);
        printf("모험 횟수: %d회, 게임승리 포인트: %d \n", adventure, victory);
        on_off = ChoonMove(&x, &y, &type);
        switch (on_off)
        {
        case 0:
            break;
        case 1:
            victory = Game1(victory, &adventure); // 슈팅게임
            break;
        case 2:
            victory = Game2(victory, &adventure); // 팩맨
            break;
        case 3:
            victory = Game3(victory, &adventure); // 애니팡
            break;
        case 4:
            victory = Game4(victory, &adventure); // 지뢰찾기
            break;
        case 5:
            return 0;
        default:
            break;
        }

        fflush(stdout);
        usleep(150000);
    }
    return 0;
}

void ReadMe(void)
{
    int key;
    printf("WASD로 이동, 게임기 근처에서 R을 눌러 게임을 시작합니다.\n");
    printf("X를 눌러 종료할 수 있습니다.\n");
    printf("아무키나 정수나 입력하면 설명을 종료합니다.\n");
    scanf("%d", &key);
}

int ChoonMove(int *x, int *y, int *type) // 캐릭터 좌표 입력받아 입력받은 방향으로 좌표를 이동
{
    int key; // 입력받을 값을 저장할 key변수
    int on_off = 0;

    key = getch(); // key에 1문자를 입력받음

    switch (key)
    {
    case 'w':
        if (*y == 5 && (*x > 19 && *x < 23)) // 게임기 못뚫게
            break;
        else if (*type == 2 || *type == 3) // type 2:상,좌 입구맵 type 3:상,우 입구맵
        {
            if ((*x > 20 && *x < 24) && *y == 1) // 입구로 들어가기 위함
            {
                *y -= 1;
                if (*type == 2)
                    *type = 1;
                else if (*type == 3)
                    *type = 0;
                else
                    break;
                *y = 8;
            }
            else
            {
                if (*y > 1)
                    *y -= 1;
            }
        }
        else
        {
            if (*y != 1)
                *y -= 1;
        }
        break;

    case 's':
        if (*y == 3 && (*x > 19 && *x < 23)) // 게임기 못뚫게
            break;
        else if (*type == 0 || *type == 1) // type 0:하,우 입구맵 type 1: 하,좌 입구맵
        {
            if ((*x > 19 && *x < 24) && *y == 8)
            {
                *y += 1;
                if (*type == 0)
                    *type = 3;
                else if (*type == 1)
                    *type = 2;
                else
                    break;
                *y = 1;
            }
            else
            {
                if (*y < 8)
                    *y += 1;
            }
        }
        else
        {
            if (*y < 8)
                *y += 1;
        }
        break;

    case 'a':
        if (*y == 4 && *x == 23)
            break;
        else if ((*type == 1 || *type == 2)) // type 1: 하,좌 입구맵 type 2:상,좌 입구맵
        {
            if ((*y == 4 || *y == 5) && *x == 1)
            {
                *x -= 1;
                if (*type == 1)
                    *type = 0;
                else if (*type == 2)
                    *type = 3;
                else
                    break;
                *x = 41;
            }
            else
            {
                if (*x > 1)
                    *x -= 1;
            }
        }
        else
        {
            if (*x > 1)
                *x -= 1;
        }
        break;

    case 'd':
        if (*y == 4 && *x == 19)
            break;
        else if (*type == 0 || *type == 3) // type 0:하,우 입구맵 type 3:상,우 입구맵
        {
            if ((*y == 4 || *y == 5) && *x == 41)
            {
                *x += 1;
                if (*type == 0)
                    *type = 1;
                else if (*type == 3)
                    *type = 2;
                else
                    break;
                *x = 1;
            }
            else
            {
                if (*x < 41)
                    *x += 1;
            }
        }
        else
        {
            if (*x < 41)
                *x += 1;
        }
        break;

    case 'r':
        if (*y == 4) // 게임기 기준 왼쪽 오른쪽
        {
            if (*x == 19 || *x == 23)
            {
                switch (*type)
                {
                case 0:
                    on_off = 1;
                    break;
                case 1:
                    on_off = 2;
                    break;
                case 2:
                    on_off = 3;
                    break;
                case 3:
                    on_off = 4;
                    break;
                default:
                    break;
                }
            }
        }
        else if (*x > 19 && *x < 23) // 게임기 기준 위 아래
        {
            if (*y == 5 || *y == 3)
            {
                switch (*type)
                {
                case 0:
                    on_off = 1;
                    break;
                case 1:
                    on_off = 2;
                    break;
                case 2:
                    on_off = 3;
                    break;
                case 3:
                    on_off = 4;
                    break;
                default:
                    break;
                }
            }
        }
        break;

    case 'x':
        on_off = 5;
        break;
    default:
        // printf("ERROR! key");
        break;
    }
    return on_off;
}

void ChoonMaps(int x, int y, int type) // 맵을 출력하고 춘식이의 x,y좌표에 춘식이 출력
{
    int i, j, here = 0;
    int map[10][44] = {
        0,
    };

    system("clear");
    for (i = 0; i < 10; i++) // 세로0~9
    {
        for (j = 0; j < 44; j++) // 가로0~43
        {
            map[0][j] = 1;        // 0번줄 벽 생성
            if (i >= 1 && i <= 8) // 1번줄~9번줄까지 좌 우 벽 생성
            {
                map[i][0] = 1;
                map[i][43] = 1;
            }
            map[9][j] = 1; // 0번줄 벽 생성

            switch (type) // 맵 타입에따른 입구 위치
            {
            case 0:                     // 디폴트. 좌상단맵에서 시작 우,하 입구
                if (j >= 21 && j <= 24) // 하단 입구
                    map[9][j] = 2;
                if (j == 43) // 우측 입구
                {
                    map[4][j] = 2;
                    map[5][j] = 2;
                }
                break;

            case 1:
                if (j >= 21 && j <= 24) // 하단 입구
                    map[9][j] = 2;
                if (j == 0) // 좌측 입구
                {
                    map[4][j] = 2;
                    map[5][j] = 2;
                }
                break;

            case 2:
                if (j >= 21 && j <= 24) // 상단 입구
                    map[0][j] = 2;
                if (j == 0) // 좌측 입구
                {
                    map[4][j] = 2;
                    map[5][j] = 2;
                }
                break;

            case 3:
                if (j >= 21 && j <= 24) // 상단 입구
                    map[0][j] = 2;
                if (j == 43) // 우측 입구
                {
                    map[4][j] = 2;
                    map[5][j] = 2;
                }
                break;

            default:
                printf("ERROR! MAP\n");
                break;
            }

            if (i == y && j == x || here > 0) // 캐릭터 위치 출력
            {
                if (here == 0)
                {
                    printf("😺");
                    here++; // 벽 밀리는 것 방지
                }
                else
                    here++; // 캐릭터를 그린 다음 2칸은 공백을 그리지 않음. 춘식이 크기==" "2칸
            }
            if (i == 4 && j == 21)
            {
                printf("🕹 "); // 게임기
                here++;
            }

            if (here == 0)
            {
                if (map[i][j] == 0) // 빈칸
                    printf(" ");
                else if (map[i][j] == 1) // 벽
                    printf("▒");
                else if (map[i][j] == 2) // 입구
                    printf(" ");
                else
                {
                    printf("ERROR\n");
                    break;
                }
            }

            if (here >= 2) // 2칸을 아무것도 출력하지 않았으면 초기화
                here = 0;
        }
        printf("\n");
        here = 0; // 춘식이가 가로 끝에있을때 다음줄에서 벽이 밀리는 것 방지
    }
    // printf("x:%d y:%d\n", x, y);    //테스트용 나중에 지움
}

void Start() // 게임설명을 들을지 선택 후 스킵하지 않으면 설명 메세지를 출력하는 함수
{
    int s; // select, 스킵할지 선택

    printf("설명을 스킵하려면 1 아니면 다른 숫자를 입력해주세요.\n");
    scanf("%d", &s);

    if (s != 1)
    {
        system("clear");
        printf("유령을 죽여 30점을 달성하세요!\n");
        printf("WASD로 이동과 사격을 동시에 할 수 있으며,\n");
        printf("적은 1, 캐릭터는 3의 체력을 가지고 있습니다.\n");
        printf("적 1마리에 1점입니다.\n");
        printf("아무키나 입력해 시작\n");
        scanf("%d", &s);
    }
}

void InitObj(int maps[10][40]) // 시작 오브젝트 초기화
{
    int i, j, rand_x, rand_y, chr_x = 10, chr_y = 8;
    const int empty = 0, space = 1, wall = 2, chr = 3, enemy = 4, boss = 5, heart = 6, item = 7, bullet = 8; // 오브젝트 값 고정

    for (i = 0; i < 10; i++)
    {
        for (j = 0; j < 40; j++)
        {
            maps[i][j] = space;
            if ((i == 0 || i == 9) && j < 20) // 맨 윗줄, 아랫줄 벽 값
                maps[i][j] = wall;
            if (j == 0 || j == 19) // 양끝벽
                maps[i][j] = wall;
            if (j > 20 && j < 24) // 목숨3개
                maps[5][j] = heart;
        }
    }
    maps[chr_y][chr_x] = chr;
}

int CheckObj(int maps[10][40], int x, int y, int dir) // 움직일곳에 있는 오브젝트 종류를 반환
{
    int check = 0;

    switch (dir)
    {
    case 1: // w 위
        check = maps[y - 1][x];
        break;
    case 2: // a 왼쪽
        check = maps[y][x - 1];
        break;
    case 3: // s 아래
        check = maps[y + 1][x];
        break;
    case 4: // d 오른쪽
        check = maps[y][x + 1];
        break;
    default:
        break;
    }
    return check;
}

void KillEnemy(int maps[10][40], int x, int y, int score, int *now_item)
{
    int item;
    maps[y][x] = 9;
    PaintObj(maps, score);
    usleep(250000);
    maps[y][x] = 1;
    item = rand() % 10 + 1;
    if (item > 8)
    {
        if (*now_item <= 3)
        {
            maps[y][x] = 7;
            *now_item += 1;
        }
    }
}

int Shoot(int maps[10][40], int chr_x, int chr_y, int score, int *now_item, char dir)
{
    int check, cnt = 0, first = 0, end = 0;
    while (1)
    {
        switch (dir)
        {
        case 'w': // 위
            check = CheckObj(maps, chr_x, chr_y - cnt, 1);
            switch (check)
            {
            case 0:
            case 1:
                maps[chr_y - cnt - 1][chr_x] = 8;
                if (first > 0)
                    maps[chr_y - cnt][chr_x] = check;
                break;
            case 4:
                score += 1;
                KillEnemy(maps, chr_x, chr_y - cnt - 1, score, now_item);
                end = 1;
                if (first > 0)
                    maps[chr_y - cnt][chr_x] = 1;
                break;
            default:
                if (first > 0)
                    maps[chr_y - cnt][chr_x] = 1;
                end = 1;
                break;
            }
            break;
        case 'a': // 왼쪽
            check = CheckObj(maps, chr_x - cnt, chr_y, 2);
            switch (check)
            {
            case 0:
            case 1:
                maps[chr_y][chr_x - cnt - 1] = 8;
                if (first > 0)
                    maps[chr_y][chr_x - cnt] = check;
                break;
            case 4:
                score += 1;
                KillEnemy(maps, chr_x - cnt - 1, chr_y, score, now_item);
                end = 1;
                if (first > 0)
                    maps[chr_y][chr_x - cnt] = 1;
                break;
            default:
                if (first > 0)
                    maps[chr_y][chr_x - cnt] = 1;
                end = 1;
                break;
            }
            break;
        case 's': // 아래
            check = CheckObj(maps, chr_x, chr_y + cnt, 3);
            switch (check)
            {
            case 0:
            case 1:
                maps[chr_y + cnt + 1][chr_x] = 8;
                if (first > 0)
                    maps[chr_y + cnt][chr_x] = check;
                break;
            case 4:
                score += 1;
                KillEnemy(maps, chr_x, chr_y + cnt + 1, score, now_item);
                end = 1;
                if (first > 0)
                    maps[chr_y + cnt][chr_x] = 1;
                break;
            default:
                if (first > 0)
                    maps[chr_y + cnt][chr_x] = 1;
                end = 1;
                break;
            }
            break;
        case 'd': // 오른쪽
            check = CheckObj(maps, chr_x + cnt, chr_y, 4);
            switch (check)
            {
            case 0:
            case 1:
                maps[chr_y][chr_x + cnt + 1] = 8;
                if (first > 0)
                    maps[chr_y][chr_x + cnt] = check;
                break;
            case 4:
                score += 1;
                KillEnemy(maps, chr_x + cnt + 1, chr_y, score, now_item);
                end = 1;
                if (first > 0)
                    maps[chr_y][chr_x + cnt] = 1;
                break;
            default:
                if (first > 0)
                    maps[chr_y][chr_x + cnt] = 1;
                end = 1;
                break;
            }
            break;
        default:
            end = 1;
        }
        if (end == 1)
            break;
        cnt++;
        first = 1;
        PaintObj(maps, score);
        usleep(50000);
    }
    return score;
}

int ChrMove(int maps[10][40], int *chr_x, int *chr_y, int *item, int hp, int *score, int *now_item)
{
    int check = 0;
    char key;

    fflush(stdout);
    key = getch();
    maps[*chr_y][*chr_x] = 1;
    switch (key)
    {
    case 'w':
        check = CheckObj(maps, *chr_x, *chr_y, 1); // 배열, x, y, 방향(1:w 2:a 3:s 4:d)
        switch (check)                             // const int empty=0, space=1, wall=2, chr=3, enemy=4, boss=5, heart=6, item=7, bullet=8;
        {
        case 0:
            *chr_y -= 1;
            break;
        case 1:
            *chr_y -= 1;
            break;
        case 2:
            break;
        case 3:
            // printf("ERROR! check");
            break;
        case 4:
            hp -= 1;
            break;
        case 5:
            hp -= 1;
            break;
        case 7:
            if (*item <= 3)
            {
                printf("아이템 1개 획득!\n");
                *item += 1;
                *now_item -= 1;
                *chr_y -= 1;
                usleep(800000);
            }
            else
            {
                printf("아이템이 최대치입니다.\n");
                usleep(800000);
            }
            break;
        default:
            break;
        }
        break;
    case 'a':
        check = CheckObj(maps, *chr_x, *chr_y, 2);
        switch (check)
        {
        case 0:
            *chr_x -= 1;
            break;
        case 1:
            *chr_x -= 1;
            break;
        case 2:
            break;
        case 3:
            // printf("ERROR! check");
            break;
        case 4:
            hp -= 1;
            break;
        case 5:
            hp -= 1;
            break;
        case 7:
            if (*item <= 3)
            {
                printf("아이템 1개 획득!\n");
                *item += 1;
                *now_item -= 1;
                *chr_x -= 1;
                usleep(800000);
            }
            else
            {
                printf("아이템이 최대치입니다.\n");
                usleep(800000);
            }
            break;
        default:
            break;
        }
        break;
    case 's':
        check = CheckObj(maps, *chr_x, *chr_y, 3);
        switch (check)
        {
        case 0:
            *chr_y += 1;
            break;
        case 1:
            *chr_y += 1;
            break;
        case 2:
            break;
        case 3:
            // printf("ERROR! check");
            break;
        case 4:
            hp -= 1;
            break;
        case 5:
            hp -= 1;
            break;
        case 7:
            if (*item <= 3)
            {
                printf("아이템 1개 획득!\n");
                *item += 1;
                *now_item -= 1;
                *chr_y += 1;
                usleep(800000);
            }
            else
            {
                printf("아이템이 최대치입니다.\n");
                usleep(800000);
            }
            break;
        default:
            break;
        }
        break;
    case 'd':
        check = CheckObj(maps, *chr_x, *chr_y, 4);
        switch (check)
        {
        case 0:
            *chr_x += 1;
            break;
        case 1:
            *chr_x += 1;
            break;
        case 2:
            break;
        case 3:
            // printf("ERROR! check");
            break;
        case 4:
            hp -= 1;
            break;
        case 5:
            hp -= 1;
            break;
        case 7:
            if (*item <= 3)
            {
                printf("아이템 1개 획득!\n");
                *item += 1;     // 소지 아이템
                *now_item -= 1; // 필드에 있는 아이템
                *chr_x += 1;
                usleep(800000);
            }
            else
            {
                printf("아이템이 최대치입니다.\n");
                usleep(800000);
            }
            break;
        default:
            break;
        }
        break;
    case 't':
        if (*item > 0)
        {
            if (hp >= 3)
            {
                printf("이미 최대 체력입니다.\n");
                usleep(800000);
            }
            else
            {
                printf("체력이 1회복 되었습니다.\n");
                usleep(800000);
                hp += 1;
                *item -= 1;
            }
        }
        else
        {
            printf("item이 부족합니다.\n");
            usleep(800000);
        }
        break;
    case 'x':
        hp = 4;
        break;
    default:
        break;
    }
    maps[*chr_y][*chr_x] = 3;
    *score = Shoot(maps, *chr_x, *chr_y, *score, now_item, key);
    return hp;
}

void PaintObj(int maps[10][40], int score) // 배열 요소값에 따라 다른 오브젝트 그려줌
{
    int i, j, obj, here = 0;
    const int empty = 0, space = 1, wall = 2, chr = 3, enemy = 4, boss = 5, heart = 6, item = 7, bullet = 8, fire = 9; // 오브젝트 값 고정

    system("clear");
    for (i = 0; i < 10; i++)
    {
        for (j = 0; j < 40; j++)
        {
            obj = maps[i][j];
            switch (obj)
            {
            case 0: // 없음
                break;
            case 1: // 공백
                printf("  ");
                break;
            case 2: // 벽
                printf("▒▒");
                break;
            case 3: // 캐릭터
                printf("🤖");
                break;
            case 4: // 적
                printf("👻");
                break;
            /*case 5:     //보스
            printf("😈");
            break;*/
            case 6: // 체력
                printf("🔩 ");
                break;
            case 7: // 아이템
                printf("🔧");
                break;
            case 8: // 탄환
                printf("⚙️ ");
                break;
            case 9: // 적 죽음
                printf("🔥");
                break;
            default:
                break;
            }
            if (i == 4 && j == 20)
                printf("점수:%d", score);
            if (i == 5 && j == 20)
                printf("체력:");
            if (i == 6 && j == 20)
                printf("회복 아이템: ");
            if (i == 7 && j == 20)
                printf("W A S D = 이동 및 사격");
            if (i == 8 && j == 20)
                printf("T = 회복 아이템 사용");
            if (i == 9 && j == 20)
                printf("X = 종료");
        }
        printf("\n");
    }
}

void RegenEnemy(int maps[10][40]) // 랜덤 빈공간에 적 생성
{
    int i;
    int x, y;

    while (1)
    {
        x = rand() % 18 + 1;
        y = rand() % 8 + 1;
        if (maps[x][y] == 4 || maps[x][y] == 3 || maps[x][y] == 7)
            continue;
        else
            maps[y][x] = 4;

        break;
    }
}

int CheckEnemy(int maps[10][40])
{
    int i, j, now_enemy = 0;

    for (i = 1; i < 10; i++)
    {
        for (j = 1; j < 40; j++)
        {
            if (maps[i][j] == 4)
                now_enemy++;
        }
    }
    return now_enemy;
}

void MoveEnemy(int maps[10][40], int *hp, int type)
{
    int i, j, k = 0;
    int enemy_move[6] = {0};
    int check, rand_dir;

    for (i = 1; i < 9; i++)
    {
        for (j = 1; j < 38; j++)
        {
            if (maps[i][j] == 4)
            {
                enemy_move[k] = i * 100 + j;
                k++;
            }
        }
    }
    for (k = 0; k < 6; k++)
    {
        if (enemy_move[k] != 0)
        {
            i = enemy_move[k] / 100;
            j = enemy_move[k] % 100;
            rand_dir = rand() % 4 + 1;
            check = CheckObj(maps, j, i, rand_dir);
            switch (rand_dir)
            {
            case 1: // 위
                switch (check)
                {
                case 0:
                    maps[i][j] = 1;
                    maps[i - 1][j] = 4;
                    break;
                case 1:
                    maps[i][j] = 1;
                    maps[i - 1][j] = 4;
                    break;
                case 3: // 캐릭터
                    hp -= 1;
                    break;
                case 4:
                    maps[i][j] = 4;
                    maps[i - 1][j] = 4;
                    break;
                default:
                    break;
                }
                break;
            case 2: // 왼쪽
                switch (check)
                {
                case 0:
                    maps[i][j] = 1;
                    maps[i][j - 1] = 4;
                    break;
                case 1:
                    maps[i][j] = 1;
                    maps[i][j - 1] = 4;
                    break;
                case 3: // 캐릭터
                    hp -= 1;
                    break;
                case 4:
                    maps[i][j] = 4;
                    maps[i][j - 1] = 4;
                default:
                    break;
                }
                break;
            case 3: // 아래
                switch (check)
                {
                case 0:
                    maps[i][j] = 1;
                    maps[i + 1][j] = 4;
                    break;
                case 1:
                    maps[i][j] = 1;
                    maps[i + 1][j] = 4;
                    break;
                case 3: // 캐릭터
                    hp -= 1;
                    break;
                case 4:
                    maps[i][j] = 4;
                    maps[i + 1][j] = 4;
                default:
                    break;
                }
                break;
            case 4: // 오른쪽
                switch (check)
                {
                case 0: // 빈공간
                    maps[i][j] = 1;
                    maps[i][j + 1] = 4;
                    break;
                case 1: // 빈공간
                    maps[i][j] = 1;
                    maps[i][j + 1] = 4;
                    break;
                case 3: // 캐릭터
                    hp -= 1;
                    break;
                case 4:
                    maps[i][j] = 4;
                    maps[i][j + 1] = 4;
                default:
                    break;
                }
                break;
            }
        }
        else
        {
            break;
        }
    }
}

int Game1(int victory, int *ad)
{
    int maps[10][40] = {
        0,
    };
    int chr_x = 10, chr_y = 8, inven = 0, hp = 3, now_enemy = 0, now_item = 0, score = 0, win = 0; // 캐릭터 초기위치
    const int empty = 0, space = 1, wall = 2, chr = 3, enemy = 4, boss = 5, heart = 6, item = 7;   // 배열 요소 고정값

    Start();
    InitObj(maps);
    while (1)
    {
        // for()
        PaintObj(maps, score);
        hp = ChrMove(maps, &chr_x, &chr_y, &inven, hp, &score, &now_item);
        if (hp <= 0)
        {
            printf("패배!\n");
            *ad += 1;
            usleep(1000000);
            break;
        }

        if (score >= 30)
        {
            printf("승리!\n");
            victory++;
            *ad += 1;
            usleep(1000000);
            break;
        }
        switch (hp) // 현재 체력에 따라 요소값 바꿔 우측 그려진 체력 바꾸기
        {
        case 1:
            maps[5][21] = 6;
            maps[5][22] = 1;
            maps[5][23] = 1;
            break;
        case 2:
            maps[5][21] = 6;
            maps[5][22] = 6;
            maps[5][23] = 1;
            break;
        case 3:
            maps[5][21] = 6;
            maps[5][22] = 6;
            maps[5][23] = 6;
            break;
        default:
            break;
        }

        switch (inven)
        {
        case 0:
            maps[6][21] = 1;
            maps[6][22] = 1;
            maps[6][23] = 1;
            break;
        case 1:
            maps[6][21] = 7;
            maps[6][22] = 1;
            maps[6][23] = 1;
            break;
        case 2:
            maps[6][21] = 7;
            maps[6][22] = 7;
            maps[6][23] = 1;
            break;
        case 3:
            maps[6][21] = 7;
            maps[6][22] = 7;
            maps[6][23] = 7;
            break;
        }
        MoveEnemy(maps, &hp, enemy);
        now_enemy = CheckEnemy(maps);
        if (now_enemy < 6)
        {
            RegenEnemy(maps);
            now_enemy++;
        }
        usleep(100000);
    }
    return victory;
}

int Game2(int victory, int *ad) // 팩맨
{
    srand(time(NULL));

    char move;      // 이동 wasd
    int move_enemy; // 적 이동

    int title; // 타이틀 화면
    int a, b, i, j, k, l;

    while (1) // 타이틀
    {
        int field[10][15] = {
            {4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4},
            {4, 2, 2, 5, 2, 2, 2, 2, 2, 2, 2, 5, 2, 2, 4},
            {4, 2, 4, 4, 4, 2, 4, 4, 4, 2, 4, 4, 4, 2, 4},
            {4, 2, 4, 4, 4, 2, 4, 4, 4, 2, 4, 4, 4, 2, 4},
            {4, 2, 4, 4, 4, 2, 4, 4, 4, 2, 4, 4, 4, 2, 4},
            {4, 2, 2, 2, 2, 2, 2, 1, 2, 2, 2, 2, 2, 2, 4},
            {4, 2, 4, 4, 4, 2, 4, 4, 4, 2, 4, 4, 4, 2, 4},
            {4, 2, 4, 4, 4, 2, 4, 4, 4, 2, 4, 4, 4, 2, 4},
            {4, 3, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 3, 4},
            {4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4}}; // 게임 필드 구현
        // 0:빈공간
        // 1:주인공
        // 2:먹이
        // 3:아이템(10번 움직임 동안 적 처치 가능)
        // 4:벽(장애물)
        // 5:적
        // 6:적(처치가능)
        // 7:적(먹이와 겹침)
        // 8:적(처치가능)(먹이와 겹침)
        /*
        주인공1+먹이2=빈공간0
        주인공1+아이템3=적6(처치가능)
        주인공1+벽4=이동불가 continue 사용해서 되돌아옴
        주인공1+적5=게임오버 break 사용해서 메인화면
        주인공1+적6(처치가능)=빈공간0

        적5+벽4=이동불가
        적5+먹이2=적7(먹이와 겹침)
        적6+벽4=이동불가
        적6+먹이2=적8(먹이와 겹침)
        적5+아이템3=아이템 증발
        적6+아이템3=아이템 효과 해제
        */

        int game_over = 0; // 게임오버
        int food = 0;      // 먹이를 포식할 때 마다 1씩 증가, 먹이를 모두 포식하면 클리어
        int item = 0;      // 아이템
        int turn = 0;      // 턴 수
        int clr = 0;       // 클리어

        system("clear");
        printf("1:게임시작 \n");
        printf("2:게임종료 \n");
        scanf("%d", &title);

        if (title == 2)
        {
            break;
        }
        else if (title == 1)
        {
        }
        else
        {
            continue;
        }

        while (1) // 적과 닿았을 때 게임오버
        {
            while (1) // 1반복문 1움직임(1턴)
            {
                system("clear");

                int find = 0;
                int find_enemy = 0;

                for (i = 0; i < 10; i++) // 게임 필드 출력
                {
                    for (j = 0; j < 15; j++)
                    {
                        if (field[i][j] == 1)
                        {
                            printf("🙂 ");
                        }
                        else if (field[i][j] == 2)
                        {
                            printf("🍬 ");
                        }
                        else if (field[i][j] == 3)
                        {
                            printf("✨ ");
                        }
                        else if (field[i][j] == 4)
                        {
                            printf("⬜ ");
                        }
                        else if (field[i][j] == 5)
                        {
                            printf("😈 ");
                        }
                        else if (field[i][j] == 6)
                        {
                            printf("💀 ");
                        }
                        else if (field[i][j] == 7)
                        {
                            printf("😈 ");
                        }
                        else if (field[i][j] == 8)
                        {
                            printf("💀 ");
                        }
                        else if (field[i][j] == 0)
                        {
                            printf("   ");
                        }
                    }
                    printf("\n");
                }

                if (game_over == 1) // 게임오버 시 게임오버 화면 출력
                {
                    system("clear");
                    printf("GameOver \n");
                    sleep(2);
                    break;
                }

                scanf(" %c", &move);

                for (i = 0; i < 10; i++) // 주인공 위치 찾기
                {
                    for (j = 0; j < 15; j++)
                    {

                        if (field[i][j] == 1)
                        {
                            find = 1;
                            break;
                        }
                    }

                    if (find == 1)
                    {
                        break;
                    }
                }

                if (move == 'w') // 위로 이동
                {
                    if (field[i - 1][j] == 0) // 이동하려는 칸이 빈칸일 때
                    {
                        field[i][j] = 0;
                        field[i - 1][j] = 1;
                    }
                    else if (field[i - 1][j] == 2)
                    { // 이동하려는 칸이 먹이일 때
                        field[i][j] = 0;
                        field[i - 1][j] = 1;
                        food++;
                    }
                    else if (field[i - 1][j] == 3)
                    { // 이동하려는 칸이 아이템일 때
                        field[i][j] = 0;
                        field[i - 1][j] = 1;
                        item += 10; // 10회 움직일 동안 유지

                        for (a = 0; a < 10; a++) // 적을 처치 가능한 적으로 변경
                        {
                            for (b = 0; b < 15; b++)
                            {
                                if (field[a][b] == 5 || field[a][b] == 6)
                                {
                                    field[a][b] = 6;
                                }
                                else if (field[a][b] == 7 || field[a][b] == 8)
                                {
                                    field[a][b] = 8;
                                }
                            }
                        }
                    }
                    else if (field[i - 1][j] == 4)
                    { // 이동하려는 칸이 벽일 때
                        continue;
                    }
                    else if (field[i - 1][j] == 5 || field[i - 1][j] == 7)
                    { // 이동하려는 칸이 적일 때
                        field[i][j] = 0;
                        game_over = 1;
                        continue;
                    }
                    else if (field[i - 1][j] == 6)
                    { // 이동하려는 칸이 처치 가능한 적일 때
                        field[i][j] = 0;
                        field[i - 1][j] = 1;
                    }
                    else if (field[i - 1][j] == 8)
                    { // 이동하려는 칸이 먹이와 겹친 처치 가능한 적일 때
                        field[i][j] = 0;
                        field[i - 1][j] = 1;
                        food++;
                    } // 이하 복붙하고 이동 방향만 변경
                }
                else if (move == 's')
                { // 아래로 이동
                    if (field[i + 1][j] == 0)
                    {
                        field[i][j] = 0;
                        field[i + 1][j] = 1;
                    }
                    else if (field[i + 1][j] == 2)
                    {
                        field[i][j] = 0;
                        field[i + 1][j] = 1;
                        food++;
                    }
                    else if (field[i + 1][j] == 3)
                    {
                        field[i][j] = 0;
                        field[i + 1][j] = 1;
                        item += 10;

                        for (a = 0; a < 10; a++)
                        {
                            for (b = 0; b < 15; b++)
                            {
                                if (field[a][b] == 5 || field[a][b] == 6)
                                {
                                    field[a][b] = 6;
                                }
                                else if (field[a][b] == 7 || field[a][b] == 8)
                                {
                                    field[a][b] = 8;
                                }
                            }
                        }
                    }
                    else if (field[i + 1][j] == 4)
                    {
                        continue;
                    }
                    else if (field[i + 1][j] == 5 || field[i + 1][j] == 7)
                    {
                        field[i][j] = 0;
                        game_over = 1;
                        continue;
                    }
                    else if (field[i + 1][j] == 6)
                    {
                        field[i][j] = 0;
                        field[i + 1][j] = 1;
                    }
                    else if (field[i + 1][j] == 8)
                    {
                        field[i][j] = 0;
                        field[i + 1][j] = 1;
                        food++;
                    }
                }
                else if (move == 'a')
                { // 좌로 이동
                    if (field[i][j - 1] == 0)
                    {
                        field[i][j] = 0;
                        field[i][j - 1] = 1;
                    }
                    else if (field[i][j - 1] == 2)
                    {
                        field[i][j] = 0;
                        field[i][j - 1] = 1;
                        food++;
                    }
                    else if (field[i][j - 1] == 3)
                    {
                        field[i][j] = 0;
                        field[i][j - 1] = 1;
                        item += 10;

                        for (a = 0; a < 10; a++)
                        {
                            for (b = 0; b < 15; b++)
                            {
                                if (field[a][b] == 5 || field[a][b] == 6)
                                {
                                    field[a][b] = 6;
                                }
                                else if (field[a][b] == 7 || field[a][b] == 8)
                                {
                                    field[a][b] = 8;
                                }
                            }
                        }
                    }
                    else if (field[i][j - 1] == 4)
                    {
                        continue;
                    }
                    else if (field[i][j - 1] == 5 || field[i][j - 1] == 7)
                    {
                        field[i][j] = 0;
                        game_over = 1;
                        continue;
                    }
                    else if (field[i][j - 1] == 6)
                    {
                        field[i][j] = 0;
                        field[i][j - 1] = 1;
                    }
                    else if (field[i][j - 1] == 8)
                    {
                        field[i][j] = 0;
                        field[i][j - 1] = 1;
                        food++;
                    }
                }
                else if (move == 'd')
                { // 우로 이동
                    if (field[i][j + 1] == 0)
                    {
                        field[i][j] = 0;
                        field[i][j + 1] = 1;
                    }
                    else if (field[i][j + 1] == 2)
                    {
                        field[i][j] = 0;
                        field[i][j + 1] = 1;
                        food++;
                    }
                    else if (field[i][j + 1] == 3)
                    {
                        field[i][j] = 0;
                        field[i][j + 1] = 1;
                        item += 10;

                        for (a = 0; a < 10; a++)
                        {
                            for (b = 0; b < 15; b++)
                            {
                                if (field[a][b] == 5 || field[a][b] == 6)
                                {
                                    field[a][b] = 6;
                                }
                                else if (field[a][b] == 7 || field[a][b] == 8)
                                {
                                    field[a][b] = 8;
                                }
                            }
                        }
                    }
                    else if (field[i][j + 1] == 4)
                    {
                        continue;
                    }
                    else if (field[i][j + 1] == 5 || field[i][j + 1] == 7)
                    {
                        field[i][j] = 0;
                        game_over = 1;
                        continue;
                    }
                    else if (field[i][j + 1] == 6)
                    {
                        field[i][j] = 0;
                        field[i][j + 1] = 1;
                    }
                    else if (field[i][j + 1] == 8)
                    {
                        field[i][j] = 0;
                        field[i][j + 1] = 1;
                        food++;
                    }
                }
                else
                { // wasd 이외의 문자 입력 시
                    continue;
                }

                for (i = 0; i < 10; i++) // 적 위치 찾기
                {
                    for (j = 0; j < 15; j++)
                    {
                        if (field[i][j] >= 5 && field[i][j] <= 8)
                        {
                            find_enemy = 1;
                            break;
                        }
                    }

                    if (find_enemy == 1)
                    {
                        break;
                    }
                }

                for (k = 9; k >= 0; k--) // 두번째 적 위치 찾기
                {
                    for (l = 14; l >= 0; l--)
                    {
                        if (field[k][l] >= 5 && field[k][l] <= 8)
                        {
                            find_enemy = 2;
                            break;
                        }
                    }

                    if (find_enemy == 2)
                    {
                        break;
                    }
                }

                while (1) // 벽에 막혀 이동 불가일 때 돌아오기 위한 반복문
                {
                    move_enemy = rand() % 4;

                    if (move_enemy == 0) // 적 위로 이동
                    {
                        if (field[i - 1][j] == 1) // 주인공과 충돌 시
                        {
                            if (field[i][j] == 5)
                            {
                                field[i][j] = 0;
                                field[i - 1][j] = 5;
                                game_over = 1;
                                break;
                            }
                            else if (field[i][j] == 6)
                            {
                                field[i][j] = 0;
                            }
                            else if (field[i][j] == 7)
                            {
                                field[i][j] = 2;
                                field[i - 1][j] = 5;
                                game_over = 1;
                                break;
                            }
                            else if (field[i][j] == 8)
                            {
                                field[i][j] = 2;
                            }
                        }
                        else if (field[i - 1][j] == 2)
                        { // 먹이와 충돌 시
                            if (field[i][j] == 5)
                            {
                                field[i][j] = 0;
                                field[i - 1][j] = 7;
                            }
                            else if (field[i][j] == 6)
                            {
                                field[i][j] = 0;
                                field[i - 1][j] = 8;
                            }
                            else if (field[i][j] == 7)
                            {
                                field[i][j] = 2;
                                field[i - 1][j] = 7;
                            }
                            else if (field[i][j] == 8)
                            {
                                field[i][j] = 2;
                                field[i - 1][j] = 8;
                            }
                        }
                        else if (field[i - 1][j] == 3)
                        { // 아이템과 충돌 시
                            if (field[i][j] == 5)
                            {
                                field[i][j] = 0;
                                field[i - 1][j] = 5;
                            }
                            else if (field[i][j] == 6)
                            {
                                field[i][j] = 0;
                                field[i - 1][j] = 5;
                                item = 0;
                            }
                            else if (field[i][j] == 7)
                            {
                                field[i][j] = 2;
                                field[i - 1][j] = 5;
                            }
                            else if (field[i][j] == 8)
                            {
                                field[i][j] = 2;
                                field[i - 1][j] = 5;
                                item = 0;
                            }
                        }
                        else if (field[i - 1][j] >= 4 && field[i - 1][j] <= 8)
                        { // 벽과 충돌 또는 적끼리 충돌 시
                            continue;
                        }
                        else if (field[i - 1][j] == 0)
                        { // 빈공간으로 이동 시
                            if (field[i][j] == 5)
                            {
                                field[i][j] = 0;
                                field[i - 1][j] = 5;
                            }
                            else if (field[i][j] == 6)
                            {
                                field[i][j] = 0;
                                field[i - 1][j] = 6;
                            }
                            else if (field[i][j] == 7)
                            {
                                field[i][j] = 2;
                                field[i - 1][j] = 5;
                            }
                            else if (field[i][j] == 8)
                            {
                                field[i][j] = 2;
                                field[i - 1][j] = 6;
                            }
                        } // 이하 복붙하고 이동 방향만 변경
                    }
                    else if (move_enemy == 1)
                    { // 적 아래로 이동
                        if (field[i + 1][j] == 1)
                        {
                            if (field[i][j] == 5)
                            {
                                field[i][j] = 0;
                                field[i + 1][j] = 5;
                                game_over = 1;
                                break;
                            }
                            else if (field[i][j] == 6)
                            {
                                field[i][j] = 0;
                            }
                            else if (field[i][j] == 7)
                            {
                                field[i][j] = 2;
                                field[i + 1][j] = 5;
                                game_over = 1;
                                break;
                            }
                            else if (field[i][j] == 8)
                            {
                                field[i][j] = 2;
                            }
                        }
                        else if (field[i + 1][j] == 2)
                        {
                            if (field[i][j] == 5)
                            {
                                field[i][j] = 0;
                                field[i + 1][j] = 7;
                            }
                            else if (field[i][j] == 6)
                            {
                                field[i][j] = 0;
                                field[i + 1][j] = 8;
                            }
                            else if (field[i][j] == 7)
                            {
                                field[i][j] = 2;
                                field[i + 1][j] = 7;
                            }
                            else if (field[i][j] == 8)
                            {
                                field[i][j] = 2;
                                field[i + 1][j] = 8;
                            }
                        }
                        else if (field[i + 1][j] == 3)
                        {
                            if (field[i][j] == 5)
                            {
                                field[i][j] = 0;
                                field[i + 1][j] = 5;
                            }
                            else if (field[i][j] == 6)
                            {
                                field[i][j] = 0;
                                field[i + 1][j] = 5;
                                item = 0;
                            }
                            else if (field[i][j] == 7)
                            {
                                field[i][j] = 2;
                                field[i + 1][j] = 5;
                            }
                            else if (field[i][j] == 8)
                            {
                                field[i][j] = 2;
                                field[i + 1][j] = 5;
                                item = 0;
                            }
                        }
                        else if (field[i + 1][j] >= 4 && field[i + 1][j] <= 8)
                        {
                            continue;
                        }
                        else if (field[i + 1][j] == 0)
                        {
                            if (field[i][j] == 5)
                            {
                                field[i][j] = 0;
                                field[i + 1][j] = 5;
                            }
                            else if (field[i][j] == 6)
                            {
                                field[i][j] = 0;
                                field[i + 1][j] = 6;
                            }
                            else if (field[i][j] == 7)
                            {
                                field[i][j] = 2;
                                field[i + 1][j] = 5;
                            }
                            else if (field[i][j] == 8)
                            {
                                field[i][j] = 2;
                                field[i + 1][j] = 6;
                            }
                        }
                    }
                    else if (move_enemy == 2)
                    { // 적 좌로 이동
                        if (field[i][j - 1] == 1)
                        {
                            if (field[i][j] == 5)
                            {
                                field[i][j] = 0;
                                field[i][j - 1] = 5;
                                game_over = 1;
                                break;
                            }
                            else if (field[i][j] == 6)
                            {
                                field[i][j] = 0;
                            }
                            else if (field[i][j] == 7)
                            {
                                field[i][j] = 2;
                                field[i][j - 1] = 5;
                                game_over = 1;
                                break;
                            }
                            else if (field[i][j] == 8)
                            {
                                field[i][j] = 2;
                            }
                        }
                        else if (field[i][j - 1] == 2)
                        {
                            if (field[i][j] == 5)
                            {
                                field[i][j] = 0;
                                field[i][j - 1] = 7;
                            }
                            else if (field[i][j] == 6)
                            {
                                field[i][j] = 0;
                                field[i][j - 1] = 8;
                            }
                            else if (field[i][j] == 7)
                            {
                                field[i][j] = 2;
                                field[i][j - 1] = 7;
                            }
                            else if (field[i][j] == 8)
                            {
                                field[i][j] = 2;
                                field[i][j - 1] = 8;
                            }
                        }
                        else if (field[i][j - 1] == 3)
                        {
                            if (field[i][j] == 5)
                            {
                                field[i][j] = 0;
                                field[i][j - 1] = 5;
                            }
                            else if (field[i][j] == 6)
                            {
                                field[i][j] = 0;
                                field[i][j - 1] = 5;
                                item = 0;
                            }
                            else if (field[i][j] == 7)
                            {
                                field[i][j] = 2;
                                field[i][j - 1] = 5;
                            }
                            else if (field[i][j] == 8)
                            {
                                field[i][j] = 2;
                                field[i][j - 1] = 5;
                                item = 0;
                            }
                        }
                        else if (field[i][j - 1] >= 4 && field[i][j - 1] <= 8)
                        {
                            continue;
                        }
                        else if (field[i][j - 1] == 0)
                        {
                            if (field[i][j] == 5)
                            {
                                field[i][j] = 0;
                                field[i][j - 1] = 5;
                            }
                            else if (field[i][j] == 6)
                            {
                                field[i][j] = 0;
                                field[i][j - 1] = 6;
                            }
                            else if (field[i][j] == 7)
                            {
                                field[i][j] = 2;
                                field[i][j - 1] = 5;
                            }
                            else if (field[i][j] == 8)
                            {
                                field[i][j] = 2;
                                field[i][j - 1] = 6;
                            }
                        }
                    }
                    else if (move_enemy == 3)
                    { // 적 우로 이동
                        if (field[i][j + 1] == 1)
                        {
                            if (field[i][j] == 5)
                            {
                                field[i][j] = 0;
                                field[i][j + 1] = 5;
                                game_over = 1;
                                break;
                            }
                            else if (field[i][j] == 6)
                            {
                                field[i][j] = 0;
                            }
                            else if (field[i][j] == 7)
                            {
                                field[i][j] = 2;
                                field[i][j + 1] = 5;
                                game_over = 1;
                                break;
                            }
                            else if (field[i][j] == 8)
                            {
                                field[i][j] = 2;
                            }
                        }
                        else if (field[i][j + 1] == 2)
                        {
                            if (field[i][j] == 5)
                            {
                                field[i][j] = 0;
                                field[i][j + 1] = 7;
                            }
                            else if (field[i][j] == 6)
                            {
                                field[i][j] = 0;
                                field[i][j + 1] = 8;
                            }
                            else if (field[i][j] == 7)
                            {
                                field[i][j] = 2;
                                field[i][j + 1] = 7;
                            }
                            else if (field[i][j] == 8)
                            {
                                field[i][j] = 2;
                                field[i][j + 1] = 8;
                            }
                        }
                        else if (field[i][j + 1] == 3)
                        {
                            if (field[i][j] == 5)
                            {
                                field[i][j] = 0;
                                field[i][j + 1] = 5;
                            }
                            else if (field[i][j] == 6)
                            {
                                field[i][j] = 0;
                                field[i][j + 1] = 5;
                                item = 0;
                            }
                            else if (field[i][j] == 7)
                            {
                                field[i][j] = 2;
                                field[i][j + 1] = 5;
                            }
                            else if (field[i][j] == 8)
                            {
                                field[i][j] = 2;
                                field[i][j + 1] = 5;
                                item = 0;
                            }
                        }
                        else if (field[i][j + 1] >= 4 && field[i][j + 1] <= 8)
                        {
                            continue;
                        }
                        else if (field[i][j + 1] == 0)
                        {
                            if (field[i][j] == 5)
                            {
                                field[i][j] = 0;
                                field[i][j + 1] = 5;
                            }
                            else if (field[i][j] == 6)
                            {
                                field[i][j] = 0;
                                field[i][j + 1] = 6;
                            }
                            else if (field[i][j] == 7)
                            {
                                field[i][j] = 2;
                                field[i][j + 1] = 5;
                            }
                            else if (field[i][j] == 8)
                            {
                                field[i][j] = 2;
                                field[i][j + 1] = 6;
                            }
                        }
                    }

                    move_enemy = rand() % 4;

                    if (move_enemy == 0) // 적 위로 이동
                    {
                        if (field[k - 1][l] == 1)
                        {
                            if (field[k][l] == 5)
                            {
                                field[k][l] = 0;
                                field[k - 1][l] = 5;
                                game_over = 1;
                                break;
                            }
                            else if (field[k][l] == 6)
                            {
                                field[k][l] = 0;
                            }
                            else if (field[k][l] == 7)
                            {
                                field[k][l] = 2;
                                field[k - 1][l] = 5;
                                game_over = 1;
                                break;
                            }
                            else if (field[k][l] == 8)
                            {
                                field[k][l] = 2;
                            }
                        }
                        else if (field[k - 1][l] == 2)
                        {
                            if (field[k][l] == 5)
                            {
                                field[k][l] = 0;
                                field[k - 1][l] = 7;
                            }
                            else if (field[k][l] == 6)
                            {
                                field[k][l] = 0;
                                field[k - 1][l] = 8;
                            }
                            else if (field[k][l] == 7)
                            {
                                field[k][l] = 2;
                                field[k - 1][l] = 7;
                            }
                            else if (field[k][l] == 8)
                            {
                                field[k][l] = 2;
                                field[k - 1][l] = 8;
                            }
                        }
                        else if (field[k - 1][l] == 3)
                        {
                            if (field[k][l] == 5)
                            {
                                field[k][l] = 0;
                                field[k - 1][l] = 5;
                            }
                            else if (field[k][l] == 6)
                            {
                                field[k][l] = 0;
                                field[k - 1][l] = 5;
                                item = 0;
                            }
                            else if (field[k][l] == 7)
                            {
                                field[k][l] = 2;
                                field[k - 1][l] = 5;
                            }
                            else if (field[k][l] == 8)
                            {
                                field[k][l] = 2;
                                field[k - 1][l] = 5;
                                item = 0;
                            }
                        }
                        else if (field[k - 1][l] >= 4 && field[k - 1][l] <= 8)
                        {
                            continue;
                        }
                        else if (field[k - 1][l] == 0)
                        {
                            if (field[k][l] == 5)
                            {
                                field[k][l] = 0;
                                field[k - 1][l] = 5;
                            }
                            else if (field[k][l] == 6)
                            {
                                field[k][l] = 0;
                                field[k - 1][l] = 6;
                            }
                            else if (field[k][l] == 7)
                            {
                                field[k][l] = 2;
                                field[k - 1][l] = 5;
                            }
                            else if (field[k][l] == 8)
                            {
                                field[k][l] = 2;
                                field[k - 1][l] = 6;
                            }
                        }
                    }
                    else if (move_enemy == 1)
                    { // 적 아래로 이동
                        if (field[k + 1][l] == 1)
                        {
                            if (field[k][l] == 5)
                            {
                                field[k][l] = 0;
                                field[k + 1][l] = 5;
                                game_over = 1;
                                break;
                            }
                            else if (field[k][l] == 6)
                            {
                                field[k][l] = 0;
                            }
                            else if (field[k][l] == 7)
                            {
                                field[k][l] = 2;
                                field[k + 1][l] = 5;
                                game_over = 1;
                                break;
                            }
                            else if (field[k][l] == 8)
                            {
                                field[k][l] = 2;
                            }
                        }
                        else if (field[k + 1][l] == 2)
                        {
                            if (field[k][l] == 5)
                            {
                                field[k][l] = 0;
                                field[k + 1][l] = 7;
                            }
                            else if (field[k][l] == 6)
                            {
                                field[k][l] = 0;
                                field[k + 1][l] = 8;
                            }
                            else if (field[k][l] == 7)
                            {
                                field[k][l] = 2;
                                field[k + 1][l] = 7;
                            }
                            else if (field[k][l] == 8)
                            {
                                field[k][l] = 2;
                                field[k + 1][l] = 8;
                            }
                        }
                        else if (field[k + 1][l] == 3)
                        {
                            if (field[k][l] == 5)
                            {
                                field[k][l] = 0;
                                field[k + 1][l] = 5;
                            }
                            else if (field[k][l] == 6)
                            {
                                field[k][l] = 0;
                                field[k + 1][l] = 5;
                                item = 0;
                            }
                            else if (field[k][l] == 7)
                            {
                                field[k][l] = 2;
                                field[k + 1][l] = 5;
                            }
                            else if (field[k][l] == 8)
                            {
                                field[k][l] = 2;
                                field[k + 1][l] = 5;
                                item = 0;
                            }
                        }
                        else if (field[k + 1][l] >= 4 && field[k + 1][l] <= 8)
                        {
                            continue;
                        }
                        else if (field[k + 1][l] == 0)
                        {
                            if (field[k][l] == 5)
                            {
                                field[k][l] = 0;
                                field[k + 1][l] = 5;
                            }
                            else if (field[k][l] == 6)
                            {
                                field[k][l] = 0;
                                field[k + 1][l] = 6;
                            }
                            else if (field[k][l] == 7)
                            {
                                field[k][l] = 2;
                                field[k + 1][l] = 5;
                            }
                            else if (field[k][l] == 8)
                            {
                                field[k][l] = 2;
                                field[k + 1][l] = 6;
                            }
                        }
                    }
                    else if (move_enemy == 2)
                    { // 적 좌로 이동
                        if (field[k][l - 1] == 1)
                        {
                            if (field[k][l] == 5)
                            {
                                field[k][l] = 0;
                                field[k][l - 1] = 5;
                                game_over = 1;
                                break;
                            }
                            else if (field[k][l] == 6)
                            {
                                field[k][l] = 0;
                            }
                            else if (field[k][l] == 7)
                            {
                                field[k][l] = 2;
                                field[k][l - 1] = 5;
                                game_over = 1;
                                break;
                            }
                            else if (field[k][l] == 8)
                            {
                                field[k][l] = 2;
                            }
                        }
                        else if (field[k][l - 1] == 2)
                        {
                            if (field[k][l] == 5)
                            {
                                field[k][l] = 0;
                                field[k][l - 1] = 7;
                            }
                            else if (field[k][l] == 6)
                            {
                                field[k][l] = 0;
                                field[k][l - 1] = 8;
                            }
                            else if (field[k][l] == 7)
                            {
                                field[k][l] = 2;
                                field[k][l - 1] = 7;
                            }
                            else if (field[k][l] == 8)
                            {
                                field[k][l] = 2;
                                field[k][l - 1] = 8;
                            }
                        }
                        else if (field[k][l - 1] == 3)
                        {
                            if (field[k][l] == 5)
                            {
                                field[k][l] = 0;
                                field[k][l - 1] = 5;
                            }
                            else if (field[k][l] == 6)
                            {
                                field[k][l] = 0;
                                field[k][l - 1] = 5;
                                item = 0;
                            }
                            else if (field[k][l] == 7)
                            {
                                field[k][l] = 2;
                                field[k][l - 1] = 5;
                            }
                            else if (field[k][l] == 8)
                            {
                                field[k][l] = 2;
                                field[k][l - 1] = 5;
                                item = 0;
                            }
                        }
                        else if (field[k][l - 1] >= 4 && field[k][l - 1] <= 8)
                        {
                            continue;
                        }
                        else if (field[k][l - 1] == 0)
                        {
                            if (field[k][l] == 5)
                            {
                                field[k][l] = 0;
                                field[k][l - 1] = 5;
                            }
                            else if (field[k][l] == 6)
                            {
                                field[k][l] = 0;
                                field[k][l - 1] = 6;
                            }
                            else if (field[k][l] == 7)
                            {
                                field[k][l] = 2;
                                field[k][l - 1] = 5;
                            }
                            else if (field[k][l] == 8)
                            {
                                field[k][l] = 2;
                                field[k][l - 1] = 6;
                            }
                        }
                    }
                    else if (move_enemy == 3)
                    { // 적 우로 이동
                        if (field[k][l + 1] == 1)
                        {
                            if (field[k][l] == 5)
                            {
                                field[k][l] = 0;
                                field[k][l + 1] = 5;
                                game_over = 1;
                                break;
                            }
                            else if (field[k][l] == 6)
                            {
                                field[k][l] = 0;
                            }
                            else if (field[k][l] == 7)
                            {
                                field[k][l] = 2;
                                field[k][l + 1] = 5;
                                game_over = 1;
                                break;
                            }
                            else if (field[k][l] == 8)
                            {
                                field[k][l] = 2;
                            }
                        }
                        else if (field[k][l + 1] == 2)
                        {
                            if (field[k][l] == 5)
                            {
                                field[k][l] = 0;
                                field[k][l + 1] = 7;
                            }
                            else if (field[k][l] == 6)
                            {
                                field[k][l] = 0;
                                field[k][l + 1] = 8;
                            }
                            else if (field[k][l] == 7)
                            {
                                field[k][l] = 2;
                                field[k][l + 1] = 7;
                            }
                            else if (field[k][l] == 8)
                            {
                                field[k][l] = 2;
                                field[k][l + 1] = 8;
                            }
                        }
                        else if (field[k][l + 1] == 3)
                        {
                            if (field[k][l] == 5)
                            {
                                field[k][l] = 0;
                                field[k][l + 1] = 5;
                            }
                            else if (field[k][l] == 6)
                            {
                                field[k][l] = 0;
                                field[k][l + 1] = 5;
                                item = 0;
                            }
                            else if (field[k][l] == 7)
                            {
                                field[k][l] = 2;
                                field[k][l + 1] = 5;
                            }
                            else if (field[k][l] == 8)
                            {
                                field[k][l] = 2;
                                field[k][l + 1] = 5;
                                item = 0;
                            }
                        }
                        else if (field[k][l + 1] >= 4 && field[k][l + 1] <= 8)
                        {
                            continue;
                        }
                        else if (field[k][l + 1] == 0)
                        {
                            if (field[k][l] == 5)
                            {
                                field[k][l] = 0;
                                field[k][l + 1] = 5;
                            }
                            else if (field[k][l] == 6)
                            {
                                field[k][l] = 0;
                                field[k][l + 1] = 6;
                            }
                            else if (field[k][l] == 7)
                            {
                                field[k][l] = 2;
                                field[k][l + 1] = 5;
                            }
                            else if (field[k][l] == 8)
                            {
                                field[k][l] = 2;
                                field[k][l + 1] = 6;
                            }
                        }
                    }
                    break;
                }

                if (item > 0) // 아이템 지속시간 감소 및 아이템 효과 해제
                {
                    item--;
                }
                else
                {
                    for (i = 0; i < 10; i++)
                    {
                        for (j = 0; j < 15; j++)
                        {
                            if (field[i][j] == 6)
                            {
                                field[i][j] = 5;
                            }
                            else if (field[i][j] == 8)
                            {
                                field[i][j] = 7;
                            }
                        }
                    }
                }

                turn++;
                if (food == 54) // 클리어 시 클리어 화면 출력
                {
                    system("clear");
                    clr = 1;
                    printf("Clear! \n");
                    printf("클리어하는데 걸린 턴 수:%d \n", turn);
                    break;
                }
            }
            if (clr == 1)
            {
                break;
            }

            if (game_over == 1)
            {
                break;
            }
        }

        if (game_over == 1) // 게임오버 시 메인화면으로 돌아감
        {
            *ad += 1;
            break;
        }

        if (clr == 1) // 클리어 시 게임 종료
        {
            victory++;
            *ad += 1;
            break;
        }
    }
    return victory;
}

int Game3(int victory, int *ad) // 애니팡
{
    srand((unsigned int)time(NULL));
    int scr[10][10]; // 게임화면
    /*
    1~5 색깔블록
    6 아아템
    */
    int title; // 타이틀화면
    int a, b;  // 좌표 입력
    char mv;   // 교체할 방향 입력
    int mix;   // 블록 섞기
    int temp;
    int i, j, k, l;

    while (1)
    {
        system("clear");
        int score = 0; // 점수 100점 달성 시 클리어

        printf("1.게임시작 2.종료 \n");
        scanf("%d", &title);

        if (title == 1)
        {
        }
        else if (title == 2)
        {
            break;
        }
        else
        {
            continue;
        }

        while (1) // 3블록 연속인 블록 제거
        {
            int dup = 0;

            for (i = 0; i < 10; i++)
            {
                for (j = 0; j < 10; j++)
                {
                    mix = (rand() % 5) + 1;

                    if (mix == 1)
                    {
                        scr[i][j] = 1;
                    }
                    else if (mix == 2)
                    {
                        scr[i][j] = 2;
                    }
                    else if (mix == 3)
                    {
                        scr[i][j] = 3;
                    }
                    else if (mix == 4)
                    {
                        scr[i][j] = 4;
                    }
                    else if (mix == 5)
                    {
                        scr[i][j] = 5;
                    }
                }
            }

            for (i = 0; i < 8; i++)
            {
                for (j = 0; j < 10; j++)
                {
                    if (scr[i][j] == scr[i + 1][j] && scr[i + 1][j] == scr[i + 2][j])
                    {
                        dup = 1;
                        break;
                    }
                }

                if (dup == 1)
                {
                    break;
                }
            }

            if (dup == 1)
            {
                continue;
            }

            for (i = 0; i < 10; i++)
            {
                for (j = 0; j < 8; j++)
                {
                    if (scr[i][j] == scr[i][j + 1] && scr[i][j + 1] == scr[i][j + 2])
                    {
                        dup = 2;
                        break;
                    }
                }

                if (dup == 2)
                {
                    break;
                }
            }

            if (dup == 2)
            {
                continue;
            }
            else
            {
                break;
            }
        }

        while (1)
        {
            system("clear");
            int chg[10][10] = {
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0}}; // 블록 교체 및 스코어링 하기 위한 배열

            int cvr = 0; // 3연속 블록을 하나도 발견하지 못했을 때 되돌리기 위한 변수
            int chk = 0; // 움직일 블록이 있는지 체크

            for (i = 0; i < 8; i++) // 움직일 블록이 있는지 체크(세로)
            {
                for (j = 0; j < 10; j++)
                {
                    if (j >= 1 && j <= 8)
                    {
                        if (scr[i][j] == scr[i + 1][j] && scr[i][j] == scr[i + 2][j + 1])
                        {
                            chk = 1;
                        }
                        else if (scr[i][j] == scr[i + 1][j] && scr[i][j] == scr[i + 2][j - 1])
                        {
                            chk = 1;
                        }
                        else if (scr[i][j] == scr[i + 1][j + 1] && scr[i][j] == scr[i + 2][j + 1])
                        {
                            chk = 1;
                        }
                        else if (scr[i][j] == scr[i + 1][j - 1] && scr[i][j] == scr[i + 2][j - 1])
                        {
                            chk = 1;
                        }
                        else if (scr[i][j] == scr[i + 1][j + 1] && scr[i][j] == scr[i + 2][j])
                        {
                            chk = 1;
                        }
                        else if (scr[i][j] == scr[i + 1][j - 1] && scr[i][j] == scr[i + 2][j])
                        {
                            chk = 1;
                        }
                    }
                    else if (j == 0)
                    {
                        if (scr[i][j] == scr[i + 1][j] && scr[i][j] == scr[i + 2][j + 1])
                        {
                            chk = 1;
                        }
                        else if (scr[i][j] == scr[i + 1][j + 1] && scr[i][j] == scr[i + 2][j + 1])
                        {
                            chk = 1;
                        }
                        else if (scr[i][j] == scr[i + 1][j + 1] && scr[i][j] == scr[i + 2][j])
                        {
                            chk = 1;
                        }
                    }
                    else if (j == 9)
                    {
                        if (scr[i][j] == scr[i + 1][j] && scr[i][j] == scr[i + 2][j - 1])
                        {
                            chk = 1;
                        }
                        else if (scr[i][j] == scr[i + 1][j - 1] && scr[i][j] == scr[i + 2][j - 1])
                        {
                            chk = 1;
                        }
                        else if (scr[i][j] == scr[i + 1][j - 1] && scr[i][j] == scr[i + 2][j])
                        {
                            chk = 1;
                        }
                    }
                }
            }

            for (i = 0; i < 10; i++) // 움직일 블록이 있는지 체크(가로)
            {
                for (j = 0; j < 8; j++)
                {
                    if (i >= 1 && i <= 8)
                    {
                        if (scr[i][j] == scr[i][j + 1] && scr[i][j] == scr[i + 1][j + 2])
                        {
                            chk = 1;
                        }
                        else if (scr[i][j] == scr[i][j + 1] && scr[i][j] == scr[i - 1][j + 2])
                        {
                            chk = 1;
                        }
                        else if (scr[i][j] == scr[i + 1][j + 1] && scr[i][j] == scr[i + 1][j + 2])
                        {
                            chk = 1;
                        }
                        else if (scr[i][j] == scr[i - 1][j + 1] && scr[i][j] == scr[i - 1][j + 2])
                        {
                            chk = 1;
                        }
                        else if (scr[i][j] == scr[i + 1][j + 1] && scr[i][j] == scr[i][j + 2])
                        {
                            chk = 1;
                        }
                        else if (scr[i][j] == scr[i - 1][j + 1] && scr[i][j] == scr[i][j + 2])
                        {
                            chk = 1;
                        }
                    }
                    else if (i == 0)
                    {
                        if (scr[i][j] == scr[i][j + 1] && scr[i][j] == scr[i + 1][j + 2])
                        {
                            chk = 1;
                        }
                        else if (scr[i][j] == scr[i + 1][j + 1] && scr[i][j] == scr[i + 1][j + 2])
                        {
                            chk = 1;
                        }
                        else if (scr[i][j] == scr[i + 1][j + 1] && scr[i][j] == scr[i][j + 2])
                        {
                            chk = 1;
                        }
                    }
                    else if (i == 9)
                    {
                        if (scr[i][j] == scr[i][j + 1] && scr[i][j] == scr[i - 1][j + 2])
                        {
                            chk = 1;
                        }
                        else if (scr[i][j] == scr[i - 1][j + 1] && scr[i][j] == scr[i - 1][j + 2])
                        {
                            chk = 1;
                        }
                        else if (scr[i][j] == scr[i - 1][j + 1] && scr[i][j] == scr[i][j + 2])
                        {
                            chk = 1;
                        }
                    }
                }
            }

            if (chk == 0) // 움직일 블록이 없으면 반복문 탈출
            {
                printf("움직일 블록이 없습니다 \n");
                // sleep(1); 리눅스로 옮길 때 주석 해제
                break;
            }

            // system("clear"); 리눅스로 옮길 때 주석 해제

            printf("점수:%d \n", score);

            for (i = 0; i < 10; i++) // 게임화면 출력
            {
                for (j = 0; j < 10; j++)
                {
                    if (scr[i][j] == 1)
                    {
                        printf("🟥 ");
                    }
                    else if (scr[i][j] == 2)
                    {
                        printf("🟨 ");
                    }
                    else if (scr[i][j] == 3)
                    {
                        printf("🟩 ");
                    }
                    else if (scr[i][j] == 4)
                    {
                        printf("🟦 ");
                    }
                    else if (scr[i][j] == 5)
                    {
                        printf("🟪 ");
                    }
                    else if (scr[i][j] == 6)
                    {
                        printf("🔳 ");
                    }
                }
                printf("\n");
            }

            printf("좌표 입력: "); // 0~9까지 입력
            scanf("%d %d", &a, &b);

            if (a < 0 || a > 9 || b < 0 || b > 9)
            {
                continue;
            }

            printf("이동 방향 입력: "); // 방향입력 wasd, 아이템 i
            scanf(" %c", &mv);

            if (mv == 'w')
            {
                if (a <= 0)
                {
                    continue;
                }
                temp = scr[a][b];
                scr[a][b] = scr[a - 1][b];
                scr[a - 1][b] = temp;
            }
            else if (mv == 's')
            {
                if (a >= 9)
                {
                    continue;
                }
                temp = scr[a][b];
                scr[a][b] = scr[a + 1][b];
                scr[a + 1][b] = temp;
            }
            else if (mv == 'a')
            {
                if (b <= 0)
                {
                    continue;
                }
                temp = scr[a][b];
                scr[a][b] = scr[a][b - 1];
                scr[a][b - 1] = temp;
            }
            else if (mv == 'd')
            {
                if (b >= 9)
                {
                    continue;
                }
                temp = scr[a][b];
                scr[a][b] = scr[a][b + 1];
                scr[a][b + 1] = temp;
            }
            else if (mv == 'i')
            { // 폭탄 구현
                if (scr[a][b] == 6)
                {
                    cvr = 1;

                    if (a == 0)
                    {
                        if (b == 0)
                        {
                            chg[a][b] = 1;
                            chg[a + 1][b] = 1;
                            chg[a][b + 1] = 1;
                            chg[a + 1][b + 1] = 1;
                        }
                        else if (b == 9)
                        {
                            chg[a][b] = 1;
                            chg[a + 1][b] = 1;
                            chg[a][b - 1] = 1;
                            chg[a + 1][b - 1] = 1;
                        }
                        else
                        {
                            chg[a][b] = 1;
                            chg[a + 1][b] = 1;
                            chg[a][b + 1] = 1;
                            chg[a][b - 1] = 1;
                            chg[a + 1][b - 1] = 1;
                            chg[a + 1][b + 1] = 1;
                        }
                    }
                    else if (a == 9)
                    {
                        if (b == 0)
                        {
                            chg[a][b] = 1;
                            chg[a - 1][b] = 1;
                            chg[a][b + 1] = 1;
                            chg[a - 1][b + 1] = 1;
                        }
                        else if (b == 9)
                        {
                            chg[a][b] = 1;
                            chg[a - 1][b] = 1;
                            chg[a][b - 1] = 1;
                            chg[a - 1][b - 1] = 1;
                        }
                        else
                        {
                            chg[a][b] = 1;
                            chg[a - 1][b] = 1;
                            chg[a][b + 1] = 1;
                            chg[a][b - 1] = 1;
                            chg[a - 1][b - 1] = 1;
                            chg[a - 1][b + 1] = 1;
                        }
                    }
                    else if (b == 0)
                    {
                        chg[a][b] = 1;
                        chg[a - 1][b] = 1;
                        chg[a + 1][b] = 1;
                        chg[a][b + 1] = 1;
                        chg[a - 1][b + 1] = 1;
                        chg[a + 1][b + 1] = 1;
                    }
                    else if (b == 9)
                    {
                        chg[a][b] = 1;
                        chg[a - 1][b] = 1;
                        chg[a + 1][b] = 1;
                        chg[a][b - 1] = 1;
                        chg[a - 1][b - 1] = 1;
                        chg[a + 1][b - 1] = 1;
                    }
                    else
                    {
                        chg[a][b] = 1;
                        chg[a - 1][b] = 1;
                        chg[a + 1][b] = 1;
                        chg[a][b - 1] = 1;
                        chg[a][b + 1] = 1;
                        chg[a - 1][b - 1] = 1;
                        chg[a - 1][b + 1] = 1;
                        chg[a + 1][b - 1] = 1;
                        chg[a + 1][b + 1] = 1;
                    }
                }
                else
                {
                    continue;
                }
            }
            else
            {
                continue;
            }

            for (i = 0; i < 8; i++) // 세로 검사
            {
                for (j = 0; j < 10; j++)
                {
                    if (scr[i][j] == scr[i + 1][j] && scr[i][j] == scr[i + 2][j])
                    {
                        cvr = 1;

                        chg[i][j] = 1;
                        chg[i + 1][j] = 1;
                        chg[i + 2][j] = 1;
                    }
                }
            }

            for (i = 0; i < 10; i++) // 가로 검사
            {
                for (j = 0; j < 8; j++)
                {
                    if (scr[i][j] == scr[i][j + 1] && scr[i][j] == scr[i][j + 2])
                    {
                        cvr = 1;

                        chg[i][j] = 1;
                        chg[i][j + 1] = 1;
                        chg[i][j + 2] = 1;
                    }
                }
            }

            if (cvr == 0) // 3연속 블록을 발견하지 못하면 다시 원위치 시켜주고 돌아감
            {
                if (mv == 'w')
                {
                    temp = scr[a][b];
                    scr[a][b] = scr[a - 1][b];
                    scr[a - 1][b] = temp;
                }
                else if (mv == 's')
                {
                    temp = scr[a][b];
                    scr[a][b] = scr[a + 1][b];
                    scr[a + 1][b] = temp;
                }
                else if (mv == 'a')
                {
                    temp = scr[a][b];
                    scr[a][b] = scr[a][b - 1];
                    scr[a][b - 1] = temp;
                }
                else if (mv == 'd')
                {
                    temp = scr[a][b];
                    scr[a][b] = scr[a][b + 1];
                    scr[a][b + 1] = temp;
                }
                continue;
            }

            for (i = 0; i < 10; i++) // 스코어링
            {
                for (j = 0; j < 10; j++)
                {
                    if (chg[i][j] == 1)
                    {
                        score++;
                    }
                }
            }

            while (1) // 빈자리 채우기
            {
                int dup0 = 0;

                for (i = 0; i < 10; i++)
                {
                    for (j = 0; j < 10; j++)
                    {
                        if (chg[i][j] == 1)
                        {
                            mix = (rand() % 20) + 1;

                            if (mix == 1) // 5프로 확률로 아이템 등장
                            {
                                scr[i][j] = 6;
                            }
                            else
                            {
                                mix = (rand() % 5) + 1;

                                if (mix == 1)
                                {
                                    scr[i][j] = 1;
                                }
                                else if (mix == 2)
                                {
                                    scr[i][j] = 2;
                                }
                                else if (mix == 3)
                                {
                                    scr[i][j] = 3;
                                }
                                else if (mix == 4)
                                {
                                    scr[i][j] = 4;
                                }
                                else if (mix == 5)
                                {
                                    scr[i][j] = 5;
                                }
                            }
                        }
                    }
                }

                for (i = 0; i < 8; i++) // 채워진 블록으로 인해 3연속 블록이 됐다면 돌아가서 다시 채워줌
                {
                    for (j = 0; j < 10; j++)
                    {
                        if (scr[i][j] == scr[i + 1][j] && scr[i + 1][j] == scr[i + 2][j])
                        {
                            dup0 = 1;
                            break;
                        }
                    }

                    if (dup0 == 1)
                    {
                        break;
                    }
                }

                if (dup0 == 1)
                {
                    continue;
                }

                for (i = 0; i < 10; i++)
                {
                    for (j = 0; j < 8; j++)
                    {
                        if (scr[i][j] == scr[i][j + 1] && scr[i][j + 1] == scr[i][j + 2])
                        {
                            dup0 = 2;
                            break;
                        }
                    }

                    if (dup0 == 2)
                    {
                        break;
                    }
                }

                if (dup0 == 2)
                {
                    continue;
                }
                else
                {
                    break;
                }
            }

            if (score >= 100)
            {
                printf("Clear! \n");
                victory++;
                *ad += 1;
                break;
            }
        }

        if (score >= 100) // 이건 뭐지?
        {
            break;
        }
    }
    return victory;
}

int Game4(int victory, int *ad) // 지뢰 찾기
{
    int map[11][11] = {0};
    int boom = 0;       // 근처 지뢰 탐색용 근처 지뢰 몇개 인지
    int x, y;           // x,y 좌표 반복문
    int i, j, r = 1;    // 반복문 돌리는 그냥 변수
    int life = 1;       // 목숨 1 시작
    int item = 2;       // 숨겨진 아이템 개수
    int btn;            // 선택용 변수
    int vp = 0;         // 밝혀지지 않은 부분 파악을 위한 변수, 클리어를 위한 변수
    int count = 0;      // 임시 숫자 파악용
    int heart[3] = {7}; // 목숨 표시용

    srand((unsigned int)time(NULL)); // 난수
    for (i = 0; i < 10; i++)         // 배열 맵에 지뢰 생성!
    {
        system("clear");
        x = (rand() % 10) + 1; // 1~10
        y = (rand() % 10) + 1; // 1~10
        if (map[x][y] == 9)    // x,y 자리에 지뢰가 배치 되었다면 다시 반복!
        {
            i--;      // 증가된 i값 을 낮춰 중복 제외 10회 돌게 만듬
            continue; // 아래 내용 무시 반복
        }
        map[x][y] = 9; // 지뢰 표시
    }
    for (x = 1; x < 11; x++) // (0,0)라인 제외 배열 탐색 후 각 좌표에 지뢰가 근처에 몇개가 있는지 표시
    {
        for (y = 1; y < 11; y++)
        {
            if (map[x][y] != 9) // 지뢰가 아니면 근처에 탐색 하러
            {
                boom = 0;
                if ((x == 1) && (y == 1)) // <1-1> 왼쪽위 꼭지점 4번 탐색
                {
                    for (i = 0; i < 2; i++)
                    {
                        for (j = 0; j < 2; j++)
                        {
                            if (map[x + i][y + j] == 9)
                            {
                                boom++;
                            }
                        }
                    }
                }
                else if ((x == 10) && (y == 1)) // <1-2> 왼쪽아래 꼭지점 4번 탐색
                {
                    for (i = 0; i < 2; i++)
                    {
                        for (j = 0; j < 2; j++)
                        {
                            if (map[(x - 1) + i][y + j] == 9)
                            {
                                boom++;
                            }
                        }
                    }
                }
                else if ((x == 1) && (y == 10)) // <1-3> 오른쪽위 꼭지점 4번 탐색
                {
                    for (i = 0; i < 2; i++)
                    {
                        for (j = 0; j < 2; j++)
                        {
                            if (map[x + i][(y - 1) + j] == 9)
                            {
                                boom++;
                            }
                        }
                    }
                }
                else if ((x == 10) && (y == 10)) // <1-4> 오른쪽아래 꼭지점 4번 탐색
                {
                    for (i = 0; i < 2; i++)
                    {
                        for (j = 0; j < 2; j++)
                        {
                            if (map[(x - 1) + i][(y - 1) + j] == 9)
                            {
                                boom++;
                            }
                        }
                    }
                }
                else if ((1 < x < 10) && (y == 1)) // <2-1> 왼쪽 끝에 사이 6번 탐색
                {
                    for (i = 0; i < 3; i++)
                    {
                        for (j = 0; j < 2; j++)
                        {
                            if (map[(x - 1) + i][y + j] == 9)
                            {
                                boom++;
                            }
                        }
                    }
                }
                else if ((1 < x < 10) && (y == 10)) // <2-2> 오른쪽 끝에 사이 6번 탐색
                {
                    for (i = 0; i < 3; i++)
                    {
                        for (j = 0; j < 2; j++)
                        {
                            if (map[(x - 1) + i][(y - 1) + j] == 9)
                            {
                                boom++;
                            }
                        }
                    }
                }
                else if ((x == 1) && (1 < y < 10)) // <2-3> 위쪽 끝에 사이 6번 탐색
                {
                    for (i = 0; i < 2; i++)
                    {
                        for (j = 0; j < 3; j++)
                        {
                            if (map[x + i][(y - 1) + j] == 9)
                            {
                                boom++;
                            }
                        }
                    }
                }
                else if ((x == 10) && (1 < y < 10)) // <2-4> 아래쪽 끝에 사이 6번 탐색
                {
                    for (i = 0; i < 2; i++)
                    {
                        for (j = 0; j < 3; j++)
                        {
                            if (map[(x - 1) + i][(y - 1) + j] == 9)
                            {
                                boom++;
                            }
                        }
                    }
                }
                else if ((1 < x < 10) && (1 < y < 10)) // <3-1> 나머지 9번 탐색
                {
                    for (i = 0; i < 3; i++)
                    {
                        for (j = 0; j < 3; j++)
                        {
                            if (map[(x - 1) + i][(y - 1) + j] == 9)
                            {
                                boom++;
                            }
                        }
                    }
                }
                map[x][y] = boom;
            }
        }
    }
    for (i = 0; i < 2; i++) // 배열 맵에 목숨 아이템 생성!
    {
        system("clear");
        x = (rand() % 10) + 1; // 1~10
        y = (rand() % 10) + 1; // 1~10
        if (map[x][y] == 9)    // x,y 자리에 지뢰가 배치 되었다면 다시 반복!
        {
            i--;      // 증가된 i값 을 낮춰 중복 제외 10회 돌게 만듬
            continue; // 아래 내용 무시 반복
        }
        map[x][y] = 37; // 목숨 아이템 표시
    }
    while (r) // 본격 게임 화면
    {
        system("clear");
        for (i = 0; i < 11; i++)
        {
            for (j = 0; j < 11; j++)
            {
                if (i == 0 && j == 0)
                {
                    map[0][0] = 99;
                    printf(" 😺 "); // 춘식이
                }
                else if (i == 0)
                {
                    map[0][j] = j;
                    printf(" %2d", j);
                }
                else if (j == 0)
                {
                    map[i][0] = i;
                    printf(" %2d ", i);
                }
                // 그냥 풀, 지뢰, 지뢰 근처
                else if (map[i][j] == 9 || map[i][j] == 0 || map[i][j] == 1 || map[i][j] == 2 || map[i][j] == 3 || map[i][j] == 4 || map[i][j] == 5 || map[i][j] == 6 || map[i][j] == 7 || map[i][j] == 8 || map[i][j] == 37)
                {
                    printf(" ● "); // 풀숲
                }
                else if (map[i][j] == 10)
                {
                    printf(" ○ "); // 아무것도 없음
                }
                else if (map[i][j] == 11)
                {
                    printf(" ① "); // 근처에 지뢰가?! 1개가 있다!
                }
                else if (map[i][j] == 12)
                {
                    printf(" ② "); // 근처에 지뢰가?! 2개가 있다!
                }
                else if (map[i][j] == 13)
                {
                    printf(" ③ "); // 근처에 지뢰가?! 3개가 있다!
                }
                else if (map[i][j] == 14)
                {
                    printf(" ④ "); // 근처에 지뢰가?! 4개가 있다!
                }
                else if (map[i][j] == 15)
                {
                    printf(" ⑤ "); // 근처에 지뢰가?! 5개가 있다!
                }
                else if (map[i][j] == 16)
                {
                    printf(" ⑥ "); // 근처에 지뢰가?! 6개가 있다!
                }
                else if (map[i][j] == 17)
                {
                    printf(" ⑦ "); // 근처에 지뢰가?! 7개가 있다!
                }
                else if (map[i][j] == 18)
                {
                    printf(" ⑧ "); // 근처에 지뢰가?! 8개가 있다!
                }
                else if (map[i][j] == 20 || map[i][j] == 21 || map[i][j] == 22 || map[i][j] == 23 || map[i][j] == 24 || map[i][j] == 25 || map[i][j] == 26 || map[i][j] == 27 || map[i][j] == 28 || map[i][j] == 29 || map[i][j] == 47)
                {
                    printf(" ❗"); // 지뢰가 있을 것 같다!
                }
                else if (map[i][j] == 19)
                {
                    printf(" 💣"); // 지뢰 발견!
                }
                else if (map[i][j] == 77)
                {
                    printf(" ❤️ "); // 아이템 발견!
                }
            }
            printf("\n");
        }
        printf("\n");
        if (r == 1) // 클리어 조건 출력 1턴만
        {
            printf(" @클리어 조건@ \n");
            printf("  아이템을 모두 먹고 \n");
            printf("  지뢰지역을 표시한다음 \n");
            printf("  맵을 모두 밝혀라! \n");
            printf("\n");
            r++;
        }
        if (vp == 100)
        {
            printf("   클리어!!! \n");
            victory++;
            *ad += 1;
            usleep(1000000);
            break;
        }
        else if (vp < 100)
        {
            for (i = 1; i <= 10; i++)
            {
                for (j = 1; j <= 10; j++)
                {
                    if (map[i][j] == 29 || map[i][j] == 10 || map[i][j] == 11 || map[i][j] == 12 || map[i][j] == 13 || map[i][j] == 14 || map[i][j] == 15 || map[i][j] == 16 || map[i][j] == 17 || map[i][j] == 18 || map[i][j] == 19 || map[i][j] == 77)
                    {
                        vp++;
                        if (vp == 100)
                        {
                            printf("춘식이가 모든 맵을 밝혔다! \n");
                        }
                    }
                }
            }
        }
        if (life > 0)
        {
            printf("목숨 > ");
            for (i = 0; i < 3; i++)
            {
                if (heart[i] == 7)
                    printf(" ❤️ ");
            }
            printf("\n");
            printf("숨겨진 아이템 개수 > %d개 \n", item);
            printf("1. 탐색할 위치 지정, 2. 지뢰지역 표시하기 \n");
            printf("선택 > ");
            scanf("%d", &btn);
            getchar();
            if (btn == 1)
            {
                printf("춘식이가 탐색할 곳 좌표(왼쪽,위 번호 순서로) \n");
                printf("입력 > ");
                scanf("%d %d", &x, &y);
                getchar();
                if (map[x][y] == 10 || map[x][y] == 11 || map[x][y] == 12 || map[x][y] == 13 || map[x][y] == 14 || map[x][y] == 15 || map[x][y] == 16 || map[x][y] == 17 || map[x][y] == 18 || map[x][y] == 77)
                {
                    printf("이미 탐색한 곳입니다. 다시! \n");
                    usleep(1000000);
                    continue;
                }
                else if (map[x][y] == 9)
                {
                    printf("춘식이가 지뢰를 밟아서 터져버렸다! \n");
                    sleep(1);
                    map[x][y] = 19;
                    if (life > 0)
                    {
                        for (i = 2; i >= 0; i--)
                        {
                            if (heart[i] == 7)
                            {
                                heart[i] = 0;
                                break;
                            }
                        }
                    }
                    life--;
                    if (life == 0)
                    {
                        printf("춘식이의 목숨이 모두 사라졌다... \n");
                    }
                }
                else if (map[x][y] == 37)
                {
                    printf("춘식이가 아이템을 발견했다! \n");
                    sleep(1);
                    map[x][y] = 77;
                    for (i = 0; i < 3; i++)
                    {
                        if (heart[i] == 0)
                        {
                            heart[i] = 7;
                            break;
                        }
                    }
                    life++;
                    item--;
                }
                else if ((1 <= x <= 10) || (1 <= y <= 10)) // 탐색
                {
                    if ((x == 1) && (y == 1)) // <1-1> 왼쪽위 꼭지점 4번 탐색
                    {
                        for (i = 0; i < 2; i++)
                        {
                            for (j = 0; j < 2; j++)
                            {
                                if (map[x + i][y + j] == 0)
                                {
                                    map[x + i][y + j] = 10;
                                }
                                else if (map[x + i][y + j] == 1)
                                {
                                    map[x + i][y + j] = 11;
                                }
                                else if (map[x + i][y + j] == 2)
                                {
                                    map[x + i][y + j] = 12;
                                }
                                else if (map[x + i][y + j] == 3)
                                {
                                    map[x + i][y + j] = 13;
                                }
                                else if (map[x + i][y + j] == 4)
                                {
                                    map[x + i][y + j] = 14;
                                }
                                else if (map[x + i][y + j] == 5)
                                {
                                    map[x + i][y + j] = 15;
                                }
                                else if (map[x + i][y + j] == 6)
                                {
                                    map[x + i][y + j] = 16;
                                }
                                else if (map[x + i][y + j] == 7)
                                {
                                    map[x + i][y + j] = 17;
                                }
                                else if (map[x + i][y + j] == 8)
                                {
                                    map[x + i][y + j] = 18;
                                }
                            }
                        }
                    }
                    else if ((x == 10) && (y == 1)) // <1-2> 왼쪽아래 꼭지점 4번 탐색
                    {
                        for (i = 0; i < 2; i++)
                        {
                            for (j = 0; j < 2; j++)
                            {
                                if (map[(x - 1) + i][y + j] == 0)
                                {
                                    map[(x - 1) + i][y + j] = 10;
                                }
                                else if (map[(x - 1) + i][y + j] == 1)
                                {
                                    map[(x - 1) + i][y + j] = 11;
                                }
                                else if (map[(x - 1) + i][y + j] == 2)
                                {
                                    map[(x - 1) + i][y + j] = 12;
                                }
                                else if (map[(x - 1) + i][y + j] == 3)
                                {
                                    map[(x - 1) + i][y + j] = 13;
                                }
                                else if (map[(x - 1) + i][y + j] == 4)
                                {
                                    map[(x - 1) + i][y + j] = 14;
                                }
                                else if (map[(x - 1) + i][y + j] == 5)
                                {
                                    map[(x - 1) + i][y + j] = 15;
                                }
                                else if (map[(x - 1) + i][y + j] == 6)
                                {
                                    map[(x - 1) + i][y + j] = 16;
                                }
                                else if (map[(x - 1) + i][y + j] == 7)
                                {
                                    map[(x - 1) + i][y + j] = 17;
                                }
                                else if (map[(x - 1) + i][y + j] == 8)
                                {
                                    map[(x - 1) + i][y + j] = 18;
                                }
                            }
                        }
                    }
                    else if ((x == 1) && (y == 10)) // <1-3> 오른쪽위 꼭지점 4번 탐색
                    {
                        for (i = 0; i < 2; i++)
                        {
                            for (j = 0; j < 2; j++)
                            {
                                if (map[x + i][(y - 1) + j] == 0)
                                {
                                    map[x + i][(y - 1) + j] = 10;
                                }
                                else if (map[x + i][(y - 1) + j] == 1)
                                {
                                    map[x + i][(y - 1) + j] = 11;
                                }
                                else if (map[x + i][(y - 1) + j] == 2)
                                {
                                    map[x + i][(y - 1) + j] = 12;
                                }
                                else if (map[x + i][(y - 1) + j] == 3)
                                {
                                    map[x + i][(y - 1) + j] = 13;
                                }
                                else if (map[x + i][(y - 1) + j] == 4)
                                {
                                    map[x + i][(y - 1) + j] = 14;
                                }
                                else if (map[x + i][(y - 1) + j] == 5)
                                {
                                    map[x + i][(y - 1) + j] = 15;
                                }
                                else if (map[x + i][(y - 1) + j] == 6)
                                {
                                    map[x + i][(y - 1) + j] = 16;
                                }
                                else if (map[x + i][(y - 1) + j] == 7)
                                {
                                    map[x + i][(y - 1) + j] = 17;
                                }
                                else if (map[x + i][(y - 1) + j] == 8)
                                {
                                    map[x + i][(y - 1) + j] = 18;
                                }
                            }
                        }
                    }
                    else if ((x == 10) && (y == 10)) // <1-4> 오른쪽아래 꼭지점 4번 탐색
                    {
                        for (i = 0; i < 2; i++)
                        {
                            for (j = 0; j < 2; j++)
                            {
                                if (map[(x - 1) + i][(y - 1) + j] == 0)
                                {
                                    map[(x - 1) + i][(y - 1) + j] = 10;
                                }
                                else if (map[(x - 1) + i][(y - 1) + j] == 1)
                                {
                                    map[(x - 1) + i][(y - 1) + j] = 11;
                                }
                                else if (map[(x - 1) + i][(y - 1) + j] == 2)
                                {
                                    map[(x - 1) + i][(y - 1) + j] = 12;
                                }
                                else if (map[(x - 1) + i][(y - 1) + j] == 3)
                                {
                                    map[(x - 1) + i][(y - 1) + j] = 13;
                                }
                                else if (map[(x - 1) + i][(y - 1) + j] == 4)
                                {
                                    map[(x - 1) + i][(y - 1) + j] = 14;
                                }
                                else if (map[(x - 1) + i][(y - 1) + j] == 5)
                                {
                                    map[(x - 1) + i][(y - 1) + j] = 15;
                                }
                                else if (map[(x - 1) + i][(y - 1) + j] == 6)
                                {
                                    map[(x - 1) + i][(y - 1) + j] = 16;
                                }
                                else if (map[(x - 1) + i][(y - 1) + j] == 7)
                                {
                                    map[(x - 1) + i][(y - 1) + j] = 17;
                                }
                                else if (map[(x - 1) + i][(y - 1) + j] == 8)
                                {
                                    map[(x - 1) + i][(y - 1) + j] = 18;
                                }
                            }
                        }
                    }
                    else if ((1 < x < 10) && (y == 1)) // <2-1> 왼쪽 끝에 사이 6번 탐색
                    {
                        for (i = 0; i < 3; i++)
                        {
                            for (j = 0; j < 2; j++)
                            {
                                if (map[(x - 1) + i][y + j] == 0)
                                {
                                    map[(x - 1) + i][y + j] = 10;
                                }
                                else if (map[(x - 1) + i][y + j] == 1)
                                {
                                    map[(x - 1) + i][y + j] = 11;
                                }
                                else if (map[(x - 1) + i][y + j] == 2)
                                {
                                    map[(x - 1) + i][y + j] = 12;
                                }
                                else if (map[(x - 1) + i][y + j] == 3)
                                {
                                    map[(x - 1) + i][y + j] = 13;
                                }
                                else if (map[(x - 1) + i][y + j] == 4)
                                {
                                    map[(x - 1) + i][y + j] = 14;
                                }
                                else if (map[(x - 1) + i][y + j] == 5)
                                {
                                    map[(x - 1) + i][y + j] = 15;
                                }
                                else if (map[(x - 1) + i][y + j] == 6)
                                {
                                    map[(x - 1) + i][y + j] = 16;
                                }
                                else if (map[(x - 1) + i][y + j] == 7)
                                {
                                    map[(x - 1) + i][y + j] = 17;
                                }
                                else if (map[(x - 1) + i][y + j] == 8)
                                {
                                    map[(x - 1) + i][y + j] = 18;
                                }
                            }
                        }
                    }
                    else if ((1 < x < 10) && (y == 10)) // <2-2> 오른쪽 끝에 사이 6번 탐색
                    {
                        for (i = 0; i < 3; i++)
                        {
                            for (j = 0; j < 2; j++)
                            {
                                if (map[(x - 1) + i][(y - 1) + j] == 0)
                                {
                                    map[(x - 1) + i][(y - 1) + j] = 10;
                                }
                                else if (map[(x - 1) + i][(y - 1) + j] == 1)
                                {
                                    map[(x - 1) + i][(y - 1) + j] = 11;
                                }
                                else if (map[(x - 1) + i][(y - 1) + j] == 2)
                                {
                                    map[(x - 1) + i][(y - 1) + j] = 12;
                                }
                                else if (map[(x - 1) + i][(y - 1) + j] == 3)
                                {
                                    map[(x - 1) + i][(y - 1) + j] = 13;
                                }
                                else if (map[(x - 1) + i][(y - 1) + j] == 4)
                                {
                                    map[(x - 1) + i][(y - 1) + j] = 14;
                                }
                                else if (map[(x - 1) + i][(y - 1) + j] == 5)
                                {
                                    map[(x - 1) + i][(y - 1) + j] = 15;
                                }
                                else if (map[(x - 1) + i][(y - 1) + j] == 6)
                                {
                                    map[(x - 1) + i][(y - 1) + j] = 16;
                                }
                                else if (map[(x - 1) + i][(y - 1) + j] == 7)
                                {
                                    map[(x - 1) + i][(y - 1) + j] = 17;
                                }
                                else if (map[(x - 1) + i][(y - 1) + j] == 8)
                                {
                                    map[(x - 1) + i][(y - 1) + j] = 18;
                                }
                            }
                        }
                    }
                    else if ((x == 1) && (1 < y < 10)) // <2-3> 위쪽 끝에 사이 6번 탐색
                    {
                        for (i = 0; i < 2; i++)
                        {
                            for (j = 0; j < 3; j++)
                            {
                                if (map[x + i][(y - 1) + j] == 0)
                                {
                                    map[x + i][(y - 1) + j] = 10;
                                }
                                else if (map[x + i][(y - 1) + j] == 1)
                                {
                                    map[x + i][(y - 1) + j] = 11;
                                }
                                else if (map[x + i][(y - 1) + j] == 2)
                                {
                                    map[x + i][(y - 1) + j] = 12;
                                }
                                else if (map[x + i][(y - 1) + j] == 3)
                                {
                                    map[x + i][(y - 1) + j] = 13;
                                }
                                else if (map[x + i][(y - 1) + j] == 4)
                                {
                                    map[x + i][(y - 1) + j] = 14;
                                }
                                else if (map[x + i][(y - 1) + j] == 5)
                                {
                                    map[x + i][(y - 1) + j] = 15;
                                }
                                else if (map[x + i][(y - 1) + j] == 6)
                                {
                                    map[x + i][(y - 1) + j] = 16;
                                }
                                else if (map[x + i][(y - 1) + j] == 7)
                                {
                                    map[x + i][(y - 1) + j] = 17;
                                }
                                else if (map[x + i][(y - 1) + j] == 8)
                                {
                                    map[x + i][(y - 1) + j] = 18;
                                }
                            }
                        }
                    }
                    else if ((x == 10) && (1 < y < 10)) // <2-4> 아래쪽 끝에 사이 6번 탐색
                    {
                        for (i = 0; i < 2; i++)
                        {
                            for (j = 0; j < 3; j++)
                            {
                                if (map[(x - 1) + i][(y - 1) + j] == 0)
                                {
                                    map[(x - 1) + i][(y - 1) + j] = 10;
                                }
                                else if (map[(x - 1) + i][(y - 1) + j] == 1)
                                {
                                    map[(x - 1) + i][(y - 1) + j] = 11;
                                }
                                else if (map[(x - 1) + i][(y - 1) + j] == 2)
                                {
                                    map[(x - 1) + i][(y - 1) + j] = 12;
                                }
                                else if (map[(x - 1) + i][(y - 1) + j] == 3)
                                {
                                    map[(x - 1) + i][(y - 1) + j] = 13;
                                }
                                else if (map[(x - 1) + i][(y - 1) + j] == 4)
                                {
                                    map[(x - 1) + i][(y - 1) + j] = 14;
                                }
                                else if (map[(x - 1) + i][(y - 1) + j] == 5)
                                {
                                    map[(x - 1) + i][(y - 1) + j] = 15;
                                }
                                else if (map[(x - 1) + i][(y - 1) + j] == 6)
                                {
                                    map[(x - 1) + i][(y - 1) + j] = 16;
                                }
                                else if (map[(x - 1) + i][(y - 1) + j] == 7)
                                {
                                    map[(x - 1) + i][(y - 1) + j] = 17;
                                }
                                else if (map[(x - 1) + i][(y - 1) + j] == 8)
                                {
                                    map[(x - 1) + i][(y - 1) + j] = 18;
                                }
                            }
                        }
                    }
                    else if ((1 < x < 10) && (1 < y < 10)) // <3-1> 나머지 9번 탐색
                    {
                        for (i = 0; i < 3; i++)
                        {
                            for (j = 0; j < 3; j++)
                            {
                                if (map[(x - 1) + i][(y - 1) + j] == 0)
                                {
                                    map[(x - 1) + i][(y - 1) + j] = 10;
                                }
                                else if (map[(x - 1) + i][(y - 1) + j] == 1)
                                {
                                    map[(x - 1) + i][(y - 1) + j] = 11;
                                }
                                else if (map[(x - 1) + i][(y - 1) + j] == 2)
                                {
                                    map[(x - 1) + i][(y - 1) + j] = 12;
                                }
                                else if (map[(x - 1) + i][(y - 1) + j] == 3)
                                {
                                    map[(x - 1) + i][(y - 1) + j] = 13;
                                }
                                else if (map[(x - 1) + i][(y - 1) + j] == 4)
                                {
                                    map[(x - 1) + i][(y - 1) + j] = 14;
                                }
                                else if (map[(x - 1) + i][(y - 1) + j] == 5)
                                {
                                    map[(x - 1) + i][(y - 1) + j] = 15;
                                }
                                else if (map[(x - 1) + i][(y - 1) + j] == 6)
                                {
                                    map[(x - 1) + i][(y - 1) + j] = 16;
                                }
                                else if (map[(x - 1) + i][(y - 1) + j] == 7)
                                {
                                    map[(x - 1) + i][(y - 1) + j] = 17;
                                }
                                else if (map[(x - 1) + i][(y - 1) + j] == 8)
                                {
                                    map[(x - 1) + i][(y - 1) + j] = 18;
                                }
                            }
                        }
                    }
                }
                else
                {
                    printf("잘못 입력 하셨습니다. 처음으로! \n");
                    usleep(1000000);
                    continue;
                }
            }
            else if (btn == 2)
            {
                printf("(이미 표시 한곳 다시 입력하면 원래대로 돌아감) \n");
                printf("지뢰가 있을 곳을 표시 하기 위한 좌표찍기. \n");
                printf("입력 > ");
                scanf("%d %d", &x, &y);
                if (map[x][y] == 0)
                {
                    map[x][y] = 20;
                }
                else if (map[x][y] == 1)
                {
                    map[x][y] = 21;
                }
                else if (map[x][y] == 2)
                {
                    map[x][y] = 22;
                }
                else if (map[x][y] == 3)
                {
                    map[x][y] = 23;
                }
                else if (map[x][y] == 4)
                {
                    map[x][y] = 24;
                }
                else if (map[x][y] == 5)
                {
                    map[x][y] = 25;
                }
                else if (map[x][y] == 6)
                {
                    map[x][y] = 26;
                }
                else if (map[x][y] == 7)
                {
                    map[x][y] = 27;
                }
                else if (map[x][y] == 8)
                {
                    map[x][y] = 28;
                }
                else if (map[x][y] == 9)
                {
                    map[x][y] = 29;
                }
                else if (map[x][y] == 37)
                {
                    map[x][y] = 47;
                }
                else if (map[x][y] == 20)
                {
                    map[x][y] = 0;
                }
                else if (map[x][y] == 21)
                {
                    map[x][y] = 1;
                }
                else if (map[x][y] == 22)
                {
                    map[x][y] = 2;
                }
                else if (map[x][y] == 23)
                {
                    map[x][y] = 3;
                }
                else if (map[x][y] == 24)
                {
                    map[x][y] = 4;
                }
                else if (map[x][y] == 25)
                {
                    map[x][y] = 5;
                }
                else if (map[x][y] == 26)
                {
                    map[x][y] = 6;
                }
                else if (map[x][y] == 27)
                {
                    map[x][y] = 7;
                }
                else if (map[x][y] == 28)
                {
                    map[x][y] = 8;
                }
                else if (map[x][y] == 29)
                {
                    map[x][y] = 9;
                }
                else if (map[x][y] == 47)
                {
                    map[x][y] = 37;
                }
                else
                {
                    printf("잘못 입력 하셨습니다. 처음으로! \n");
                    usleep(1000000);
                    continue;
                }
            }
            else
            {
                printf("잘못 입력 하셨습니다. 처음으로! \n");
                usleep(1000000);
                continue;
            }
        }
        else
        {
            printf("   게임 오버! \n");
            *ad += 1;
            printf("\n");
            usleep(1000000);
            break;
        }
        usleep(1000000);
    }
    return victory;
}

int getch() // 엔터를 누르지 않아도 1문자씩 키입력을 받기 위한 함수(wasd)
{
    int c;
    struct termios oldattr, newattr;

    tcgetattr(STDIN_FILENO, &oldattr); // 현재 터미널 설정 읽음
    newattr = oldattr;
    newattr.c_lflag &= ~(ICANON | ECHO);        // CANONICAL과 ECHO 끔
    newattr.c_cc[VMIN] = 1;                     // 최소 입력 문자 수를 1로 설정
    newattr.c_cc[VTIME] = 0;                    // 최소 읽기 대기 시간을 0으로 설정
    tcsetattr(STDIN_FILENO, TCSANOW, &newattr); // 터미널에 설정 입력
    c = getchar();                              // 키보드 입력 읽음
    tcsetattr(STDIN_FILENO, TCSANOW, &oldattr); // 원래의 설정으로 복구
    return c;
}