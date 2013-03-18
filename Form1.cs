using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace JSON_to_dict
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        #region Тестовые строки

        static string test1 = "{\n" +
                "\"id\" : 234,\n" + 
                "\"userid\": 2412,\n" + 
                "\"money\": 20.7,\n" + 
                "\"phone_accepted\": true,\n" + 
                "\"rubrics\":\n" +
                "\t[ 12,14,16 ]," + 
                "\"services\":\n" +
                "\t[ 12,14,16 ]\n" + 
                "}";

        static string test2 = "{ \"id\" : 2354, \"status\" :true, "+
            "\"message\" : \"text\", \"phone\" : \"+323424343\", "+
            "\"workrequestid\" : 123, \"staff_money\" : 47.50 }";

        static string test3 = "{\"id\" : 800, \"workrequestid\": 1234, \"status\": 12435}";

        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            int key = test3.IndexOf('"');
            JSONParser jsn = new JSONParser();
            Dictionary<string, string> dict = jsn.parse(test1);
            foreach (KeyValuePair<string, string> a in dict)
            {
                //MessageBox.Show(a.Key + " => " + a.Value);
                listBox1.Items.Add(a.Key + " => " + a.Value);
            }
        }
    }
}
