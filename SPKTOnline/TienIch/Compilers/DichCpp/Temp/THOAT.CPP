#include "cstdio"
#include "vector"
#include "map"
#include "queue"
//#include "conio.h"
using namespace std;

// Shortcuts for "common" data types in contests
typedef vector<int>           vi;
typedef pair<int, int>        ii;
typedef vector<ii>            vii;
typedef vector<vii>           vvii;

#define isOn(i, bitmask) (bitmask & (1<<i))

#define REP(i, a, b) \
  for (int i = int(a); i <= int(b); i++)
// Loop from 1 to n, variable i is local
#define REPN(i, n) \
  REP (i, 1, int(n))
// Loop down from a to b, variable i is local
#define REPD(i, a, b) \
  for (int i = int(a); i >= int(b); i--)
// These variations are manual at the moment
#define TRvi(c, it) \
  for (vi::iterator it = (c).begin(); it != (c).end(); it++)
#define TRvii(c, it) \
  for (vii::iterator it = (c).begin(); it != (c).end(); it++)

vvii AdjList;
vi V;
int M, N, X, Y, T;

int isBound(int x)
{
    if (1<=x && x<=N)
       return 1;
    if (x%N==1 || x%N==0)
       return 1;
    if (M*N-N+1<=x && x<=M*N)
       return 1;
       
    return 0;
}

int main() 
{
  freopen("THOAT.INP","r",stdin);
  freopen("THOAT.OUT","w",stdout);
  scanf("%d %d", &M, &N);
  scanf("%d %d", &X, &Y);
  scanf("%d", &T);
  AdjList.assign(M*N+1, vii()); // assign blank vectors of pair<int, int>s to AdjList
  V.push_back(0);
  //                                        8   9  10
  int d[]={-N,-N+1, 1, N+1, N, N-1,-1,-N-1,-N,-N+1, 1};
  int a;
  REP(i,1,M)
      REP(j,1,N)
      {
          scanf("%d",&a);
          V.push_back(a);
      }
  /*
  REP(i,1,M*N)
  {
      printf("%d ",V[i]);
      if (i%N==0)
         printf("\n");
  }
  getch();
  */
  
  REP (i, 1, N)
  {
      if (V[i]==1)
      {
          if (i==1)
            REP (b,2,4)
            {
                if (V[i+d[b]]==1)
                    AdjList[i].push_back(ii(i+d[b], 0));
            }
          else if (i==N)
            REP (b,4,6)
            {
                if (V[i+d[b]]==1)
                    AdjList[i].push_back(ii(i+d[b], 0));
            }
          else
            REP (b,2,6)
            {
                if (V[i+d[b]]==1)
                    AdjList[i].push_back(ii(i+d[b], 0));
            }
      }
  }
  
  REP (i, N+1, M*N-N)
  {
    //scanf("%d", &a);
    //V.push_back(a);
    if (V[i]==1)
    {
        if (i%N==1)
           REP (b,0,4)
           {
                if (V[i+d[b]]==1)
                    AdjList[i].push_back(ii(i+d[b], 0));
           }
        else if (i%N==0)
            REP (b,4,8)
            {
                if (V[i+d[b]]==1)
                    AdjList[i].push_back(ii(i+d[b], 0));
            }
        else
            REP(b,0,7)
            {
                if (V[i+d[b]]==1)
                    AdjList[i].push_back(ii(i+d[b], 0));
            }
    }
  }
    
    REP (i,M*N-N+1,M*N)
    {
      //scanf("%d",&a);
      //V.push_back(a);
      if (V[i]==1)
      {
          if (i%N==1)
            REP (b,8,10)
            {
                if (V[i+d[b]]==1)
                    AdjList[i].push_back(ii(i+d[b], 0));
            }
          else if (i==M*N)
            REP (b,6,8)
            {
                if (V[i+d[b]]==1)
                    AdjList[i].push_back(ii(i+d[b], 0));
            }
          else
            REP (b,6,10)
            {
                if (V[i+d[b]]==1)
                    AdjList[i].push_back(ii(i+d[b], 0));
            }
      }
    }
    
    
  /*  show AdjList to test 
  REP (i,1, M*N)
  {
      printf("%2d :: ",i);
      TRvii(AdjList[i],it)
          printf("%3d ",it->first);
      printf("\n");  
  }
  printf("\n");  
  //getch();
  */
  //BFS
  
  int s = (X-1)*N + Y;
  
  if (isBound(s))
  {
      printf("0");
      return 0;
  }
  queue<int> Q; 
  map<int,int> dist; 
  
  Q.push(s); dist[s]=0;//start from source
  
  vi trace(M*N+1);
  int t=0, bound;
  while (!Q.empty() && t!=1)
  {
        int u=Q.front(); Q.pop();
        //printf("Visit %d, Layer %d\n", u, dist[u]);
        TRvii (AdjList[u],v)
              if (!dist.count(v->first))
              {
                  dist[v->first]=dist[u]+1;
                  Q.push(v->first);
                  trace[v->first]=u;
                  if (isBound(v->first) && dist[v->first]<T)
                  {
                     //printf("%d\n",v->first);
                     t=1;
                     bound=v->first;
                     break;
                     //getch();
                  }
              }
  }
  if (Q.empty() && t!=1)
     printf("0");
  else
  {   
      vi result;
      t=bound;
      while (t!=s)
      {
            result.push_back(t);
            t=trace[t];
      }
      result.push_back(s);

      printf("%d\n",result.size());
      for (vi::reverse_iterator it=result.rbegin(); it!=result.rend(); ++it)
      {
          if (*it%N != 0)
             printf("%d %d\n",*it/N + 1,*it % N);
          else
             printf("%d %d\n",*it/N , N);
      }
  }
  //getch();
  fclose(stdin);
  fclose(stdout);
  return 0;
}
