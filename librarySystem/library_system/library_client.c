#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <unistd.h>
#include <fcntl.h>
#include <termios.h>
#include <arpa/inet.h>
#include <sys/socket.h>

#define BUF_SIZE 1024

typedef struct

{
    char name[30];
    char residence[30];
    char id[30];
    char pw[30];
    char pnum[30];
} User;

typedef struct Book
{
    char code[50];       // 등록번호
    char title[200];     // 도서명
    char writer[100];    // 저자
    char publisher[100]; // 출판사
    char year[50];       // 출판연도
    char rent[50];       // 대여정보
    char period[50];     // 대여기간
} Book;

char getch();

void error_handling(char *message);

int main(int argc, char *argv[])
{
    int sock;
    int menu, flag, end = 1;
    char id[30];
    char pw[30];
    int find = 0;
    int loginss = 0;
    int index = 0;
    int period;
    char buf[BUF_SIZE] = {};
    int read_cnt;
    char buffer[1024];

    FILE *fp_user;
    User user;
    Book book;
    struct sockaddr_in serv_adr;
    if (argc != 3)
    {
        printf("Usage: %s <IP> <port>\n", argv[0]);
        exit(1);
    }

    sock = socket(PF_INET, SOCK_STREAM, 0);

    memset(&serv_adr, 0, sizeof(serv_adr));
    serv_adr.sin_family = AF_INET;
    serv_adr.sin_addr.s_addr = inet_addr(argv[1]);
    serv_adr.sin_port = htons(atoi(argv[2]));

    if (connect(sock, (struct sockaddr *)&serv_adr, sizeof(serv_adr)) == -1)
        error_handling("connect() error!");
    else
        puts("접속 대기중입니다........");

    //서버로부터 메시지 수신
    memset(buffer, 0, sizeof(buffer));
    read(sock, buffer, 1024);
    printf("서버로부터 받은 메시지: %s\n", buffer);

    while (1)
    {
        while (1)
        {
            printf("1.로그인  2.회원가입\n");
            scanf("%d", &menu);
            getchar();
            write(sock, (int *)&menu, sizeof(menu));

            if (menu == 1)
            {
                int index = 0;
                printf("ID: ");
                scanf("%s", id);
                getchar();
                printf("Pass Word: ");
                while (1)
                {
                    char c = getch(); // getch 함수로 키 입력 받음

                    if (c == '\n') // Enter 키를 누르면 입력 종료
                    {
                        break;
                    }

                    if (c == 127) // 백스페이스 키를 누른 경우
                    {
                        if (index > 0) // 입력된 문자가 있는 경우에만 처리
                        {
                            printf("\b \b"); // 커서를 앞으로 이동하여 백스페이스 효과 구현
                            index--;
                        }
                    }
                    else // 다른 문자를 입력한 경우
                    {
                        pw[index] = c;
                        printf("*");
                        index++;
                    }
                }

                pw[index] = '\0'; // 문자열 종료를 위해 널 문자 추가

                printf("\n");
                write(sock, (char *)&id, sizeof(id));
                write(sock, (char *)&pw, sizeof(pw));
                read(sock, (int *)&loginss, sizeof(loginss));
                if (loginss == 1)
                {
                    puts("로그인 성공 하였습니다.");
                    loginss = 3;
                    // write(sock, (int *)&loginss, sizeof(loginss));
                    break;
                }
                else if (loginss == 2)
                {
                    puts("ID 또는 PW가 일치하지 않습니다.");
                }
            }
            else
            {
                printf("이름: ");
                scanf("%s", user.name);
                printf("거주지: ");
                scanf("%s", user.residence);
                printf("연락처: ");
                scanf("%s", user.pnum);
                printf("ID: ");
                scanf("%s", user.id);
                printf("Pass Word: ");
                scanf("%s", user.pw);

                write(sock, user.id, sizeof(user.id));
                read(sock, (int *)&loginss, sizeof(loginss));

                if (loginss == 1)
                {
                    write(sock, (User *)&user, sizeof(user));
                    puts("회원가입 되었습니다.");
                }
                else if (loginss == 2)
                {
                    puts("이미 존재하는 ID입니다.");
                }
            }
        }
        // 로그인을 해야 반복문에 들어옴 (로그인은 팀원 담당부분으로 추가 예정)
        // while안에 로그인 부분의 while과 이하의 도서 시스템부분의 while을 넣어 로그아웃 기능 구현 예정
        while (end) // 종료를 위한 조건변수 end를 0으로 만들면 반복을 종료함
        {
            fputs("1.도서조회  2.도서대여  3.도서반납  4.로그아웃  5.종료\n", stdout);
            scanf("%d", &menu);
            while (getchar() != '\n')           // 입력버퍼를 비워주기 위함
                continue;

            write(sock, &menu, sizeof(menu));   // 선택한 메뉴를 서버에 전송
            switch (menu)
            {
            case 1: // 도서 조회
                printf("조회할 도서의 제목이나 등록번호를 입력해주세요\n");
                fgets(buf, BUF_SIZE, stdin);
                buf[strcspn(buf, "\n")] = '\0';         // null 문자 추가
                write(sock, buf, strlen(buf) + 1);      // null 문자까지 전송
                read(sock, &flag, sizeof(flag));        // 일치하는 도서를 찾았는지 판단하기 위한 flag변수
                if (flag == 1)                          // 입력받은 도서를 BookInfo.csv 파일에서 찾았음
                {
                    read(sock, &book, sizeof(book));    // 도서 정보를 담은 Book구조체를 받아옴

                    printf("등록번호: %s\n제목: %s\n저자: %s\n출판사: %s\n출판년도: %s\n",
                           book.code, book.title, book.writer, book.publisher, book.year);
                    if (strlen(book.period) > 0)        // BookInfo.csv 대여되지 않은 도서는 반납 예정일이 없기 때문에 그에 대한 처리
                        printf("대여 여부: %s\n반납 예정일: %s\n", book.rent, book.period);
                    else
                        printf("대여 여부: %s반납 예정일: \n", book.rent);
                }
                else                                    // 입력받은 도서가 파일에 없음
                    printf("등록되지 않은 책입니다.\n");
                break;

            case 2: // 대여
                printf("대여할 도서의 제목이나 등록번호를 입력해주세요\n");
                fgets(buf, BUF_SIZE, stdin);
                buf[strcspn(buf, "\n")] = '\0';    // null 문자 추가
                write(sock, buf, strlen(buf) + 1); // null 문자까지 전송

                printf("대여일은 며칠인가요?\n");
                scanf("%d", &period);
                while (getchar() != '\n')               // 입력 버퍼 청소
                    continue;
                write(sock, &period, sizeof(period));

                read(sock, &flag, sizeof(flag));        // 대여 성공 여부를 판단하기 위한 flag변수
                if (flag == 1)                          // 대여 성공
                    printf("대여되었습니다.\n");
                else if (flag == 2)                     // 일치하는 도서가 없음
                    printf("등록되지 않은 책입니다.\n");
                else if (flag == 3)                     // 일치하는 도서가 존재하나, 대여 불가상태
                    printf("이미 대여된 도서입니다.\n");
                break;

            case 3: // 반납
                printf("반납할 도서의 제목이나 등록번호를 입력해주세요\n");
                fgets(buf, BUF_SIZE, stdin);
                buf[strcspn(buf, "\n")] = '\0';         // null 문자 추가
                write(sock, buf, strlen(buf) + 1);      // null 문자까지 전송
                read(sock, &flag, sizeof(flag));        // 반납 성공 여부를 판단하기 위한 flag변수
                if (flag == 1)                          // 반납 성공
                    printf("반납되었습니다.\n");
                else if (flag == 2)                     // 일치하는 도서가 없음
                    printf("등록되지 않은 책입니다.\n");
                else if (flag == 3)                     // 일치하는 도서가 존재하나, 대여되지 않은 상태
                    printf("대여되지 않은 도서입니다.\n");
                break;

            case 4: // 로그아웃
                end = 0;                                // 반복문의 조건인 end를 0으로 만들어 반복문에 들어가지 않음
                loginss = 3;
                printf("로그아웃합니다.\n");
                break;

            case 5: // 종료
                end = 0;
                loginss=5;
                printf("종료합니다.\n");
                break;
            }
        }
        if (end == 0 && loginss == 3)
        {
            end=1;
            continue;
        }
        else if(end==0)
        {
            break;
        }
    }
    shutdown(sock, SHUT_WR);
    close(sock);
    return 0;
}

void error_handling(char *message)
{
    fputs(message, stderr);
    fputc('\n', stderr);
    exit(1);
}

char getch()
{
    struct termios oldattr, newattr;
    char ch;

    tcgetattr(STDIN_FILENO, &oldattr);
    newattr = oldattr;
    newattr.c_lflag &= ~(ICANON | ECHO);
    tcsetattr(STDIN_FILENO, TCSANOW, &newattr);

    ch = getchar();

    tcsetattr(STDIN_FILENO, TCSANOW, &oldattr);

    return ch;
}