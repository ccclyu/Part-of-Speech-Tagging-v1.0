using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;

namespace tag
{
    public partial class tag : Form
    {
        public const int k = 41;
        public SortedDictionary<string, int> trans = new SortedDictionary<string,int>();
        Tnode root = new Tnode();
        public tag()
        {
            InitializeComponent();
            getTrie();
        }

        // 向trie树中插入一个词 和 它相应的词性

        void insert(string tmp,int sp)
        {
            Tnode now = root;
            int ptr = 0;
            while (ptr < tmp.Length){
                char t=tmp[ptr];
                if (!now.mp.ContainsKey(t))
                {
                    now.mp[t] = new Tnode();
                }
                now = now.mp[t];
                ptr++;
            }
            now.ed = true;
            now.sum++;
            now.each[sp]++;
        }

        string[] rtrans = new string[k + 1];

        //制作词性和编号之间的对应关系

        void makeTrans()
        {
            trans["Ag"] = 0;
            trans["Bg"] = 1;
            trans["Dg"] = 2;
            trans["Mg"] = 3;
            trans["Ng"] = 4;
            trans["Rg"] = 5;
            trans["Tg"] = 6;
            trans["Vg"] = 7;
            trans["a"] = 8;
            trans["ad"] = 9;
            trans["an"] = 10;
            trans["b"] = 11;
            trans["c"] = 12;
            trans["d"] = 13;
            trans["e"] = 14;
            trans["f"] = 15;
            trans["h"] = 16;
            trans["i"] = 17;
            trans["j"] = 18;
            trans["k"] = 19;
            trans["l"] = 20;
            trans["m"] = 21;
            trans["n"] = 22;
            trans["nr"] = 23;
            trans["ns"] = 24;
            trans["nt"] = 25;
            trans["nx"] = 26;
            trans["nz"] = 27;
            trans["o"] = 28;
            trans["p"] = 29;
            trans["q"] = 30;
            trans["r"] = 31;
            trans["s"] = 32;
            trans["t"] = 33;
            trans["u"] = 34;
            trans["v"] = 35;
            trans["vd"] = 36;
            trans["vn"] = 37;
            trans["w"] = 38;
            trans["y"] = 39;
            trans["z"] = 40;
            trans["uk"] = 41;

            rtrans[0] = "Ag";
            rtrans[1] = "Bg";
            rtrans[2] = "Dg";
            rtrans[3] = "Mg";
            rtrans[4] = "Ng";
            rtrans[5] = "Rg";
            rtrans[6] = "Tg";
            rtrans[7] = "Vg";
            rtrans[8] = "a";
            rtrans[9] = "ad";
            rtrans[10] = "an";
            rtrans[11] = "b";
            rtrans[12] = "c";
            rtrans[13] = "d";
            rtrans[14] = "e";
            rtrans[15] = "f";
            rtrans[16] = "h";
            rtrans[17] = "i";
            rtrans[18] = "j";
            rtrans[19] = "k";
            rtrans[20] = "l";
            rtrans[21] = "m";
            rtrans[22] = "n";
            rtrans[23] = "nr";
            rtrans[24] = "ns";
            rtrans[25] = "nt";
            rtrans[26] = "nx";
            rtrans[27] = "nz";
            rtrans[28] = "o";
            rtrans[29] = "p";
            rtrans[30] = "q";
            rtrans[31] = "r";
            rtrans[32] = "s";
            rtrans[33] = "t";
            rtrans[34] = "u";
            rtrans[35] = "v";
            rtrans[36] = "vd";
            rtrans[37] = "vn";
            rtrans[38] = "w";
            rtrans[39] = "y";
            rtrans[40] = "z";
            rtrans[41] = "uk";
        }

        int[] cnt = new int[k];
        int[,] ccnt = new int[k, k];

        double[,] p = new double[k + 1, k + 1];

        //读字典，将字典中的词插入trie树，计算词性转移矩阵

        void getTrie()
        {
            makeTrans();
            Array.Clear(cnt, 0, cnt.Length);
            Array.Clear(ccnt, 0, ccnt.Length);
            string path = @"dict.txt";
            //StreamReader dict = new StreamReader(path, Encoding.GetEncoding("GB18030"));
            StreamReader dict = new StreamReader(path, Encoding.GetEncoding("GB18030"));
            string cat;
            while (!dict.EndOfStream)
            {
                cat = dict.ReadLine();
                cat = cat.Replace("  ", " ");
                cat = cat.Replace("  ", " ");
                cat = cat.Replace("  ", " ");
                string[] wds = cat.Replace(" ", "@").Split('@');
                int last = -1;
                foreach (string wd in wds)
                {
                    if (wd.Length == 0) break;
                    string[] t = wd.Split('/');
                    int now = trans[t[1]];
                    insert(t[0], now);
                    cnt[now]++;
                    if (last != -1)
                    {
                        ccnt[last, now]++;
                    }
                    last = now;
                }
            }
            for (int i = 0; i < k; ++i)
            {
                for (int j = 0; j < k; ++j)
                {
                    p[i, j] = ccnt[i, j] * 1.0 / cnt[j];
                }
            }
            for (int i = 0; i <= k; ++i)
            {
                p[k, i] = p[i, k] = 1.0;
            }

            //MessageBox.Show(p[trans["p"],trans["n"]].ToString());
        }

