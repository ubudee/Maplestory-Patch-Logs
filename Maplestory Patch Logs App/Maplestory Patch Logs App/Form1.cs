using System;
using HtmlAgilityPack;
using System.Windows.Forms;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;

namespace Maplestory_Patch_Logs_App
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            textBox3.SelectionStart = textBox3.Text.Length;
            textBox3.ScrollToCaret();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var Head = new System.Text.StringBuilder();
            var Body = new System.Text.StringBuilder();
            var Bottom = new System.Text.StringBuilder();

            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(textBox3.Text);

            int i = 0;

            foreach (HtmlNode node in htmlDoc.DocumentNode.SelectNodes("(./ul/li)"))
            {
                int ii = 0;
                i++;
                HtmlNode titleElement = node.SelectSingleNode("(//ul/li[" + i + "]//strong)");


                foreach (HtmlNode update in htmlDoc.DocumentNode.SelectNodes("(//ul/li[" + i + "]/ul/li)"))
                {
                    int iii = 0;
                    ii++;

                    HtmlNode anchorElement = update.SelectSingleNode("(//ul/li[" + i + "]/ul/li[" + ii + "]//a)");

                    Body.AppendLine("     - " + titleElement.InnerText + ": " + anchorElement.InnerText);

                }
            }

            Head.AppendLine("<details>");
            Head.AppendLine("     <summary>");
            Head.AppendLine("            " + textBox1.Text + " (" + textBox2.Text + ")");
            Head.AppendLine("     </summary>" + System.Environment.NewLine);

            Bottom.AppendLine("     ");
            Bottom.AppendLine("</details>");
            Bottom.AppendLine("");

            textBox4.Text = Head.ToString() + Body.ToString() + Bottom.ToString();
        }
    }
}
