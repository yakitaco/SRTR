using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SRTR {
    class SRTR {

        char end;
        int count;
        bool reuse;
        public List<List<int>> sList;

        // しりとり作成
        public void make(char _start, char _end, int _count, bool _reuse) {
            end = _end;
            count = _count;
            reuse = _reuse;
            sList = new List<List<int>>();

            // しりとりリスト作成再帰関数
            bool ret = getSR(_start, 1, new List<int>());
            if (sList.Count > 0) {
                // 短い順に並べ替える
                sList = sList.OrderBy(a => a.Count).ToList();
            }
        }

        // 
        public bool getSR(char c, int num, List<int> uList) {
            //oList = new List<int>();
            bool ret = false;
            int i;
            for (i = 0; i < Words.wList.Count; i++) {
                string wStr = Words.wList[i].word;
                if ((wStr[0] == c) && (Words.wList[i].used == false)) {
                    List<int> _nList = new List<int>(uList);
                    _nList.Add(i);
                    //Debug.WriteLine(num + ":["+ i + "]" + wStr);
                    // 終わりが一致
                    if (wStr[wStr.Length - 1] == end) {
                        ret = true;
                        sList.Add(_nList);
                        //break;
                    }
                    Words.wList[i].used = true;
                    ret = getSR(wStr[wStr.Length - 1], num + 1, _nList);
                    Words.wList[i].used = false;
                }
            }

            return ret;
        }
    }
}
