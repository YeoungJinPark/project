#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <unistd.h>
#include <arpa/inet.h>
#include <sys/socket.h>

#define BUF_SIZE 1024
#define FAIL 2
#define SUCCESS 1
#define MAX_CLIENTS 1000

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

void error_handling(char *message);

int Login(char id[], char pw[], User *user)
{
    FILE *fp_user1 = fopen("Userinfo.csv", "r");
    if (fp_user1 == NULL)
    {
        puts("파일 오픈 실패");
        return -1;
    }
    int found = 0; // 로그인 성공 여부를 나타내는 변수
    while (fread(user, sizeof(User), 1, fp_user1) > 0)
    {
        if (strcmp(user->id, id) == 0 && strcmp(user->pw, pw) == 0)
        {
            found = 1;
            break;
        }
    }
    fclose(fp_user1);
    return found;
}

int Membership(char id[], User*user, User *compuser)
{
    // User user;
    // User compuser;
    // int find = 0;
    // int loginss = 1;
    // char id[30];

    FILE *fp_user1 = fopen("Userinfo.csv", "a");
    if (fp_user1 == NULL)
    {
        puts("파일 오픈 실패");
        return -1;
    }

    FILE *fp_user2 = fopen("Userinfo.csv", "r");
    if (fp_user2 == NULL)
    {
        puts("파일 오픈 실패");
        return -1;
    }
    int found = 1; // 회원가입 성공 여부를 나타내는 변수
    while (fread(compuser, sizeof(User), 1, fp_user2) > 0)
    {
        if (strcmp(compuser->id, user->id) == 0)
        {
            found = 2;
            break;
        }
    }
    fclose(fp_user2);
    fclose(fp_user1);
    return found;
}

Book CheckBookInfo(char book[], int *flag)  // 도서 조회를 위한 함수
{
    FILE *fp = NULL;
    Book result;    // 입력받은 도서와 일치하는 도서의 내용을 담은 구조체를 반환하기 위해 저장할 구조체 변수
    
    fp = fopen("BookInfo.csv", "r");
    if(fp==NULL)
    {
        error_handling("fopen() error");
        exit(1);
    }
    while(!feof(fp))
    {
        char line[BUF_SIZE];
        Book tmp={0};

        fgets(line, sizeof(line), fp);  // 파일의 내용을 한줄씩 읽어와 버퍼 line에 저장
        sscanf(line, "%12[^,],%199[^,],%99[^,],%99[^,],%49[^,],%49[^,],%49[^\n]", tmp.code, tmp.title,
        tmp.writer, tmp.publisher, tmp.year, tmp.rent, tmp.period); // ,로 구분된 내용을 구조체의 각 멤버에 나누어 저장

        if(strncmp(tmp.code, book, 12)==0 || strncmp(tmp.title, book, strlen(book))==0) // 도서명이나 등록번호와 일치할때
        {
            result=tmp; // 찾은 도서의 내용을 반환해주기 위해 result에 저장
            *flag=1;    // 도서 조회에 성공했음을 알리기 위한 flag 변수
            fclose(fp);
            fp=NULL;
            return result;
        }
    }
    *flag=2;    // 일치하는 도서가 없음을 알리기 위한 flag 변수
    fclose(fp);
    fp=NULL;
    return result;
}

