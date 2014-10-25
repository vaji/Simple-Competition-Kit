#include <string>
#include <WinSock2.h>
#include <iostream>
#include <time.h> 

using namespace std;


string serverIP = "127.0.0.1";
u_short port = 8000;

bool game_finished = false;
string my_sign = "";

WSAData winSocketData;
SOCKET	mainSocket;


struct  field
{
	int nr;
	bool taken;
	int priority;
	string sign;
};

field pola[9];

void Communicate(string data_to_send, string response_data[2]);
string EncodeData(string data[2]);
int PickField();
void ReceiveFromServer(string response_data[2]);
void SetPriority(int picked_field_index, int prior);

int main(string args[])
{
	int socket_init = WSAStartup(MAKEWORD(2, 2), &winSocketData);

	if (socket_init != NO_ERROR)
		cout << "init failed \n";

	mainSocket = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP);

	if (mainSocket == INVALID_SOCKET)
	{
		cout << "socket creation failed \n";
		WSACleanup();
		return 1;
	}


	sockaddr_in service;
	memset(&service, 0, sizeof(service));
	service.sin_family = AF_INET;
	service.sin_addr.S_un.S_addr = inet_addr(serverIP.c_str());
	service.sin_port = htons(port);

	if (connect(mainSocket, (SOCKADDR *)&service, sizeof(service)) == SOCKET_ERROR)
	{
		cout <<  " connection server failed";
		WSACleanup();
		return 1;
	}

	srand(time(NULL));

	

	for (int i = 0; i < 9; i++)
	{
		pola[i].taken = false;
		pola[i].nr = i;
		pola[i].priority = 0;
		if (i == 4) pola[i].priority = 1;
	}
	

	while (!game_finished)
	{
		string received_data[2];
		if (my_sign == "")
		{
			string hello_data[2];
			ReceiveFromServer(&hello_data[2]);
			if (hello_data[0] == "youAre")
			{
				my_sign = hello_data[1];
			}
		
			Communicate(EncodeData(hello_data), &received_data[2]);
		}
		
		if (received_data[0] == "move" && received_data[1] == "")
		{
			string my_Move[2];
			my_Move[0] = received_data[0];
			my_Move[1] = PickField() + "";

			Communicate(EncodeData(my_Move), received_data);
		}
		else if (received_data[0] == "X" && received_data[1] != "")
		{
			int field = atoi(received_data[1].c_str());
			pola[field].taken = true;
			pola[field].sign = "X";
			if (my_sign == "X") SetPriority(field, 2);
			else SetPriority(field, 1);
		}
		else if (received_data[0] == "X" && received_data[1] != "")
		{
			int field = atoi(received_data[1].c_str());
			pola[field].sign = "O";
			pola[field].taken = true;
			if (my_sign == "O") SetPriority(field, 2);
			else SetPriority(field, 1);
		} 
		else if (received_data[0] == "win")
		{
			string temp[2];
			Communicate(EncodeData(received_data),temp);
		}
		else if (received_data[0] == "tie")
		{
			string temp[2];
			Communicate(EncodeData(received_data), temp);
		}

	}



}


void ReceiveFromServer(string response_data[2])
{
	int bytes_recev = SOCKET_ERROR;
	char data_recev[100];
	string string_data;

	while (bytes_recev == SOCKET_ERROR)
	{
		bytes_recev = recv(mainSocket, data_recev, 100, 0);
		string_data = data_recev;
		cout << "dosta³em 1 data: " + string_data;
	}

	string data_part_1;
	string data_part_2;

	bool to_part_1 = true;
	for (int i = 0; i < string_data.length(); i++)
	{
		if (to_part_1)
		{
			if (string_data[i] != ',')
			{
				if(string_data[i] != '<' && string_data[i] != '"' && string_data[i] != '>') data_part_1 += string_data[i];
			}
			else to_part_1 = false;
		} 
		else
		{
			if (string_data[i] != '<' && string_data[i] != '"' && string_data[i] != '>') data_part_2 += string_data[i];
		}
	}

	response_data[0] = data_part_1;
	response_data[1] = data_part_2;

}

string EncodeData(string data[2])
{
	string part_1;
	part_1 += '<"';
	part_1 += data[0];
	part_1 += '",';

	string part_2;
	part_2 += '"';
	part_2 += data[1];
	part_2 += '">';

	part_1 += part_2;
	return part_1;

}
void Communicate(string data_to_send, string respose_data[2])
{
	send(mainSocket, data_to_send.c_str(), 100, 0);
	cout << "wys³a³em na server: " + data_to_send;

	ReceiveFromServer(respose_data);
}

int PickField()
{
	bool picked = false;
	int picked_index =  -1;
	field picked_fields[9];
	int higher = 0;
	int picked_int = 0;

	for (int i = 0; i < 9; i++)
	{
		
		if (pola[i].priority > higher && !pola[i].taken) higher = pola[i].priority;
	}
	for (int i = 0; i < 9; i++)
	{
		if (pola[i].priority == higher && !pola[i].taken) {
			picked_fields[picked_int] = pola[i];
			picked_int++;
		}
	}

	if (picked_int > 1)
	{
		picked_index = rand() % picked_int;
		return picked_fields[picked_index].nr;
	}
	else
	{
		picked_index = picked_int;
		return picked_fields[picked_index].nr;
	}

}

void SetPriority(int picked_field_index, int prior)
{
	
	if (picked_field_index == 0)
	{
		pola[1].priority+= prior;
		pola[3].priority += prior;
		pola[4].priority += prior;
	}
	else if (picked_field_index == 1)
	{
		pola[0].priority += prior;
		pola[2].priority += prior;
		pola[4].priority += prior;
	}
	else if (picked_field_index == 2)
	{
		pola[1].priority += prior;
		pola[4].priority += prior;
		pola[5].priority += prior;
	}
	else if (picked_field_index == 3)
	{
		pola[0].priority += prior;
		pola[4].priority += prior;
		pola[6].priority += prior;
	}
	else if (picked_field_index == 4)
	{
		pola[0].priority += prior;
		pola[1].priority += prior;
		pola[2].priority += prior;
		pola[3].priority += prior;
		pola[5].priority += prior;
		pola[6].priority += prior;
		pola[7].priority += prior;
		pola[8].priority += prior;
	}
	else if (picked_field_index == 5)
	{
		pola[2].priority += prior;
		pola[4].priority += prior;
		pola[8].priority += prior;
	}
	else if (picked_field_index == 6)
	{
		pola[3].priority += prior;
		pola[4].priority += prior;
		pola[7].priority += prior;
	}
	else if (picked_field_index == 7)
	{
		pola[6].priority += prior;
		pola[4].priority += prior;
		pola[8].priority += prior;
	}
	else if (picked_field_index == 8)
	{
		pola[5].priority += prior;
		pola[4].priority += prior;
		pola[7].priority += prior;
	}

}