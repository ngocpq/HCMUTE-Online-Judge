#include <stdio.h>
#include <conio.h>
#include <stdlib.h>
#define input "thop.inp"
#define output "thop.out"

#define max 100

int A[9];
int Max[9];
FILE *fo;
int n,k;

void nhap()
{
	FILE *fi;
	fi=fopen(input,"r");
	if(!fi)
	{
		printf("khong doc duoc tep\n");
		getch();
		exit(1);
	}
	fscanf(fi,"%d",&n);
	fscanf(fi,"%d",&k);
	fclose(fi);
}

void xuat()
{
	for(int i=1;i<=k;i++)
		fprintf(fo,"%d ",A[i]);
	fprintf(fo,"\n");

}

void init()
{
	int tmp=n;
	for(int i=k;i>0;i--)
		Max[i]=tmp--;
	for(i=1;i<=k;i++)
			A[i]=i;
}

void process()
{
	int i,j;
	while(1){
		i=k;
		xuat();
		while(A[i]<Max[i]){
			A[i]++;
			xuat();
		}
		while(A[i]==Max[i])
			i--;
		if(i>-1)
			A[i]++;
		else
			break;
		int tmp=A[i];
		for(j=i+1;j<k;j++)
			A[j]=++tmp;
	}
}

void main()
{
	clrscr();
	nhap();
	init();
	fo=fopen(output,"w");
	if(!fo)
	{
		printf("Khong ghi duoc tep");
		getch();
		exit(1);
	}
	process();
	fclose(fo);
}