using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JSON_to_dict
{
    class JSONParser
    {
        private string findString(string val)
        { 
            return val;
        }

        private string findInteger(string val)
        {
            return val;
        }

        private string findBool(string val)
        {
            return val;
        }

        public Dictionary<string, string> parse(string json)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();

            var lines = json.Split(';');

            foreach (string line in lines)
            {
                int key_pos = line.IndexOf('"');

                if (key_pos > -1)
                {
                    string key = line.Substring(key_pos+1);
                    key_pos = key.IndexOf('"');
                    key = key.Substring(0, key_pos);

                    string val = line.Substring(2 + key.Length);

                }
            }

            return result;
        }
    }
}
