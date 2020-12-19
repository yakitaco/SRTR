using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRTR {
    class SRTR {

        char end;
        int count;
        bool reuse;

        // しりとり作成
        public void make(char _start, char _end, int _count, bool _reuse) {
            end = _end;
            count = _count;
            reuse = _reuse;

            //  
            bool ret = getSR(_start, 1, out List<int> oList);

        }

        // 
        public bool getSR(char c, int num, out List<int> oList) {
            oList = new List<int>();
            bool ret = false;
            foreach (Words w in Words.wList) {
                if (w.word[w.word.Length - 1] == end) {
                    ret = true;
                    break;
                }
                if (w.word[0] == c) {
                    Debug.WriteLine(w.word);
                    Debug.WriteLine(w.word[w.word.Length - 1]);
                    ret = getSR(w.word[w.word.Length - 1], num + 1 , out List<int> _oList);
                }
            }

            return ret;
        }
    }
}
