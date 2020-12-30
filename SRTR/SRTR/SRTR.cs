using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SRTR {
    class SRTR {

        char end;
        int count;
        bool reuse;
        public List<List<int>> sList;

        //
        System.Globalization.CompareInfo ci =
            System.Globalization.CultureInfo.CurrentCulture.CompareInfo;

        // しりとり作成
        public void make(char _start, char _end, int _count, bool _reuse) {
            end = _end;
            count = _count;
            reuse = _reuse;
            sList = new List<List<int>>();


            //thread同時数
            int workMin;
            int ioMin;
            ThreadPool.GetMinThreads(out workMin, out ioMin);

            int teCnt = 0; //手の進捗
            Object lockObj = new Object();

            // 並行処理
            Parallel.For(0, workMin, id => {
                while (true) {
                    int cnt_local;
                    lock (lockObj) {
                        if (Words.wList.Count <= teCnt) break;
                        cnt_local = teCnt;
                        teCnt++;
                    }

                    //if (Words.wList[cnt_local].word[0] == _start) {
                    if (ci.Compare(Words.wList[cnt_local].word[0].ToString(), _start.ToString(),
                        System.Globalization.CompareOptions.IgnoreWidth |
                        System.Globalization.CompareOptions.IgnoreKanaType) == 0) {
                        string wStr = Words.wList[cnt_local].word;
                        Debug.WriteLine("S[" + cnt_local + "]" + wStr);
                        List<int> _nList = new List<int>();
                        _nList.Add(cnt_local);
                        // 終わりが一致
                        //if (wStr[wStr.Length - 1] == _end) {
                        if (ci.Compare(wStr[wStr.Length - 1].ToString(), _end.ToString(),
                            System.Globalization.CompareOptions.IgnoreWidth |
                            System.Globalization.CompareOptions.IgnoreKanaType) == 0) {
                            sList.Add(_nList);
                        } else {
                            bool[] _dList = new bool[Words.wList.Count]; // 使用済みリスト
                            _dList[cnt_local] = true;
                            bool ret = getSR(wStr[wStr.Length - 1], 1, _nList, _dList);
                            _dList[cnt_local] = false;
                        }
                        Debug.WriteLine("E[" + cnt_local + "]" + wStr);
                    }

                }
            });

            if (sList.Count > 0) {
                // 短い順に並べ替える
                sList = sList.OrderBy(a => a.Count).ToList();
            }
        }

        // 
        public bool getSR(char c, int num, List<int> uList, bool[] _dList) {
            //oList = new List<int>();
            bool ret = false;
            int i;
            for (i = 0; i < Words.wList.Count; i++) {
                string wStr = Words.wList[i].word;
                //if ((wStr[0] == c) 
                if ((ci.Compare(wStr[0].ToString(), c.ToString(),
                                        System.Globalization.CompareOptions.IgnoreWidth |
                                        System.Globalization.CompareOptions.IgnoreKanaType) == 0)
                                    && (_dList[i] == false)) {
                    List<int> _nList = new List<int>(uList);
                    _nList.Add(i);
                    //Debug.WriteLine(num + ":["+ i + "]" + wStr);
                    // 終わりが一致
                    //if (wStr[wStr.Length - 1] == end) {
                    if (ci.Compare(wStr[wStr.Length - 1].ToString(), end.ToString(),
                        System.Globalization.CompareOptions.IgnoreWidth |
                        System.Globalization.CompareOptions.IgnoreKanaType) == 0) {
                        ret = true;
                        sList.Add(_nList);
                        //break;
                    }
                    _dList[i] = true;
                    ret = getSR(wStr[wStr.Length - 1], num + 1, _nList, _dList);
                    _dList[i] = false;
                }
            }

            return ret;
        }
    }
}
