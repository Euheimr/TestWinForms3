using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestWinForms3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string email = this.textBox1.Text;

            // byvalue is default.  string not affected
            ByValueExample(email);
            MessageBox.Show(email, "ByValueExample");

            // byref.  string is changed. i very rarely use this.
            ByRefExample(ref email);
            MessageBox.Show(email, "ByRefExample");


            // return.  if i wanted the string to change, i will almost always do it this way
            email = ReturnExample(email);
            MessageBox.Show(email, "ReturnExample");

            // out example.  i rarely use out because i end up wanting to send back more and move variables.
            bool goodEmail;
            string emailCheckResult;
            OutExample(email, out goodEmail, out emailCheckResult);
            MessageBox.Show(email + Environment.NewLine + goodEmail.ToString() + Environment.NewLine + emailCheckResult, "OutExample");

            // instead of out, i usually end up creating a container class, and passing that back.  
            MuhResult muhEmailResult = HowINormallyPassBackMultipleValues(email);
            MessageBox.Show(email + Environment.NewLine + muhEmailResult.IsValid.ToString() + Environment.NewLine + muhEmailResult.Result, "HowINormallyPassBackMultipleValues");


            // reference type
            var muhDataTable = new DataTable();
            muhDataTable.Columns.Add("SomeID", typeof(int));
            muhDataTable.Columns.Add("SomeDesc", typeof(string));
            ReferenceTypesExample(muhDataTable);
            MessageBox.Show("Wth?  Why is there " + muhDataTable.Rows.Count.ToString() + " rows in this DataTable now.  I didn't pass by ref!", "ReferenceTypesExample");


        }

        private void ByValueExample(string value)
        {
            value = "ByValueExample" + value.ToUpper();
        }

        private void ByRefExample(ref string value )
        {
            value = "ByRefExample" + value.ToUpper();
        }


        private string ReturnExample(string value)
        {
            return "ReturnExample" + value.ToLower();
        }

        private void OutExample(string value, out bool isValid, out string result)
        {
            if (value.Contains(".") && value.Contains("@"))
            {
                isValid = true;
                result = value + " might be a good email.";
            }
            else
            {
                isValid = false;
                result = value + " doesn't look right to me.";
            }

        }


        private class MuhResult
        {
            public bool IsValid {get; set;}
            public string Result {get; set;}
        }


        private MuhResult HowINormallyPassBackMultipleValues(string value)
        {
            MuhResult muhResult = new MuhResult();

            if (value.Contains(".") && value.Contains("@"))
            {
                muhResult.IsValid = true;
                muhResult.Result = value + " might be a good email.";
            }
            else
            {
                muhResult.IsValid = false;
                muhResult.Result = value + " doesn't look right to me.";
            }

            return muhResult;

        }

        private void ReferenceTypesExample(DataTable value)
        {

            for (int i = 0; i < 20; i++)
            {

                DataRow row = value.NewRow();
                row["SomeID"] = i;
                row["SomeDesc"] = "I lika text " + i.ToString();
                value.Rows.Add(row);

            }

            value.AcceptChanges();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Blah");
        }
    }
}