int RentalBook(char book[], int period) // 도서 대여를 위한 함수
{
    FILE *fp = NULL;
    FILE *tfp = NULL;
    int flag=2;
    //long fpos;
    char insert[50];

    sprintf(insert, "대여불가,%d일 후\n", period);  // BookInfo.csv에는 기본적으로 반납기간이 없기때문에 내용 추가를 위한 문자열 저장
    
    fp = fopen("BookInfo.csv", "rt");
    tfp = fopen("tmp.csv", "w");
    if(fp==NULL)
    {
        error_handling("fopen() error");
        exit(1);
    }
    while(!feof(fp))
    {
        char line[BUF_SIZE]={};
        Book tmp={0};
        fgets(line, sizeof(line), fp);  // 파일의 내용을 한줄씩 읽어와 line에 저장
        sscanf(line, "%12[^,],%199[^,],%*99[^,],%*99[^,],%*49[^,],%49[^,]", 
        tmp.code, tmp.title, tmp.rent); // ,로 구분된 파일의 문자열을 필요한 정보(등록번호, 제목, 대여여부)에만 저장
        if(strncmp(tmp.code, book, 12)==0 || strncmp(tmp.title, book, strlen(book))==0) // 도서명이나 등록번호와 일치할때
        {
            if(strncmp(tmp.rent, "대여가능", 12)==0)    // 찾는 도서가 대여 가능상태일때
            {
                char stmp[BUF_SIZE];

                // strncpy(stmp, line, strlen(line)-strlen(tmp.rent));
                // stmp[strlen(line) - strlen(tmp.rent)] = '\0';
                // strncpy(line, stmp, BUF_SIZE-1);
                line[strlen(line) - strlen(tmp.rent)] = '\0';   // 문자열 수정을 위해 대여 정보 이전 위치에 널문자를 추가
                // 이 조건문에 들어왔다는 것은 대여가능 상태. 즉, 반납기간이 없는 상태이기 때문에 tmp.period의 길이를 빼줄 필요가 없다.
                strcat(line, insert);   // 대여가능상태를 대여불가, 반납기간으로 바꾸기 위해 덧붙임
                
                //fp = fopen("BookInfo.csv", "wt");
                /*
                fp = fopen("BookInfo.csv", "a+");
                fseek(fp, fpos, SEEK_SET);
                fprintf(fp, "%d일 후", period);
                fflush(fp);
                */
                flag=1; // 대여 성공을 알리기 위한 flag 변수
            }
            else
            flag=3; // 이미 대여된 도서(대여불가)
        }
        fputs(line, tfp);   // 임시파일에 내용을 출력(저장)
        /* 
        대여는 특정 위치에 문자열의 추가가 이루어지기 때문에
        원본파일의 내용을 읽어와 버퍼에 저장
        -> 임시파일을 생성해 버퍼의 내용을 임시파일에 출력
        -> 버퍼의 내용이 찾고있는 정보면 버퍼의 내용을 수정
        -> 수정된 버퍼의 내용을 임시파일에 출력
        -> 모든 내용이 출력되면 원본파일을 삭제
        -> 임시파일의 내용을 원본파일로 바꿈
        의 순서로 진행된다.
        */
    }
    fclose(fp);
    fclose(tfp);
    fp=NULL;
    tfp=NULL;
    if(remove("BookInfo.csv")!=0)   // 원본파일 삭제
    {
        error_handling("BookInfo.csv remove() error!");
        exit(1);
    }
    if(rename("tmp.csv", "BookInfo.csv")!=0)    // 임시파일의 이름 변경
    {
        error_handling("rename() error!");
        exit(1);
    }
    return flag;    // 2 == 등록되지 않은 도서
    // 대여의 성공여부를 알리기 위한 flag 변수를 반환
}

int ReturnBook(char book[]) // 도서 반납을 위한 함수
{
    FILE *fp = NULL;
    FILE *tfp = NULL;
    int flag=2;
    //long fpos;
    char insert[50];

    sprintf(insert, "대여가능\n");  
    
    fp = fopen("BookInfo.csv", "r+t");
    tfp = fopen("tmp.csv", "w"); // 반납을 할 때는 파일에 반납 일자가 존재하기에 반납기간 삭제를 위해 수정이 아닌 대여와 같은 방식을 사용
    if(fp==NULL)
    {
        error_handling("fopen() error");
        exit(1);
    }
    while(!feof(fp))
    {
        // 대여함수와 마찬가지의 로직
        char line[BUF_SIZE]={};
        Book tmp={0};
        fgets(line, sizeof(line), fp);
        sscanf(line, "%12[^,],%199[^,],%99[^,],%99[^,],%49[^,],%49[^,],%49[^\n]", tmp.code, tmp.title,
        tmp.writer, tmp.publisher, tmp.year, tmp.rent, tmp.period);
        if(strncmp(tmp.code, book, 12)==0 || strncmp(tmp.title, book, strlen(book))==0) // 도서명이나 등록번호와 일치할때
        {
            if(strncmp(tmp.rent, "대여불가", 12)==0)
            {
                flag=1; // 반납 성공을 알리기 위한 flag 변수
                strncpy(tmp.rent, "대여가능\n", sizeof(tmp.rent));
                /*
                char stmp[BUF_SIZE];

                strncpy(stmp, line, strlen(line)-strlen(tmp.rent)-strlen(tmp.period));
                stmp[strlen(line) - strlen(tmp.rent)-strlen(tmp.period)] = '\0';
                strncpy(line, stmp, BUF_SIZE-1);
                line[strlen(stmp)]='\0';
                strcat(line, insert);
                */
                /*fpos=ftell(fp);
                if(strlen(tmp.period)>0)
                fpos-=strlen(tmp.period);
                fpos-=strlen(tmp.rent);

                //fp = fopen("BookInfo.csv", "wt");
                fseek(fp, fpos, SEEK_SET);
                if(strlen(tmp.period)>0)
                {
                    fprintf(fp, "대여가능");
                    fflush(fp);
                }
                else
                fprintf(fp, "대여가능");
                fflush(fp);
                */
            }
            else
            flag=3; // 대여되지 않은 도서
        }
        //fputs(line, tfp);
        fprintf(tfp, "%s,%s,%s,%s,%s,%s", tmp.code, tmp.title, tmp.writer, tmp.publisher, tmp.year, tmp.rent);
        /* 
        대여함수에서는 대여정보의 앞쪽 위치를 계산해 내용을 수정했지만
        생각해보면 읽은 파일의 내용을 위에서 구조체에 저장하기때문에
        구조체의 데이터를 순서대로 파일에 출력하는게 더 간단한 것 같다.
        */
    }
    fclose(fp);
    fclose(tfp);
    fp=NULL;
    tfp=NULL;
    if(remove("BookInfo.csv")!=0)   // 원본파일 삭제
    {
        error_handling("BookInfo.csv remove() error!");
        exit(1);
    }
    if(rename("tmp.csv", "BookInfo.csv")!=0)    // 임시파일의 이름을 원본파일의 이름으로 변경
    {
        error_handling("rename() error!");
        exit(1);
    }
    return flag;    // 2 == 등록되지 않은 도서
}

