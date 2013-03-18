using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JSON_to_dict
{
    class JSONParser
    {
        private string json;
        private int pos;
        private string key;
        private Dictionary<string, string> Result;

        private void Init(string val)
        {
            json = val;
            pos = 0;
            Result = new Dictionary<string, string>();
        }

        private void start()
        {
            key = "";
        }

        #region Поиски

        private bool findKey()
        {
            while (pos < json.Length)
            {
                if (json[pos] == '"') // нашли начало ключа
                {
                    while (json[++pos] != '"')
                    {
                        key = key + json[pos]; // записываем название ключа
                    }
                    ++pos; // переходим с кавычки на след символ
                    return true;
                }
                ++pos;
            }
            return false;
        }

        private void findVal()
        {
            while (pos < json.Length)
            {
                if (isNum())
                {
                    findNum();
                    break;
                }
                switch (json[pos])
                {
                    case '"':
                        findStr();
                        return;
                    case 't':
                        Result.Add(key, "bool");
                        pos += 4;
                        return;
                    case 'f':
                        Result.Add(key, "false");
                        pos += 5;
                        return;

                    /* обработка массива
                    case '[':
                        break;
                     * */
                    default: ++pos; break;
                }
            }
        }

        private void findNum()
        {
            string val = "" + json[pos++];
            while (isNum())
            {
                val += json[pos++];
            }
            Result.Add(key, val);
        }

        // Обработка строк
        private void  findStr()
        {
            string val = "";
            while (++pos < json.Length)
            {
                if (json[pos] != '"')
                {
                    val += json[pos];
                }
                else
                {
                    if (json[pos - 1] == '\\')
                    {
                        val += json[pos];
                    }
                    else
                    {
                        ++pos;    
                        break;
                    }
                }
            }
            Result.Add(key, val);
        }

        #endregion

        private bool isNum()
        {
            if (((json[pos] - '0') >= 0) &&
                ((json[pos] - '0') <= 9))
                return true;
            else
                if (json[pos] == '.')
                    return true;
            return false;
        }

        public Dictionary<string, string> parse(string json)
        {
            Init(json);
            start();
            while (findKey())
            {
                findVal();
                start();
            }

            return Result;
        }
    }
}
