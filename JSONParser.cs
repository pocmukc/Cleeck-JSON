using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JSON_to_dict
{
    class JSONParser
    {
        enum states 
        { 
            start
            key,
            val,
            val_string,
            val_int,
            val_bool,
            val_array,
        };

        public Dictionary<string, string> parse(string json)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();

            states state = states.start; // состояния

            bool flag = false; // флаг записи

            string key ="";
            string val ="";

            foreach (char ch in json)
            { 
                switch (state)
                {
                    case states.start:
                        flag = false; 
                        key ="";
                        val ="";
                        goto case states.key;

                    #region Поиск ключа

                    case states.key: 
                        if (flag) // если записываем
                        {
                            if (ch != '"') // пишем пока не закроется "
                            {
                                key = key + ch;
                            }
                            else
                            {
                                // запрещаем запись и устанавливаем 
                                // состояние в поиск значения
                                state = states.val;
                                flag = false;
                            }
                        }
                        else
                        {
                            if (ch == '"') // нашли начало ключа
                            {
                                flag = true;
                            }
                        }
                        break;

                    #endregion

                    #region Поиск значения

                    case states.val:
                        if ((ch - '0' >= 0) &&
                            ((ch - '0' <= '9')))
                        {
                            state = states.val_int;
                            val = val + ch;
                        }
                        switch (ch)
                        {
                            case '"':
                                state = states.val_string;
                                break;
                            case '[':
                                state = states.val_array;
                                break;
                            case 'b':
                                state = states.val_bool;
                                flag = true;
                                break;
                            case 'f':
                                state = states.val_bool;
                                flag = false;
                                break;
                        }
                        break;

                    #endregion

                    case states.val_bool:
                        if (flag)
                            result.Add(key, "true");
                        else
                            result.Add(key, "false");
                        state = states.start;
                        break;

                    case states.val_string:

                        break;
                }
            }

            return result;
        }
    }
}
