import os


def subfolder_path(folder):
    subfolder = os.path.join(os.path.dirname(__file__), folder)
    return subfolder


def recording(roomname, comm):
    if not os.path.exists(roomname):
        with open(roomname+'room.txt', 'w', encoding='utf-8') as file:
            file.write(comm+':/')
    else:
        with open(roomname, 'a', encoding='utf-8') as file:
            file.write(comm+':/')


def write_comments(id_, comm):
    with open(id_+'chat.txt', 'w', encoding='utf-8') as file:
        file.write(comm+':/\n')


def write_user(data):
    subfolder = os.path.join(subfolder_path("users"), "userInfo.txt")
    if not os.path.exists(subfolder):
        with open(subfolder, 'w', encoding='utf-8') as file:
            file.write(data+'\n')
    else:
        with open(subfolder, 'a', encoding='utf-8') as file:
            file.write(data+'\n')


def read_comments(id_):
    with open(id_+'chat.txt', 'r', encoding='utf-8') as file:
        data = file.read()
        return data


def read_user():
    subfolder = os.path.join(subfolder_path("users"), "userInfo.txt")
    if not os.path.exists(subfolder):
        return False
    else:
        with open(subfolder, 'r', encoding='utf-8') as file:
            data = file.read()
            return data


def read_list():
    room_list = []
    files = subfolder_path("chatroom")
    file_list = [file for file in os.listdir(files) if file.endswith('room.txt')]
    for room_ in file_list:
        room_list.append(room_.replace('room.txt', ''))
    room = ':'.join(room_list)
    return room

def read_room(roomname):
    roominfo = None
    subfolder = os.path.join(subfolder_path("chatroom"), roomname+"room.txt")
    if not os.path.exists(subfolder):
        return False
    else:
        with open(subfolder, 'r', encoding='utf-8') as file:
            data = file.read()
            return data


if __name__ == '__main__':
    pass
    # write_user("abc:123:홍길동")
    # write_user("def:456:길동홍")
