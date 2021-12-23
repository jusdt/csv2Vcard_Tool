﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
namespace hoc
{
    public partial class Form1 : Form
    {
        List<VCF_File> list_obj = new List<VCF_File>();
        List<String> list_of_header = new List<String>();
        String name_of_file = "";
        Stream fileStream;
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            
        }
        private List<VCF_File> get_value_from_vcf(String text)
        {
            List<VCF_File> arr = new List<VCF_File>();
            string[] words = text.Split('\n');
            int i = 0;
            Console.WriteLine("ko chay ak");
            foreach (var word in words)
            {
                VCF_File new_obj = new VCF_File();
                if (i == 0)
                {
                    String ww = (String)word;
                    string[] wwordss = ww.Split(',');
                    foreach (var q in wwordss)
                    {
                        list_of_header.Add(q);
                    }
                    i++;
                    continue;
                }
                String w = (String)word;
                string[] wordss = w.Split(',');
                int count = 0;
                foreach (var q in wordss)
                {
                    if(count == 0)
                    {
                        new_obj.set_ID((String)q);
                    }
                    else if(count == 1)
                    {
                        new_obj.set_name(q);
                    }
                    else
                    {
                        new_obj.set_age(Int32.Parse(q));
                    }
                    count++;
                }
                arr.Add(new_obj);
                i++;
            }
            return arr;
        }
        private void write_vcard(List<VCF_File> arr)
        {
            using (StreamWriter wr = new StreamWriter(fileStream))
            {
                for (int i = 0; i < arr.Count; i++)
                {
                    wr.WriteLine("BEGIN:VCARD");
                    wr.WriteLine("VERSION:3.0");
                    wr.WriteLine("N:" + arr[i].get_name() + ";");
                    wr.WriteLine("END:VCARD");
                }
            }
        }
        private void update_data_grid_view()
        {
            for(int i = 0; i < list_of_header.Count; i++)
            {
                Console.WriteLine("co vao day ne");
                dataGridView1.Rows.Add(list_of_header[i]);
                
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            int size = -1;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                string file = openFileDialog1.FileName;
                name_of_file = file;
                try
                {
                    string text = File.ReadAllText(file);
                    size = text.Length;
                    Console.Write(text);
                    list_obj = get_value_from_vcf(text);
                    update_data_grid_view();
                }
                catch (IOException)
                {
                }
            }
            Console.WriteLine(size); // <-- Shows file size in debugging mode.
            Console.WriteLine(result); // <-- For debugging use.
        }

        private void button2_Click(object sender, EventArgs e)
        {
            write_vcard(list_obj);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.MouseClick += new MouseEventHandler(mouse_click_handle);
            
        }
        private void mouse_click_handle(object sender, MouseEventArgs e)
        {
            ContextMenuStrip menu = new System.Windows.Forms.ContextMenuStrip();
            int position_xy_mouse_row = dataGridView1.HitTest(e.X, e.Y).RowIndex;
            if(e.Button == MouseButtons.Left)
            {
                menu.Items.Add("Name").Name = "Name";
                menu.Items.Add("ID").Name = "ID";
                menu.Items.Add("Age").Name = "Age";
                menu.Items.Add("Hu").Name = "A";
            }
            menu.Show(dataGridView1, new Point(e.X, e.Y));
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            

        }

        private void button4_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            string[] wordss = name_of_file.Split('.');
            saveFileDialog1.InitialDirectory = wordss[0];
            Console.WriteLine(saveFileDialog1.InitialDirectory);
            saveFileDialog1.FileName = wordss[0] + ".vcf";
            saveFileDialog1.DefaultExt = "vcf";
            saveFileDialog1.Filter =  "(*.vcf)|*.vcf";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                fileStream = saveFileDialog1.OpenFile();
            }
        }
    }
}