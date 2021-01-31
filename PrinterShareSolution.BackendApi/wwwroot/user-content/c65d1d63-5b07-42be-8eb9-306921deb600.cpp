#include <iostream>
#include <fstream>
int readFile();
int main()
{
	readFile();

	return 0;
}

int readFile()
{
		std::ifstream fileInput("C:\\Users\\QuangLH\\Desktop\\ft6.txt");

	if (fileInput.fail())
	{
		std::cout << "Failed to open this file!" << std::endl;
		return -1;
	}
//	while (!fileInput.eof())
//	{
//		int n;
//		fileInput >> n;
//		std::cout << n << " ";
//	}
	
	int nJob, mMachine;
	int ma_may, ma_job, ma_thao_tac, tgian_xl;
	fileInput >> nJob;
	fileInput >> mMachine;
	for(int i = 0; i < nJob; i++)
	{
		for(int j = 0; j < mMachine; j++)
		{
			fileInput>>ma_may;
			fileInput>>tgian_xl;
			ma_job = i+1;
			ma_thao_tac = j+1 + i*mMachine;
			//arrope[i][j] = Operation(ma_may, ma_job, ma_thao_tac, tgian_xl);
			std::cout << ma_may+1 << " " << ma_job << " " << ma_thao_tac << " " << tgian_xl << "\n ";
		}
	}
	std::cout << std::endl;

	fileInput.close();

	return 0;
}