int main(int argc, char *argv[])
{
    int serv_sd, clnt_sd, client_sockets[MAX_CLIENTS] = {0};
    int menu, flag = 0, end = 1;
    int loginss;
    int period;
    char buf[BUF_SIZE] = {};
    char name[50];
    FILE *fp_user1;
    Book book;

    struct sockaddr_in serv_adr, clnt_adr;
    socklen_t clnt_adr_sz;

    char buffer[1024];
    int clnt_sockets[MAX_CLIENTS] = {0}; // 클라이언트 소켓 배열

    if (argc != 2)
    {
        printf("Usage: %s <port>\n", argv[0]);
        exit(1);
    }

    serv_sd = socket(PF_INET, SOCK_STREAM, 0);
    if (serv_sd == -1)
        error_handling("socket() error");

    memset(&serv_adr, 0, sizeof(serv_adr));
    serv_adr.sin_family = AF_INET;
    serv_adr.sin_addr.s_addr = htonl(INADDR_ANY);
    serv_adr.sin_port = htons(atoi(argv[1]));

    if (bind(serv_sd, (struct sockaddr *)&serv_adr, sizeof(serv_adr)) == -1)
        error_handling("bind() error");

    if (listen(serv_sd, MAX_CLIENTS) == -1)
        error_handling("listen() error");

    puts("Connect on call..........");
    int connected_clients = 0; // 현재 연결된 클라이언트 수
    clnt_adr_sz = sizeof(clnt_adr);

    for (int i = 0; i < MAX_CLIENTS; i++)
    {
        // 클라이언트 연결 수락
        clnt_sd = accept(serv_sd, (struct sockaddr *)&serv_adr, (socklen_t *)&clnt_adr_sz);
        if (clnt_sd == -1)
        {
            error_handling("accept() arror");
        }
        else
        {
            printf("클라이언트 %d과 연결되었습니다.\n", i + 1);
        }
        if (connected_clients < MAX_CLIENTS)
        {
            // 대기 중인 클라이언트에게 접속 대기 중이라는 메시지 전송
            const char *message = "서버와 연결되었습니다.";
            write(clnt_sd, message, strlen(message));

            // 연결된 클라이언트 소켓 저장
            clnt_sockets[connected_clients] = clnt_sd;
            connected_clients++;
        }
        else
        {
            // 연결된 클라이언트가 MAX_CLIENTS를 초과하면 연결을 거부하고 메시지 전송
            const char *message = "서버가 현재 연결을 처리할 수 없습니다.";
            write(clnt_sd, message, strlen(message));
            close(clnt_sd);
        }

        while (1)
        {
            while (1)
            {

                read(clnt_sd, (int *)&menu, sizeof(menu));
                if (menu == 1) // 로그인
                {
                    User user;
                    char id[30];
                    char pw[30];
                    int loginss;
                    int find = 0;
                    puts("클라이언트가 로그인을 시도합니다.");
                    read(clnt_sd, (char *)&id, sizeof(id));
                    read(clnt_sd, (char *)&pw, sizeof(pw));

                    find = Login(id, pw, &user);

                    if (find == 1)
                    {
                        printf("%s님 접속\n", user.name);
                        loginss = 1;
                        strcpy(name, user.name);
                        write(clnt_sd, (int *)&loginss, sizeof(loginss));
                        break;
                    }
                    else
                    {
                        puts("ID와 PW가 일치하지 않습니다.");
                        loginss = 2;
                        write(clnt_sd, (int *)&loginss, sizeof(loginss));
                    }
                }
                else if (menu == 2) // 회원가입
                {
                    User user;
                    User compuser;
                    int find = 0;
                    int loginss = 1;
                    char id[30];

                    puts("클라이언트가 회원가입을 시도합니다.");
                    read(clnt_sd, id, sizeof(id));
                    loginss= Membership(id, &user, &compuser);
                    
                    write(clnt_sd, (int *)&loginss, sizeof(loginss));
                    if (loginss == 1)
                    {
                        read(clnt_sd, (User *)&user, sizeof(user));
                        FILE *fp_user1 = fopen("Userinfo.csv", "a");
                        fwrite(&user, sizeof(user), 1, fp_user1);
                        printf("%s님 회원가입\n", user.name);
                        fclose(fp_user1);
                    }
                    else if (loginss == 2)
                    {
                        puts("이미 존재하는 ID입니다.");
                    }
                }
            }

            User user;

            while (end) // 로그인이 성공해야만 들어옴
            {
                read(clnt_sd, &menu, sizeof(menu));       // 클라이언트에서 선택한 메뉴를 수신
                switch (menu)
                {
                case 1:                                   // 도서 조회
                    read(clnt_sd, buf, BUF_SIZE - 1);     // 조회할 도서의 정보(등록번호 or 제목)를 수신
                    book = CheckBookInfo(buf, &flag);     // 파일에서 입력받은 도서를 조회하고 일치하는 도서의 정보 반환해 book에 저장

                    write(clnt_sd, &flag, sizeof(flag));  // 조회 성공여부를 클라이언트에 전달
                    if (flag == 1)                        // 일치하는 책이 있음
                    {
                        // size_t sz = sizeof(Book);
                        // write(clnt_sd, &sz, sizeof(sz));
                        write(clnt_sd, &book, sizeof(Book));// 일치하는 도서의 정보를 담은 구조체를 클라이언트에 전송
                    }

                    flag = 0;
                    break;

                case 2:                                     // 대여
                    read(clnt_sd, buf, BUF_SIZE - 1);       // 대여할 도서의 정보(등록번호 or 제목)를 수신
                    read(clnt_sd, &period, sizeof(period)); // 반납기간을 클라이언트로부터 수신
                    flag = RentalBook(buf, period);         // 파일에서 입력받은 도서를 조회하고 파일에 일치하는 도서 부분의 대여정보, 반납일 수정
                    write(clnt_sd, &flag, sizeof(flag));    // 성공여부를 클라이언트에 전송
                    break;

                case 3:                                     // 반납
                    read(clnt_sd, buf, BUF_SIZE - 1);       // 반납할 도서의 정보(등록번호 or 제목)를 수신
                    flag = ReturnBook(buf);                 // 파일에서 입력받은 도서를 조회하고 파일에 일치하는 도서 부분의 대여정보를 수정
                    write(clnt_sd, &flag, sizeof(flag));    // 성공여부를 클라이언트에 전송
                    break;

                case 4: // 로그아웃
                    end = 0;
                    loginss = 3;
                    printf("%s님이 로그아웃 합니다\n", name);
                    break;

                case 5: // 종료
                    puts("클라이언트와의 접속이 종료됩니다.");
                    end = 0;
                    loginss = 5;
                    break;
                }
            }
            if (end == 0 && loginss == 3)
            {
                end = 1;
                continue;
            }
            else if (end == 0)
            {
                break;
            }
        }
        if (end == 0)
        {
            end = 1;
            // close(clnt_sd);
            for (int i = 0; i < connected_clients; i++)
            {
                close(clnt_sockets[i]);
            }
            continue;
        }
    }

    shutdown(clnt_sd, SHUT_WR);

    // close(clnt_sd);
    close(serv_sd);
    return 0;
}

void error_handling(char *message)
{
    fputs(message, stderr);
    fputc('\n', stderr);
    exit(1);
}