        //分词，同时标注词性

        public void div()
        {
            string now = tbIpt.Text;
            if (now.Length == 0) return;
            int ptr = 0;
            Tans[] ans = new Tans[10005];
            int s = 0;
            while (ptr<now.Length)
            {
                if (now[ptr]=='\r' && now[ptr + 1]=='\n')
                {
                    ptr+=2;
                    continue;
                }
                ans[s] = new Tans();
                int deep = 0;
                Tnode trie = root;
                for (int i = 0; i + ptr < now.Length; ++i)
                {
                    char t = now[i + ptr];
                    if (!trie.mp.ContainsKey(t)) break;
                    trie = trie.mp[t];
                    if (trie.ed)
                    {
                        deep = i + 1;
                        ans[s].str = "";
                        for (int j=0;j<deep;++j){
                            ans[s].str += now[ptr + j];
                        }
                        //MessageBox.Show(ans[s].str);
                        if (s == 0)
                        {
                            for (int j = 0; j <= k; ++j)
                            {
                                if (trie.sum > 0) ans[s].each[j] = trie.each[j] * 1.0 / trie.sum;
                                else ans[s].each[j] = 0;
                                ans[s].last[j] = -1;
                            }
                        }
                        else
                        {
                            for (int j = 0; j < k; ++j)
                            {
                                int save = -1;
                                double big = -1;
                                for (int r = 0; r <= k; ++r)
                                {
                                    if (ans[s - 1].each[r] * p[r, j] > big)
                                    {
                                        big = ans[s - 1].each[r] * p[r, j];
                                        save = r;
                                    }
                                }
                                ans[s].each[j] = big * trie.each[j]*1.0/trie.sum;
                                ans[s].last[j] = save;
                            }
                            ans[s].each[k] = 0;
                        }
                    }
                }
                if (deep == 0)
                {
                    deep++;
                    for (int j = 0; j < k; ++j) ans[s].each[j] = 0;
                    ans[s].each[k] = 1;
                    ans[s].str = "";
                    for (int j = 0; j < deep; ++j)
                    {
                        ans[s].str += now[ptr + j];
                    }
                    if (s == 0)
                    {
                        ans[s].last[k] = -1;
                    }
                    else
                    {
                        int save = -1;
                        double big = -1;
                        for (int r = 0; r <= k; ++r)
                        {
                            if (ans[s - 1].each[r] > big)
                            {
                                big = ans[s - 1].each[r];
                                save = r;
                            }
                        }
                        ans[s].last[k] = save;
                    }
                }
                ptr += deep;
                s++;
            }
            int[] optlist = new int[10005];
            double bb = -1;
            for (int i=0;i<=k;++i)
            {
                if (ans[s-1].each[i] > bb)
                {
                    optlist[s-1] = i;
                    bb = ans[s - 1].each[i];
                }
            }
            for (int i = s - 1; i >= 1; --i)
            {
                int da, db;
                da = optlist[i];
                db = ans[i].last[da];
                optlist[i - 1] = db;
            }
            for (int i = 0; i < s; ++i)
            {
                tbOpt.Text += ans[i].str + @"/" + rtrans[optlist[i]] + " ";
            }
        }

        /*
        private void button1_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(tbIpt.Text.Length.ToString());
            tbOpt.Clear();
            div();
        }
        */

        // tbIpt的TextChanged事件响应函数

        private void tbIpt_TextChanged(object sender, EventArgs e)
        {
            tbOpt.Clear();
            div();
        }
    }

    //trie树节点类

    public class Tnode
    {
        public const int k = 42;
        public SortedDictionary<char, Tnode> mp = new SortedDictionary<char,Tnode>();
        public bool ed;
        public int sum;
        public int[] each = new int[k];
        public Tnode()
        {
            ed = false;
            mp.Clear();
            Array.Clear(each, 0, each.Length);
            sum = 0;
        }
    }

    //DP时状态表示类

    public class Tans
    {
        public string str = "";
        public const int k = 42;
        public double[] each = new double[k];
        public int[] last = new int[k];
    }
}
