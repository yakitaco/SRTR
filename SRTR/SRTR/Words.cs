using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRTR {

    // しりとり候補ワード
    class Words {
        public static List<Words> wList = new List<Words>();

        public string word;  // 文字
        public bool used = false;    // 使用済みフラグ

        public Words(string _word) {
            word = _word;
            wList.Add(this);
        }

        public static void reset(){
            wList = new List<Words>();
        }



    }
}
