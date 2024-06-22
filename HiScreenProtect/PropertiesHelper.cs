using System;
using System.Collections;
using System.IO;
using System.Text;

/*
 * @author : RoadToTheExpert
 * @date : 2019.07
 * @file : Properties.cs
 */

//Re-edited by Fengye1003
//Updated by teko.IO
//Completed at 2022/08/28 12:14


namespace ScreenProtect.MVP
{
    public class PropertiesHelper
    {
        public static Hashtable Load(string file)
        {
            Hashtable ht = new Hashtable(16);
            string content;
            try
            {
                content = File.ReadAllText(file, Encoding.UTF8);
            }
            catch
            {
                return null;
            }
            string[] rows = content.Split('\n');
            foreach (string c in rows)
            {
                if (c.Trim().Length == 0)
                    continue;
                string[] kv = c.Split('=');
                if (kv.Length == 1)
                {
                    ht[kv[0].Trim()] = "";
                }
                else if (kv.Length == 2)
                {
                    ht[kv[0].Trim()] = kv[1].Trim();
                }
                else
                {
                    string value = "";
                    for (int i = 1; i < kv.Length; i++)
                    {
                        if (i == 1)
                            value = kv[i];
                        else
                            value = value + "=" + kv[i];
                    }
                    ht[kv[0].Trim()] = value.Trim();
                    //To solve the problem of multi-"=";
                }
            }
            return ht;
        }

        public static bool Save(string file, Hashtable ht)
        {
            if (ht == null || ht.Count == 0)
                return false;
            StringBuilder sb = new StringBuilder(ht.Count * 12);
            foreach (string k in ht.Keys)
            {
                sb.Append(k).Append('=').Append(ht[k]).Append(Environment.NewLine);
            }
            try
            {
                File.WriteAllText(file, sb.ToString(), Encoding.UTF8);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static Hashtable FixProperties(Hashtable htStandard, string path)
        {

            Hashtable ht = Load(path) ?? new();//如果Load出的哈希表为null 则新建一个并赋值
            //C#中两个问号（“?”）的作用是判断“?”左边的对象是否为null，如果不为null则使用“?”左边的对象，如果为null则使用“?”右边的对象。
            foreach (string key in htStandard.Keys)
            {
                if (!ht.Contains(key)) ht.Add(key, htStandard[key] as string);
            }
            Save(path, ht);
            Log.SaveLog("Hashtable fixed.");
            return ht;
        }

        public static Hashtable AutoCheck(Hashtable htStandard, string path)
        {
            bool isFixed = true;
            Hashtable ht = Load(path);
            if (ht == null)
            {
                isFixed = false;
            }
            else
            {
                foreach (string key in htStandard.Keys)
                {
                    if (!ht.Contains(key)) isFixed = false;
                }
            }

            if (!isFixed)
                return FixProperties(htStandard, path);
            else
                return ht;
        }

    }
}